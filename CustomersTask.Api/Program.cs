using CustomersTask.Infrastructure.Repositories;
using CustomersTask.Infrastructure.Contracts;
using CustomersTask.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CustomerTask.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);


CorsConfiguration corsConfiguration = new CorsConfiguration();
builder.Configuration.GetSection("CorsConfiguration").Bind(corsConfiguration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowedOrigins", builder =>
    {
        builder.WithOrigins(corsConfiguration.Origins)
       .WithMethods("GET", "POST", "PUT", "DELETE")
             .WithHeaders("Content-Type");
});
});

// Add services to the container.
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();


builder.Services.AddControllers();

var dbConfig = builder.Configuration.GetSection(nameof(DBConfiguration)).Get<DBConfiguration>();
builder.Services.AddDbContext<CustomerDbContext>(options => options.UseSqlServer(dbConfig.ConnectionString, sql => { }));

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

app.UseCors("AllowedOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
