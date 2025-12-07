using System.Collections.Generic;
using scriptableObjects;

namespace generalScripts.Interfaces
{

    public interface IDataManager
    {

        string CurrentPlayerUsername { get; }
        

        void SetCurrentPlayer(string username);
        

        void LoadLeaderboard();
        

        void AddOrUpdateHighScore(string username, int newScore);
        

        List<PlayerData> GetSortedLeaderboard();
    }
}
