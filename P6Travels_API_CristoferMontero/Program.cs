using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using P6Travels_API_CristoferMontero.Models;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        

        //agregamos codigo que permite inyeccion de la cadena
        //de conexion contenida en appsettings.json

        //1. obtener valor de cadena de conexion en appsettings
        var CnnStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("CnnStr"));

        //2. Como en la conexion eliminamos el password, lo vamos
        //a incluir directamente en este codigo
        CnnStrBuilder.Password = "123Queso";

        //3. Creamos un string con la info de la cadena de conexion
        string cnnStr = CnnStrBuilder.ConnectionString;

        //4. vamos a asignar el valor de esta cadena de conexion 
        //al DBContext que esta en Models 
        builder.Services.AddDbContext<P620242travelsContext>(options => options.UseSqlServer(cnnStr));


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

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}