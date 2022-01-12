using Microsoft.EntityFrameworkCore;

namespace test.api.Utilidades
{
    /// <summary>
    /// Esta es una clase con métodos estáticos para extender los métodos IApplicationBuilder, 
    /// IConfiguration, DbContextOptionsBuilder y demás que se necesiten.
    /// Fuente: https://code-maze.com/aspnetcore-webapi-best-practices/
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Esta funcion obtiene cadena de conexion dependiendo de la bd que se use. 
        /// Puede ser refactorizada (cambiar nombre, agregar o quitar parametros) 
        /// para en caso de utilizar varias bases de datos y/o varios
        /// administradores de BD (oracle, mssql, mysql, redis, sqlite, etc, etc).
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="dataBase"></param>
        public static string ObtenerCadenaDeConexion(this IConfiguration Configuration, string dataBase)
        {
            string connectionString;
            switch (dataBase)
            {
                case Providers.MYSQL:
                    connectionString = Configuration.GetConnectionString("databaseMYSQL");
                    break;
                case Providers.MSSQL:
                default:
                    connectionString = Configuration.GetConnectionString("databaseMSSQL");
                    break;
            }
            return connectionString;
        }


        /// <summary>
        /// Función para decidir qué base de datos utilizará el EF de la APP.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="connectionString"></param>
        /// <param name="dataBase"></param>
        public static void UseDatabase(this DbContextOptionsBuilder options, string connectionString, string dataBase, int commandTimeOut = 60)
        {
            switch (dataBase)
            {
                case Providers.MYSQL:
                    options.UseMySql(connectionString, new MySqlServerVersion(new Version(8, 0, 15)),
                                        mySqlOptions =>
                                        {
                                            mySqlOptions.CommandTimeout(commandTimeOut);
                                        });
                    break;
                case Providers.MSSQL:
                default:
                    options.UseSqlServer(connectionString,
                                        sqlServerOptions => sqlServerOptions.CommandTimeout(60));
                    break;
            }
        }
    }

}
