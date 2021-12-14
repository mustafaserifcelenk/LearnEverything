using FreeCourse.Shared.Services;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Web.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;
        public async Task<IActionResult> Index()
        {
            // Buradan sonra delege yazacağız ki, kullanıcı id alınmadan araya girecek client credential ile beraber token'ı ekleyecek ve öyle göndercek
            return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId));
        }
    }
}
