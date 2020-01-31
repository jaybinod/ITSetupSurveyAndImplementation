using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.Net;

public partial class ItemGroupMaster : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
    //string pwd="";
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
    //            this.MasterPageFile = "~/" + Session["MasterPage"];
    //        }
    //    }
    //    catch
    //    {
    //        Response.Redirect("default.aspx");
    //    }
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            try
            {
                //ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                if ((bool)Session["Validate"] == true)
                {
                    CallData();
                    //string cid=Request.QueryString["id"].ToString();
                    //Session["cid"] = cid;
                    //EngineerEmployee();
                    //ddlEmployee.SelectedValue = "";
                    //CallData(cid);
                }
            }
            catch
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    //protected void EngineerEmployee()
    //{
    //    SqlDataAdapter adpt = new SqlDataAdapter("Select * from EmployeeUserMaster EUM, DepartmentMaster DM where EUM.DepartmentID=DM.DepartmentID and DM.category='Survey' order by EmployeeName", conn);
    //    //if (conn.State == ConnectionState.Closed)
    //    //    conn.Open();
    //    DataTable dt = new DataTable();
    //    adpt.Fill(dt);
    //    conn.Close();
    //    dt.Rows.Add();
    //    dt.Rows[dt.Rows.Count - 1]["UserID"] = "";
    //    dt.Rows[dt.Rows.Count - 1]["EmployeeName"] = "(Select Item)";
    //    DataView dv = dt.DefaultView;
    //    dv.Sort = "EmployeeName asc";
    //    DataTable sortedDT = dv.ToTable();

    //    //ddlEmployee.DataSource = sortedDT;
    //    //ddlEmployee.DataBind();
    //}

    protected void CallData()
    {
        string query = "Select * from ItemGroupMaster";

        SqlDataAdapter cmd = new SqlDataAdapter(query, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        DataTable dt = new DataTable();
        cmd.Fill(dt);

        gvItemList.DataSource = dt;
        gvItemList.DataBind();
        conn.Close();
    }
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    object refUrl = ViewState["RefUrl"];
    //    if (refUrl != null)
    //        Response.Redirect((string)refUrl);
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(txtGroupName.Text.Trim().Length<=0)
        {
            lblMessage.Text = "Item Group name should not be empty";
            return;
        }
        int MItemID = 0;
        SqlCommand cmdc = new SqlCommand("Select * from ItemGroupMaster where upper(GroupName)=@GroupName", conn);
        cmdc.Parameters.Add(new SqlParameter("@GroupName", SqlDbType.NVarChar, txtGroupName.Text.Trim().Length)).Value = txtGroupName.Text.Trim().ToUpper();
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dritem = cmdc.ExecuteReader();
        bool find = false;
        if (dritem.Read())
        {
            lblMessage.Text="Duplicate Entry Not allowed";
            find = true;
        }
        dritem.Close();
        if (find ==false)
        {
            //string ItemName = txtItemName.Text.Trim() + " " + txtDescription.Text.Trim() + "-" + txtBrand.Text.Trim();
            //ItemName = ItemName.Replace("  ", " ").Trim();
            SqlCommand cmdi = new SqlCommand("Insert into ItemGroupMaster (GroupName) Values(@GroupName)", conn);
            cmdi.Parameters.Add(new SqlParameter("@GroupName", SqlDbType.NVarChar, txtGroupName.Text.Trim().Length)).Value = txtGroupName.Text.Trim();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmdi.ExecuteNonQuery();
            conn.Close();
            lblMessage.Text = "Item Group Created Sucessfully";
            refreshField();
            CallData();
        }

    }

    

    protected void refreshField()
    {
        txtGroupName.Text = "";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(ViewState["RefUrl"].ToString());
        }
        catch { }
    }
}