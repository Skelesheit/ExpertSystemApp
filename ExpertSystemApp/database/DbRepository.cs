using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExpertSystemApp.database
{
    internal class DbRepository
    {

        public DbRepository()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("Data Source=professions.db")
            .Options;
            bool anyProfession;
            using (var context = new ApplicationDbContext(options))
            {
                anyProfession = context.Professions.Any();
            }

            if (anyProfession)
            {
                LoadProffessions();
            }
        }

        public void LoadProffessions()
        {
            string filename = "professions.json";
            string folderPath = GetDatabaseFolder();
            string filepath = Path.Combine(folderPath, filename);

            ProfessionLoader loader = new ProfessionLoader();
            List<Profession> professions = loader.LoadData(filepath);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("Data Source=professions.db")
            .Options;
            using (var context = new ApplicationDbContext(options))
            {
                var databasePath = context.Database.GetDbConnection().DataSource;
                context.AddRange(professions);
            }
        }

        public static string GetDatabaseFolder()
        {
            string folder = "database";
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(currentDirectory)?.Parent?.Parent?.FullName;
            Console.WriteLine(projectDirectory);
            string folderpath = Path.Combine(projectDirectory, folder);
            return folderpath;
        }

        public Profession ChooseProfession(string name)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("Data Source=professions.db")
            .Options;
            Profession profession;
            using (var context = new ApplicationDbContext(options))
            {
                profession = context.Professions.FirstOrDefault(p => p.Name == name);
            }
            return profession;
        }
    }

}
