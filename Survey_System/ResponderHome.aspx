<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResponderHome.aspx.cs" Inherits="Survey_System.ResponderHome" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Responder Home</title>
    <link href="ResponderHome.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Available Surveys</h2>
            <div class="grid-container">
                <asp:GridView ID="SurveysGridView" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="FormID" HeaderText="Form ID" Visible="False" />
                        <asp:BoundField DataField="FormName" HeaderText="Survey Title" />
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="RespondButton" runat="server" Text="Respond" CommandArgument='<%# Eval("FormID") %>' OnClick="RespondButton_Click" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
