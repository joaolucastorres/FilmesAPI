using FilmesAPI.Authorization;
using FilmesAPI.Data;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Database connection

var connectionString = builder.Configuration["ConnectionStrings:FilmeConnection"];

builder.Services.AddDbContext<FilmeContext>(opts =>
opts.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

//Identity configuration
builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<FilmeContext>()
    .AddDefaultTokenProviders();

//Jwt bearer configuration
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey
            (
                Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"]!)
            ),
            ValidateAudience = false,
            ValidateIssuer = false,
            ClockSkew = TimeSpan.Zero
        };
    });

//Access policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("IdadeMinima", policy =>
    {
        policy.AddRequirements(new IdadeMinima(18));
    });
});

builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();

//AutoMapper configuration

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Services configuration
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
