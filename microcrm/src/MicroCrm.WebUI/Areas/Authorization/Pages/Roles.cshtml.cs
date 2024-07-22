using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MicroCrm.WebUI.Areas.Authorization.Pages
{
    [Authorize]
    public class RoleModel : PageModel
    {
    }
}
