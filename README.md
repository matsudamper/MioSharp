# OneMioSharp
GitとVisualStudioとUWPの勉強用  

##使い方
###設定
```cs
new Mio()
        .setDevId("")
        .setRedirectUri("")
        .setToken("");
```

###認証URL取得
```cs
string url = MainPage.mio.getAuthorizationUrl("state");
```

###情報取得
```cs
try
{
        CouponStatus couponStatus = await MainPage.mio.getCoupon();
        PacketStatus packetStatus = await MainPage.mio.getPacket();
}
catch (WebException e)
{

}
```
