using System;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Write;
using Microsoft.Extensions.Configuration;
using System.IO;
using Shared.Utils;


namespace Server.DAL
{
    public class DataAccessWrite
    {
        public DataAccessWrite(IConfiguration configuration)
        {
            Configuration = configuration;
            var serverFolder = Directory.GetParent(Directory.GetCurrentDirectory()).ToString() + Path.DirectorySeparatorChar + "Server" + Path.DirectorySeparatorChar;
            //_optionsBuilder.UseSqlite("DataSource=" + serverFolder + Configuration["AppSettings:DbLocation"] + Path.DirectorySeparatorChar + "Car.db");
            _optionsBuilder.UseSqlServer(Helpers.GetSqlConnection());
        }
        IConfiguration Configuration { get; set; }

        private readonly DbContextOptionsBuilder<ApiContext> _optionsBuilder = new DbContextOptionsBuilder<ApiContext>();
    }
}
