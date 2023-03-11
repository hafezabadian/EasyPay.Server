using EasyPay.Data.DatabaseContext;
using EasyPay.Repo.Infrastructure;
using EasyPay.Services.Auth.Interface;
using EasyPay.Services.Auth.Service;
using EasyPay.Common.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using Azure;
using static System.Net.Mime.MediaTypeNames;
using EasyPay.Services.Seed.Interface;
using EasyPay.Services.Seed.Service;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

#region Swagger option variables

//----------| this region include OpenApi Swagger Configuration |----------\\

var securityScheme = new OpenApiSecurityScheme()
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JSON Web Token based security",
};

var securityReq = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }
};

var contact = new OpenApiContact()
{
    Name = "Ali Hafezabadian",
    Email = "hafezabadian@gmail.com",
    Url = new Uri("http://www.hafezabadian.com")
};

var license = new OpenApiLicense()
{
    Name = "Free License",
    Url = new Uri("http://www.hafezabadian.com")
};

var info = new OpenApiInfo()
{
    Version = "v1",
    Title = "Easy Pay Api",
    Description = "third party banking gate",
    TermsOfService = new Uri("http://www.example.com"),
    Contact = contact,
    License = license
};

var doc = new OpenApiDocument()
{

};
//-------------------------------------------------------------------------//

#endregion

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    //---------| insert swagger coniguration variable here |----------\\
    o.SwaggerDoc("v1", info);
    o.AddSecurityDefinition("Bearer", securityScheme);
    o.AddSecurityRequirement(securityReq);
    //----------------------------------------------------------------//
});
builder.Services.AddCors();
builder.Services.AddScoped<IUnitOfWork<EasyPayDbContext>, UnitOfWork<EasyPayDbContext>>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISeedService, SeedService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

var app = builder.Build();

// ---------| Dependency Injection & Using Service at project startup |----------\\

//using (var serviceScope = app.Services.CreateScope())
//{
//    var services = serviceScope.ServiceProvider;

//    var Seeder = services.GetRequiredService<ISeedService>();
//    Seeder.SeedUser();
//}

// ------------------------------------------------------------------------------//

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

else
    {
        app.UseExceptionHandler(builder =>
    {
        builder.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = Text.Plain;
            var error = context.Features.Get<IExceptionHandlerFeature>();

            if (error != null)
            {
                context.Response.AddAppError(error.Error.Message);
                //context.Response.Headers.Add("App-Error", error.Error.Message);
                //context.Response.Headers.Add("Access-Control-Expose-Headers", "App-Error");
                //context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                await context.Response.WriteAsync(error.Error.Message);
            }
        });
    });
    }



app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
