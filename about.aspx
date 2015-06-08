<%@ Page Title="About" Language="C#" MasterPageFile="~/layout.master" AutoEventWireup="true" CodeFile="about.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content2" ContentPlaceHolderID="companyname" Runat="Server">
    About
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="maincontent" Runat="Server">
    <table style="display:none;">
        <tr>
            <td colspan="2">
                <h3>Management</h3>
            </td>
        </tr>
        <tr>
            <td>
                Md Anam<br />
                Mobile: 01818619647<br />
                Email: <a href ="mailto:anamuliut@gmail.com">anamuliut@gmail.com</a>
            </td>
        </tr>
        <tr>
            <td colspan ="2">
                <h3>Developer</h3>
            </td>
        </tr>
        <tr>
            <td>
                Md. Abu Zafor Fagun<br />
                Mobile: 01677813190<br />
                Website: <a href ="http://www.fagunrain.com">www.fagunrain.com</a><br />
                Email: <a href ="mailto:mdabuzaforfagun@gmail.com">mdabuzaforfagun@gmail.com</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

