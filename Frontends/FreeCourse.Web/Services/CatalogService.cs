﻿using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalogs;
using FreeCourse.Web.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        // CatalogServis için üyeliğe gerek olmadığından sadece appsettings.json dosyasındaki secret bilgileriyle token alacağız, bunu delege içinde gerçekleştireceğiz. Burada sadece service istek yapmak için bir httpclient nesnesine ihtiyaç duyacağız
        private readonly HttpClient _client;

        public CatalogService(HttpClient client)
        {
            _client = client;
        }

        public Task<bool> CreateCourseAsync(CourseCreateInput courseCreateInput)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteCourseByAsync(string courseId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task<CourseViewModel> GetByCourseId(string courseId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdateInput)
        {
            throw new System.NotImplementedException();
        }
    }
}
