using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SSOAPI.Domain.Commands;
using SSOAPI.Domain.Core.Models;
using SSOAPI.Infra.Data.Models;

namespace SSOAPI.Domain.CommandHandler
{
    public class UserCommandHandler : IRequestHandler<RegistrarNovoUserCommand, RequestResult>, 
                                        IRequestHandler<AlterarSenhaUserCommand, RequestResult>
    {

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public UserCommandHandler(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<RequestResult> Handle(RegistrarNovoUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = false,
                Nome = request.Nome
                
            };

            var result = await _userManager.CreateAsync(user, request.Senha);
            return new RequestResult
            {
                isValid = result.Succeeded,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public async Task<RequestResult> Handle(AlterarSenhaUserCommand request, CancellationToken cancellationToken)
        {

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
                return new RequestResult
                {
                    isValid = false,
                    Errors = new List<string>
                    {
                        "Usuario não encontrado"
                    }
                };

            var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Senha);

            return new RequestResult
            {
                isValid = false,
                Errors = result.Errors.Select(e => e.Description)
            };

        }
    }
}
