namespace Netproject.Types
{
    using Unity.Netcode;
    public class PlayerData
    {
        public string Guid { get; set; }
        public int Score { get; set; }

        public PlayerData(string guid, int score = 0)
        {
            Guid = guid;
            Score = 0;
        }

        public void SetPlayerData(NetworkPlayerData playerData)
        {
            Guid = playerData.Guid;
            Score = playerData.Score;
        }
	}
}
