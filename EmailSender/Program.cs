using EmailSender.Service;
using EmailSender.Service.Impl;
using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IMessageService, MessageService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
        .AddAuthentication(o =>
        {
            o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
            o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
            o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie()
        .AddGoogleOpenIdConnect(options =>
        {
            options.ClientId = "230123170832-ln9kjjvflv4faevhkt34teg0kc0nfcob.apps.googleusercontent.com";
            options.ClientSecret = "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";
        });



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12
        | System.Net.SecurityProtocolType.Tls
        | System.Net.SecurityProtocolType.Tls11
        | System.Net.SecurityProtocolType.Tls13;
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
