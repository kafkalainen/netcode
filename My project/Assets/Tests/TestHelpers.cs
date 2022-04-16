using UnityEngine;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public static class TestHelpers
{
    private static List<NetworkManager> m_networkManagerInstances = new List<NetworkManager>();
    private static int m_oldFrameRate;
    public static bool Create(int clientCount, out NetworkManager server, out NetworkManager[] clients, int targetFrameRate = 60, bool serverFirst = true)
    {
        server = null;
        if (serverFirst)
        {
            server = CreateServer();
        }
        CreateNewClients(clientCount, out clients);
        if (!serverFirst)
        {
            server = CreateServer();
        }
        m_oldFrameRate = Application.targetFrameRate;
        Application.targetFrameRate = targetFrameRate;
        return true;
    }

    public static NetworkManager CreateServer()
    {
        var go = new GameObject("NetworkManager - Server");
        var server = go.AddComponent<NetworkManager>();
        m_networkManagerInstances.Insert(0, server);
        var unityTransport = go.AddComponent<UnityTransport>();
        unityTransport.MaxPayloadSize = 256000;
        unityTransport.MaxSendQueueSize = 1024 * 1024;
        unityTransport.MaxConnectAttempts = 4;
        unityTransport.ConnectTimeoutMS = 500;
        server.NetworkConfig = new NetworkConfig()
        {
            NetworkTransport = unityTransport
        };
        return server;
    }

    /// <summary>
    /// Used to add a client to the already existing list of clients
    /// </summary>
    /// <param name="clientCount">The amount of clients</param>
    /// <param name="clients"></param>
    public static bool CreateNewClients(int clientCount, out NetworkManager[] clients)
    {
        clients = new NetworkManager[clientCount];
        var activeSceneName = SceneManager.GetActiveScene().name;
        for (int i = 0; i < clientCount; i++)
        {
            var go = new GameObject("NetworkManager - Client - " + i);
            clients[i] = go.AddComponent<NetworkManager>();
            var unityTransport = go.AddComponent<UnityTransport>();
            clients[i].NetworkConfig = new NetworkConfig()
            {
                NetworkTransport = unityTransport
            };
        }
        m_networkManagerInstances.AddRange(clients);
        return true;
    }

    public static void Destroy()
    {
        // Shutdown the server which forces clients to disconnect
        foreach (var networkManager in m_networkManagerInstances)
        {
            networkManager.Shutdown();
            // s_Hooks.Remove(networkManager);
        }

        // Destroy the network manager instances
        foreach (var networkManager in m_networkManagerInstances)
        {
            if (networkManager.gameObject != null)
            {
                Object.Destroy(networkManager.gameObject);
            }
        }

        m_networkManagerInstances.Clear();

        // CleanUpHandlers();

        Application.targetFrameRate = m_oldFrameRate;
    }
}
