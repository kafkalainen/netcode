using System.Collections;
using NUnit.Framework;
using Netproject.Managers;
using Unity.Netcode;
using UnityEngine.TestTools;

public class NetTesting
{
    private ServerManager m_server;
    private ClientManager[] m_clients;

    [OneTimeSetUp]
    public void NetManagerSetup()
    {
        NetHelpers.Create(2, out m_server, out m_clients);
        // m_server.StartDedicatedServer();
        m_clients[0].StartDedicatedClient();
        // m_clients[1].StartDedicatedClient();
    }

    [UnityTest]
    public IEnumerator ClientConnectionTestWithEnumeratorPasses()
    {
        // Assert.True(m_server.NetManager.IsServer);
        Assert.True(m_clients[0].NetManager.IsClient);
        // Assert.True(m_clients[1].NetManager.IsClient);
        yield return null;
    }

    [OneTimeTearDown]
    public void NetManagerTearDown()
    {
        TestHelpers.Destroy();
    }
}
