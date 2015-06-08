using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{

    static string itemCode = "";
    static string name = "";
    static string company = "";
    static string unit1value = "";
    static string unit2value = "";
    static string unit3value = "";
    static string unit1name = "";
    static string unit2name = "";
    static string unit3name = "";

    static string totalprice = "";
    static string gift = "";
    static string giftUnit = "";
    static string bankName = "";
    static string paymentMethod = "";
    static string dealer = "";
    static string area = "";

    static string stock1 = "";
    static string stock2 = "";
    static string stock3 = "";

    static string officerName = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (HelperClass.isExpired())
        {
            Response.Redirect("login.aspx");
        }
        btnReset.Attributes.Add("onClick", "document.forms[0].reset();return false;");
        

        drpGfitValue.Items.Add(new ListItem("Unit Name", "-1"));

        if (Request.QueryString["id"] != null)
        {
            itemCode = Request.QueryString["id"];
        }
        else
        {
            string prevPage = Request.UrlReferrer.ToString();
            Response.Redirect(prevPage);
        }

        DataTable dt = HelperClass.getSingleItem(itemCode);
        foreach (DataRow row in dt.Rows)
        {
            name = row["name"].ToString();
            company = row["company"].ToString();
            lblName.Text = name;
            lblCompany.Text = company;
            lblItemCode.Text = itemCode;
        }

        DataTable dt2 = HelperClass.getSingleItemUnitAndPrice(itemCode);
        string[] unitNames = { "", "", "" };
        string[] unitPrices = { "", "", "" };
        int i =0;
        foreach (DataRow row2 in dt2.Rows)
        {
            unitNames[i] = row2["unit_name"].ToString();
            unitPrices[i] = row2["unit_price"].ToString();
            i++;
                drpGfitValue.Items.Add(new ListItem(unitNames[i-1], i.ToString()));
        }
        if (unitNames[0] == "")
        {
            unit1.Visible = false;
            Available1.Visible = false;
        }
        if (unitNames[1] == "")
        {
            unit2.Visible = false;
            Available2.Visible = false;
        }
        if (unitNames[2] == "")
        {
            unit3.Visible = false;
            Available3.Visible = false;
        }

        List<string> ListofStock = new List<string>();
        DataTable dt3 = HelperClass.getSingleStock(itemCode);
        int[] StocksItem = { 0, 0, 0 };
        int itemI = 0;
        foreach (DataRow rowTest in dt3.Rows)
        {
            int.TryParse(rowTest["amount"].ToString(), out StocksItem[itemI]);
            itemI++;
        }


        unit1value = lblPrice1.Text =  unitPrices[0];
        unit2value = lblPrice2.Text = unitPrices[1];
        unit3value = lblPrice3.Text = unitPrices[2];
        lblAUnitValue1.Text = StocksItem[0].ToString();
        lblAUnitValue2.Text = StocksItem[1].ToString();
        lblAUnitValue3.Text = StocksItem[2].ToString();

        lblAUnitName1.Text = unit1name = lblUnit1.Text = unitNames[0];
        lblAUnitName2.Text = unit2name = lblUnit2.Text = unitNames[1];
        lblAUnitName3.Text = unit3name = lblUnit3.Text = unitNames[2];
            

        officerName = lblOfficer.Text = Session["userName"].ToString();
        
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (drpGfitValue.SelectedItem.Text == "Unit Name")
        {
            lblMessage.Text = "Please Select Gift Unit";
            return;
        }
        gift = txtGift.Text;
        giftUnit = drpGfitValue.SelectedItem.Text;
        bankName = txtBank.Text;
        paymentMethod = txtPayment.Text;
        dealer = txtDealer.Text;
        area = txtArea.Text;
        stock1 = txtUnit1.Text;
        stock2 = txtUnit2.Text;
        stock3 = txtUnit3.Text;
        string quantity = "";

        if(stock1 != "")
        {
            quantity = stock1 + " " + unit1name;
        }
        if(stock2 != "")
        {
            quantity += ", " + stock2 + " " + unit2name;
        }
        if (stock3 != "")
        {
            quantity += ", "+ stock3 + " " + unit3name;
        }
        
        int finalUnit1;
        int finalUnit2;
        int finalUnit3;
        int.TryParse(stock1, out finalUnit1);
        int.TryParse(stock2, out finalUnit2);
        int.TryParse(stock3, out finalUnit3);
        int _giftt;
        int.TryParse(gift, out _giftt);
        if (giftUnit == unit1name)
        {
            finalUnit1 += _giftt;
        }
        else if (giftUnit == unit2name)
        {
            finalUnit2 += _giftt;
        }
        else if (giftUnit == unit3name)
        {
            finalUnit3 += _giftt;
        }

        if (finalUnit1 != 0)
        {
            HelperClass.SaleUnit(itemCode, unit1name, finalUnit1.ToString());
        }

        if (finalUnit2 != 0)
        {
            HelperClass.SaleUnit(itemCode, unit2name, finalUnit2.ToString());
        }

        if (finalUnit3 != 0)
        {
            HelperClass.SaleUnit(itemCode, unit3name, finalUnit3.ToString());
        }


        int finalamount1;
        int finalamount2;
        int finalamount3;
        int finalprice1;
        int finalprice2;
        int finalprice3;
        int finaltotal;

        int.TryParse(lblAUnitValue1.Text, out finalamount1);
        int.TryParse(lblAUnitValue2.Text, out finalamount2);
        int.TryParse(lblAUnitValue3.Text, out finalamount3);

        int.TryParse(lblTotalPrice1.Text, out finalprice1);
        int.TryParse(lblTotalPrice2.Text, out finalprice2);
        int.TryParse(lblTotalPrice3.Text, out finalprice3);
        int.TryParse(lblTotal.Text, out finaltotal);

        finalamount1 = finalamount1 - finalUnit1;
        finalamount2 = finalamount2 - finalUnit2;
        finalamount3 = finalamount3 - finalUnit3;

        int _totalPriceValue1;
        int _totalPriceValue2;
        int _totalPriceValue3;

        int _unitprice1;
        int _unitprice2;
        int _unitprice3;

        int.TryParse(unit1value, out _unitprice1);
        int.TryParse(unit2value, out _unitprice2);
        int.TryParse(unit3value, out _unitprice3);

        _totalPriceValue1 = _unitprice1 * finalUnit1;
        _totalPriceValue2 = _unitprice2 * finalUnit2;
        _totalPriceValue3 = _unitprice3 * finalUnit3;

        lblAUnitValue1.Text = finalamount1.ToString();
        lblAUnitValue2.Text = finalamount2.ToString();
        lblAUnitValue3.Text = finalamount3.ToString();

        lblTotalPrice1.Text = _totalPriceValue1.ToString();
        lblTotalPrice2.Text = _totalPriceValue2.ToString();
        lblTotalPrice3.Text = _totalPriceValue3.ToString();
        
        int _totalprice;
        _totalprice = _totalPriceValue1 + _totalPriceValue2 + _totalPriceValue3;
        totalprice = _totalprice.ToString();
        lblTotal.Text = totalprice;

        string _gift = gift + " "+ giftUnit;
        string _officerId = Session["userid"].ToString();
        string officer_name = Session["userName"].ToString();

        HelperClass.AddSalesReport(itemCode, quantity, _gift, bankName, paymentMethod, _officerId, dealer, area, totalprice, officer_name);

        lblSuccess.Text = "Order Completed";

    }
}