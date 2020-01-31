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

public partial class ProjectActualUpdate : System.Web.UI.Page
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
                ViewState["RefUrl"] = Request.UrlReferrer.ToString();
                if ((bool)Session["Validate"] == true)
                {
                    string cid=Request.QueryString["id"].ToString();
                    Session["cid"] = cid;
                    try
                    {
                        string sid = Request.QueryString["sid"].ToString();
                        ViewState["SurveyID"] = int.Parse(sid);
                        btnFinishSurvey.Enabled = false;
                    }
                    catch
                    {
                        Response.Redirect("HomeSurvey.aspx");
                    }

                    int sur = int.Parse(ViewState["SurveyID"].ToString());
                    
                    //ItemMaster();
                    //ddlEmployee.SelectedValue = "";
                    CallClientData(cid);
                    CallTempData(sur);
                }
            }
            catch
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    protected void CallTempData(int SurveyID)
    {
        string query = "";
        
        query = "Select SCID.*, IM.ItemName, IM.Unit, SCIH.ProjectStatus from SurveyClientItemDetails SCID, ItemMaster IM, SurveyClientItemHead SCIH where SCID.SurveyID=SCIH.surveyID and SCID.ItemID=IM.ItemID and SCIH.SurveyID="+SurveyID;
        
        SqlCommand cmd = new SqlCommand(query, conn);
        //cmd.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, ClientID.Trim().Length)).Value = ClientID.Trim();
        //cmd.Parameters.Add(new SqlParameter("@SurveyEngineerID", SqlDbType.NVarChar, Session["userID"].ToString().Trim().Length)).Value = Session["userID"].ToString().Trim();
        SqlDataAdapter adpt = new SqlDataAdapter();
        adpt.SelectCommand = cmd;
        DataTable dt = new DataTable();
        DataColumn c = new DataColumn("sno", typeof(int));
        c.AutoIncrement = true;
        c.AutoIncrementSeed = 1;
        c.AutoIncrementStep = 1;
        dt.Columns.Add(c);
        adpt.Fill(dt);
        if (SurveyID > 0)
        {
            if (dt.Rows.Count > 0)
            {
                if ((bool)dt.Rows[dt.Rows.Count - 1]["ProjectStatus"] == true)
                {
                    gvitemdisplay.DataSource = dt;
                    gvitemdisplay.DataBind();
                    txtClientName.Enabled = false;
                }
                else
                {
                    gvFeed.DataSource = dt;
                    gvFeed.DataBind();
                    txtClientName.Enabled = true;
                }
            }
        }
        //else
        //{
        //    gvFeed.DataSource = dt;
        //    gvFeed.DataBind();
        //    txtClientName.Enabled = true;
        //    txtQuantity.Enabled = true;
        //    txtRemark.Enabled = true;
        //    btnSubmit.Enabled = true;

        //    ddlItem.Enabled = true;

        //}
        //if (conn.State == ConnectionState.Closed)
        //    conn.Open();

        //conn.Close();
    }

    protected void CallClientData(string ClientID)
    {
        string query = "Select CM.*, SCIH.SurveyID, SCIH.StartDate from SurveyClientItemHead SCIH, ClientMaster CM, ClientServiceMaster CSM where SCIH.ClientID=CM.ClientID and CM.ClientID=CSM.ClientID and CM.ClientID='" + ClientID + "' and SCIH.SurveyID=" + ViewState["SurveyID"];

        SqlCommand cmd = new SqlCommand(query, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            txtClientName.Text = "Site Survey No." + dr["SurveyID"].ToString().Trim() + " - Start Date: " + DateTime.Parse(dr["StartDate"].ToString().Trim()).ToString("dd-MMM-yyyy") + " for : " + dr["ClientName"].ToString().Trim();
            //ddlEmployee.SelectedValue = dr["SurveyEngineerID"].ToString().Trim();
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
   
    protected void gvFeed_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = Convert.ToInt32(gvFeed.DataKeys[e.RowIndex].Value);
        SqlCommand cmd = new SqlCommand("Delete from SurveyClientItemDetails where ID=" + ID, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        cmd.ExecuteNonQuery();
        CallTempData(int.Parse(ViewState["SurveyID"].ToString()));
    }

    protected void btnFinishSurvey_Click(object sender, EventArgs e)
    {

        int surveyID = int.Parse(ViewState["SurveyID"].ToString());
        //string startDate = "", finishDate = "";
        //SqlCommand cmdfind = new SqlCommand("Select min(checkedDate) StartDate, max(CheckedDate) FinishDate from SurveyClientItemDetails where SurveyID=0 and clientID=@clientID and SurveyEngineerID=@SurveyEngineerID", conn);
        //cmdfind.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, Session["cid"].ToString().Trim().Length)).Value = Session["cid"].ToString().Trim();
        //cmdfind.Parameters.Add(new SqlParameter("@SurveyEngineerID", SqlDbType.NVarChar, Session["userID"].ToString().Trim().Length)).Value = Session["userID"].ToString().Trim();
        //if (conn.State == ConnectionState.Closed)
        //    conn.Open();
        //SqlDataReader dr = cmdfind.ExecuteReader();
        //if (dr.Read())
        //{
        //    startDate = DateTime.Parse(dr["StartDate"].ToString()).ToString("yyyy-MMM-dd");
        //    finishDate = DateTime.Parse(dr["FinishDate"].ToString()).ToString("yyyy-MMM-dd");
        //}
        //dr.Close();

        SqlCommand cmd = new SqlCommand("Update SurveyClientItemHead set ProjectFinishDate=@ProjectFinishDate, Projectstatus=1 where SurveyID=" + surveyID, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        cmd.Parameters.Add(new SqlParameter("@ProjectFinishDate", SqlDbType.DateTime)).Value = DateTime.Now;
        cmd.ExecuteNonQuery();
        conn.Close();
        lblMessage.Text = "Project Finished updated Successfully";
        btnFinishSurvey.Enabled = false;

    }

    protected void gvFeed_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvFeed.EditIndex=e.NewEditIndex;
        CallTempData(int.Parse(ViewState["SurveyID"].ToString()));
    }
    
    protected void gvFeed_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvFeed.EditIndex = -1;
        CallTempData(int.Parse(ViewState["SurveyID"].ToString()));
    }
    
    protected void gvFeed_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int sitemid = Convert.ToInt32(gvFeed.DataKeys[e.RowIndex].Value.ToString());

        GridViewRow row = (GridViewRow)gvFeed.Rows[e.RowIndex];

        TextBox tActual = (TextBox)row.FindControl("txtActualUtilisation");
        TextBox tDate = (TextBox)row.FindControl("txtImplementedDate");
        //Label lqty = (Label)row.FindControl("lblQuantity");
        double ActualQuantity = 0;
        try
        {
            ActualQuantity = double.Parse(tActual.Text);
            //unitcost = totalcost / double.Parse(lqty.Text);
        }
        catch
        {
            lblMessage.Text = "Please Input correct Quantity";
            return;
        }

        DateTime implementDate;
        try
        {
            implementDate =DateTime.Parse(tDate.Text);
            //unitcost = totalcost / double.Parse(lqty.Text);
        }
        catch
        {
            lblMessage.Text = "Please Input correct Date";
            return;
        }
        SqlCommand cmdu = new SqlCommand("Update SurveyClientItemDetails set ActualUtilisation=@ActualUtilisation, UtilizeUpdateDate=@UtilizeUpdateDate where ID=" + sitemid, conn);
        cmdu.Parameters.Add(new SqlParameter("@ActualUtilisation", SqlDbType.Float)).Value = ActualQuantity;
        cmdu.Parameters.Add(new SqlParameter("@UtilizeUpdateDate", SqlDbType.DateTime)).Value = implementDate.ToString("yyyy/MM/dd");
        
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        cmdu.ExecuteNonQuery();
        gvFeed.EditIndex = -1;
        CallTempData(int.Parse(ViewState["SurveyID"].ToString()));
        checkQuotationbalanceItem(int.Parse(ViewState["SurveyID"].ToString()));
    }
    
    protected void checkQuotationbalanceItem(int SurveyID)
    {
        SqlCommand cmd = new SqlCommand("Select Count(*) from SurveyClientItemDetails SCID, SurveyClientItemHead SCIH where SCID.SurveyID=SCIH.surveyID and SCID.ActualUtilisation=0 and SCIH.SurveyID=" + SurveyID, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        int chk=(int)cmd.ExecuteScalar();
        if(chk<=0)
        btnFinishSurvey.Enabled = true;
        else
            btnFinishSurvey.Enabled = false;
    }
}