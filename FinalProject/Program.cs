using FinalProject.DAL.Context;
using FinalProject.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var services = builder.Services;

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


services.AddIdentity<AppUser, AppRole>(op =>
{
	op.Password.RequireNonAlphanumeric = false;
	op.Password.RequiredLength = 8;
	op.Password.RequireLowercase = true;
	op.Password.RequiredUniqueChars = 0;

	op.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppDBContext>().AddDefaultTokenProviders();


services.ConfigureApplicationCookie((configure) =>
{
	configure.Events.OnRedirectToLogin = context =>
	{
		context.Response.StatusCode = StatusCodes.Status401Unauthorized;
		return Task.CompletedTask;
	};

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
