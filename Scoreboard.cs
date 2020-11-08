using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osu_XZTraining
{
    class Scoreboard
    {
        public Dictionary<DateTime, int> Scores = new Dictionary<DateTime, int>();
        public KeyValuePair<DateTime, int> TopScore = new KeyValuePair<DateTime, int>(DateTime.Now, 0);
        public void Save(string fileName)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            System.IO.File.WriteAllText(fileName, json);
        }

        public static Scoreboard FromFile(string fileName)
        {
            string json = System.IO.File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<Scoreboard>(json);
        }
    }
}
