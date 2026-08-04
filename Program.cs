using VeterinariaAPI.Data;
using VeterinariaAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ConexionBD>();
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<PropietarioRepository>();
builder.Services.AddScoped<MascotaRepository>();
builder.Services.AddScoped<HojaClinicaRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())

{

    app.UseSwagger();

    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
