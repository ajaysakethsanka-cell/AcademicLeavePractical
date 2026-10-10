<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AcademicLeavePractical.aspx.cs" Inherits="AcademicLeavePractical.AcademicLeavePractical" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Academic Calendar &amp; Leave Management</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Welcome, <asp:Label ID="lblWelcome" runat="server" Font-Bold="true" />
            &nbsp;&nbsp;
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
        </div>

        <h2>Leave Application</h2>

        <table>
            <tr>
                <td>Name:</td>
                <td><asp:TextBox ID="txtName" runat="server" /></td>
            </tr>
            <tr>
                <td valign="top">Leave Date:</td>
                <td>
                    <asp:Calendar ID="cldLeave" runat="server"
                        OnDayRender="cldLeave_DayRender"
                        SelectedDayStyle-BackColor="#2c3e50"
                        SelectedDayStyle-ForeColor="White"
                        TitleStyle-BackColor="#cccccc" />
                    <small style="color:gray">Red background = academic holiday</small>
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
                <td>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear History" OnClick="btnClear_Click" />
                </td>
            </tr>
        </table>

        <br />
        <asp:Label ID="lblResult" runat="server" />
        <br /><br />

        <b>My Leave Requests</b>
        <asp:GridView ID="gvLeaves" runat="server" AutoGenerateColumns="false" CellPadding="4" BorderWidth="1">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:BoundField DataField="Date" HeaderText="Date" />
                <asp:BoundField DataField="Type" HeaderText="Type" />
                <asp:BoundField DataField="Reason" HeaderText="Reason" />
            </Columns>
        </asp:GridView>

        <br />
        <asp:Label ID="lblCount" runat="server" ForeColor="Gray" /><br />
        <asp:Label ID="lblInfo" runat="server" ForeColor="Gray" />
    </form>
</body>
</html>
