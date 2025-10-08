namespace scriptableObjects
{
    [System.Serializable]
    public class PlayerData
    {
        public string username;
        public int highScore;

        public PlayerData(string playerName, int playerScore)
        {
            this.username = playerName;
            this.highScore = playerScore;
        }
    }
}