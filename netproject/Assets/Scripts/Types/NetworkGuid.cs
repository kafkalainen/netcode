namespace Netproject.Types
{
    using Unity.Netcode;

    public struct NetworkGuid : INetworkSerializable
    {
        private ulong m_firstHalf;
        private ulong m_secondHalf;
        public ulong FirstHalf => m_firstHalf;
        public ulong SecondHalf => m_secondHalf;

        public NetworkGuid(ulong first, ulong second)
        {
            m_firstHalf = first;
            m_secondHalf = second;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref m_firstHalf);
            serializer.SerializeValue(ref m_secondHalf);
        }
    }
}
