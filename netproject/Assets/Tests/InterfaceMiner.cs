// using System;
// using System.Reflection;
// using UnityEngine;

// public static class InterfaceMiner : MonoBehaviour
// {
//     public static bool GetType(string assembly, out Type type)
//     {
//         Assembly ass = Assembly.LoadFrom(assembly);
//         object mc = ass.CreateInstance("")
//         return true;
//     }
//             Assembly assembly = Assembly.LoadFrom("mytest.dll");
//             object mc = assembly.CreateInstance("MyTest.MyClass");
//             Type t = mc.GetType();
//             BindingFlags bf = BindingFlags.Instance |  BindingFlags.NonPublic;
//             MethodInfo mi = t.GetMethod("MyMethod", bf);
//             mi.Invoke(mc, null);
//             // Console.ReadKey();
// }
