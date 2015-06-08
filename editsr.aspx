<%@ Page Title="Edit Sales Representive" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeFile="editsr.aspx.cs" Inherits="Default2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="companyname" runat="Server">
    Edit Sales Representive
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="maincontent" runat="Server">
    <form id="AddItem" runat="server">

        <div class="errormessage">
            <asp:Label runat="server" ID="lblMessage"  />
        </div>
        <div class="successmessage">
            <asp:Label runat="server" ID="lblSuccess"  />
        </div>
        
        <table class="AddItem">
            <tr>
                <td><label for="txtName">Name</label></td>
                <td><asp:TextBox ID="txtName" class="form-control" runat="server"></asp:TextBox></td>
            </tr>
                
            <tr>
                <td><label for="txtCompany">Salary</label></td>
                <td>
                    <asp:TextBox ID="txtSalary" class="form-control" runat="server"></asp:TextBox></td>
            </tr>

            <tr>
                <td><label for="txtCompany">Email</label></td>
                <td>
                    <asp:TextBox ID="txtEmail" class="form-control" TextMode="Email" runat="server"></asp:TextBox></td>
            </tr>

            <tr>
                <td><label for="txtCompany">Password</label></td>
                <td>
                    <asp:TextBox ID="txtPassword" class="form-control" TextMode="Password" runat="server"></asp:TextBox></td>
            </tr>

            <tr>
                <td><label for="txtPhone">Phone</label></td>
                <td>
                    <asp:TextBox id="txtPhone" class="form-control" TextMode="Phone" runat="server"></asp:TextBox>
                </td>
            </tr>
           
            <tr>
                <td colspan="2"><asp:Button ID="btnAdd" runat="server" Text="Save" class="btn btn-primary btn-lg" OnClick="btnAdd_Click"></asp:Button></td>
            </tr>
            
        </table>

    </form>
</asp:Content>
