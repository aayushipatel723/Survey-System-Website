<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThankYou.aspx.cs" Inherits="Survey_System.ThankYou" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Thank You!</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            text-align: center;
            padding: 50px;
        }
        .container {
            margin-top: 100px;
        }
        h1 {
            color: #4CAF50;
        }
        .btn {
            padding: 10px 20px;
            font-size: 16px;
            background-color: #4CAF50;
            color: white;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-top: 20px;
        }
        .btn:hover {
            background-color: #45a049;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Thank You for Your Response!</h1>
            <p>Your survey response has been successfully submitted.</p>
            <asp:Button ID="btnRedirectHome" runat="server" Text="Go to Home" CssClass="btn" OnClick="btnRedirectHome_Click" />
        </div>
    </form>
</body>
</html>
