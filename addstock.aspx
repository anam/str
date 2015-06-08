<%@ Page Title="Add Stock" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeFile="addstock.aspx.cs" Inherits="Default2" %>


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
    Add Items
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
                    <asp:DropDownList ID="drpCompany" CssClass="form-control" runat="server" OnSelectedIndexChanged="drpCompany_SelectedIndexChanged"  AutoPostBack="True" />
                </td>
            </tr>

            <tr>
                <td><label for="txtName">Name</label></td>
                <td>
                    <asp:DropDownList runat="server" ID="drpNames" class="form-control" />
                </td>
            </tr>
            
            <tr runat="server" id="trunit1">
                <td><asp:Label runat="server" ID="lblUnit1" /></td>
                <td>
                    <asp:TextBox id="txtUnit1" class="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                </td>
            </tr>
            <tr runat="server" id="trunit2">
                <td><asp:Label runat="server" ID="lblUnit2" /></td>
                <td>
                    <asp:TextBox ID="txtUnit2" class="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                </td>
            </tr>
            <tr runat="server" id="trunit3">
                <td><asp:Label runat="server" ID="lblUnit3" /></td>
                <td>
                    <asp:TextBox ID="txtUnit3" class="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                </td>
            </tr>
            
            <tr>
                <td><asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-lg"></asp:Button></td>
                <td colspan="2"><asp:Button ID="btnAdd" runat="server" Text="Add Item" class="btn btn-primary btn-lg" OnClick="btnAdd_Click"></asp:Button></td>
            </tr>
            
        </table>

    </form>
</asp:Content>
