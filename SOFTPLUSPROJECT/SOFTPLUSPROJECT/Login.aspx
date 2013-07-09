<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SOFTPLUSPROJECT.Login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <title>Soft Plus IT</title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <link href="Styles/App/app-style.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-1.8.3.min.js" type="text/javascript"></script>

    <script src="Scripts/jquery-ui-1.10.3.min.js" type="text/javascript"></script>

    <script src="Scripts/JQDialog/jqdialog.custom.js" type="text/javascript"></script>

    <script src="Scripts/JQDialog/jqdialog.js" type="text/javascript"></script>

    <script src="Scripts/JQDialog/jqdialog.min.js" type="text/javascript"></script>

    <script src="Scripts/modernizr-2.6.2.js" type="text/javascript"></script>

</head>
<body>
    <div class="page">
        <header>
            <div id="title">
                <h1>Soft Plus IT Test Application</h1>
            </div>
            <div id="sociallinks">
                <span><a href="https://www.facebook.com/rasel.ahmmed.bappi">
    <img src="Styles/Images/App/facebook.png" alt="Facebook" /></a></span>
<span><a href="https://twitter.com/raselbappi">
    <img src="Styles/Images/App/twitter.png" alt="Twitter" /></a></span>
<span><a href="http://bd.linkedin.com/in/raselahmmedbappi">
    <img src="Styles/Images/App/linkedin.png" alt="Linkedin" /></a></span>

            </div>
            <nav>
                <ul id="menu">
                    
                </ul>
            </nav>
        </header>
        <section id="main">
            <div id="commonMessage" class="common-message"></div>
            <div id="loginPage">
            <form id="Form1" runat=server>
            
                <fieldset>
            <legend>Login Information</legend>

            <div class="editor-label">
                <label for="UserName">User name</label>
            </div>
            <div class="editor-field">
                <%--<input  id="UserName" name="UserName" type="text" value="" />--%>
                 <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
            </div>

            <div class="editor-label">
                <label for="Password">Password</label>
            </div>
            <div class="editor-field">
                <%--<input  id="Password" name="Password" type="password" />--%>
                <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
            </div>

            <div class="editor-label">
                <%--<input  id="RememberMe" name="RememberMe" type="checkbox" value="true" />--%>
                <%--<input name="RememberMe" type="hidden" value="false" />--%>
                <asp:CheckBox ID="RememberMe" runat="server"/>
                <label for="RememberMe">Remember me?</label>
            </div>

            <p>
                <%--<input type="submit" value="Log On" />--%>
                <asp:Button ID="LoginButton" runat="server" Text="Log On" 
                    onclick="LoginButton_Click" />
            </p>
            
        </fieldset>
            
            </form>
            
            </div>
        </section>
        <footer>
        </footer>
    </div>
</body>
</html>
