using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SSOAPI.API.Configurations;
using SSOAPI.API.Extensions;
using SSOAPI.Application.Interfaces;
using SSOAPI.Application.ViewModels.User;
using SSOAPI.Infra.Data.Models;
using SSOAPI.Infra.Mail;
using SSOAPI.Infra.Mail.Models;

namespace SSOAPI.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [Authorize]
    public class OAuthController : BaseController
    {

        private readonly IUserAppService _userAppService;
        private readonly TokenExtension _tokenExtension;

        public OAuthController(IUserAppService userAppService, TokenExtension tokenExtension)
        {

            _userAppService = userAppService;
            _tokenExtension = tokenExtension;

        }


        [HttpPost("/registrar")]
        [AllowAnonymous]
        public async Task<ActionResult> Registrar(RegistroUserViewModel request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _userAppService.RegistrarNovoUser(request);

            if(!result.isValid)
                AddInRangeError(result.Errors);

            return CustomResponse(request);
        }

        [HttpPost("/recuperar-senha")]
        [AllowAnonymous]
        public async Task<ActionResult> RecuperarSenha(string email)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _userAppService.RecuperarSenha(email);
            
            if(!result.isValid)
                AddInRangeError(result.Errors);

            return CustomResponse(result.Result);


        }

        [HttpPost("/alterar-senha")]
        [AllowAnonymous]
        public async Task<ActionResult> AlterarSenha(AlterarSenhaViewModel request)
        {
            
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            var result = await _userAppService.AlterarSenhaUser(request);
            
            if(!result.isValid)
                AddInRangeError(result.Errors);

            return CustomResponse(result.Result);

        }

        [HttpPost("/autenticar")]
        [AllowAnonymous]
        public async Task<ActionResult> Autenticar(LoginUserViewModel request)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result = await _userAppService.Autenticar(request);

            return !result.isValid ? Unauthorized() : CustomResponse(await _tokenExtension.GerarJwt(request.Email));
        }

        [HttpGet("/{id}")]
        public async Task<ActionResult> ObterUsuario(string id)
        {
            var result = await _userAppService.GetUsuario(id);

            if(!result.isValid)
                AddInRangeError(result.Errors);

            return CustomResponse(result.Result);
        }

       
    }
}
