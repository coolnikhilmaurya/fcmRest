<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FCM Send Notification</title>
    <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
</head>
<body>

    <div align="center">
        <br />
        <br />
        <br />
        <br />
        <br />
        <div class="w3-card-4 w3-center" style="width: 450px; word-wrap:break-word">
            <div class="w3-container w3-green">
                <h2 class="w3-center">FCM Send Notification</h2>
            </div>
            <form class="w3-container w3-center" width="600" align="center" runat="server">
                <p>
                    <label class="w3-text-green"><b>Title</b></label>
                    <input type="text" id="tbTitle" placeholder="Title" runat="server" />
                </p>
                <p>
                    <label class="w3-text-green"><b>Body</b></label>
                    <input type="text" id="tbBody" placeholder="Body" runat="server" />
                </p>
                <p>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="w3-btn w3-green" Text="Submit" OnClick="btnSubmit_Click" />
                </p>
                <p>
                    <label id="lblStatus" class="w3-text-green" runat="server"></label>
                </p>
            </form>
        </div>
    </div>
</body>
</html>
