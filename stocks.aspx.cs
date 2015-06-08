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
        if (!IsPostBack)
        {
            DataTable dt = HelperClass.GetItems();
            StringBuilder html = new StringBuilder();
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                List<string> ListofStock = new List<string>();
                DataTable dt3 = HelperClass.getSingleStock(row["id"].ToString());
                int[] StocksItem = { 0, 0, 0 };
                int itemI=0;
                int totalStocks=0;
                foreach (DataRow rowTest in dt3.Rows)
                {
                    int.TryParse(rowTest["amount"].ToString(), out StocksItem[itemI]);
                    itemI++;
                }
                for (int k = 0; k < 3; k++)
                {
                    totalStocks += StocksItem[k];
                }

                if (totalStocks == 0)
                {
                    continue;
                }
                i++;
                html.Append("<tr>");
                html.Append("<td>");
                html.Append(row["id"].ToString());
                html.Append("</td>");

                html.Append("<td width='20%'>");
                html.Append(row["name"]);
                html.Append("</td>");

                html.Append("<td width='20%'>");
                html.Append(row["company"]);
                html.Append("</td>");
                html.Append("<td>");
                double commission = double.Parse(row["commission"].ToString());
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

                
                foreach (DataRow row3 in dt3.Rows)
                {
                    ListofStock.Add(row3["amount"].ToString());
                }
                html.Append("<td>");

                for (int j = 0; j < ListofStock.Count; j++)
                {

                    html.Append(ListofStock[j].ToString());
                    html.Append("<hr/>");
                    
                }
                html.Append("</td>");

                html.Append("<td width='8%'>");

                html.Append("<a href = 'order.aspx?id=" + row["id"].ToString() + "'><button class='btn btn-primary btnedit'>Order</button></a>");

                html.Append("</td>");

                html.Append("</tr>");
            }

            ItemsData.Controls.Add(new Literal { Text = html.ToString() });
        }
    }
}