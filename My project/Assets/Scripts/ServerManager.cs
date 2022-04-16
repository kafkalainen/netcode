namespace Project.Managers
{
    using UnityEngine;
    using Unity.Netcode;
	using Unity.Netcode.Transports.UTP;

	public class ServerManager : MonoBehaviour
    {
        [SerializeField]
        private NetworkManager m_netManager;
        [SerializeField]
        private UnityTransport m_transport;
        public NetworkManager NetManager => m_netManager;

        public void StartDedicatedServer()
        {
            m_transport.MaxPayloadSize = 256000;
            m_transport.MaxSendQueueSize = 1024 * 1024;
            m_transport.MaxConnectAttempts = 4;
            m_transport.ConnectTimeoutMS = 500;
            NetManager.StartServer();
            Debug.Log("Started server");
        }
    }
}
