
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Tls;
using WebApplication1.Data;
   
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);



builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(politica =>
    politica.WithOrigins("http://127.0.0.1:5500").AllowAnyHeader().AllowAnyMethod());
});



// Add services to the container.
builder.Services.AddDbContext<NorthwindContext>(Options => Options.UseMySQL(builder.Configuration.GetConnectionString("northwindDB")));

builder.Services.AddControllers();
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

app.UseDefaultFiles();
app.UseStaticFiles();




app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();


