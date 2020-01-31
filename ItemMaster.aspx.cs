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

public partial class ItemMaster : System.Web.UI.Page
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
                    CallData(txtItemName.Text.Trim(), txtBrand.Text.Trim(), txtPartNumber.Text.Trim(), txtDescription.Text.Trim());
                    GroupList();
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

    protected void GroupList()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("Select * from ItemGroupMaster order by GroupName", conn);
        DataTable dtg = new DataTable();
        adpt.Fill(dtg);
        ddlGroup.DataSource = dtg;
        ddlGroup.DataBind();
    }

    protected void EngineerEmployee()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("Select * from EmployeeUserMaster EUM, DepartmentMaster DM where EUM.DepartmentID=DM.DepartmentID and DM.category='Survey' order by EmployeeName", conn);
        //if (conn.State == ConnectionState.Closed)
        //    conn.Open();
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        conn.Close();
        dt.Rows.Add();
        dt.Rows[dt.Rows.Count - 1]["UserID"] = "";
        dt.Rows[dt.Rows.Count - 1]["EmployeeName"] = "(Select Item)";
        DataView dv = dt.DefaultView;
        dv.Sort = "EmployeeName asc";
        DataTable sortedDT = dv.ToTable();

        //ddlEmployee.DataSource = sortedDT;
        //ddlEmployee.DataBind();
    }

    protected void CallData(string item, string brand, string PartNumber, string description)
    {
        if (ddlGroup.SelectedValue !="")
        {
            string query = "Select * from ItemMaster where Mitem like '%" + item + "%' and Brand like '%" + brand + "%' and PartNumber like '%" + PartNumber + "%' and description like '%" + description + "%' and groupid=" + ddlGroup.SelectedValue;

            SqlDataAdapter cmd = new SqlDataAdapter(query, conn);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            DataTable dt = new DataTable();
            cmd.Fill(dt);

            gvItemList.DataSource = dt;
            gvItemList.DataBind();
            conn.Close();
        }
    }
    //protected void btnCancel_Click(object sender, EventArgs e)
    //{
    //    object refUrl = ViewState["RefUrl"];
    //    if (refUrl != null)
    //        Response.Redirect((string)refUrl);
    //}
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if(txtItemName.Text.Trim().Length<=0)
        {
            lblMessage.Text = "Item name should not be empty";
            return;
        }
        int MItemID = 0;
        SqlCommand cmdc = new SqlCommand("Select * from ItemMainMaster where upper(Item)=@Item", conn);
        cmdc.Parameters.Add(new SqlParameter("@Item",SqlDbType.NVarChar,txtItemName.Text.Trim().Length)).Value = txtItemName.Text.Trim().ToUpper();
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dr= cmdc.ExecuteReader();
        if(dr.Read())
        {
            MItemID = (int)dr["MItemID"];
        }
        dr.Close();
        if(MItemID<=0)
        {
            SqlCommand cmdi = new SqlCommand("Insert into ItemMainMaster (Item) output inserted.MItemID Values(@Item)", conn);
            cmdi.Parameters.Add(new SqlParameter("@Item", SqlDbType.NVarChar, txtItemName.Text.Trim().Length)).Value = txtItemName.Text.Trim();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            MItemID = cmdi.ExecuteNonQuery();
            conn.Close();
        }

        int BrandID = 0;
        SqlCommand cmdb = new SqlCommand("Select * from BrandMaster where upper(Brand)=@Brand", conn);
        cmdb.Parameters.Add(new SqlParameter("@Brand", SqlDbType.NVarChar, txtBrand.Text.Trim().Length)).Value = txtBrand.Text.Trim().ToUpper();
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader drb = cmdb.ExecuteReader();
        if (drb.Read())
        {
            BrandID = (int)drb["BrandID"];
        }
        drb.Close();
        if (BrandID <= 0)
        {
            SqlCommand cmdi = new SqlCommand("Insert into BrandMaster (Brand) output inserted.BrandID Values(@Brand)", conn);
            cmdi.Parameters.Add(new SqlParameter("@Brand", SqlDbType.NVarChar, txtBrand.Text.Trim().Length)).Value = txtBrand.Text.Trim();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            BrandID = cmdi.ExecuteNonQuery();
            conn.Close();
        }

        //int ItemID = 0;
        SqlCommand cmdItemr = new SqlCommand("Select * from ItemMaster where MItemID=@MItemID and BrandID=@BrandID and upper(PartNumber)=@PartNumber and upper(Description)=@Description", conn);
        cmdItemr.Parameters.Add(new SqlParameter("@MItemID", SqlDbType.Int)).Value = MItemID;
        cmdItemr.Parameters.Add(new SqlParameter("@BrandID", SqlDbType.Int)).Value = BrandID;
        cmdItemr.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, txtDescription.Text.Trim().Length)).Value = txtDescription.Text.Trim().ToUpper();
        cmdItemr.Parameters.Add(new SqlParameter("@PartNumber", SqlDbType.NVarChar, txtPartNumber.Text.Trim().Length)).Value = txtPartNumber.Text.Trim().ToUpper();
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dritem = cmdItemr.ExecuteReader();
        bool find = false;
        if (dritem.Read())
        {
            lblMessage.Text="Duplicate Entry Not allowed";
            find = true;
        }
        dritem.Close();
        if (find ==false)
        {
            if (txtCGSTPercentage.Text.ToString().Trim().Length <= 0)
                txtCGSTPercentage.Text = "0.00";
            if (txtSGSTPercentage.Text.ToString().Trim().Length <= 0)
                txtSGSTPercentage.Text = "0.00";
            if (txtIGSTPercentage.Text.ToString().Trim().Length <= 0)
                txtIGSTPercentage.Text = "0.00";
            string ItemName = txtItemName.Text.Trim() + " " + txtDescription.Text.Trim() + "-" + txtBrand.Text.Trim();

            string ItemNameWithPartnumber = txtItemName.Text.Trim() + " " + txtDescription.Text.Trim() + "-" + txtBrand.Text.Trim() +"-"+ txtPartNumber.Text.Trim();

            ItemName = ItemName.Replace("  ", " ").Trim();
            SqlCommand cmdi = new SqlCommand("Insert into ItemMaster (MItemID,GroupID,MItem,BrandID,Brand,PartNumber,Description,Unit,ItemName, ItemNameWithPartNumber, HSNCode, CGST, SGST, IGST) Values(@MItemID,@GroupID,@MItem,@BrandID,@Brand,@PartNumber,@Description,@Unit,@ItemName, @ItemNameWithPartNumber, @HSNCode, @CGST, @SGST, @IGST)", conn);
            cmdi.Parameters.Add(new SqlParameter("@MItemID", SqlDbType.Int)).Value = MItemID;
            cmdi.Parameters.Add(new SqlParameter("@MItem", SqlDbType.NVarChar, txtItemName.Text.Trim().Length)).Value = txtItemName.Text.Trim();
            cmdi.Parameters.Add(new SqlParameter("@BrandID", SqlDbType.Int)).Value = BrandID;
            cmdi.Parameters.Add(new SqlParameter("@Brand", SqlDbType.NVarChar, txtBrand.Text.Trim().Length)).Value = txtBrand.Text.Trim();
            cmdi.Parameters.Add(new SqlParameter("@PartNumber", SqlDbType.NVarChar, txtPartNumber.Text.Trim().Length)).Value = txtPartNumber.Text.Trim();
            cmdi.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar, txtDescription.Text.Trim().Length)).Value = txtDescription.Text.Trim();
            cmdi.Parameters.Add(new SqlParameter("@Unit", SqlDbType.NVarChar, txtUnit.Text.Trim().Length)).Value = txtUnit.Text.Trim();
            cmdi.Parameters.Add(new SqlParameter("@ItemName", SqlDbType.NVarChar, ItemName.Trim().Length)).Value = ItemName;
            cmdi.Parameters.Add(new SqlParameter("@ItemNameWithPartNumber", SqlDbType.NVarChar, ItemNameWithPartnumber.Trim().Length)).Value = ItemNameWithPartnumber;
            cmdi.Parameters.Add(new SqlParameter("@GroupID", SqlDbType.Int)).Value = ddlGroup.SelectedValue;
            cmdi.Parameters.Add(new SqlParameter("@HSNCode", SqlDbType.NVarChar, txtHSN.Text.Trim().Length)).Value = txtHSN.Text.Trim();
            cmdi.Parameters.Add(new SqlParameter("@CGST", SqlDbType.Float)).Value = double.Parse(txtCGSTPercentage.Text.ToString().Trim());
            cmdi.Parameters.Add(new SqlParameter("@SGST", SqlDbType.Float)).Value = double.Parse(txtSGSTPercentage.Text.ToString().Trim());
            cmdi.Parameters.Add(new SqlParameter("@IGST", SqlDbType.Float)).Value = double.Parse(txtIGSTPercentage.Text.ToString().Trim());

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmdi.ExecuteNonQuery();
            conn.Close();
            lblMessage.Text = "Item Created Sucessfully";
            refreshField();
            CallData(txtItemName.Text.Trim(), txtBrand.Text.Trim(), txtPartNumber.Text.Trim(), txtDescription.Text.Trim());
        }

    }

    

    protected void refreshField()
    {
        txtItemName.Text = "";
        txtBrand.Text = "";
        txtPartNumber.Text = "";
        txtDescription.Text = "";
        txtUnit.Text = "";
        txtHSN.Text = "";
        txtIGSTPercentage.Text = "";
        txtSGSTPercentage.Text = "";
        txtCGSTPercentage.Text = "";
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect(ViewState["RefUrl"].ToString());
        }
        catch { }
    }
    protected void txtItemName_TextChanged(object sender, EventArgs e)
    {
        CallData(txtItemName.Text.Trim(), txtBrand.Text.Trim(), txtPartNumber.Text.Trim(), txtDescription.Text.Trim());
        txtBrand.Focus();
    }
    protected void txtBrandName_TextChanged(object sender, EventArgs e)
    {
        CallData(txtItemName.Text.Trim(), txtBrand.Text.Trim(), txtPartNumber.Text.Trim(), txtDescription.Text.Trim());
        txtPartNumber.Focus();
    }
    protected void txtDescription_TextChanged(object sender, EventArgs e)
    {
        CallData(txtItemName.Text.Trim(), txtBrand.Text.Trim(), txtPartNumber.Text.Trim(), txtDescription.Text.Trim());
    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        CallData(txtItemName.Text.Trim(), txtBrand.Text.Trim(), txtPartNumber.Text.Trim(), txtDescription.Text.Trim());
    }
    protected void txtPartNumber_TextChanged(object sender, EventArgs e)
    {
        CallData(txtItemName.Text.Trim(), txtBrand.Text.Trim(), txtPartNumber.Text.Trim(), txtDescription.Text.Trim());
        txtDescription.Focus();
    }
}