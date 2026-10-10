<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AcademicLeavePractical.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Login</h2>

        <table>
            <tr>
                <td>Username:</td>
                <td><asp:TextBox ID="txtUser" runat="server" /></td>
            </tr>
            <tr>
                <td>Password:</td>
                <td><asp:TextBox ID="txtPass" runat="server" TextMode="Password" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:CheckBox ID="chkRemember" runat="server" Text="Remember me" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" /></td>
            </tr>
        </table>

        <br />
        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
    </form>
</body>
</html>
