# OneMioSharp
GitとVisualStudioとUWPの勉強用  

##使い方
###設定
```cs
var mio = new Mio()
        .setDevId("")
        .setRedirectUri("")
        .setToken("");
```

###認証URL取得
```cs
string url = mio.getAuthorizationUrl("state");
```

###情報取得
```cs
try
{
        CouponStatus couponStatus = await mio.getCoupon();
        PacketStatus packetStatus = await mio.getPacket();
}
catch (WebException e)
{

}
```
