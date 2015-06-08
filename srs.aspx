<%@ Page Title="Sales Representives" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeFile="srs.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">

    <link href="../../css/datatables/dataTables.bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../css/ionicons.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="companyname" runat="Server">
    Sales Representive
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="maincontent" runat="Server">
    <div class="ActionButtons">
        <a class="btn btn-app" href="addsr.aspx">
            <i class="fa fa-plus"></i> Add New
        </a>
        <a class="btn btn-app" href="bonus.aspx">
            <i class="fa fa-gear "></i> Settings
        </a>
    </div>
    <asp:Label ID ="lblMessage" runat="server" />
    <div class="box-body table-responsive">
        <table id="example1" class="table table-bordered table-striped">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Total Sales</th>
                    <th>Target</th>
                    <th>Salary</th>
                    <th>Bonus</th>
                    <th>Grand Salary</th>
                    <th>Action</th>
                </tr>
            </thead>
            <tbody>
                <asp:PlaceHolder runat="server" id="SRsData"></asp:PlaceHolder>
            </tbody>
            <tfoot>
                <tr>
                    <th>ID</th>
                    <th>Name</th>
                    <th>Total Sales</th>
                    <th>Target</th>
                    <th>Salary</th>
                    <th>Bonus</th>
                    <th>Grand Salary</th>
                    <th>Action</th>
                </tr>
            </tfoot>
        </table>
    </div>
    <!-- /.box-body -->
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" runat="Server">
    <script src="js/plugins/datatables/jquery.dataTables.js" type="text/javascript"></script>
    <script src="js/plugins/datatables/dataTables.bootstrap.js" type="text/javascript"></script>

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
