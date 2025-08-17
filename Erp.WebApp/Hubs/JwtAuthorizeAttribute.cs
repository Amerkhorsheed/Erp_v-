using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.Configuration;

namespace Erp.WebApp.Hubs
{
    public class JwtAuthorizeAttribute : AuthorizeAttribute
    {
        public override bool AuthorizeHubConnection(HubDescriptor hubDescriptor, IRequest request)
        {
            try
            {
                // Try Authorization header first
                var authHeader = request.Headers["Authorization"];
                string token = null;
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    token = authHeader.Substring("Bearer ".Length).Trim();
                }

                // Fallback to querystring (recommended for SignalR JS client)
                if (string.IsNullOrEmpty(token))
                {
                    token = request.QueryString["access_token"];
                }

                if (string.IsNullOrEmpty(token))
                {
                    return false;
                }

                var tokenHandler = new JwtSecurityTokenHandler();

                var secret = ConfigurationManager.AppSettings["JWT:SecretKey"];
                var issuer = ConfigurationManager.AppSettings["JWT:Issuer"];
                var audience = ConfigurationManager.AppSettings["JWT:Audience"];
                if (string.IsNullOrWhiteSpace(secret) || string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience))
                {
                    return false;
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
                }, out SecurityToken _);

                // If token is valid, authorize the connection
                return true;
            }
            catch
            {
                // Invalid token
                return false;
            }
        }
    }
}