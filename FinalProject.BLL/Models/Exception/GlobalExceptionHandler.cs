using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.Exception
{
	public class GlobalExceptionHandler : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{

			var statusCode = context.Exception switch
			{
				ValidationException => StatusCodes.Status400BadRequest,

				NullReferenceException => StatusCodes.Status404NotFound,

				UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
				
				_ => StatusCodes.Status500InternalServerError
			};

			context.Result = new ObjectResult(new
			{
				error = context.Exception.Message,
				stackTrace = context.Exception.StackTrace
			})
			{
				StatusCode = statusCode
			};
		}
	}
}
