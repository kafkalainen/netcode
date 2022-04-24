
namespace Netproject.Managers
{
    using Types;
    using Extensions;
    using System.Linq;
	using System.Collections.Generic;
    using UnityEngine;
    using Unity.Netcode;

    [RequireComponent(typeof(NetworkObject))]
	public class SessionManager : NetworkBehaviour
    {
        [SerializeField]
        private double m_disconnectTime = 30.0f;
        private NetworkManager m_networkManager;
        private Dictionary<string, ulong> m_playerKeyMap;
        private Dictionary<ulong, NetworkPlayerData> m_playerData;
        private Dictionary<string, double> m_timeOutedClients;

        private void Awake()
        {
            m_playerData = new Dictionary<ulong, NetworkPlayerData>();
            m_playerKeyMap = new Dictionary<string, ulong>();
            m_timeOutedClients = new Dictionary<string, double>();
        }

        private void Update() => DisconnectTimeoutedClients();

        public bool AddSession(string guid, ulong clientId)
        {
            //If player has already a session, change clientId to point to session
            if (m_playerKeyMap.TryGetValue(guid, out var oldClientId))
            {
                Debug.Log("Already a session, updating clientId");
                m_playerData[clientId] = m_playerData[oldClientId];
                m_playerData.Remove(oldClientId);
                m_playerKeyMap[guid] = clientId;
                return false;
            }
            else
            {
                Debug.Log("New session, creating new player data");
                m_playerData[clientId] = new NetworkPlayerData(guid);
                m_playerKeyMap[guid] = clientId;
                return true;
            }
        }

        public bool RemoveSession(string guid)
        {
            if (!m_playerKeyMap.TryGetValue(guid, out var clientId))
            {
                Debug.Log("No session with player guid.");
                return false;
            }
            else
            {
                Debug.Log("Removing session for client");
                m_playerKeyMap.Remove(guid);
                m_playerData.Remove(clientId);
                RemoveClientRpc(clientId);
                return true;
            }
        }

        public NetworkPlayerData? GetPlayerData(ulong clientId)
        {
            if (m_playerData.TryGetValue(clientId, out var playerData))
            {
                return playerData;
            }
            else
            {
                Debug.Log("No player data found");
                return null;
            }
        }

        public NetworkPlayerData? GetPlayerData(string guid)
        {
            if (m_playerKeyMap.TryGetValue(guid, out var clientId))
            {
                if (m_playerData.TryGetValue(clientId, out var playerData))
                {
                    return playerData;
                }
            }
            Debug.Log("No player data found");
            return null;
        }

        public void SetTimeoutToClient(ulong clientId)
        {
            if (!NetworkManager.Singleton.IsServer) return;
            if (!m_playerData.TryGetValue(clientId, out var playerData))
            {
                Debug.Log("No player data associated with client");
            }
            m_timeOutedClients[playerData.Guid] = NetworkManager.Singleton.ServerTime.Time + m_disconnectTime;
            Debug.Log($"Disconnecting {clientId} in {NetworkManager.Singleton.ServerTime.Time + m_disconnectTime}");
        }

        private void DisconnectTimeoutedClients()
        {
            if (!NetworkManager.Singleton.IsServer) return;
            var timeOutedClients = from client in m_timeOutedClients
                        where client.Value < NetworkManager.Singleton.ServerTime.Time
                        select client.Key;
            foreach (var client in timeOutedClients)
            {
                RemoveSession(client);
            }
            m_timeOutedClients.RemoveAll(x => x < NetworkManager.Singleton.ServerTime.Time);
        }

        public void UpdateScore(ulong clientId, NetworkPlayerData data)
        {
            Debug.Log("Updated serverside");
            m_playerData[clientId] = data;
            UpdateScoreClientRpc(clientId, data);
        }

        [ClientRpc]
        private void UpdateScoreClientRpc(ulong clientId, NetworkPlayerData data)
        {
            m_playerData[clientId] = data;
            Debug.Log("Updated data");
        }

        [ClientRpc]
        private void RemoveClientRpc(ulong clientId)
        {
            if (!m_playerData.ContainsKey(clientId)) return;
            m_playerData.Remove(clientId);
            Debug.Log("Removed client");
        }
    }
}
