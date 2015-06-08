using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HelperClass.isExpired())
        {
            Response.Redirect("login.aspx");
        }
        string LoginStatus = HelperClass.IsLogin();
        if (LoginStatus != "Manager")
        {
            if (LoginStatus == "SR")
            {
                Response.Redirect("stocks.aspx");
            }
            Response.Redirect("login.aspx");
        }

        var comname = HelperClass.getSetting();
        var dt = HelperClass.getSetting();
        foreach (DataRow row in dt.Rows)
        {
            lblCompanyName.Text = row["name"].ToString();
        }
        if (!IsPostBack)
        {
            lblSaleofMonth.Text = HelperClass.GetSalesOfCurrentMonth();
            lblItemAvailable.Text = HelperClass.GetAvailableItems();
            lblAvailableSRs.Text = HelperClass.GetAvailableSRs();
        }
    }
}