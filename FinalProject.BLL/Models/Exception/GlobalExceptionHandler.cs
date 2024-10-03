using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging;

namespace FinalProject.BLL.Models.Exception
{
	public class GlobalExceptionHandler : IExceptionFilter
	{
		private readonly ILogger<GlobalExceptionHandler> logger;

		public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
			this.logger = logger;
		}
        public void OnException(ExceptionContext context)
		{

			var statusCode = context.Exception switch
			{
				ValidationException => StatusCodes.Status400BadRequest,

				NullReferenceException => StatusCodes.Status404NotFound,

				UnauthorizedAccessException => StatusCodes.Status401Unauthorized,

				_ => StatusCodes.Status500InternalServerError
			};

			var result = new
			{
				error = statusCode == StatusCodes.Status500InternalServerError
				? "An unexpected error occurred. Please try again later."
				: context.Exception.Message, 
				stackTrace = statusCode == StatusCodes.Status500InternalServerError ? null : context.Exception.StackTrace 
			};

			if(statusCode == StatusCodes.Status500InternalServerError)
			{
				logger.LogError(context.Exception,"An unexpected error occurred: {ErrorMessage}", context.Exception.Message);
			}
			else
			{
				logger.LogWarning("Handled exception: {ErrorMessage}", context.Exception.Message);
			}

			context.Result = new ObjectResult(result)
			{
				StatusCode = statusCode
			};
			context.ExceptionHandled = true;
		}
	}
}
