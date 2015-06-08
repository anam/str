<%@ Page Title="Edit Item" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeFile="edititem.aspx.cs" Inherits="Default2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport' />
    <link href="../../css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <link href="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/jquery-ui.min.js"></script>
    
    
    <script type="text/javascript">
        function isNumber(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57)) {
                alert("Allow Only Numbers as Amount");
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="companyname" runat="Server">
    Edit Item
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
                <td><label for="txtCompany">Type</label></td>
                <td>
                    <asp:TextBox ID="txtCompany" class="form-control" runat="server"></asp:TextBox></td>
            </tr>

            <tr>
                <td><label for="txtName">Name</label></td>
                <td><asp:TextBox ID="txtName" class="form-control" runat="server" ></asp:TextBox></td>
            </tr>
            
            <tr>
                <td><label for="txtUnit1">Unit Name</label></td>
                <td>
                    <asp:TextBox id="txtUnit1" class="form-control" runat="server" ></asp:TextBox>
                </td>
                <td><label for="txtUnitPrice1">Unit Price</label></td>
                <td>
                    <asp:TextBox ID="txtUnitPrice1" class="form-control" runat="server" hint="Unit Price" onkeypress="return isNumber(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><label for="txtUnit2">Unit Name</label></td>
                <td>
                    <asp:TextBox ID="txtUnit2" class="form-control" runat="server"></asp:TextBox>
                </td>
                <td><label for="txtUnitPrice2">Unit Price</label></td>
                <td>
                    <asp:TextBox ID="txtUnitPrice2" class="form-control" runat="server" hint="Unit Price" onkeypress="return isNumber(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><label for="txtUnit3">Unit Name</label></td>
                <td>
                    <asp:TextBox ID="txtUnit3" class="form-control" runat="server" ></asp:TextBox>
                </td>
                <td><label for="txtUnitPrice3">Unit Price</label></td>
                <td>
                    <asp:TextBox ID="txtUnitPrice3" class="form-control" runat="server" hint="Unit Price" onkeypress="return isNumber(event)"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td><label for="txtComission">Comission</label></td>
                <td>
                    <div class="input-group">
                        <asp:TextBox runat="server" ID="txtComission" class="form-control" onkeypress="return isNumber(event)"></asp:TextBox>
                        <span class="input-group-addon">%</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2"><asp:Button ID="btnAdd" runat="server" Text="Save" class="btn btn-primary btn-lg" OnClick="btnAdd_Click"></asp:Button></td>
            </tr>
            
        </table>

    </form>
</asp:Content>