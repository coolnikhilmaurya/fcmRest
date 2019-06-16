<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Send Notification</title>
</head>
<body>
    <form id="form1" runat="server" style="align-content:center" >
        <div align="center">
			<input type="text" id="tbTitle" placeholder="Title" runat="server" />
			<input type="text" id="tbBody" placeholder="Body" runat="server" />
			<asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
        </div>
    </form>
</body>
</html>
