using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class SupplierList : System.Web.UI.Page
{

    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

    //protected void Page_PreInit(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        if (Session["userType"].ToString().Trim() == "Super")
    //        {
    //            this.MasterPageFile = "~/MainMasterPagea.master";
    //        }
    //        else
    //        {
    //            this.MasterPageFile = "~/MainMasterPage.master";
    //        }
    //    }
    //    catch
    //    {
    //        Response.Redirect("default.aspx");
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        
        try
        {
            if ((bool)Session["Validate"] == true)
            {
                BindDetailsView();
            }
        }
        catch
        {
            Response.Redirect("default.aspx");
        }
    }

    protected void  BindDetailsView()
    {
        SqlDataAdapter cmd = new SqlDataAdapter("Select * from SupplierMaster where Suppliername!='(Select Supplier Name)' order by SupplierName", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        DataTable dt = new DataTable();
        cmd.Fill(dt);
        gvSupplier.DataSource = dt;
        gvSupplier.DataBind();
        //lblCompany.Text = dt.Rows[0]["Company"].ToString();
        //lblGST.Text = dt.Rows[0]["GSTNo"].ToString();
        //lblAddress.Text = dt.Rows[0]["Address"].ToString();
        //lblCity.Text = dt.Rows[0]["City"].ToString();
        //lblPincode.Text = dt.Rows[0]["Pincode"].ToString();
        //lblContact.Text = dt.Rows[0]["Contact"].ToString();
        //lblMobile.Text = dt.Rows[0]["Mobile"].ToString();
        //lblEmail.Text = dt.Rows[0]["Email"].ToString();
        //lblAddNewDealerAlert.Text = dt.Rows[0]["NewDealerAdded"].ToString();
        //lblDealerModificationAlert.Text = dt.Rows[0]["ExistingDealerModification"].ToString();
    }

    //protected void btnEdit_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("updatesupplier.aspx");
    //}
    //protected void gvEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow &&
    //    (e.Row.RowState & DataControlRowState.Edit) == DataControlRowState.Edit)
    //    {
    //        // Here you will get the Control you need like:
    //        DropDownList dl = (DropDownList)e.Row.FindControl("ddlDepartment");
    //        SqlCommand cmd = new SqlCommand("Select * from DepartmentMaster order by Department", conn);
    //        if (conn.State == ConnectionState.Closed)
    //            conn.Open();
    //        dl.DataSource = cmd.ExecuteReader();
    //        dl.DataBind();
    //        conn.Close();
    //    }
    //}
    //protected void gvEmployee_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    gvEmployee.EditIndex = e.NewEditIndex;
    //    gvEmployee.DataBind();
    //}
}