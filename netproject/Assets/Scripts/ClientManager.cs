namespace Project.Managers
{
    using UnityEngine;
    using Unity.Netcode;
	using UnityEngine.SceneManagement;

	public class ClientManager : MonoBehaviour
    {
        [SerializeField]
        private NetworkManager m_netManager;
        public NetworkManager NetManager => m_netManager;

        public void StartDedicatedClient()
        {
            // m_netManager.NetworkConfig.ConnectionApproval = true;
            // ConnectClient();
            NetManager.StartClient();
            // Debug.Log(m_netManager.);
            Debug.Log("Started client");
        }

        private void ConnectClient()
        {

            var payload = JsonUtility.ToJson(new ConnectionPayload()
            {
                playerId = "0",
                clientScene = SceneManager.GetActiveScene().buildIndex,
                playerName = "William Curtis"
            });
            var payloadBytes = System.Text.Encoding.UTF8.GetBytes(payload);
            m_netManager.NetworkConfig.ConnectionData = payloadBytes;
            NetManager.StartClient();
        }
    }
}
