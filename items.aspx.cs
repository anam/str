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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HelperClass.isExpired())
        {
            Response.Redirect("login.aspx");
        }
        string LoginStatus = HelperClass.IsLogin();
        if (LoginStatus != "Manager")
        {
            Response.Redirect("login.aspx");
        }

        if (Session["ActionMessage"] != null)
        {
            lblMessage.Text = Session["ActionMessage"].ToString();
            Session.Remove("ActionMessage");
        }

        if(!IsPostBack)
        {
            DataTable dt = HelperClass.GetItems();
            StringBuilder html = new StringBuilder();
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                i++;
                html.Append("<tr>");
                html.Append("<td>");
                html.Append(i);
                html.Append("</td>");

                html.Append("<td width='20%'>");
                html.Append(row["name"]);
                html.Append("</td>");

                html.Append("<td width='20%'>");
                html.Append(row["company"]);
                html.Append("</td>");
                html.Append("<td>");
                double commission =double.Parse(row["commission"].ToString());
                html.Append(commission.ToString() + "%");
                html.Append("</td>");
                html.Append("<td>");
                DataTable dtForPrice = HelperClass.GetPricesSingle(row["id"].ToString());
                StringBuilder Prices = new StringBuilder();
                foreach (DataRow row2 in dtForPrice.Rows)
                {
                    html.Append(row2["unit_name"]);
                    html.Append("<hr/>");
                }
                html.Append("</td>");
                html.Append("<td>");

                List<string> listOfPrices = new List<string>();
                foreach (DataRow row2 in dtForPrice.Rows)
                {
                    if (row2["unit_price"].ToString() != "0")
                    {
                        html.Append(row2["unit_price"].ToString());
                        html.Append("<hr/>");
                    }
                    
                    listOfPrices.Add(row2["unit_price"].ToString());
                    
                }
                html.Append("</td>");
                
                
                html.Append("<td>");

                for (int j = 0; j < listOfPrices.Count; j++)
                {
                    double UnitPrice = double.Parse(listOfPrices[j]);
                    double GrandTotal = UnitPrice - ((UnitPrice * commission) / 100);
                    if (GrandTotal != 0)
                    {
                        html.Append(GrandTotal.ToString());
                        html.Append("<hr/>");
                    }
                    
                }
                html.Append("</td>");

                html.Append("<td width='14%'>");

                html.Append("<a href = 'edititem.aspx?id=" + row["id"].ToString() + "'><button class='btn btn-primary btnedit'>Edit</button></a>");
                
                html.Append("<a href = 'delete.aspx?id=" + row["id"].ToString() + "&type=item'><button class='btn btn-primary btnedit'>Delete</button></a>");
                html.Append("</td>");

                html.Append("</tr>");
            }

            ItemsData.Controls.Add(new Literal { Text = html.ToString() });

        }
    }
}