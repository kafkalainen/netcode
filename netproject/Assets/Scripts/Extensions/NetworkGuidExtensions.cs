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
            var bytes = parsedGuid.ToByteArray();
            var node = new byte[8];
            node[0] = bytes[8];
            node[1] = bytes[9];
            node[2] = bytes[10];
            node[3] = bytes[11];
            node[4] = bytes[12];
            node[5] = bytes[13];
            node[6] = bytes[14];
            node[7] = bytes[15];
            var networkId = new NetworkGuid
            (
                BitConverter.ToInt32(bytes, 0),
                BitConverter.ToInt16(bytes, 4),
                BitConverter.ToInt16(bytes, 6),
                node
            );
            return networkId;
        }
        public static NetworkGuid ToNetworkGuid(this Guid id)
        {
            var bytes = id.ToByteArray();
            var node = new byte[8];
            node[0] = bytes[8];
            node[1] = bytes[9];
            node[2] = bytes[10];
            node[3] = bytes[11];
            node[4] = bytes[12];
            node[5] = bytes[13];
            node[6] = bytes[14];
            node[7] = bytes[15];
            var networkId = new NetworkGuid
            (
                BitConverter.ToInt32(bytes, 0),
                BitConverter.ToInt16(bytes, 4),
                BitConverter.ToInt16(bytes, 6),
                node
            );
            return networkId;
        }


        public static Guid ToGuid(this NetworkGuid networkId)
            => new Guid
            (
                networkId.TimeLow,
                networkId.TimeMid,
                networkId.TimeHighAndVersion,
                networkId.Node[0],
                networkId.Node[1],
                networkId.Node[2],
                networkId.Node[3],
                networkId.Node[4],
                networkId.Node[5],
                networkId.Node[6],
                networkId.Node[7]
            );
    }
}
