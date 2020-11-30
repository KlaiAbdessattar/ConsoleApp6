using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace ConsoleApp6.Utils
{
    public class GroupEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ConnectionString { get; set; }
    }
    public static class DependencyInjectionHelper
    {

        private static List<GroupEntity> GroupEntities = null;
        public static List<GroupEntity> LoadGroupEntities()
        {
            if (GroupEntities == null)
            {
                IConfigurationBuilder Builder = new ConfigurationBuilder()
                   .SetBasePath(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                GroupEntities = new List<GroupEntity>();
                IConfigurationRoot configuration = Builder.Build();
                IConfigurationSection section = configuration.GetSection("Entities");
                foreach (IConfigurationSection entity in section.GetChildren())
                {
                    string entityCode = entity.Key;
                    string connectionStringPath = "Entities:" + entityCode + ":ConnectionString";
                    string namePath = "Entities:" + entityCode + ":Name";
                    GroupEntities.Add(new GroupEntity()
                    {
                        Name = configuration.GetSection(namePath).Get<string>(),
                        ConnectionString = configuration.GetSection(connectionStringPath).Get<string>(),
                        Code = entityCode
                    });
                }
            }
            return GroupEntities;
        }

        private static string GetConnectionStringByEntityCode(string code)
        {
            return GroupEntities.Find(e => e.Code == code)?.ConnectionString;
        }

        public static void LoadDBContext(IServiceCollection Services)
        {
            LoadGroupEntities();
            IConfigurationBuilder Builder = new ConfigurationBuilder()
               .SetBasePath(System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Services.AddScoped(typeof(DEV_KLAIContext), provider =>
            {
                HttpContext httpContext = provider.GetRequiredService<IHttpContextAccessor>().HttpContext;
                string entityCode = httpContext.Request.Headers["X-ENTITY"].ToString();
                string connectionString = GetConnectionStringByEntityCode(entityCode);
                if (connectionString == null)
                    return null;
                DbContextOptionsBuilder<DEV_KLAIContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<DEV_KLAIContext>().UseSqlServer(connectionString);
                // dbContextOptionsBuilder.UseLazyLoadingProxies();
                return new DEV_KLAIContext(dbContextOptionsBuilder.Options);
            });
        }
       // SI besoin de besoin d'in couche DAO
        public static void LoadDAOs(IServiceCollection Services)
        {
            foreach (Type daoType in Utilities.LoadClassesByNameSpace("ConsoleApp6.DAL").ToList())
            {
                Services.AddScoped(daoType, daoType);
            }
        }
        public static void LoadServices(IServiceCollection Services)
        {
            foreach (Type serviceType in Utilities.LoadClassesByNameSpace("ConsoleApp6.Services").ToList())
            {
                Services.AddScoped(serviceType, Utilities.GetTypeByIdentity("ConsoleApp6.ServicesImpl." + serviceType.Name.Substring(1)));
            }
        }
    }
}
