﻿using FinalProject.BLL.Models.Exception.GenericResponseApi;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.BLL.Models.Filters
{
	public class ValidateModelFilters : IActionFilter
	{
		public void OnActionExecuted(ActionExecutedContext context)
		{
			
		}

		public void OnActionExecuting(ActionExecutingContext context)
		{

			if (!context.ModelState.IsValid)
			{

				if (!context.ModelState.IsValid)
				{
					var messages = context.ModelState
						.SelectMany(message => message.Value.Errors)
						.Select(error => error.ErrorMessage)
						.ToList();

					var response = new GenericResponseApi<List<string>>(messages)
					{
						IsSuccess = false,
						StatusCode = 400
					};

					context.Result = new JsonResult(response)
					{
						StatusCode = 400
					};

				}
			}
		}
	}
}
