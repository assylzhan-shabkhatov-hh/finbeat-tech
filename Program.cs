using FinBetApi.Infrastructure.DataAccess.SqlServer;
using FinBetApi.Infrastructure.DataAccess.SqlServer.Extensions;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddFinbetDbContext(configuration);


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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var db = builder.Services.BuildServiceProvider().GetRequiredService<FinBetDbContext>();
db.Database.EnsureCreated();

app.Run();
