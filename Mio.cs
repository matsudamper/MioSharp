using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using static MioSharp.SwitchStatus;

namespace MioSharp
{
    public class Mio
    {
        private const String authUrl = "https://api.iijmio.jp/mobile/d/v1/authorization/";
        private const String couponUrl = "https://api.iijmio.jp/mobile/d/v1/coupon/";
        private const String switchUrl = "https://api.iijmio.jp/mobile/d/v1/coupon/";
        private const String packetUrl = "https://api.iijmio.jp/mobile/d/v1/log/packet/";

        private String devId;
        private String token;
        private String redirectUri;

        public Mio()
        {

        }

        public Mio setDevId(String devId)
        {
            this.devId = devId;

            return this;
        }

        public Mio setRedirectUri(String redirectUri)
        {
            this.redirectUri = redirectUri;

            return this;
        }

        public Mio setToken(String token)
        {
            this.token = token;

            return this;
        }

        public String getAuthorizationUrl(String state)
        {
            String url = authUrl + "?response_type=token";
            url += "&client_id=" + devId;
            url += "&state=" + state;
            url += "&redirect_uri=" + Uri.EscapeDataString(redirectUri);

            return url;
        }

        public async Task<CouponStatus> getCoupon()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(couponUrl);
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Headers["X-IIJmio-Developer"] = devId;
            req.Headers["X-IIJmio-Authorization"] = token;

            var responce = (HttpWebResponse)await req.GetResponseAsync();

            Stream stream = responce.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string json = reader.ReadToEnd();

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(CouponStatus));
                CouponStatus data = (CouponStatus)serializer.ReadObject(ms);

                return data;
            }
        }

        public async Task<PacketStatus> getPacket()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(packetUrl);
            req.Method = "GET";
            req.ContentType = "application/x-www-form-urlencoded";
            req.Headers["X-IIJmio-Developer"] = devId;
            req.Headers["X-IIJmio-Authorization"] = token;

            var responce = (HttpWebResponse)await req.GetResponseAsync();

            Stream stream = responce.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            string json = reader.ReadToEnd();

            var serializer = new DataContractJsonSerializer(typeof(PacketStatus));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                PacketStatus data = (PacketStatus)serializer.ReadObject(ms);

                return data;
            }
        }

        public async Task setCoupon(Dictionary<string, bool> dic)
        {
            var status = new SwitchStatus();
            status.switchInfos = new List<SwitchCouponInfo>();

            var info = new SwitchCouponInfo();
            info.switchHdoInfo = new List<SwitchHdoInfo>();

            foreach (var item in dic)
            {
                var hdo = new SwitchHdoInfo();
                hdo.hdoServiceCode = item.Key;
                hdo.couponUse = item.Value;

                info.switchHdoInfo.Add(hdo);
            }

            status.switchInfos.Add(info);

            await setCoupon(status);
        }

        private async Task setCoupon(SwitchStatus info)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(switchUrl);
            req.Method = "PUT";
            req.Headers["Content-Type"] = "application/json";
            req.Headers["X-IIJmio-Developer"] = devId;
            req.Headers["X-IIJmio-Authorization"] = token;

            var serializer = new DataContractJsonSerializer(typeof(SwitchStatus));

            MemoryStream ms = new MemoryStream();

            serializer.WriteObject(ms, info);

            string json = Encoding.UTF8.GetString(ms.ToArray());
            byte[] jsonByte = Encoding.UTF8.GetBytes(json);

            // 送信
            var stream = await req.GetRequestStreamAsync();

            foreach (var item in jsonByte)
                stream.WriteByte(item);

            // 受信
            var responce = (HttpWebResponse)await req.GetResponseAsync();
        }
    }
}
