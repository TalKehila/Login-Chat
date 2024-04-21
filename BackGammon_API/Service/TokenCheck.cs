using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BackGammon_API.Service
{
    public class TokenCheck
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenCheck> _logger;

        public TokenCheck(RequestDelegate next, IConfiguration configuration, ILogger<TokenCheck> logger)
        {
            _next = next;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            
            if (context.Request.Path.StartsWithSegments("/api/auth/login") || context.Request.Path.StartsWithSegments("/api/auth/register"))
            {
                await _next(context);
                return;
            }

            string authorizationHeader = context.Request.Headers["Authorization"]!;

            if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }

            string token = authorizationHeader.Substring("Bearer ".Length);

            if (ValidationToken(token))
            {
                await _next(context);
            }
            else
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
            }
        }
        internal bool ValidationToken(string token)
        {
            if(string.IsNullOrEmpty(token))
                return false;
            
            try
            {
                var jwtSettings = _configuration.GetSection("JWTSettings");

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["ValidIssuer"],
                    ValidAudience = jwtSettings["ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings["SecurityKey"]!))
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                SecurityToken validatedToken;
                tokenHandler.ValidateToken(token, tokenValidationParameters,out validatedToken);
                
                return true;
            }
            catch(SecurityTokenException exception)
            {
                _logger.LogInformation(exception.Message);
                return false;
            }
            catch (Exception exception) {
            _logger.LogInformation(exception.Message);
            return false;
            }
}

    }
}
