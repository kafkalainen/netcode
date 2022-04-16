using System.Collections;
using NUnit.Framework;
using Project.Managers;
using Unity.Netcode;
using UnityEngine.TestTools;

public class NetTesting
{
    private ServerManager m_server;
    private ClientManager[] m_clients;

    [OneTimeSetUp]
    public void NetManagerSetup()
    {
        NetHelpers.Create(1, out m_server, out m_clients);
        m_server.StartDedicatedServer();
        m_clients[0].StartDedicatedClient();
    }

    [UnityTest]
    public IEnumerator ClientConnectionTestWithEnumeratorPasses()
    {
        Assert.True(m_server.NetManager.IsServer);
        Assert.True(m_clients[0].NetManager.IsClient);
        yield return null;
    }

    [OneTimeTearDown]
    public void NetManagerTearDown()
    {
        TestHelpers.Destroy();
    }
}
