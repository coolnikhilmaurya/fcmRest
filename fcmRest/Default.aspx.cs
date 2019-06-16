using System;
using System.Collections.Generic;
using FCMHelper;

public partial class _Default : System.Web.UI.Page
{

    // tset FCM fcm = new FCM("AAAALCoP_fE:APA91bFYLhjeS0qS98-lqnoGV6UY7Fan4YcqWNhbSCGgCjs4FBhOMAw8jcPReg9dXYJ3KjBeosEDQyTvOusz_x0HdWn5lzP_FnEb4g4iKM2mwjuEof7Su8nFwga0GVeriEDnU5sdLQLj", "189684252145");
    // kickkare cred
    FCM fcm = new FCM(
        FCM_SERVER_KEY : "AIzaSyArI2OIGyEX24Sx60t7JlzBugfG5kr4DoE",
        FCM_SENDER_ID: "160807372587");
 

    protected void Page_Load(object sender, EventArgs e)
    {
        //Dictionary<string, string> dic = new Dictionary<string, string>();
        //dic.Add(FCM.title, "val1");
        //dic.Add("key2", "val2");



        //var load = new {
        //    to = "/topic/mytopic",
        //    registration_tokens = dic,
        //};

        //Response.Write(JsonConvert.SerializeObject(load));

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("title", tbTitle.Value.ToString());
        dic.Add("body", tbBody.Value.ToString());

        // 6pro device token
        var registrationToken = "eMcRLuPXt4g:APA91bExZTzl19LHKsbBuw41lXY0eUDugWIe-kpK8RPC-Q9QpZB4ex-YDlTLK3vSYNB9hhm0y30G2Ilqoz59jwvjeMrB5NVBxN30j7oHeaRgN1hexNjOHvpRm0s1dxcLUhic_-zu0gZr";
        // string res = fcm.Send_Data_Message_To_Topic(dic,"users");
        string res = fcm.Send_Data_Message_To_FcmToken(dic, registrationToken);
        Response.Write(res);
    }
}