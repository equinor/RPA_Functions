using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using rpa_functions.rpa_pc269;

[assembly: FunctionsStartup(typeof(rpa_functions.Startup))]

namespace rpa_functions
{
    class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string SqlConnection = Environment.GetEnvironmentVariable("PC269_DBCONNECTION");
            builder.Services.AddDbContext<PC269Context>(
                options => options.UseSqlServer(SqlConnection));
        }
    }
}
