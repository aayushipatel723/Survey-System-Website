<%@ Page Language="C#" MasterPageFile="~/Surveyer.master" AutoEventWireup="true" CodeBehind="SurveyDashboard.aspx.cs" Inherits="Survey_System.SurveyDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 style="text-align: center">Surveyer Dashboard</h2>
    <p>&nbsp;</p>
    <h2>Your Surveys</h2>
    <asp:Button ID="btnCreateNewSurvey" runat="server" Text="Create New Survey" CssClass="btn btn-primary" OnClick="btnCreateNewSurvey_Click" />
    <br /><br />
    <asp:GridView ID="gvSurveys" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" DataKeyNames="Id">
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Survey ID" />
            <asp:BoundField DataField="SurveyName" HeaderText="Survey Name" />
            <asp:BoundField DataField="CategoryName" HeaderText="Category" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-secondary">Edit</asp:LinkButton>
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-danger">Delete</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
