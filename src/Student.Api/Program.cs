using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StudentApi.Common;
using StudentApi.Data;       // your DbContext namespace
using StudentApi.Middleware;
using StudentApi.Models;
using StudentApi.Repositories;
using StudentApi.Services;     // if needed
using System.Text;



var builder = WebApplication.CreateBuilder(args);

//Configure Versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;

    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

/*builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});*/

//Enable Versioned Swagger
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});


// Add services to the container.
builder.Services.AddControllers();   // important for JsonPatchDocument
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<StudentApi.Swagger.ConfigureSwaggerOptions>();


// DbContext registration (what we already added earlier)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


//Configure JWT Authentication
builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});
//Register Auth Service
builder.Services.AddScoped<IAuthService, AuthService>();



var app = builder.Build();

Console.WriteLine(app.Environment.EnvironmentName);
//Enable Swagger Middleware
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();

//    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

//    app.UseSwaggerUI(options =>
//    {
//        foreach (var description in provider.ApiVersionDescriptions)
//        {
//            options.SwaggerEndpoint(
//                $"/swagger/{description.GroupName}/swagger.json",
//                $"Student API {description.GroupName.ToUpperInvariant()}"
//            );
//        }
//    });
//}
app.UseSwagger();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

app.UseSwaggerUI(options =>
{
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint(
            $"/swagger/{description.GroupName}/swagger.json",
            $"Student API {description.GroupName.ToUpperInvariant()}"
        );
    }
});

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthentication(); // 👈 BEFORE authorization
app.UseAuthorization();

app.UseMiddleware<ExceptionMiddleware>();

// Map controllers
app.MapControllers();


app.Run();
