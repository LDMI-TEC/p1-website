using Microsoft.EntityFrameworkCore;
using poke_poke.Repository;
using poke_poke.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// adding controlers
builder.Services.AddControllers();

// enable HTTPS redrirection
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7005;
});

// DB Config
builder.Services.AddDbContext<GameScoreContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// app
var app = builder.Build();

// serves default files this should be called before UseStaticFiles() method
app.UseDefaultFiles();

// Serve static files like css, js, images and so on from wwwroot folder
app.UseStaticFiles();

// enable routing
app.UseRouting();

// enalbes authorization
app.UseAuthorization();

// map Controller routes directly 
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// enforces HTTPS
//app.UseHttpsRedirection();

app.Run();