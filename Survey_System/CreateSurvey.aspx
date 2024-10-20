<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateSurvey.aspx.cs" Inherits="Survey_System.CreateSurvey" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Survey</title>
    <link href="CreateSurvey.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label for="FormName">Survey Name:</label>
            <asp:TextBox ID="FormName" runat="server" />
            <asp:Button ID="CreateSurveyButton" runat="server" Text="Create Survey" OnClick="CreateSurveyButton_Click" />
        </div>
        <asp:HiddenField ID="FormID" runat="server" />
    </form>
</body>
</html>
