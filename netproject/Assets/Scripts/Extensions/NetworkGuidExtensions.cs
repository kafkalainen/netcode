namespace Netproject.Extensions
{
	using System;
	using Netproject.Types;
	using UnityEngine;
    public static class NetworkGuidExtensions
    {

        public static NetworkGuid ToNetworkGuid(this string id)
        {
            var parsedGuid = new Guid(id);
            var networkId = new NetworkGuid
            (
                BitConverter.ToUInt64(parsedGuid.ToByteArray(), 0),
                BitConverter.ToUInt64(parsedGuid.ToByteArray(), 8)
            );
            return networkId;
        }
        public static NetworkGuid ToNetworkGuid(this Guid id)
        {
            var networkId = new NetworkGuid
            (
                BitConverter.ToUInt64(id.ToByteArray(), 0),
                BitConverter.ToUInt64(id.ToByteArray(), 8)
            );
            return networkId;
        }

        public static string ToString(this NetworkGuid networkId)
        {
            var bytes = new char[16];
            Buffer.BlockCopy(BitConverter.GetBytes(networkId.FirstHalf), 0, bytes, 0, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(networkId.SecondHalf), 0, bytes, 8, 8);
            return new string(bytes);
        }

        public static Guid ToGuid(this NetworkGuid networkId)
        {
            var bytes = new byte[16];
            Buffer.BlockCopy(BitConverter.GetBytes(networkId.FirstHalf), 0, bytes, 0, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(networkId.SecondHalf), 0, bytes, 8, 8);
            return new Guid(bytes);
        }
    }
}
