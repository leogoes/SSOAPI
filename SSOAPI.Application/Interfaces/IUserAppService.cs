using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SSOAPI.Application.ViewModels.User;
using SSOAPI.Domain.Core.Models;

namespace SSOAPI.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<RequestResult> RegistrarNovoUser(RegistroUserViewModel registroUserViewModel);
        Task<RequestResult> AlterarSenhaUser(AlterarSenhaViewModel alterarSenhaViewModel);
        Task<RequestResult> RecuperarSenha(string email);
        Task<RequestResult> Autenticar(LoginUserViewModel loginUserViewModel);
        Task<RequestResult> GetUsuario(string Id);
    }
}
