<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewResponses.aspx.cs" Inherits="Survey_System.ViewResponses" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Form Responses</title>
    <link href="ViewResponses.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Responses for Survey: </h2>
            <asp:GridView ID="ResponsesGridView" runat="server" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="QuestionText" HeaderText="Question" />
                    <asp:BoundField DataField="ResponseText" HeaderText="Response Text" />
                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
