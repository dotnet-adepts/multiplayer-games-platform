using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using GameApplication.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace GameApplication.Models.Games
{
    public class Lobby
    {
        private static int _idGenerator = 0;
        public long Id;

        public Game Game { get; }
        public Player Owner;
        public readonly List<Player> ConnectedPlayers;

        public Lobby(Game game, Player owner)
        {
            Id = System.Threading.Interlocked.Increment(ref _idGenerator);
            Game = game;
            Owner = owner;
            ConnectedPlayers = new List<Player> { owner };
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddPlayer(Player newPlayer)
        {
            if (ConnectedPlayers.Contains(newPlayer))
            {
                return;
            }

            var numberOfPlayer = ConnectedPlayers.Count();
            if (numberOfPlayer < Game.MaxNumberOfPlayers)
            {
                ConnectedPlayers.Add(newPlayer);
            } else
            {
                throw new FullLobbyExceptioncs();
            }
        }

        public void RemovePlayer(Player player)
        {
            lock (this)
            {
                ConnectedPlayers.Remove(player);
            }
        }

        public int GetNumberOfConnectedPlayers()
        {
            return ConnectedPlayers.Count;
        }
    }
}
