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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HelperClass.isExpired())
        {
            Response.Redirect("login.aspx");
        }
        btnReset.Attributes.Add("onClick", "document.forms[0].reset();return false;");
        if (!IsPostBack)
        {
            DataTable dt = HelperClass.getItemTypesName();
            List<ListItem> Items = new List<ListItem>();
            drpCompany.Items.Add(new ListItem("Select Type", "-1"));
            drpNames.Items.Add(new ListItem("Select Item", "-1"));
            foreach (DataRow row in dt.Rows)
            {
                Items.Add(new ListItem(row["company"].ToString(), row["id"].ToString()));
                
            }

            for(int i =0 ;i<Items.Count;i++)
            {
                drpCompany.Items.Add(Items[i]);
            }
        }

        trunit1.Visible = false;
        trunit2.Visible = false;
        trunit3.Visible = false;
        
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {

        string unit1, unit2, unit3;
        unit1 = unit2 = unit3 = "";
        string stock1, stock2, stock3;
        stock1 = stock2 = stock3 = "";

        if (lblUnit1.Text != "")
        {
            DataTable dt = HelperClass.getSingleItemStock(drpNames.SelectedItem.Value, lblUnit1.Text);
            foreach (DataRow row in dt.Rows)
            {
                stock1 = row["amount"].ToString();
            }
            unit1 = txtUnit1.Text;
        }
        if (lblUnit2.Text != "")
        {
            DataTable dt = HelperClass.getSingleItemStock(drpNames.SelectedItem.Value, lblUnit2.Text);
            foreach (DataRow row in dt.Rows)
            {
                stock2 = row["amount"].ToString();
            }
            unit2 = txtUnit2.Text;
        }
        if (lblUnit3.Text != "")
        {
            DataTable dt = HelperClass.getSingleItemStock(drpNames.SelectedItem.Value, lblUnit3.Text);
            foreach (DataRow row in dt.Rows)
            {
                stock3 = row["amount"].ToString();
            }
            unit3 = txtUnit3.Text;
        }

        int stockInt1, stockInt2, stockInt3;
        int.TryParse(stock1, out stockInt1);
        int.TryParse(stock2, out stockInt2);
        int.TryParse(stock3, out stockInt3);

        int unitInt1, unitInt2, unitInt3;
        int.TryParse(unit1, out unitInt1);
        int.TryParse(unit2, out unitInt2);
        int.TryParse(unit3, out unitInt3);

        stockInt1 += unitInt1;
        stockInt2 += unitInt2;
        stockInt3 += unitInt3;

        HelperClass.AddItemsInStock(drpNames.SelectedItem.Value, lblUnit1.Text, stockInt1.ToString());
        HelperClass.AddItemsInStock(drpNames.SelectedItem.Value, lblUnit2.Text, stockInt2.ToString());
        HelperClass.AddItemsInStock(drpNames.SelectedItem.Value, lblUnit3.Text, stockInt3.ToString());

        lblMessage.Text = "Item Added in the Stock";

        drpCompany.ClearSelection();
        drpNames.ClearSelection();
        txtUnit1.Text = txtUnit2.Text = txtUnit3.Text = "";
        drpNames.Items.Clear();
        drpNames.Items.Add(new ListItem("Select Item", "-1"));
    }
    protected void drpCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpNames.Items.Clear();
        DataTable dt = HelperClass.getItemsOfType(drpCompany.SelectedItem.Text);
        foreach (DataRow row in dt.Rows)
        {
            drpNames.Items.Add(new ListItem(row["name"].ToString(), row["id"].ToString()));
        }

        DataTable dt2 = HelperClass.getItmesUnitName(drpCompany.SelectedItem.Value);
        List<string> uniteNames = new List<string>();
        foreach (DataRow row in dt2.Rows)
        {
            uniteNames.Add(row["unit_name"].ToString());
        }

        if (uniteNames.Count == 3)
        {
            lblUnit1.Text = uniteNames[0];
            lblUnit2.Text = uniteNames[1];
            lblUnit3.Text = uniteNames[2];
            trunit1.Visible = trunit2.Visible = trunit3.Visible = true;
        }
        else if (uniteNames.Count == 2)
        {
            lblUnit1.Text = uniteNames[0];
            lblUnit2.Text = uniteNames[1];
            trunit1.Visible = trunit2.Visible =  true;
        }
        else if (uniteNames.Count == 1)
        {
            lblUnit1.Text = uniteNames[0];
            trunit1.Visible = true;
        }
    }
}