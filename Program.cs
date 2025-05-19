using Microsoft.EntityFrameworkCore;
using poke_poke.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// adding controllers
builder.Services.AddControllers();

// enable HTTPS redirection
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 7005;
});

// DB Config
builder.Services.AddDbContext<GameScoreContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddDbContext<JokeAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("JokeConnection"))
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// cores policies TODO: remove in Production!!!!
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// app
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// add cores policy
//TODO: Remove in production!!!
app.UseCors("AllowAll");

// serves default files this should be called before UseStaticFiles() method
app.UseDefaultFiles();

// Serve static files like css, js, images and so on from wwwroot folder
app.UseStaticFiles();

// enable routing
app.UseRouting();

// map Controller routes directly 
app.MapControllers();

// enforces HTTPS
//app.UseHttpsRedirection();

app.Run();