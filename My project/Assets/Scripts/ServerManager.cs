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
        int maxPlayerAmount = 4;

        public void StartDedicatedServer()
        {
            m_transport.MaxPayloadSize = 256000;
            m_transport.MaxSendQueueSize = 1024 * 1024;
            m_transport.MaxConnectAttempts = 4;
            m_transport.ConnectTimeoutMS = 500;
            m_netManager.NetworkConfig.ConnectionApproval = true;
            m_netManager.ConnectionApprovalCallback += ApprovalCheck;
            NetManager.StartServer();
            Debug.Log("Started server");
        }

        void ApprovalCheck(byte[] connectionData, ulong clientId, NetworkManager.ConnectionApprovedDelegate connectionApprovedCallback)
        {
            if (maxPlayerAmount >= m_netManager.ConnectedClients.Count)
            {
                NetManager.DisconnectClient(clientId);
                Debug.Log("Max player amount reached, disconnecting client.");
            }
            Debug.Log("Connection approved.");
            connectionApprovedCallback(true, null, true, Vector3.zero, Quaternion.identity);
        }
    }
}
