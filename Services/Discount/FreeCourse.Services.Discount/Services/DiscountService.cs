﻿using Dapper;
using FreeCourse.Shared.Dtos;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }
        public async Task<Response<List<Models.Discount>>> GetDiscountListAsync()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount");

            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }
        public async Task<Response<Models.Discount>> GetDiscountByIdAsync(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount WHERE id = @Id", new { Id = id })).SingleOrDefault();

            if (discount == null) return Response<Models.Discount>.Fail("Discount not found.", 404);

            return Response<Models.Discount>.Success(discount, 404);
        }
        public async Task<Response<NoContent>> Save(Models.Discount discount)
        {
            var saveStatus = await _dbConnection.ExecuteAsync("INSERT INTO discount (userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);
            if (saveStatus > 0) return Response<NoContent>.Success(204);
            return Response<NoContent>.Fail("Bir hata meydana geldi.", 404);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var updateStatus = await _dbConnection.ExecuteAsync("UPDATE discount SET userid=@UserId,code=@Code,rate=@Rate where id=@Id", new
            {
                Id = discount.Id,
                Code = discount.Code,
                UserId = discount.UserId,
                Rate = discount.Rate
            });
            if (updateStatus > 0) return Response<NoContent>.Success(204);
            return Response<NoContent>.Fail("Discount bulunamadı.", 404);
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var deleteStatus = await _dbConnection.ExecuteAsync("DELETE FROM discount WHERE id=@Id", new { Id = id });
            return deleteStatus > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount bulunamadı", 404);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = await _dbConnection.QueryAsync<Models.Discount>("SELECT * FROM discount WHERE userid=@UserId AND code=@Code", new { userId, code });
            var hasDiscount = discount.FirstOrDefault();

            return hasDiscount == null ? Response<Models.Discount>.Fail("Discount bulunamadı.", 404) : Response<Models.Discount>.Success(hasDiscount, 200);
        }

    }
}
