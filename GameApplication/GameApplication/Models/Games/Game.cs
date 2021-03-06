﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameApplication.Factories;
using GameApplication.Models.Games.Battleship;
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
        public IGameSessionFactory GameSessionFactory;

        public Game(string name, string description, int minNumberOfPlayers, int maxNumberOfPlayers, string category, IGameSessionFactory gameSessionFactory)
        {
            Name = name;
            Description = description;
            MinNumberOfPlayers = minNumberOfPlayers;
            MaxNumberOfPlayers = maxNumberOfPlayers;
            Category = category;
            GameSessionFactory = gameSessionFactory;
        }

        public IGameSession StartGameSession(long lobbyId, List<Player> players)
        {
            return GameSessionFactory.Create(lobbyId, players);
        }

        public static class Names
        {
            public const string Snake = "Snake";
            public const string Battleship = "Statki";
        }


    }


}
