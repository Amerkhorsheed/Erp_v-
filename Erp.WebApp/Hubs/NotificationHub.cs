using Microsoft.AspNet.SignalR;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Configuration;

namespace Erp.WebApp.Hubs
{
    [JwtAuthorize]
    public class NotificationHub : Hub
    {
        public override Task OnConnected()
        {
            var userRole = GetUserRole();
            var userId = GetUserId();
            
            if (!string.IsNullOrEmpty(userRole) && !string.IsNullOrEmpty(userId))
            {
                // Add user to role-based group
                Groups.Add(Context.ConnectionId, userRole);
                
                // Add user to personal group for targeted notifications
                Groups.Add(Context.ConnectionId, $"User_{userId}");
            }
            
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var userRole = GetUserRole();
            var userId = GetUserId();
            
            if (!string.IsNullOrEmpty(userRole) && !string.IsNullOrEmpty(userId))
            {
                Groups.Remove(Context.ConnectionId, userRole);
                Groups.Remove(Context.ConnectionId, $"User_{userId}");
            }
            
            return base.OnDisconnected(stopCalled);
        }

        public void JoinNotificationGroup(string groupName)
        {
            Groups.Add(Context.ConnectionId, groupName);
        }

        public void LeaveNotificationGroup(string groupName)
        {
            Groups.Remove(Context.ConnectionId, groupName);
        }

        private string GetUserRole()
        {
            try
            {
                var token = Context.Request.Headers["Authorization"]?.Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                {
                    // Try to get from query string (for SignalR connection)
                    token = Context.QueryString["access_token"];
                }

                if (!string.IsNullOrEmpty(token))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();

                    var secret = ConfigurationManager.AppSettings["JWT:SecretKey"];
                    var issuer = ConfigurationManager.AppSettings["JWT:Issuer"];
                    var audience = ConfigurationManager.AppSettings["JWT:Audience"];
                    if (string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience))
                    {
                        return null;
                    }
                    var key = Encoding.UTF8.GetBytes(secret);
                    
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var roleClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
                    return roleClaim?.Value;
                }
            }
            catch
            {
                // Token validation failed
            }
            
            return null;
        }

        private string GetUserId()
        {
            try
            {
                var token = Context.Request.Headers["Authorization"]?.Replace("Bearer ", "");
                if (string.IsNullOrEmpty(token))
                {
                    token = Context.QueryString["access_token"];
                }

                if (!string.IsNullOrEmpty(token))
                {
                    var tokenHandler = new JwtSecurityTokenHandler();

                    var secret = ConfigurationManager.AppSettings["JWT:SecretKey"];
                    var issuer = ConfigurationManager.AppSettings["JWT:Issuer"];
                    var audience = ConfigurationManager.AppSettings["JWT:Audience"];
                    if (string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience))
                    {
                        return null;
                    }
                    var key = Encoding.UTF8.GetBytes(secret);
                    
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidIssuer = issuer,
                        ValidateAudience = true,
                        ValidAudience = audience,
                        ClockSkew = TimeSpan.Zero
                    }, out SecurityToken validatedToken);

                    var jwtToken = (JwtSecurityToken)validatedToken;
                    var subClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
                    return subClaim?.Value;
                }
            }
            catch
            {
                // Token validation failed
            }
            
            return null;
        }
    }
}