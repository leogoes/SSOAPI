using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SSOAPI.API.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly List<string> _errors = new List<string>();

        protected ActionResult CustomResponse(object result = null)
        {
            if (!IsOperationValid())
                return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
                {
                    {"Messages", _errors.ToArray()}
                }));

            return result == null ? StatusCode(StatusCodes.Status204NoContent, "") : Ok(new
            {
                success = true,
                data = result
            });
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                AddError(error.ErrorMessage);
            }

            return CustomResponse();
        }
        
        protected bool IsOperationValid()
        {
            return !_errors.Any();
        }

        protected void AddError(string erro)
        {
            _errors.Add(erro);
        }

        protected void AddInRangeError(IEnumerable<string> erro)
        {
            _errors.AddRange(erro);
        }

        protected void ClearErrors()
        {
            _errors.Clear();
        }
    }
}
