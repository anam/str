<%@ Page Title="Settings" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeFile="settings.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport' />
    <link href="../../css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <link href="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/jquery-ui.min.js"></script>
    
    
    <script type="text/javascript">

        $(document).ready(function () {
            $("#txtAutoComplete").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "Default.aspx/GetCategory",
                        data: "{'term':'" + $("#txtAutoComplete").val() + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        }
                    });
                }
            });
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="companyname" Runat="Server">
    Settings
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="maincontent" Runat="Server">
    <form id="AddItem" runat="server">

        <div class="errormessage">
            <asp:Label runat="server" ID="lblMessage"  />
        </div>
        <div class="successmessage">
            <asp:Label runat="server" ID="lblSuccess"  />
        </div>
        
        <table class="AddItem">
            <tr id="company" runat="server">
                <td><label for="txtCompanyName">Company Name</label></td>
                <td><asp:TextBox ID="txtCompanyName" class="form-control" runat="server"></asp:TextBox></td>
            </tr>
            <tr id="password" runat ="server">
                <td><label for="txtPassword">Password</label></td>
                <td><asp:TextBox ID="txtPassword" TextMode="Password" class="form-control" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2"><asp:Button ID="btnAdd" runat="server" Text="Save" class="btn btn-primary btn-lg" OnClick="btnAdd_Click"></asp:Button></td>
            </tr>
        </table>
    </form>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

