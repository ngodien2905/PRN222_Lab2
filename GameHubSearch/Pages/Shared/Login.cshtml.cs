using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GameHubSearch.Pages.Shared
{
    public class LoginModel : PageModel
    {
        private readonly IUserService _userService;

        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty] public string Email { get; set; } = string.Empty;
        [BindProperty] public string Password { get; set; } = string.Empty;

        [TempData] public string ErrorMessage { get; set; } = string.Empty;

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Email and Password cannot be empty.";
                return Page();
            }

            var user = await _userService.AuthenticateAsync(Email, Password);
            if (user is null)
            {
                ErrorMessage = "Invalid Email or Password.";
                return Page();
            }


            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Role", user.RoleId.ToString());


            if (user.RoleId == DAL.Models.User.Role.Player)
            {
                return RedirectToPage("/Shared/PlayerGames/RegisterGames");
            }
            else if (user.RoleId == DAL.Models.User.Role.Admin)
            {

                return RedirectToPage("/Shared/GamePage/Index");
            }
            else if (user.RoleId == DAL.Models.User.Role.Developer)
            {
                return Page();
            }
            else
            {
                // Unexpected role value; clear session and show error
                HttpContext.Session.Clear();
                ErrorMessage = "Your account role is not recognized. Please contact support.";
                return Page();
            }

        }
    }
}
