// using System;
// using System.Collections.Generic;
// using Unity.Netcode;

// private class MultiInstanceHooks : INetworkHooks
// {
//     public Dictionary<Type, List<MessageHandleCheckWithResult>> HandleChecks = new Dictionary<Type, List<MessageHandleCheckWithResult>>();

//     public static bool CheckForMessageOfType<T>(object receivedMessage) where T : INetworkMessage
//     {
//         return receivedMessage.GetType() == typeof(T);
//     }

//     public void OnBeforeSendMessage<T>(ulong clientId, ref T message, NetworkDelivery delivery) where T : INetworkMessage
//     {
//     }

//     public void OnAfterSendMessage<T>(ulong clientId, ref T message, NetworkDelivery delivery, int messageSizeBytes) where T : INetworkMessage
//     {
//     }

//     public void OnBeforeReceiveMessage(ulong senderId, Type messageType, int messageSizeBytes)
//     {
//     }

//     public void OnAfterReceiveMessage(ulong senderId, Type messageType, int messageSizeBytes)
//     {
//     }

//     public void OnBeforeSendBatch(ulong clientId, int messageCount, int batchSizeInBytes, NetworkDelivery delivery)
//     {
//     }

//     public void OnAfterSendBatch(ulong clientId, int messageCount, int batchSizeInBytes, NetworkDelivery delivery)
//     {
//     }

//     public void OnBeforeReceiveBatch(ulong senderId, int messageCount, int batchSizeInBytes)
//     {
//     }

//     public void OnAfterReceiveBatch(ulong senderId, int messageCount, int batchSizeInBytes)
//     {
//     }

//     public bool OnVerifyCanSend(ulong destinationId, Type messageType, NetworkDelivery delivery)
//     {
//         return true;
//     }

//     public bool OnVerifyCanReceive(ulong senderId, Type messageType)
//     {
//         return true;
//     }

//     public void OnBeforeHandleMessage<T>(ref T message, ref NetworkContext context) where T : INetworkMessage
//     {
//     }

//     public void OnAfterHandleMessage<T>(ref T message, ref NetworkContext context) where T : INetworkMessage
//     {
//         if (HandleChecks.ContainsKey(typeof(T)))
//         {
//             foreach (var check in HandleChecks[typeof(T)])
//             {
//                 if (check.Check(message))
//                 {
//                     check.Result = true;
//                     HandleChecks[typeof(T)].Remove(check);
//                     break;
//                 }
//             }
//         }
//     }
// }
