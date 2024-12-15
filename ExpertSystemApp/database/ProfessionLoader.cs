using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Windows.Markup;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace ExpertSystemApp.database
{
    class ProfessionLoader
    {
        public ProfessionLoader() { }
        

        public List<Profession> LoadData(string filepath)
        {
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException("Файл не найден: " + filepath);
            }
            string jsonData = File.ReadAllText(filepath);

            List<Profession> Proffesions = JsonConvert.DeserializeObject<List<Profession>>(jsonData);
            return Proffesions;
        }
        public List<Profession> Proffesions { get; set; } 

    }
}
