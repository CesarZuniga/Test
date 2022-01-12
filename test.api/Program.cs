
using test.api.Data;
using test.api.Models;
using test.api.Repositories;
using test.api.Utilidades;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string baseDeDatos = builder.Configuration.GetSection("Provider").Value;
string smConnectionString = builder.Configuration.ObtenerCadenaDeConexion(baseDeDatos);
builder.Services.AddDbContext<TestContext>(options =>
                                    options.UseDatabase(smConnectionString, baseDeDatos), ServiceLifetime.Transient);
builder.Services.AddHttpContextAccessor();
builder.Services.AddMvc()
                .AddNewtonsoftJson();
builder.Services.AddMvcCore();
builder.Services.AddCors();
// Add services to the container.
builder.Services.AddScoped<IGenircRepository<Clientes>, GenircRepository<TestContext, Clientes>>();
builder.Services.AddScoped<IClientesRepository, ClientesRepository>();
builder.Services.AddScoped<IGenircRepository<Cuentas>, GenircRepository<TestContext, Cuentas>>();
builder.Services.AddScoped<ICuentasRepository, CuentasRepository>();
builder.Services.AddScoped<IGenircRepository<TipoMovimiento>, GenircRepository<TestContext, TipoMovimiento>>();
builder.Services.AddScoped<ITipoMovimientoRepository, TipoMovimientoRepository>();
builder.Services.AddScoped<IGenircRepository<Movimientos>, GenircRepository<TestContext, Movimientos>>();
builder.Services.AddScoped<IMovimientosRepository, MovimientosRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
                    builder.SetIsOriginAllowed(host => true)
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials());
app.UseAuthentication();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});
app.UseStaticFiles();

app.MapControllers();

app.Run();
