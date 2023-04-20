// 1. Using to work with EntityFramework
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);

// 2. TODO: Connection with SQL Server Express
const string CONECTIONNAME = "UniversityDB";
var connectionString= builder.Configuration.GetConnectionString(CONECTIONNAME);

// 3. Add Context

builder.Services.AddDbContext<UniversityDBContext>(options=>options.UseSqlServer(connectionString));

// 7. Add Services of JWT Autorization
// TODO:
// builder.Services.Add(builder.Configuration);


// Add services to the container.

builder.Services.AddControllers();

// 4. Add Custom Services (Folder Services)
builder.Services.AddScoped<IStudentsService,StudentsService>();
// TODO : Add the rest of services 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// 8. TODO: conf Swagger to take care of Autorization of  JWT

builder.Services.AddSwaggerGen();

// 5. CORS Configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin();
        builder.AllowAnyMethod();
        builder.AllowAnyHeader();
    });
}
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// 6. Tell app to use Cors
app.UseCors("CorsPolicy");

app.Run();
