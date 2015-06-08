<%@ Page Title="Items" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeFile="items.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <link href="../../css/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ionicons.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="companyname" runat="Server">
    Items
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="maincontent" runat="Server">
    <div class="ItemsButton">
        <div class="ActionButtons">
            <a class="btn btn-app" href ="additem.aspx">
                <i class="fa fa-plus"></i>Add Item
            </a>
        </div>
    </div>
    
    <div style ="clear:both;" />
    <asp:Label ID ="lblMessage" runat="server" />

    <div class="box-body table-responsive">
        <h3>All Items</h3>
        <table id="example1" class="table table-bordered table-striped itemsdata">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Type</th>
                    <th>Comission</th>
                    <th>Unit Name</th>
                    <th>Price</th>
                    <th>Current Price</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                
                    <asp:PlaceHolder runat="server" id="ItemsData"></asp:PlaceHolder>
 
            </tbody>
            <tfoot>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Type</th>
                    <th>Comission</th>
                    <th>Unit Name</th>
                    <th>Unit Price</th>
                    <th>Current Price</th>
                    <th>Action</th>
                </tr>
            </tfoot>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="Server">
    <script src="../../js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="../../js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $("#example1").dataTable();
            $('#example2').dataTable({
                "bPaginate": true,
                "bLengthChange": false,
                "bFilter": false,
                "bSort": true,
                "bInfo": true,
                "bAutoWidth": false
            });
        });
    </script>

</asp:Content>

