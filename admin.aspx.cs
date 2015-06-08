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
        
        if (!IsPostBack)
        {
            StringBuilder html = new StringBuilder();
            DataTable dt = HelperClass.getCompanyNames();
            
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                html.Append("<td>");
                html.Append(row["name"]);
                html.Append("</td>");
                DataTable dt2 = HelperClass.getCompanyDetails(row["com_id"].ToString());
                foreach (DataRow row2 in dt2.Rows)
                {
                    html.Append("<td>");
                    html.Append(row2["name"].ToString());
                    html.Append("</td>");

                    html.Append("<td>");
                    html.Append(row2["email"].ToString());
                    html.Append("</td>");

                    html.Append("<td>");
                    html.Append(row2["password"].ToString());
                    html.Append("</td>");
                }
                html.Append("<td>");
                html.Append("<a href = 'editcompany.aspx?id=" + row["id"].ToString() + "'><button class='btn btn-primary btnedit'>Edit</button></a>");
                html.Append("<a href = 'deletecompany.aspx?id=" + row["id"].ToString() + "'><button class='btn btn-primary btnedit'>Delete</button></a>");
                html.Append("</tr>");
            }
            
            ItemsData.Controls.Add(new Literal { Text = html.ToString() });
        }
        
    }
}