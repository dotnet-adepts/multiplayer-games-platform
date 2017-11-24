using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GameApplication.Models.Games
{
    public class Game
    {
        public string Name { get; set; }
        public string Description {get; set;}
        public string ThumbnailImagePath {get; set;}
        public int MinNumberOfPlayers { get; set; }
        public int MaxNumberOfPlayers { get; set; }
        public string Category { get; set; }
        public string GameSessionControllerName { get; set; } 

        public Game(string name, string description, int minNumberOfPlayers, int maxNumberOfPlayers, string category, Type gameSessionController)
        {
            Name = name;
            Description = description;
            MinNumberOfPlayers = minNumberOfPlayers;
            MaxNumberOfPlayers = maxNumberOfPlayers;
            Category = category;
            GameSessionControllerName = getControllerNameWithoutSuffix(gameSessionController.Name);
        }

        private string getControllerNameWithoutSuffix(string controllerName)
        {
            var controllerSuffix = "Controller";
            if (!controllerName.EndsWith(controllerSuffix))
            {
                throw new ArgumentException("'Controller' suffix was not found in " + controllerName);
            }

            int suffixIndex = controllerName.Length - controllerSuffix.Length;
            return controllerName.Remove(suffixIndex);
        }

        public static class Names
        {
            public const string Snake = "Snake";
            public const string Battleship = "Statki";
        }

    }
}
