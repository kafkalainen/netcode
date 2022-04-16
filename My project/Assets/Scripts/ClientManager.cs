namespace Project.Managers
{
    using UnityEngine;
    using Unity.Netcode;
    public class ClientManager : MonoBehaviour
    {
        [SerializeField]
        private NetworkManager m_netManager;
        public NetworkManager NetManager => m_netManager;

        public void StartDedicatedClient()
        {
            NetManager.StartClient();
            Debug.Log("Started client");
        }
    }
}
