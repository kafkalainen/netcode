using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Netproject.Extensions;

public class ExtensionsTest
{
    [UnityTest]
    public IEnumerator SessionManagerWithReconnect()
    {
        var str = "084f8e63-5acb-4825-b919-22338909a44f";
        var networkGuid = str.ToNetworkGuid();
        var guid = networkGuid.ToGuid();
        var str2 = guid.ToString();
        Assert.True(str == str2);
        yield return null;
    }

}
