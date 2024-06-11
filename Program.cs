using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Services.B.Core.Enums;
using Services.B.Core.Events;
using Services.B.Core.Handlers;
using Services.B.Core.Helpers;
using Services.B.Core.Interfaces;
using Services.B.Core.Models;
using Services.B.Shared;

var builder = WebApplication.CreateBuilder(args);

#region *** ConfigureServices
var appSettings = builder.Configuration.GetSection(nameof(AppSettings));
builder.Services.Configure<AppSettings>(appSettings);

var jwtSettings = builder.Configuration.GetSection(nameof(JwtSettings)).ToJWTSettings();
builder.Services.ConfigureJwtSettings(jwtSettings);
builder.Services.AddJWTAuthentication(jwtSettings);

builder.Services.ConfigureRepositories();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Service B", Version = "v1" });
});

builder.Services.AddHttpClient();
#endregion

var app = builder.Build();

#region *** Configure
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Service B v1"));
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

var eventBus = app.Services.GetRequiredService<IEventBus>();
eventBus.Subscribe<ServiceBEvent, ServiceBEventHandler>(ExchangeTypes.Direct);
#endregion

app.Run();
