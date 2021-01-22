using ChatApp.Core.Infrastructure;
using ChatApp.Core.Infrastructure.DataProvider;
using ChatApp.Core.Models.ApiModel;
using ChatApp.Core.Models.Entity;
using ChatApp.Core.Models.ViewModel;
using ChatApp.Core.Resources;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Web;

namespace ChatApp.Core.Infrastructure
{
    public static class ApiHelper
    {
        /// <summary>
        /// Secret ky for generate/validate JWT token
        /// </summary>
        private static string SecretKey = ConfigurationManager.AppSettings["JWTSecretKey"];

        /// <summary>
        /// getter for fetch Header token from request.
        /// </summary>
        public static string Token
        {
            get
            {
                var a = HttpContext.Current.Request.Headers[Constants.TokenHeaderName];
                if (!string.IsNullOrEmpty(a))
                {
                    return a.Replace("Bearer ", "");
                }

                return null;
            }
        }


        /// <summary>
        /// OnSuccess
        ///     Step 1: Validate JWT Token and store UserName in variable
        ///     Step 2: Get user token details from Server cache and extend expire time duration.
        ///     Step 3: If #2 not found, Validate username -> Save token details in server cache for next request -> Return token details.
        /// OnFail
        ///     Bypass request from here using Throws an exception
        /// </summary>
        /// <returns></returns>
        public static TokenModel AuthenticateToken()
        {
            ObjectCache cache = MemoryCache.Default;
            if (!string.IsNullOrEmpty(Token))
            {

                //Step 1
                var userName = ValidateJwtToken(Token);
                if (!string.IsNullOrEmpty(userName))
                {
                    if (cache.Contains(userName))
                    {
                        //Step 2
                        TokenModel tokenvalue = (TokenModel)cache.Get(userName);

                        //Code For Making Cache Extended.
                        CacheItem item = cache.GetCacheItem(userName);
                        CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                        cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(Constants.CacheExpiryPeriod);
                        cache.Set(item, cacheItemPolicy);
                        return tokenvalue;
                    }
                    else
                    {
                        ITokenDataProvider _securityDataProvider = new TokenDataProvider();

                        //Step 3
                        ApiResponse response = _securityDataProvider.AuthenticateUserName(userName);
                        if (response.IsSuccess)
                        {
                            var user = (User)response.Data;
                            TokenModel tokenvalue = new TokenModel
                            {
                                UserId = user.UserId,
                                Token = Token,
                                UserName = user.UserName
                            };

                            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                            cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(Constants.CacheExpiryPeriod);
                            cache.Add(userName, tokenvalue, cacheItemPolicy);
                            return tokenvalue;
                        }
                    }
                }
            }

            Common.ThrowErrorMessage(Resource.InvalidToken, HttpStatusCode.Unauthorized);
            return null;
        }

        /// <summary>
        /// Return boolean value based on the user is Authorized or not.
        /// </summary>
        /// <returns></returns>
        public static bool IsAuthorizedUser()
        {
            return AuthenticateToken() != null;
        }

        #region JWT Token: Generate & Validate

        public static string GenerateJwtToken(string username)
        {
            byte[] key = Convert.FromBase64String(SecretKey);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                      new Claim(ClaimTypes.Name, username)}),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(securityKey,
                SecurityAlgorithms.HmacSha256Signature)
            };

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
            return handler.WriteToken(token);
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null;
                byte[] key = Convert.FromBase64String(SecretKey);
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                      parameters, out securityToken);
                return principal;
            }
            catch
            {
                return null;
            }
        }

        public static string ValidateJwtToken(string token)
        {
            string username = null;
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null)
                return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }

            Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim.Value;
            return username;
        }

        #endregion
    }
}
