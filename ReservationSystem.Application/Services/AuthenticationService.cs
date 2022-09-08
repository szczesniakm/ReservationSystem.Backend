using FluentValidation;
using Microsoft.Extensions.Options;
using ReservationSystem.Application.Models;
using ReservationSystem.Infrastructure;
using ReservationSystem.Infrastructure.Jwt;
using ReservationSystem.Infrastructure.Settings;
using System.DirectoryServices.Protocols;
using System.Net;

namespace ReservationSystem.Application.Services
{
    public class AuthenticationService
    {
        private readonly IValidator<LoginRequest> _loginRequestValidator;
        private readonly LdapAuthenticationProvider _ldapAuthenticationProvider;
        private readonly JwtTokenService _jwtTokenService;

        public AuthenticationService(
            IValidator<LoginRequest> loginRequestValidator,
            //LdapAuthenticationProvider ldapAuthenticationProvider,
            JwtTokenService jwtTokenService)
        {
            _loginRequestValidator = loginRequestValidator;
            //_ldapAuthenticationProvider = ldapAuthenticationProvider;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest model)
        {
            await _loginRequestValidator.ValidateAndThrowAsync(model);

            //var groups = await _ldapAuthenticationProvider.AuthenticateAsync(model.Username, model.Password);

            var token = _jwtTokenService.CreateToken(model.Username);

            return new LoginResponse(token);
        }
    }
}
