using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HomeWork_5_1_2019.Data
{
    class PeopleQuestionsFactory: IDesignTimeDbContextFactory<PeopleQuestionsContext>
    {
        public PeopleQuestionsContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}HomeWork-5_1_2019"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new PeopleQuestionsContext(config.GetConnectionString("ConStr"));
        }
    }
}
