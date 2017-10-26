using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

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
            var numberOfPlayer = ConnectedPlayers.Count();
            if (numberOfPlayer < Game.MaxNumberOfPlayers)
            {
                ConnectedPlayers.Add(newPlayer);
            } else
            {
                //TODO : throw some exception
            }
        }

        public int GetNumberOfConnectedPlayers()
        {
            return ConnectedPlayers.Count;
        }
    }
}
