using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Unity.Netcode;
using Netproject.Managers;
using System.Reflection;
using Netproject.Types;

public class SessionManagerTests
{
    NetworkManager m_manager;
    SessionManager m_sessionManager;

    [OneTimeSetUp]
    public void SetupServer()
    {
        m_manager = TestHelpers.CreateServer();
        NetworkManager.Singleton.StartServer();
    }

    [SetUp]
    public void SetupSessionManager()
    {
        m_sessionManager = CreateSessionManager();
        typeof(SessionManager).GetField("m_disconnectTime", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(m_sessionManager, 0.1);
    }

    private SessionManager CreateSessionManager()
    {
        var go = new GameObject("SessionManager");
        var sessionManager = go.AddComponent<SessionManager>();
        return sessionManager;
    }

    [UnityTest]
    public IEnumerator SessionManagerWithReconnect()
    {
        Assert.True(m_sessionManager.AddSession("084f8e63-5acb-4825-b919-22338909a44f", 1));
        Assert.True(m_sessionManager.AddSession("62f5170b-adb8-46bb-9d7a-f0f8ef7ca3ac", 2));
        Assert.False(m_sessionManager.AddSession("62f5170b-adb8-46bb-9d7a-f0f8ef7ca3ac", 3));
        yield return null;
    }

    [UnityTest]
    public IEnumerator SessionManagerDisconnect()
    {
        Assert.True(m_sessionManager.AddSession("62f5170b-adb8-46bb-9d7a-f0f8ef7ca3ac", 2));
        Assert.True(m_sessionManager.GetPlayerData("62f5170b-adb8-46bb-9d7a-f0f8ef7ca3ac")?.Guid == "62f5170b-adb8-46bb-9d7a-f0f8ef7ca3ac");
        m_sessionManager.SetTimeoutToClient(2);
        yield return new WaitForSeconds(0.3f);
        Assert.Null(m_sessionManager.GetPlayerData("62f5170b-adb8-46bb-9d7a-f0f8ef7ca3ac"));
        Assert.Null(m_sessionManager.GetPlayerData(2));
    }

    [UnityTest]
    public IEnumerator UpdatePlayerScore()
    {
        Assert.True(m_sessionManager.AddSession("62f5170b-adb8-46bb-9d7a-f0f8ef7ca3ac", 2));
        var playerData = new NetworkPlayerData("62f5170b-adb8-46bb-9d7a-f0f8ef7ca3ac", 1);
        m_sessionManager.UpdateScore(2, playerData);
        Debug.Log(m_sessionManager.GetPlayerData(2)?.Score);
        Assert.True(m_sessionManager.GetPlayerData(2)?.Score == 1);
        yield return null;
    }

    [TearDown]
    public void Destroy()
    {
        Object.Destroy(m_sessionManager.gameObject);
    }

    [OneTimeTearDown]
    public void DestroyNetwork()
    {
        NetworkManager.Singleton.Shutdown();
        Object.Destroy(m_manager.gameObject);
    }
}
