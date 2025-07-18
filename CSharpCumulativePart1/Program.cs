using CSharpCumulativePart1.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<SchoolDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("SchoolDb"),
        new MySqlServerVersion(new Version(8, 0, 28))
    ));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();
    db.Teachers.FirstOrDefault();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Use((context, next) =>
{
    context.Request.Path = context.Request.Path.Value!.TrimEnd('/');
    return next();
});


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TeacherPage}/{action=List}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TeacherPage}/{action=List}/{id?}");


app.UseDeveloperExceptionPage();


app.Run();
