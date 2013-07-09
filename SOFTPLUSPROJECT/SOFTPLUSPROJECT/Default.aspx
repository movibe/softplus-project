<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SOFTPLUSPROJECT._Default" %>

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
            <div id="defaultPage">
                <form id="Form1" runat=server>
                    <div id="masterZone" style="padding-bottom: 30px;">
                        <div id="masterForm">
                            <div class="editor-field">
                                <strong>  <label for="CustomerName">Customer name</label>: </strong>
                                <input runat=server id="CustomerId" name="CustomerId" type="hidden" value="1" />
                                <input id="CustomerName" name="CustomerName" type="text" value="" />
                            </div>
                            <div class="editor-field">
                                <strong><label for="Address">Address</label>: </strong>
                                <input id="Address" name="Address" type="text" value="" />
                            </div>
                        </div>
                    </div>
                    <div id="detailsZone" style="padding-bottom: 30px;">
                        <div id="detailsForm" style="display: inline-block;">
                            <div style="float: left; width: 300px;">
                                <strong><label for="ItemSizeId">Item Size</label>: </strong>
                                   <%--<select id="ItemSizeId" name="ItemSizeId">
                                    <option selected="selected" value="0">- Select -</option>
                                    <option value="1">5</option>
                                    <option value="2">10</option>
                                    <option value="3">20</option>
                                    <option value="4">50</option>
                                    <option value="5">100</option>
                                    <option value="6">500</option>
                                    <option value="7">1000</option>
                                   </select>--%>
                                   <asp:DropDownList ID="ItemSizeId" name="ItemSizeId" runat="server" DataTextField="ItemSize" DataValueField="ItemSizeID" ></asp:DropDownList>
                            </div>
                            <div style="float: left; width: 300px;">
                                <strong><label for="Quantity">Quantity</label>: </strong>
                                <input id="Quantity" name="Quantity" type="text" value="" />
                            </div>
                            <div style="float: left; width: 150px;">
                                <button id="btnDetailsAdd" class="button">
                                    Add
                                </button>
                                &nbsp;&nbsp;&nbsp;
                                <button id="btnDetailsReset" class="button">
                                    Reset
                                </button>
                            </div>
                        </div>
                        <div id="detailsData" style="width: 750px;">
                            <div id="detailsGridZone">
                                <table id="tblDetails">
                                    <tr>
                                        <!-- Item Id -->
                                        <td>
                                            Item Id
                                        </td>
                                        <!-- Item Size -->
                                        <td>
                                            Item Size
                                        </td>
                                        <!-- Quantity -->
                                        <td>
                                            Quantity
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div id="formButton">
                        <button id="btnSave" class="button">
                            Save
                        </button>
                    </div>
                </form>
                <script src="Scripts/App/app-jq.js" type="text/javascript"></script>
            </div>
        </section>
        <footer>
        </footer>
    </div>
</body>
</html>
