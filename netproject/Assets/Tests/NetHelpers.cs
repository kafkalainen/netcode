using Netproject.Managers;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class NetHelpers
{
    private static bool m_hasStarted;
    private static int m_oldFrameRate;
    private static int m_clientCount;
    private static List<NetworkManager> m_networkManagerInstances = new List<NetworkManager>();
    public static bool Create(int clientCount, out ServerManager server, out ClientManager[] clients, int targetFrameRate = 60)
    {
        var serverObj = UnityEngine.Object.Instantiate(Resources.Load("Net") as GameObject);
        var networkManager = serverObj.GetComponent<NetworkManager>();
        server = serverObj.GetComponent<ServerManager>();
        m_networkManagerInstances.Insert(0, networkManager);
        CreateMultipleNets(clientCount, out clients);
        m_oldFrameRate = Application.targetFrameRate;
        Application.targetFrameRate = targetFrameRate;
        return true;
    }

    public static bool CreateMultipleNets(int netCount, out ClientManager[] nets)
    {
        nets = new ClientManager[netCount];
        var managers = new NetworkManager[netCount];
        var activeSceneName = SceneManager.GetActiveScene().name;
        for (int i = 0; i < netCount; i++)
        {
            var go = UnityEngine.Object.Instantiate(Resources.Load("Net") as GameObject);
            go.name = $"NetworkManager - Client - {i}";
            nets[i] = go.GetComponent<ClientManager>();
            managers[i] = go.GetComponent<NetworkManager>();
        }
        m_networkManagerInstances.AddRange(managers);
        return true;
    }

    public static void Destroy()
    {
        foreach (var networkManager in m_networkManagerInstances)
        {
            networkManager.Shutdown();
            // s_Hooks.Remove(networkManager);
        }
        foreach (var networkManager in m_networkManagerInstances)
        {
            if (networkManager.gameObject != null)
            {
                UnityEngine.Object.Destroy(networkManager.gameObject);
            }
        }
        m_networkManagerInstances.Clear();
        // CleanUpHandlers();
        Application.targetFrameRate = m_oldFrameRate;
    }

    /// <summary>
        /// Starts NetworkManager instances created by the Create method.
        /// </summary>
        /// <param name="host">Whether or not to create a Host instead of Server</param>
        /// <param name="server">The Server NetworkManager</param>
        /// <param name="clients">The Clients NetworkManager</param>
        /// <param name="callback">called immediately after server is started and before client(s) are started</param>
        /// <returns></returns>
        // public static bool Start(bool host, ServerManager server, ClientManager[] clients)//, BeforeClientStartCallback callback = null)
        // {
        //     if (m_hasStarted)
        //     {
        //         throw new InvalidOperationException($"{nameof(NetHelpers)} already thinks it is started. Did you forget to Destroy?");
        //     }

        //     m_hasStarted = true;
        //     m_clientCount = clients.Length;
        //     server.StartDedicatedServer();
        //     var hooks = new MultiInstanceHooks();
        //     server.NetManager.MessagingSystem.Hook(hooks);
        //     s_Hooks[server] = hooks;

        //     // if set, then invoke this for the server
        //     RegisterHandlers(server);

        //     callback?.Invoke();

        //     for (int i = 0; i < clients.Length; i++)
        //     {
        //         clients[i].StartClient();
        //         hooks = new MultiInstanceHooks();
        //         clients[i].MessagingSystem.Hook(hooks);
        //         s_Hooks[clients[i]] = hooks;

        //         // if set, then invoke this for the client
        //         RegisterHandlers(clients[i]);
        //     }

        //     return true;
        // }
}
