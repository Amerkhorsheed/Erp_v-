using Erp_V1.BLL;
using Erp_V1.DAL.DTO;
using Seller.API.Models; // Your LoginModel is here
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;

namespace Seller.API.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private readonly EmployeeBLL _employeeBll = new EmployeeBLL();

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IHttpActionResult Login([FromBody] LoginModel loginModel)
        {
            if (loginModel == null)
            {
                return BadRequest("Invalid client request");
            }

            EmployeeDetailDTO employee = _employeeBll.GetByUserNo(loginModel.UserNo);
            if (employee == null)
            {
                return Unauthorized();
            }

            bool isAuthenticated = _employeeBll.Authenticate(employee.EmployeeID, loginModel.Password);
            if (!isAuthenticated)
            {
                return Unauthorized();
            }

            // --- CHANGE 1: Determine role based on ID ---
            string role = DetermineUserRole(employee);

            // Only allow users with a valid role to log in
            if (string.IsNullOrEmpty(role))
            {
                return Unauthorized(); // User does not have a permitted role
            }

            var token = GenerateJwtToken(employee.UserNo.ToString(), role);

            // --- CHANGE 2: Return role along with the token ---
            return Ok(new { token = token, role = role });
        }

        private string DetermineUserRole(EmployeeDetailDTO employee)
        {
            // Based on your requirement for RoleID
            if (employee.RoleID == 3) return "Admin";
            if (employee.RoleID == 1013) return "Seller";

            // Return null if the user does not have one of the required roles
            return null;
        }

        private string GenerateJwtToken(string userNo, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKeyThatIsLongAndSecure"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userNo),
                new Claim(ClaimTypes.Role, role), // The role is embedded here
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "YourAPI",
                audience: "YourApp",
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}