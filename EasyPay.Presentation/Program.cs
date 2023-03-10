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

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddScoped<IUnitOfWork<EasyPayDbContext>, UnitOfWork<EasyPayDbContext>>();
builder.Services.AddScoped<IAuthService, AuthService>();
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
//app.Use(async (context, next) =>
//{
//    var error = context.Features.Get<IExceptionHandlerFeature>();
//    if (error != null)
//    {
//    //context.Response.AddAppError(error.Error.Message);
//    //context.Response.Headers.Add("App-Error", error.Error.Message);
//    //context.Response.Headers.Add("Access-Control-Expose-Headers", "App-Error");
//    //context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

//    }
//    //context.Response.Headers.Add("App-Error", error.Error.Message);
//    context.Response.Headers.Add("x-my-custom-header", "middleware response");
//    await next();
//});

app.UseCors(p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
