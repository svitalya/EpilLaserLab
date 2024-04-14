using EpilLaserLab.Server.Data;
using EpilLaserLab.Server.Data.References;
using EpilLaserLab.Server.Data.UserData;
using EpilLaserLab.Server.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/accessdenied";
        options.LoginPath = "/accessdenied";
    });
builder.Services.AddAuthorization();


// Add services to the container.

Action<DbContextOptionsBuilder> contextBuilder = (opt) =>
    opt.UseMySQL(builder.Configuration.GetConnectionString("Default")
    ?? throw new Exception("Не удалось подключиться к БД"));
builder.Services.AddDbContext<EpilLaserContext>(contextBuilder);


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:5173");
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.AllowCredentials();
                      });
});


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/accessdenied", async (HttpContext context) =>
{
    context.Response.StatusCode = 403;
    var json = JsonSerializer.Serialize(new { Message = "ACCESS DENIED" });
    await context.Response.WriteAsync(json);
});


app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
