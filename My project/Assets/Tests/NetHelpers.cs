using System.Collections;
using System.Collections.Generic;
using Project.Managers;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class NetHelpers
{
    private static int m_oldFrameRate;
    private static List<NetworkManager> m_networkManagerInstances = new List<NetworkManager>();
    public static bool Create(int clientCount, out ServerManager server, out ClientManager[] clients, int targetFrameRate = 60)
    {
        var serverObj = Object.Instantiate(Resources.Load("Net") as GameObject);
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
            var go = Object.Instantiate(Resources.Load("Net") as GameObject);
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
                Object.Destroy(networkManager.gameObject);
            }
        }
        m_networkManagerInstances.Clear();
        // CleanUpHandlers();
        Application.targetFrameRate = m_oldFrameRate;
    }
}
