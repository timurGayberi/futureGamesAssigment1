using System.Collections.Generic;
using scriptableObjects;

namespace generalScripts.Interfaces
{
    public interface IDataBackend
    {
        void SaveScore(string playerName, int score);
        
        List<PlayerData> GetLeaderboard();
        
        void SavePlayerData(string playerName);
        
    }
}
