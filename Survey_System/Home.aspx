<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Survey_System.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="HomeStyle.css" type="text/css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <header class="site-header">
        <p>SurveyHub</p>
        <nav class="site-navigation">
            <ul>
                <li><a href="Login.aspx">Login</a></li>
                <li><a href="Signup.aspx">SignUp</a></li>
            </ul>
        </nav>
    </header>
        <main class="site-main">
        <section class="main-header">
            <article>
                <h1>Why SurveyHub Matters?</h1>
                <p>Surveys are more than just questions—they’re the key to unlocking insights that can drive your product’s success. Whether you're launching a new feature or improving an existing one, our survey system allows you to gather critical feedback directly from your audience. This is your opportunity to listen, adapt, and innovate based on what really matters—your users' needs.</p>
                <img src="Images/SurveyImage.jpg" alt="SurveyHub Image" />
            </article>
        </section>
        </main>
</asp:Content>