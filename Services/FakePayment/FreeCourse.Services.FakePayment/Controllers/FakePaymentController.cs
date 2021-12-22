﻿using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentController : CustomBaseController
    {
        [HttpPost]
        public IActionResult ReceivePayment(PaymnentDto paymnentDto)
        {
            // PaymentDto ile ödeme işlemi gerçekleştir
            return CreateActionResultInstance(Response<NoContent>.Success(200));
        }
    }
}
