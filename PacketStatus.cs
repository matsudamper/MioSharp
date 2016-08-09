using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MioSharp
{
    [DataContract]
    public class PacketStatus
    {
        [DataMember(Name = "packetLogInfo")]
        public List<PacketLogInfo> packetLogsInfo { get; set; }

        [DataMember]
        public string returnCode { get; set; }

        [DataContract]
        public class PacketLogInfo
        {
            [DataMember]
            public string hddServiceCode { get; set; }

            [DataMember]
            public List<HdoPacketInfo> hdoInfo { get; set; }

            [DataMember]
            public string plan { get; set; }
        }

        [DataContract(Name = "HdoInfo")]
        public class HdoPacketInfo
        {
            [DataMember]
            public string hdoServiceCode { get; set; }

            [DataMember]
            public List<PacketLog> packetLog { get; set; }
        }

        [DataContract]
        public class PacketLog
        {
            [DataMember]
            public string date { get; set; }

            [DataMember]
            public int withCoupon { get; set; }

            [DataMember]
            public int withoutCoupon { get; set; }
        }
    }
}