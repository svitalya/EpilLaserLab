using EpilLaserLab.Server.Data;
using EpilLaserLab.Server.Data.Applications;
using EpilLaserLab.Server.Data.Auths;
using EpilLaserLab.Server.Data.Branches;
using EpilLaserLab.Server.Data.Employees;
using EpilLaserLab.Server.Data.References;
using EpilLaserLab.Server.Data.Schedules;
using EpilLaserLab.Server.Data.SeasonTicket;
using EpilLaserLab.Server.Data.Services;
using EpilLaserLab.Server.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/api/accessdenied";
        options.LoginPath = "/api/accessdenied";
    });
builder.Services.AddAuthorization();


// Add services to the container.

Action<DbContextOptionsBuilder> contextBuilder = (opt) =>
    opt.UseMySQL(DBConnectData.ConnectString);
builder.Services.AddDbContext<EpilLaserLabContext>(contextBuilder);


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITypeRepository, TypeRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<IServicePricesRepository, ServicePricesRepository>();
builder.Services.AddScoped<ISeasonTicketPriceRepository, SeasonTicketPriceRepository>();
builder.Services.AddScoped<ISeasonTicketRepository, SeasonTicketRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IMasterRepository, MasterRepository>();
builder.Services.AddScoped<IEmployeRepository, EmployeRepository>();
builder.Services.AddScoped<ISchedulesRepository, SchedulesRepository>();
builder.Services.AddScoped<IIntervalsRepository, IntervalsRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IPurchasedSeasonTicketsRepository, PurchasedSeasonTicketsRepository>();
builder.Services.AddScoped<IApplicationsRepository, ApplicationsRepository>();

builder.Services.AddScoped<ImageSaveService>();
builder.Services.AddScoped<TestDocumentService>();

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

app.MapGet("/api/accessdenied", async (HttpContext context) =>
{
    context.Response.StatusCode = 403;
    JsonSerializerOptions options = new JsonSerializerOptions() {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    var json = JsonSerializer.Serialize(new { Message = "ACCESS DENIED" }, options: options);
    await context.Response.WriteAsync(json);
});



app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
