using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Netcode;
using Netproject.Managers;
using System.Collections.Generic;
using System.Collections;
public class ClientConnectionTest
{
    private NetworkManager m_server;
    private NetworkManager[] m_clients;

    [OneTimeSetUp]
    public void NetManagerSetup()
    {
        TestHelpers.Create(1, out m_server, out m_clients);
        m_server.StartServer();
        m_clients[0].StartClient();
    }

    [UnityTest]
    public IEnumerator ClientConnectionTestWithEnumeratorPasses()
    {
        Assert.True(m_server.IsServer);
        Assert.True(m_clients[0].IsClient);
        yield return null;
    }

    [OneTimeTearDown]
    public void NetManagerTearDown()
    {
        TestHelpers.Destroy();
    }
}
