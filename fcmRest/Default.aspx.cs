using System;
using System.Collections.Generic;
using FCMHelper;

public partial class _Default : System.Web.UI.Page
{

    // tset FCM fcm = new FCM("AAAALCoP_fE:APA91bFYLhjeS0qS98-lqnoGV6UY7Fan4YcqWNhbSCGgCjs4FBhOMAw8jcPReg9dXYJ3KjBeosEDQyTvOusz_x0HdWn5lzP_FnEb4g4iKM2mwjuEof7Su8nFwga0GVeriEDnU5sdLQLj", "189684252145");
    // kickkare cred
    FCM fcm = new FCM(
        FCM_SERVER_KEY : "YOUR_FCM_SERVER_KEY",
        FCM_SENDER_ID: "YOUR_FCM_SENDER_ID");
 

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("title", tbTitle.Value.ToString());
        dic.Add("body", tbBody.Value.ToString());

        // device token
        var registrationToken = "eMcRLuPXt4g:APA91bExZTzl19LHKsbBuw41lXY0eUDugWIe-kpK8RPC-Q9QpZB4ex-YDlTLK3vSYNB9hhm0y30G2Ilqoz59jwvjeMrB5NVBxN30j7oHeaRgN1hexNjOHvpRm0s1dxcLUhic_-zu0gZr";
        
        // send to topic
        // string res = fcm.Send_Data_Message_To_Topic(dic,"users");

        // send to specific token
        string res = fcm.Send_Data_Message_To_FcmToken(dic, registrationToken);
        lblStatus.InnerText = res;
        tbTitle.Value = "";
        tbBody.Value = "";
    }
}