using BaltaStore.Domain.StoreContext.Handlers;
using BaltaStore.Domain.StoreContext.Repositories;
using BaltaStore.Domain.StoreContext.Services;
using BaltaStore.Infra.StoreContext.DataContexts;
using BaltaStore.Infra.StoreContext.Repositories;
using BaltaStore.Infra.StoreContext.Services;
using BaltaStore.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddResponseCompression();

builder.Services.AddScoped<BaltaDataContext, BaltaDataContext>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddTransient<CustomerHandler, CustomerHandler>();

// Settings.ConnectionString = $"{builder.Configuration["ConnectionString"]}";
Settings.ConnectionString = $"{builder.Configuration.GetConnectionString("DefaultConnectionString")}";


builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "Balta store", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Balta store - v1");
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseResponseCompression();

app.Run();
