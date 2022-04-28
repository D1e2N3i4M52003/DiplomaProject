using System;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;

namespace PresentationLayer.Services
{
    public class JwtToken
    {
        public long exp { get; set; }
        public string role { get; set; }
        public string id { get; set; }
    }
    public interface IJWTService
    {
        DateTime GetExpiryTimestamp(string accessToken);
        string GetRole(string accessToken);
        Guid GetId(string accessToken);
    }
        public class JWTService : IJWTService
    {
        private IJsonSerializer _serializer = new JsonNetSerializer();
        private IDateTimeProvider _provider = new UtcDateTimeProvider();
        private IBase64UrlEncoder _urlEncoder = new JwtBase64UrlEncoder();
        private IJwtAlgorithm _algorithm = new HMACSHA256Algorithm();
        

        public string GetRole(string accessToken)
        {

            try
            {
                IJwtValidator _validator = new JwtValidator(_serializer, _provider);
                IJwtDecoder decoder = new JwtDecoder(_serializer, _validator, _urlEncoder, _algorithm);
                var token = decoder.DecodeToObject<JwtToken>(accessToken);
                string role = token.role;
                return role;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Role doesn't work");
            }
        }

        public Guid GetId(string accessToken)
        {

            try
            {
                IJwtValidator _validator = new JwtValidator(_serializer, _provider);
                IJwtDecoder decoder = new JwtDecoder(_serializer, _validator, _urlEncoder, _algorithm);
                var token = decoder.DecodeToObject<JwtToken>(accessToken);
                string id = token.id;
                return Guid.Parse(id);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Id can't be found!");
            }
        }

        public DateTime GetExpiryTimestamp(string accessToken)
        {
            try
            {
                IJwtValidator _validator = new JwtValidator(_serializer, _provider);
                IJwtDecoder decoder = new JwtDecoder(_serializer, _validator, _urlEncoder, _algorithm);
                var token = decoder.DecodeToObject<JwtToken>(accessToken);
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(token.exp);
                return dateTimeOffset.LocalDateTime;
            }
            catch (TokenExpiredException)
            {
                return DateTime.MinValue;
            }
            catch (SignatureVerificationException)
            {
                return DateTime.MinValue;
            }
            catch (Exception ex)
            {
                // ... remember to handle the generic exception ...
                return DateTime.MinValue;
            }
        }
    }
}