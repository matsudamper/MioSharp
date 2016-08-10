using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MioSharp
{
    [DataContract]
    public class SwitchStatus
    {
        [DataMember(Name = "couponInfo")]
        public List<SwitchCouponInfo> switchInfos { get; set; }

        [DataContract]
        public class SwitchCouponInfo
        {
            [DataMember(Name = "hdoInfo")]
            public List<SwitchHdoInfo> switchHdoInfo { get; set; }
        }

        [DataContract]
        public class SwitchHdoInfo
        {
            [DataMember]
            public string hdoServiceCode { get; set; }

            [DataMember]
            public bool couponUse { get; set; }
        }
    }
}