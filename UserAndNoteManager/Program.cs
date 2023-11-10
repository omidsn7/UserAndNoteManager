using Microsoft.EntityFrameworkCore;
using UserAndNoteManager.Data;
using UserAndNoteManager.DAL;
using UserAndNoteManager.Interface;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using UserAndNoteManager.MyHub;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<UANDbContext>(Options => Options.UseSqlite(builder.Configuration.GetConnectionString("MyCon")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserManager, UserManager>();
builder.Services.AddTransient<INoteManager, NoteManager>();

builder.Services.AddSignalR();

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

app.MapHub<HubContext>("/HubContext");

app.Run();
