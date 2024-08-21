<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="Survey_System.Signup" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Signup - SurveyHub</title>
    <link href="SignupStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="signup-container">
            <h2>Create Your Account</h2>

            <!-- Email TextBox -->
            <asp:TextBox ID="tbEmail" runat="server" CssClass="form-control" TextMode="Email" placeholder="Enter your email"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="tbEmail" ErrorMessage="Email is required" CssClass="error-message"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="tbEmail" ErrorMessage="Invalid email format" ValidationExpression="^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$" CssClass="error-message"></asp:RegularExpressionValidator>
            <br />

            <!-- Password TextBox -->
            <asp:TextBox ID="tbPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Enter your password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="tbPassword" ErrorMessage="Password is required" CssClass="error-message"></asp:RequiredFieldValidator>
            <br />

            <!-- Confirm Password TextBox -->
            <asp:TextBox ID="tbConfPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Confirm your password"></asp:TextBox>
            <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="tbPassword" ControlToValidate="tbConfPassword" ErrorMessage="Passwords do not match" CssClass="error-message"></asp:CompareValidator>
            <br />

            <!-- User Type RadioButtonList -->
            <asp:RadioButtonList ID="rblUserType" runat="server" CssClass="form-control">
                <asp:ListItem Selected="True" Value="Respondent">Respondent</asp:ListItem>
                <asp:ListItem Value="Surveyer">Surveyer</asp:ListItem>
            </asp:RadioButtonList>
            <br />

            <!-- Profession TextBox -->
            <asp:TextBox ID="tbProfession" runat="server" CssClass="form-control" placeholder="Enter your profession"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvProfession" runat="server" ControlToValidate="tbProfession" ErrorMessage="Profession is required" CssClass="error-message"></asp:RequiredFieldValidator>
            <br />

            <!-- Organization TextBox -->
            <asp:TextBox ID="tbOrganization" runat="server" CssClass="form-control" placeholder="Enter your organization"></asp:TextBox>
            <br />

            <asp:TextBox ID="tbDob" runat="server" CssClass="form-control" placeholder="Enter your Date of Birth in (YYYY-MM-DD) format"></asp:TextBox>
            <br />

            <!-- Gender RadioButtonList -->
            <asp:RadioButtonList ID="rblGender" runat="server" CssClass="form-control">
                <asp:ListItem Selected="True" Value="select">Select</asp:ListItem>
                <asp:ListItem Value="male">Male</asp:ListItem>
                <asp:ListItem Value="female">Female</asp:ListItem>
                <asp:ListItem Value="others">Others</asp:ListItem>
            </asp:RadioButtonList>
            <br />

            <!-- Submit Button -->
            <asp:Button ID="btnSignup" runat="server" Text="Sign Up" CssClass="btn-primary" OnClick="btnSignup_Click" />
        </div>
    </form>
</body>
</html>
