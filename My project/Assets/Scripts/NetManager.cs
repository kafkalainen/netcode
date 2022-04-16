namespace Project.Managers
{
    using UnityEngine;
    using Unity.Netcode;
	using Unity.Netcode.Transports.UTP;

    [RequireComponent(typeof(ServerManager))]
    [RequireComponent(typeof(ClientManager))]
	public class NetManager : MonoBehaviour
    {
		private NetworkManager m_networkManager;
        private ClientManager m_clientManager;
        private ServerManager m_serverManager;

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 100, 50), "Start Server")) m_serverManager.StartDedicatedServer();
            if (GUI.Button(new Rect(10, 70, 100, 120), "Start Client")) m_clientManager.StartDedicatedClient();
        }
    }
}
