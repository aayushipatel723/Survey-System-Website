<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewSurveys.aspx.cs" Inherits="Survey_System.ViewSurveys" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Your Created Surveys</title>
    <link href="ViewSurveys.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="home-button">
            <asp:Button ID="HomeButton" runat="server" Text="Home" OnClick="HomeButton_Click" />
        </div>
        <h1>Your Created Surveys</h1>
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="GridView1_RowCommand">
                <Columns>
                    <asp:BoundField DataField="FormName" HeaderText="Survey Name" />
                    <asp:BoundField DataField="CreatedDate" HeaderText="Created On" />
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="ViewResponsesButton" runat="server" Text="View Responses" CommandName="ViewResponses" CommandArgument='<%# Eval("FormID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="DeleteFormButton" runat="server" Text="Delete Form" CommandName="DeleteForm" CommandArgument='<%# Eval("FormID") %>' OnClientClick="return confirm('Are you sure you want to delete this form?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
            <asp:Label ID="NoFormsLabel" runat="server" Text="You have not created any forms." Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>
