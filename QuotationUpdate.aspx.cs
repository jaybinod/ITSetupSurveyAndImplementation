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

public partial class QuotationUpdate : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            try
            {
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                if ((bool)Session["Validate"] == true)
                {
                    
                    try
                    {
                        string iid = Request.QueryString["iid"].ToString();
                        Session["iid"] = iid;
                        SupplierMaster();
                    }
                    catch
                    {
                        Response.Redirect(ViewState["RefUrl"].ToString());
                    }

                    CallItemData(Session["iid"].ToString());
                    
                }
            }
            catch
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    protected void SupplierMaster()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("Select * from SupplierMaster order by SupplierName", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        DataTable dtv = new DataTable();
        adpt.Fill(dtv);
        ddlSupplierName.DataSource = dtv;
        ddlSupplierName.DataBind();
    }

    protected void CallItemData(string IID)
    {
        SqlCommand cmd = new SqlCommand("Select * from SurveyClientItemDetails SCID, ItemMaster IM, ItemGroupMaster IGM where SCID.ItemID=IM.ItemID and IM.GroupID=IGM.GroupID and ID="+IID, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if(dr.Read())
        {
            lblGroup.Text = dr["GroupName"].ToString();
            lblItem.Text = dr["ItemName"].ToString();
            lblQuantity.Text = dr["Quantity"].ToString();
            lblUnit.Text = dr["Unit"].ToString();
            ddlSupplierName.SelectedValue = dr["SupplierID"].ToString();
            txtTotalValue.Text = double.Parse(dr["Quotedpricetotal"].ToString()).ToString("N2");
            txtUnitCost.Text = double.Parse(dr["QuotedpriceUnit"].ToString()).ToString("N2");
            txtPurchaseCost.Text = double.Parse(dr["PurchaseCost"].ToString()).ToString("N2");
            txtHSN.Text = dr["HSNCode"].ToString();
            txtCGSTPercentage.Text = dr["CGST"].ToString();
            txtSGSTPercentage.Text = dr["SGST"].ToString();
            txtIGSTPercentage.Text = dr["IGST"].ToString();
            Session["ItemID"] = dr["ItemID"].ToString();
            double TotalValue = double.Parse(txtTotalValue.Text);
            //double unitCost = TotalValue / double.Parse(lblQuantity.Text);
            double purchaseCost = double.Parse(txtPurchaseCost.Text);
            double margin = ((TotalValue - purchaseCost) * 100) / purchaseCost;
            if (purchaseCost <=0)
                margin = 0;
            //txtUnitCost.Text = (double.Parse(txtTotalValue.Text) / double.Parse(lblQuantity.Text)).ToString("N2");
            txtMargin.Text = margin.ToString("N2");

        }
        conn.Close();
    }

   
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        object refUrl = ViewState["RefUrl"];
        if (refUrl != null)
            Response.Redirect((string)refUrl);
    }
    
   
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewState["RefUrl"].ToString());
    }
   
    protected void TotalValue_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double TotalValue = double.Parse(txtTotalValue.Text);
            double unitCost = TotalValue / double.Parse(lblQuantity.Text);
            double purchaseCost = double.Parse(txtPurchaseCost.Text);
            double margin = ((TotalValue-purchaseCost)*100)/purchaseCost;

            txtUnitCost.Text = (double.Parse(txtTotalValue.Text) / double.Parse(lblQuantity.Text)).ToString("N2");
            txtMargin.Text = margin.ToString("N2");
            btnFinishSurvey.Focus();
        }
        catch { }
    }
    protected void btnFinishSurvey_Click(object sender, EventArgs e)
    {
        bool validate = false;
        double TotalValue = 0;
        double unitCost = 0;
        double purchaseCost = 0;
        double margin = 0;
        string SupplierID = "";

        try
        {
            TotalValue = double.Parse(txtTotalValue.Text);
            unitCost = TotalValue / double.Parse(lblQuantity.Text);
            purchaseCost = double.Parse(txtPurchaseCost.Text);
            margin = ((TotalValue-purchaseCost)*100)/purchaseCost;
            if (purchaseCost<= 0 || TotalValue<=0)
                margin = 0;
            SupplierID = ddlSupplierName.SelectedValue;
            validate = true;
        }
        catch
        {
            validate = false;
        }
        if (validate == true)
        {
            if (txtCGSTPercentage.Text.ToString().Trim().Length <= 0)
                txtCGSTPercentage.Text = "0.00";
            if (txtSGSTPercentage.Text.ToString().Trim().Length <= 0)
                txtSGSTPercentage.Text = "0.00";
            if (txtIGSTPercentage.Text.ToString().Trim().Length <= 0)
                txtIGSTPercentage.Text = "0.00";

            SqlCommand cmditem = new SqlCommand("Update ItemMaster set HSNCode=@HSNCode, CGST=@CGST, SGST=@SGST, IGST=@IGST where ItemID=" + int.Parse(Session["ItemID"].ToString()), conn);
            cmditem.Parameters.Add(new SqlParameter("@HSNCode", SqlDbType.NVarChar, txtHSN.Text.Trim().Length)).Value = txtHSN.Text.Trim();
            cmditem.Parameters.Add(new SqlParameter("@CGST", SqlDbType.Float)).Value = double.Parse(txtCGSTPercentage.Text.ToString().Trim());
            cmditem.Parameters.Add(new SqlParameter("@SGST", SqlDbType.Float)).Value = double.Parse(txtSGSTPercentage.Text.ToString().Trim());
            cmditem.Parameters.Add(new SqlParameter("@IGST", SqlDbType.Float)).Value = double.Parse(txtIGSTPercentage.Text.ToString().Trim());
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmditem.ExecuteNonQuery();

            SqlCommand cmdu = new SqlCommand("Update SurveyClientItemDetails set QuotedPriceTotal=@QuotedPriceTotal, QuotedPriceUnit=@QuotedPriceUnit, SupplierID=@SupplierID, PurchaseCost=@PurchaseCost, SalesMargin=@SalesMargin, ChangeStatus=1 where ID=" + int.Parse(Session["iid"].ToString()), conn);
            cmdu.Parameters.Add(new SqlParameter("@QuotedPriceTotal", SqlDbType.Float)).Value = TotalValue;
            cmdu.Parameters.Add(new SqlParameter("@QuotedPriceUnit", SqlDbType.Float)).Value = unitCost;
            cmdu.Parameters.Add(new SqlParameter("@PurchaseCost", SqlDbType.Float)).Value = purchaseCost;
            cmdu.Parameters.Add(new SqlParameter("@SalesMargin", SqlDbType.Float)).Value = margin;
            cmdu.Parameters.Add(new SqlParameter("@SupplierID", SqlDbType.NVarChar, SupplierID.Length)).Value = SupplierID;
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmdu.ExecuteNonQuery();
            Response.Redirect(ViewState["RefUrl"].ToString());
        }
        else
        {
            lblMessage.Text = "Please check Entered Data... There is somthing wrong";
        }
            
    }
    protected void txtMargin_TextChanged(object sender, EventArgs e)
    {
        //TotalValue = double.Parse(txtTotalValue.Text);
        //unitCost = TotalValue / double.Parse(lblQuantity.Text);
        double purchaseCost = double.Parse(txtPurchaseCost.Text);
        double margin = double.Parse(txtMargin.Text);
        double marginValue = (purchaseCost * margin) / 100;
        double TotalValue = purchaseCost + marginValue;
        txtTotalValue.Text = TotalValue.ToString("N2");
        txtUnitCost.Text = double.Parse((TotalValue / double.Parse(lblQuantity.Text)).ToString()).ToString("N2");
        //margin = ((TotalValue - purchaseCost) * 100) / purchaseCost;
       
    }
    protected void txtPurchaseCost_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double TotalValue = double.Parse(txtTotalValue.Text);
            double unitCost = TotalValue / double.Parse(lblQuantity.Text);
            double purchaseCost = double.Parse(txtPurchaseCost.Text);
            double margin = ((TotalValue - purchaseCost) * 100) / purchaseCost;

            txtUnitCost.Text = (double.Parse(txtTotalValue.Text) / double.Parse(lblQuantity.Text)).ToString("N2");
            txtMargin.Text = margin.ToString("N2");
            btnFinishSurvey.Focus();
        }
        catch { }
    }
}