<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Survey_System.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="LoginStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h2>Login</h2>
            <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="label"></asp:Label>
            <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
            
            <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="label"></asp:Label>
            <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
            
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn-primary" OnClick="btnLogin_Click" />
            
            <asp:Label ID="lblMessage" runat="server" CssClass="error-message"></asp:Label>
        </div>
    </form>
</body>
</html>
