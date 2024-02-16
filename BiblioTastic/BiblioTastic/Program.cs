var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(); 

var app = builder.Build();
                app.UseCors(builder =>
                    // builder.WithOrigins("http://localhost:4200")
                    builder.WithOrigins("https://bibliotastic.azurewebsites.net")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());

app.UseHttpsRedirection();

app.MapControllers(); 

app.Run();
