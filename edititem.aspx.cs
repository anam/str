using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : System.Web.UI.Page
{
    static string[] unitid = { "", "", "" };
    static string itemId;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HelperClass.isExpired())
        {
            Response.Redirect("login.aspx");
        }
        string LoginStatus = HelperClass.IsLogin();

        string comID = HttpContext.Current.Session["CompanyID"].ToString();
        string id = string.Empty;

        if (LoginStatus != "Manager")
        {
            Response.Redirect("login.aspx");
        }
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"];
                DataTable dt = HelperClass.getSingleItem(id);
                
                foreach (DataRow row in dt.Rows)
                {
                    itemId = row["id"].ToString();
                    txtComission.Text = row["commission"].ToString();
                    txtCompany.Text = row["company"].ToString();
                    txtName.Text = row["name"].ToString();
                    string[] unitname = {"","",""};
                    string[] unitprice = {"","",""};

                    DataTable dtForPrice = HelperClass.GetPricesSingle(row["id"].ToString());
                    StringBuilder Prices = new StringBuilder();
                    int i = 0;
                    foreach (DataRow row2 in dtForPrice.Rows)
                    {
                        unitname[i] = row2["unit_name"].ToString();
                        unitprice[i] = row2["unit_price"].ToString();
                        unitid[i] = row2["id"].ToString();
                        i++;
                    }

                    txtUnit1.Text = unitname[0];
                    txtUnit2.Text = unitname[1];
                    txtUnit3.Text = unitname[2];
                    txtUnitPrice1.Text = unitprice[0];
                    txtUnitPrice2.Text = unitprice[1];
                    txtUnitPrice3.Text = unitprice[2];

                }
            }
            else
            {
                Session.RemoveAll();
                Response.Redirect("login.aspx");
            }
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        string name, company, commission, unitname1, unitname2, unitname3, unitprice1, unitprice2, unitprice3 = "";
        name = txtName.Text;
        company = txtCompany.Text;
        commission = txtComission.Text;
        unitname1 = txtUnit1.Text;
        unitname2 = txtUnit2.Text;
        unitname3 = txtUnit3.Text;
        unitprice1 = txtUnitPrice1.Text;
        unitprice2 = txtUnitPrice2.Text;
        unitprice3 = txtUnitPrice3.Text;
        HelperClass.UpdateItem(itemId, name, company, unitname1, unitname2, unitname3, unitprice1, unitprice2, unitprice3, commission, unitid[0], unitid[1], unitid[2]);
        Session["ActionMessage"] = "Item Saved";
        Response.Redirect("items.aspx");
    }
}