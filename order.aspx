<%@ Page Title="Add Item" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeFile="order.aspx.cs" Inherits="Default2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="header" runat="Server">
    <meta content='width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no' name='viewport' />
    <link href="../../css/ionicons.min.css" rel="stylesheet" type="text/css" />
    <link href="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/themes/smoothness/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script type="text/javascript" src="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.1/jquery-ui.min.js"></script>
    
    
    <script type="text/javascript">

         function isNumber(evt) 
         {
             if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57))
             {
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
                <td><label for="txtName">Item Code</label></td>
                <td>
                    <asp:Label runat="server" ID="lblItemCode" />
                </td>
            </tr>
            <tr>
                <td><label for="txtName">Name</label></td>
                <td>
                    <asp:Label runat="server" ID="lblName" />
                </td>
                <td><label for="txtCompany">Type</label></td>
                <td>
                    <asp:Label runat="server" ID="lblCompany" />
                </td>
            </tr>
            <tr>
                <td colspan="4"><hr /></td>
            </tr>

            <tr>
                <td><strong>Availability</strong></td>
            </tr>
            <tr id="Available1" runat="server">
                <td>
                    <asp:Label runat="server" ID="lblAUnitName1" />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblAUnitValue1" />
                </td>
            </tr>
            <tr id="Available2" runat="server">
                <td>
                    <asp:Label runat="server" ID="lblAUnitName2" />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblAUnitValue2" />
                </td>
            </tr>

            <tr id="Available3" runat="server">
                <td>
                    <asp:Label runat="server" ID="lblAUnitName3" />
                </td>
                <td>
                    <asp:Label runat="server" ID="lblAUnitValue3" />
                </td>
            </tr>
                
            <tr>
                <td colspan="4">
                    <hr />

                </td>
            </tr>
            <tr runat="server" id="unit1">
                <td>
                    <asp:Label runat="server" ID="lblUnit1" />
                </td>
                <td>
                    <asp:TextBox id="txtUnit1" class="form-control" runat="server" onkeypress="return isNumber(event)" ></asp:TextBox>
                    
                </td>
                <td>
                    <asp:Label runat="server" ID="Label2" Text=" X "/>
                    <asp:Label runat="server" ID="lblPrice1" Text=""/>
                </td>
                <td>
                    <asp:Label runat="server" ID="lblequal1" Text=" = "/>
                    <asp:Label runat="server" ID="lblTotalPrice1" Text=""/>
                </td>
            </tr>

            <tr runat="server" id="unit2">
                <td>
                    <asp:Label runat="server" ID="lblUnit2" />
                </td>
                <td>
                    <asp:TextBox id="txtUnit2" class="form-control" runat="server" onkeypress="return isNumber(event)" ></asp:TextBox>
                    
                </td>
                <td>
                    <asp:Label runat="server" ID="Label1" Text="X"/> 
                    <asp:Label runat="server" ID="lblPrice2" Text=""/> 
                </td>
                <td>
                    <asp:Label runat="server" ID="lblequal2" Text=" = "/>
                    <asp:Label runat="server" ID="lblTotalPrice2" Text=""/>
                </td>
            </tr>
            <tr runat="server" id="unit3">
                <td>
                    <asp:Label runat="server" ID="lblUnit3" />
                </td>
                <td>
                    <asp:TextBox id="txtUnit3" class="form-control" runat="server" onkeypress="return isNumber(event)" ></asp:TextBox>
                    
                </td>
                <td>
                    <asp:Label runat="server" ID="Label3" Text=" X "/>
                    <asp:Label runat="server" ID="lblPrice3" Text=""/>
                </td>
                <td>
                    <asp:Label runat="server" ID="lblequal3" Text="= "/>
                    <asp:Label runat="server" ID="lblTotalPrice3" Text=""/>
                </td>

                <td>
                    
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label runat="server" ID="Label4" Text="Total"/>
                </td>
                <td>
                    <asp:Label runat="server" ID="lblTotal" Font-Bold="true" ForeColor="Blue" Text=""/>
                </td>
            </tr>

            <tr>
                <td><label for="txtComission">Gift</label></td>
                <td>
                    <asp:TextBox id="txtGift" class="form-control" runat="server" onkeypress="return isNumber(event)" ></asp:TextBox>
                </td>
                <td>
                    <asp:DropDownList runat="server" ID="drpGfitValue" class="form-control" />
                </td>
            </tr>

            <tr>
                <td colspan="4"><hr /></td>
            </tr>
            <tr>
                <td><label for="txtComission">Bank Name</label></td>
                <td>
                    <asp:TextBox id="txtBank" class="form-control" runat="server"></asp:TextBox>
                </td>
                <td><label for="txtComission">Payment Method</label></td>
                <td>
                    <asp:TextBox id="txtPayment" class="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>


            <tr>
                <td><label for="txtComission">Dealer</label></td>
                <td>
                    <asp:TextBox id="txtDealer" class="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td><label for="txtComission">Area</label></td>
                <td>
                    <asp:TextBox id="txtArea" class="form-control" runat="server"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td><label for="txtComission">Officer Name</label></td>
                <td>
                    <asp:Label runat="server" ID="lblOfficer" />
                </td>
            </tr>
            <tr>
                <td><asp:Button ID="btnReset" runat="server" Text="Reset" class="btn btn-primary btn-lg"></asp:Button></td>
                <td colspan="2"><asp:Button ID="btnAdd" runat="server" Text="Order" class="btn btn-primary btn-lg" OnClick="btnAdd_Click"></asp:Button></td>
            </tr>
            
        </table>

    </form>

    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="footer" runat="Server">
    <script>
        var a = 0;
        var b = 0;
        var c = 0;
        var d = 0;
        $(function(){
            $('#<%= txtUnit1.ClientID %>').keyup(function(){
                var total1 = $(this).val() * $('#<%= lblPrice1.ClientID %>').text();
                
                $('#<%= lblTotalPrice1.ClientID %>').text(total1);
                a = total1;
                var total = a + b + c;
                $('#<%= lblTotal.ClientID %>').text(total);
            });
        });


        $(function(){
            $('#<%= txtUnit2.ClientID %>').keyup(function(){
                var total1 = $(this).val() * $('#<%= lblPrice2.ClientID %>').text();
                
                $('#<%= lblTotalPrice2.ClientID %>').text(total1);
                b = total1;
                var total = a + b + c;
                var totalMain2 = $('#<%= lblPrice1.ClientID %>').text() + $('#<%= lblPrice2.ClientID %>').text() + $('#<%= lblPrice3.ClientID %>').text();
                
                $('#<%= lblTotal.ClientID %>').text(total);
            });
        });

        $(function(){
            $('#<%= txtUnit3.ClientID %>').keyup(function(){
                var total1 = $(this).val() * $('#<%= lblPrice3.ClientID %>').text();
                
                $('#<%= lblTotalPrice3.ClientID %>').text(total1);
                c = total1;
                var total = a + b + c;
                var totalMain3 = $('#<%= lblPrice1.ClientID %>').text() + $('#<%= lblPrice2.ClientID %>').text() + $('#<%= lblPrice3.ClientID %>').text();
                $('#<%= lblTotal.ClientID %>').text(total);
            });
        });


    </script>
</asp:Content>