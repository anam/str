<%@ Page Title="Sales" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeFile="sales.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <link href="../../css/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ionicons.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="companyname" runat="Server">
    Sales
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="maincontent" runat="Server">
    
    <div class="box-body table-responsive">
        
        <table id="example1" class="table table-bordered table-striped itemsdata">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Type</th>
                    <th>Quantity</th>
                    <th>Gift</th>
                    <th>Price</th>
                    <th>Pay. Method</th>
                    <th>Bank</th>
                    <th>Dealer</th>
                    <th>Area</th>
                    <th>Officer</th>
                    <th>Date</th>
                </tr>
            </thead>
            <tbody>

                <asp:PlaceHolder runat="server" ID="ItemsData"></asp:PlaceHolder>

            </tbody>
            <tfoot>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Type</th>
                    <th>Quantity</th>
                    <th>Gift</th>
                    <th>Price</th>
                    <th>Pay. Method</th>
                    <th>Bank</th>
                    <th>Dealer</th>
                    <th>Area</th>
                    <th>Officer</th>
                    <th>Date</th>
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
