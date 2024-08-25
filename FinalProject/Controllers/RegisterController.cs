﻿using FinalProject.BLL.Models.DTOs.RegisterDTOs;
using FinalProject.BLL.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace FinalProject.API.Controllers
{
	[Route("api/[controller]/[action]")]
	[ApiController]
	public class RegisterController : ControllerBase
	{
		private readonly IRegisterService registerService;

		public RegisterController(IRegisterService registerService)
		{
			this.registerService = registerService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllRegisterUser()
		{
			var result = await registerService.GelAllUser();
			return StatusCode(result.StatusCode, result);
		}

		[HttpGet]
		public async Task<IActionResult> GetRolesToUserAsync(string Id)
		{
			var result = await registerService.GetRolesToUserAsync(Id);
			return StatusCode(result.StatusCode, result);
		}


		[HttpPost]
		public async Task<IActionResult> CreateUser(UserCreateDTO userCreateDTO)
		{
			var result = await registerService.Create(userCreateDTO);
			return StatusCode(result.StatusCode, result);
		}



		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> CreateCompany(UserCreateDTO user)
		{
			var result = await registerService.Create(user);
			return StatusCode(result.StatusCode, result);
		}


		[HttpPost]
		
		public async Task<IActionResult> AssignRoleToUserAsync(string userId, string[] roles)
		{
			var result = await	registerService.AssignRoleToUserAsync($"{userId}", roles);
			return StatusCode(result.StatusCode, result);
		}


	}
}
