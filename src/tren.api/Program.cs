using System.Threading.RateLimiting;
using Microsoft.EntityFrameworkCore;
using tren.api.Api.Data;
using tren.api.Api.Data.Repositories;
using tren.api.Api.Services;
using tren.api.HttpContextExtensions;
using tren.api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = 429;
    options.GlobalLimiter = PartitionedRateLimiter.CreateChained(
        PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(httpContext.ResolveClientIpAddress(), partition =>
                new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = 30,
                    Window = TimeSpan.FromMinutes(1)//10 request per minute
                })),
        PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(httpContext.ResolveClientIpAddress(), partition =>
                new FixedWindowRateLimiterOptions
                {
                    AutoReplenishment = true,
                    PermitLimit = 6000,
                    Window = TimeSpan.FromHours(1)//6000 request per hour
                })));
});


// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IWordRepository, WordRepository>();
builder.Services.AddScoped<ITranslateService, TranslateService>();

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"), p => p.SetPostgresVersion(9,6)));

builder.Services.AddCors(o => o.AddPolicy("AllowAllPolicy", options =>
{
    options.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
}));

builder.Services.AddLogging();
builder.Services.AddControllers();

var app = builder.Build();
app.UseRateLimiter();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<AppDbContext>();
    if (context.Database.GetPendingMigrations().Any())
    {
        context.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}
app.UseCors("AllowAllPolicy");

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();