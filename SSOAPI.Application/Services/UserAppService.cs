using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SSOAPI.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using SSOAPI.API.Extensions;
using SSOAPI.Application.ViewModels.User;
using SSOAPI.Domain.Commands;
using SSOAPI.Domain.Core.Bus;
using SSOAPI.Domain.Core.Models;
using SSOAPI.Infra.Data.Models;
using SSOAPI.Infra.Mail;

namespace SSOAPI.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IEventBus _bus;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserAppService(IConfiguration configuration, IWebHostEnvironment hostingEnvironment, IEventBus bus,
                SignInManager<User> signInManager,
                UserManager<User> userManager)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _bus = bus;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public async Task<RequestResult> RegistrarNovoUser(RegistroUserViewModel registroUserViewModel)
        {
            var registrarNovoUserCommand = registroUserViewModel.Map(new RegistrarNovoUserCommand());

            var result = await _bus.SendCommand(registrarNovoUserCommand);

            if (!result.isValid) return result;

            var mailVariables = new Dictionary<string, string>
            {
                {"#USUARIONOME", registroUserViewModel.Nome},
                {"#USUARIOEMAIL", registroUserViewModel.Email}
            };

            var message = new MailMessage();
            message.To.Add(registroUserViewModel.Email);

            var mailBuilder = new MailBuilder(message, _configuration);
            var mailResult = mailBuilder.PreparaTemplate("Confirmação de Cadastro", "ConfirmarCadastro", mailVariables);


            new MailService(mailResult, _configuration).Send();

            return result;

        }

        public async Task<RequestResult> AlterarSenhaUser(AlterarSenhaViewModel alterarSenhaViewModel)
        {
            var alterarSenhaCommand = alterarSenhaViewModel.Map(new AlterarSenhaUserCommand());
            return await _bus.SendCommand(alterarSenhaCommand);
        }

        public async Task<RequestResult> RecuperarSenha(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
                return new RequestResult
                {
                    isValid = false,
                    Errors = new List<string>
                    {
                        "Usuário não encontrado"
                    }
                };

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            var mailVariables = new Dictionary<string, string>
            {
                {"#USUARIONOME", user.Nome},
                {"#USUARIOEMAIL", user.Email},
                {"#URI", $"{_configuration["Client:ChangePassword"]}?token={resetToken}&email={user.Email}"}
            };

            var message = new MailMessage();
            message.To.Add(user.Email);

            var mailBuilder = new MailBuilder(message, _configuration);
            var mailResult = mailBuilder.PreparaTemplate("Recuperação de Senha", "RecuperacaoSenha", mailVariables);


            new MailService(mailResult, _configuration).Send();


            return new RequestResult
            {
                isValid = true,
                Result = new RecuperarSenhaViewModel { Token = resetToken }
            };

        }

        public async Task<RequestResult> Autenticar(LoginUserViewModel loginUserViewModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginUserViewModel.Email, loginUserViewModel.Senha, false, true);

            return new RequestResult
            {
                isValid = result.Succeeded
            };
        }

        public async Task<RequestResult> GetUsuario(string Id)
        {
            var result = await _userManager.FindByIdAsync(Id);

            if (result is null)
                return new RequestResult
                {
                    isValid = false,
                    Errors = new List<string>
                {
                    "Usuário não encontrado"
                }
                };

            return new RequestResult
            {
                isValid = true,
                Result = new UserResponseViewModel
                {
                    Email = result.Email,
                    Nome = result.Nome
                }
            };

        }

    }
}
