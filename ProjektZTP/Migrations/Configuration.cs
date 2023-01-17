namespace ProjektZTP.Migrations
{
    using Newtonsoft.Json;
    using ProjektZTP.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ProjektZTP.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        private class JsonWord
        {
            public string Eng { get; set; }
            public string Pol { get; set; }
        }

        protected override void Seed(ProjektZTP.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (context.Words.Count() < 100)
            {
                string wordsJson;
                List<JsonWord> words = new List<JsonWord>();
                string path = System.AppDomain.CurrentDomain.BaseDirectory;

                for (int i = 0; i < 1075; i++)
                {
                    words.Clear();

                    using (StreamReader reader = new StreamReader(path + "/Words/json" + i + ".json"))
                    {
                        wordsJson = reader.ReadToEnd();
                        words = JsonConvert.DeserializeObject<List<JsonWord>>(wordsJson);

                        foreach (var word in words)
                        {
                            Word newWord = new Word()
                            {
                                WordEn = word.Eng,
                                WordPl = word.Pol
                            };

                            context.Words.Add(newWord);
                            context.SaveChanges();
                        }
                    }
                }
            }
        }
    }
}