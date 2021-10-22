using FreeCourse.IdentityServer.Models;
using IdentityModel;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Services
{
    public class IdentityResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityResourceOwnerPasswordValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            // UserName'de Email gönderiyoruz
            var existUser = await _userManager.FindByEmailAsync(context.UserName);

            if (existUser==null)
            {
                var errors  = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Email veya şifreniz yanlış." });
                context.Result.CustomResponse = errors;
                return;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(existUser, context.Password); 
            if (passwordCheck==false)
            {
                var errors = new Dictionary<string, object>();
                errors.Add("errors", new List<string> { "Email veya şifreniz yanlış." });
                context.Result.CustomResponse = errors;
                return;
            }

            // Grant_type olarak kullandığımız ResourceOwnerCredentials'ın kısaltımı Password'dür
            // Result olarak kullanıcının id'si ve grant tipini dönüyoruz
            context.Result = new GrantValidationResult(existUser.Id.ToString(), OidcConstants.AuthenticationMethods.Password);
             
        }
    }
}
