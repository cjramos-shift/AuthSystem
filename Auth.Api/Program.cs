using System.Security.Claims;
using Auth.Application.DTOs.Requests;
using Auth.Application.Ports;
using Auth.Application.Services;
using Auth.Domain.AccountContext.Entities;
using Auth.Domain.AccountContext.Extensions;
using Auth.Domain.AccountContext.ValueObjects;
using Auth.Infra.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Add services to the container.
builder.Services.AddAuthorization();

builder.Services.AddScoped<IAuth, AuthenticationJwtService>();
builder.Services.AddScoped<ILogin, UserRepository>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(builder.Configuration["AuthSecretKey"])),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/auth", ([FromBody] UserLoginDTO login, [FromServices] IAuth auth) =>
{
    return auth.JwtAuthHandler(login);
})
.WithName("PostAuthentication");

app.MapGet("/restrito", () =>
{

}).RequireAuthorization().WithDescription("Restrito");

app.MapGet("/gerarClaim", (ClaimsPrincipal userClaim) => new User
{
    Id = userClaim.GetId(),
    Name = userClaim.GetName(),
    Password = new Password(userClaim.GetPassword()),
    Email = new Email(userClaim.GetEmail()),
    Role = userClaim.GetRoles()
}).RequireAuthorization().WithDescription("Retorna os Claims contidos dentro do JWT.");

app.Run();
