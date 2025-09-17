using System.Collections.Generic;
using scriptableObjects;
using UnityEngine;

namespace MainCharacterScripts
{
    [System.Serializable]
    public class PlayerLeaderboard
    {
        public List<PlayerData> players = new List<PlayerData>();
    }
}