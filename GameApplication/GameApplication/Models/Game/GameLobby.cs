using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace GameApplication.Models.Game
{
    public class GameLobby
    {
        private static int IdGenerator = 0;
        private long LobbyId { get; }

        private GameDescription gameDescription { get; }
        private Player LobbyCreator;
        private List<Player> ConnectedPlayers;

        public GameLobby(GameDescription gameDescription, Player lobbyCreator)
        {
            this.LobbyId = System.Threading.Interlocked.Increment(ref IdGenerator);
            this.gameDescription = gameDescription;
            this.LobbyCreator = lobbyCreator;
            this.ConnectedPlayers = new List<Player> { lobbyCreator };
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Connect(Player newPlayer)
        {
            var numberOfPlayer = ConnectedPlayers.Count();
            if (numberOfPlayer < gameDescription.MaxNumberOfPlayers)
            {
                ConnectedPlayers.Add(newPlayer);
                //newPlayer.handleEvent(new LobbyJoinEvent(LobbyId));
            } else
            {
                //newPlayer.handleEvent(new FullLobbyEvent());
            }
        }
    }
}
