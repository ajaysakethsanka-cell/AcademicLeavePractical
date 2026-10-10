<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveApplication.aspx.cs" Inherits="AcademicLeavePractical.LeaveApplication" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Application</title>
</head>
<body>
    <form id="form1" runat="server">
        <h2>Leave Application</h2>

        <table>
            <tr>
                <td>Name:</td>
                <td><asp:TextBox ID="txtName" runat="server" /></td>
            </tr>
            <tr>
                <td valign="top">Leave Date:</td>
                <td>
                    <!-- RICH CONTROL -->
                    <asp:Calendar ID="cldLeave" runat="server"
                        OnDayRender="cldLeave_DayRender"
                        SelectedDayStyle-BackColor="#2c3e50"
                        SelectedDayStyle-ForeColor="White"
                        TitleStyle-BackColor="#cccccc" />
                </td>
            </tr>
            <tr>
                <td>Leave Type:</td>
                <td>
                    <asp:DropDownList ID="ddlType" runat="server">
                        <asp:ListItem>Casual Leave</asp:ListItem>
                        <asp:ListItem>Sick Leave</asp:ListItem>
                        <asp:ListItem>Earned Leave</asp:ListItem>
                        <asp:ListItem>Academic Leave</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td valign="top">Reason:</td>
                <td><asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" Rows="3" Columns="30" /></td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" /></td>
            </tr>
        </table>

        <br />
        <asp:Label ID="lblResult" runat="server" />
        <br /><br />
        <asp:Label ID="lblInfo" runat="server" ForeColor="Gray" />
    </form>
</body>
</html>
