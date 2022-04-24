namespace Netproject.Types
{
    using Unity.Netcode;

    public struct NetworkGuid : INetworkSerializable
    {

        private int m_timeLow;
        private short m_timeMid;
        private short m_timeHighAndVersion;
        private byte[] m_node;

        public int TimeLow => m_timeLow;
        public short TimeMid => m_timeMid;
        public short TimeHighAndVersion => m_timeHighAndVersion;
        public byte[] Node => m_node;

        public NetworkGuid(int timeLow, short timeMid, short timeHighAndVersion, byte[] node)
        {
            m_timeLow = timeLow;
            m_timeMid = timeMid;
            m_timeHighAndVersion = timeHighAndVersion;
            m_node = node;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref m_timeLow);
            serializer.SerializeValue(ref m_timeMid);
            serializer.SerializeValue(ref m_timeHighAndVersion);
            serializer.SerializeValue(ref m_node);
        }
    }
}
