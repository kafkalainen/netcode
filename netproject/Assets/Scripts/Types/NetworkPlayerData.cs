
namespace Netproject.Types
{
    using Unity.Netcode;
    public struct NetworkPlayerData : INetworkSerializable
    {
        public string Guid;
        public int Score;

        public NetworkPlayerData(string guid, int score = 0)
        {
            Guid = guid;
            Score = score;
        }

		public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref Guid);
            serializer.SerializeValue(ref Score);
        }
	}
}
