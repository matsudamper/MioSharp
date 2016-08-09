using System.Collections.Generic;
using System.Runtime.Serialization;

namespace MioSharp
{
    [DataContract]
    public class CouponStatus
    {
        [DataMember(Name = "couponInfo")]
        public List<CouponInfo> couponInfoList { get; set; }

        [DataMember]
        public string returnCode { get; set; }

        [DataContract]
        public class CouponInfo
        {
            [DataMember]
            public string hddServiceCode { get; set; }

            [DataMember]
            public List<Coupon> coupon { get; set; }

            [DataMember]
            public List<HdoInfo> hdoInfo { get; set; }

            [DataMember]
            public string plan { get; set; }
        }

        [DataContract]
        public class HdoInfo
        {
            public bool regulation { get; set; }

            [DataMember]
            public bool couponUse { get; set; }

            [DataMember]
            public string iccid { get; set; }

            [DataMember]
            public List<Coupon> coupon { get; set; }

            [DataMember]
            public string hdoServiceCode { get; set; }

            [DataMember]
            public bool voice { get; set; }

            [DataMember]
            public bool sms { get; set; }

            [DataMember]
            public string number { get; set; }
        }

        [DataContract]
        public class Coupon
        {
            [DataMember]
            public int volume { get; set; }

            [DataMember]
            public string expire { get; set; }

            [DataMember]
            public string type { get; set; }
        }
    }
}