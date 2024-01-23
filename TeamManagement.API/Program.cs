using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using TeamManagement.API.APIs;
using TeamManagement.Application.DTOs.Member;
using TeamManagement.Application.Services;
using TeamManagement.Domain.AggregatesModel.CountryAggregate;
using TeamManagement.Domain.AggregatesModel.MemberAggregate;
using TeamManagement.Infrastructure.ConnectionFactory;
using TeamManagement.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
   options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
builder.Services.Configure<JsonOptions>(options =>
{
   options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddValidatorsFromAssemblyContaining(typeof(MemberInputValidator));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IDbConnectionFactory, DapperConnectionFactory>();
builder.Services.AddSingleton<IMemberRepository, MemberRepository>();
builder.Services.AddSingleton<ICountryService, CountryService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.ConfigureMembersApi();

app.Run();

namespace TeamManagement.API
{
   public partial class Program { }
}
