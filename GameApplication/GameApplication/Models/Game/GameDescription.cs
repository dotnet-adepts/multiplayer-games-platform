using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameApplication.Models
{
    public class GameDescription
    {
        public string Name { get; set; }
        public string Desription {get; set;}
        public int MinNumberOfPlayers { get; set; }
        public int MaxNumberOfPlayers { get; set; }
        public string Category { get; set; }

        public GameDescription(string name, string desription, int minNumberOfPlayers, int maxNumberOfPlayers, string category)
        {
            Name = name;
            Desription = desription;
            MinNumberOfPlayers = minNumberOfPlayers;
            MaxNumberOfPlayers = maxNumberOfPlayers;
            Category = category;
        }
    }
}
