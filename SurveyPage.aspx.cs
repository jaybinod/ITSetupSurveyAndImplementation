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

public partial class SurveyPage : System.Web.UI.Page
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
                        //btnFinishSurvey.Enabled = false;
                    }
                    catch
                    {
                        Response.Redirect("HomeSurvey.aspx");
                    }

                    int sur = int.Parse(ViewState["SurveyID"].ToString());

                    GroupMaster();
                    ItemMaster();

                    //ddlEmployee.SelectedValue = "";
                    CallClientData(int.Parse(ViewState["SurveyID"].ToString()));
                    CallTempData(cid,sur);
                }
            }
            catch
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    protected void ItemMaster()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("Select * from ItemMaster where groupid="+listGroup.SelectedValue, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        conn.Close();
        dt.Rows.Add();
        dt.Rows[dt.Rows.Count - 1]["ItemID"] = "0";
        dt.Rows[dt.Rows.Count - 1]["ItemNameWithPartNumber"] = "(Select Item)";
        DataView dv = dt.DefaultView;
        dv.Sort = "ItemNameWithPartNumber asc";
        DataTable sortedDT = dv.ToTable();

        ddlItem.DataSource = sortedDT;
        ddlItem.DataBind();
    }

    protected void GroupMaster()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("Select * from ItemGroupMaster order by groupname", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        conn.Close();
        
        listGroup.DataSource = dt;
        listGroup.DataBind();
    }

    protected void CallTempData(string ClientID, int SurveyID)
    {
        string query = "";

        query = "Select SCID.*, IM.ItemName, IM.Unit, SCIH.SubmittedForQuotation, SCIH.quotationdone, IGM.GroupName from SurveyClientItemDetails SCID, ItemMaster IM, SurveyClientItemHead SCIH,ItemGroupMaster IGM where SCID.SurveyID=SCIH.surveyID and SCID.ClientID=@ClientID and SCID.ItemID=IM.ItemID and IM.GroupID=IGM.GroupID and SCID.SurveyEngineerID=@SurveyEngineerID and SCIH.SurveyID=" + SurveyID;
        
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, ClientID.Trim().Length)).Value = ClientID.Trim();
        cmd.Parameters.Add(new SqlParameter("@SurveyEngineerID", SqlDbType.NVarChar, Session["userID"].ToString().Trim().Length)).Value = Session["userID"].ToString().Trim();
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
                if ((bool)dt.Rows[dt.Rows.Count - 1]["SubmittedForQuotation"] == true)
                {
                    btnFinishSurvey.Enabled = false;
                }
                else
                {
                    btnFinishSurvey.Enabled = true;
                }
                if ((bool)dt.Rows[dt.Rows.Count - 1]["quotationdone"] == true)
                {
                    gvitemdisplay.DataSource = dt;
                    gvitemdisplay.DataBind();
                    txtClientName.Enabled = false;
                    txtQuantity.Enabled = false;
                    txtRemark.Enabled = false;
                    btnSubmit.Enabled = false;
                    ddlItem.Enabled = false;
                    //btnFinishSurvey.Enabled = false;
                }
                else
                {
                    gvFeed.DataSource = dt;
                    gvFeed.DataBind();
                    txtClientName.Enabled = true;
                    txtQuantity.Enabled = true;
                    txtRemark.Enabled = true;
                    btnSubmit.Enabled = true;
                    //btnFinishSurvey.Enabled = true;
                    ddlItem.Enabled = true;
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

    protected void CallClientData(int SurveyID)
    {
        string query = "Select SCIH.*, CM.ClientName from SurveyClientItemHead SCIH, ClientMaster CM where SCIH.ClientID=CM.ClientID and SCIH.SurveyID=" + SurveyID;

        SqlCommand cmd = new SqlCommand(query, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            txtClientName.Text = "Site Survey No." + dr["SurveyID"].ToString().Trim() + " - Start Date: " + DateTime.Parse(dr["SubmittedDate"].ToString().Trim()).ToString("dd-MMM-yyyy") + " for : " + dr["ClientName"].ToString().Trim();
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
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (ddlItem.Text.Length > 0)
        {
            try
            {
                double qty = double.Parse(txtQuantity.Text);

            }
            catch{
            lblMessage.Text="Please Input correct quantity";
                return;
            }
            if(double.Parse(txtQuantity.Text)>0)
            {
                SqlCommand cmdfirst = new SqlCommand("Select count(*) from SurveyClientItemDetails where SurveyID=" + ViewState["SurveyID"], conn);
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                int chkf = (int)cmdfirst.ExecuteScalar();
                if (chkf <= 0)
                {
                    SqlCommand cmdupdate = new SqlCommand("Update SurveyClientItemHead set StartDate=@Startdate where SurveyID=" + ViewState["SurveyID"], conn);
                    cmdupdate.Parameters.Add(new SqlParameter("@Startdate", SqlDbType.Date)).Value = DateTime.Now.ToString("yyyy/MM/dd");
                    cmdupdate.ExecuteNonQuery();
                }
                
            //    SqlCommand cmdc = new SqlCommand("Select count(*) from SurveyClientItemDetails where SurveyID=" + ViewState["SurveyID"] + " and ItemID=@ItemID and ClientID=@ClientID and SurveyEngineerID=@SurveyEngineerID", conn);
            //    cmdc.Parameters.Add(new SqlParameter("@ItemID", SqlDbType.Int)).Value = ddlItem.SelectedValue;
            //    cmdc.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, Session["cid"].ToString().Trim().Length)).Value = Session["cid"].ToString().Trim();
            //    cmdc.Parameters.Add(new SqlParameter("@SurveyEngineerID", SqlDbType.NVarChar, Session["userID"].ToString().Trim().Length)).Value = Session["userID"].ToString().Trim();
            //    if (conn.State == ConnectionState.Closed)
            //        conn.Open();
            //    int chk = (int)cmdc.ExecuteScalar();
            //    if(chk>0)
            //    {
            //        lblMessage.Text = "Item Already added for this Survey, Please delete first to add fresh Entry";
            //        conn.Close();
            //        return;
            //    }
            //    conn.Close();
            ////string EmployeeID = System.Guid.NewGuid().ToString().ToUpper().Trim();
                SqlCommand cmd = new SqlCommand("Insert into SurveyClientItemDetails (SurveyID, ItemID, Quantity,ClientID,SurveyEngineerID, Remark, CheckedDate, changestatus) values(@SurveyID, @ItemID, @Quantity,@ClientID,@SurveyEngineerID,@Remark,@CheckedDate,1)", conn);

                cmd.Parameters.Add(new SqlParameter("@SurveyID", SqlDbType.Int)).Value = int.Parse(ViewState["SurveyID"].ToString());
                cmd.Parameters.Add(new SqlParameter("@ItemID", SqlDbType.Int)).Value = ddlItem.SelectedValue;
                cmd.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Float)).Value = txtQuantity.Text;
                cmd.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, Session["cid"].ToString().Trim().Length)).Value = Session["cid"].ToString().Trim();
                cmd.Parameters.Add(new SqlParameter("@SurveyEngineerID", SqlDbType.NVarChar, Session["userID"].ToString().Trim().Length)).Value = Session["userID"].ToString().Trim();
                cmd.Parameters.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, txtRemark.Text.ToString().Trim().Length)).Value = txtRemark.Text.ToString().Trim();
                cmd.Parameters.Add(new SqlParameter("@CheckedDate", SqlDbType.DateTime)).Value = DateTime.Now.ToString("yyyy/MM/dd");

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            conn.Close();
            lblMessage.Text = "Item Added in cart successfully";
            CallTempData(Session["cid"].ToString(), int.Parse(ViewState["SurveyID"].ToString()));
            btnFinishSurvey.Enabled = true;
            //EmployeeEmail(Session["cid"].ToString(), ddlEmployee.SelectedValue.ToString().Trim());
            //refreshField();
            //}
            //else
            //{
            //    lblMessage.Text = "Dealer already Existing";
            //    //refreshField();
            }
        }
        else
        {
            lblMessage.Text = "Item Name should not be Empty";
        }


    }

    //protected void EmployeeEmail(string ClientID, string EngineerID)
    //{
    //    string mailbody = "";

    //    MailMessage msg = new MailMessage();
    //    msg.BodyEncoding = System.Text.ASCIIEncoding.ASCII;
    //    msg.IsBodyHtml = true;
    //    msg.Priority = MailPriority.Normal;
    //    //msg.To.Add("binod@mosindia.co.in");
    //    //msg.To.Add(txtEmail.Text.Trim());
        
    //    msg.Bcc.Add("jaybinod@gmail.com");
    //    //msg.Bcc.Add("amangoenka@gmail.com");
    //    msg.From = new MailAddress("noreply@infrasol.com");
    //    mailbody = "<html>";
    //    mailbody = mailbody + "<body>";
    //    mailbody = mailbody + "Dear "+ddlEmployee.SelectedItem.Text.Trim()+",<br><br>"+Session["EmployeeName"].ToString()+", has assigned you new client "+txtClientName.Text.Trim()+" for survey.<br><br>Please call on below mentioned number and proceed for survey<br><br>";
    //    //mailbody = mailbody + "Designation : " + txtDesignation.Text.Trim();
    //    //mailbody = mailbody + "<br>Deprtment : " + ddlDepartment.SelectedItem.Text.Trim();
    //    ////mailbody = mailbody + "<br>Address : " + txtAddress.Text.Trim();
    //    //mailbody = mailbody + "<br>City : " + txtCity.Text.Trim();
    //    //mailbody = mailbody + "<br>State : " + txtState.Text.Trim();
    //    //mailbody = mailbody + "<br>Postal Code : " + txtPinCode.Text.Trim();
    //    //mailbody = mailbody + "<br>Mobile : " + txtmobile.Text.Trim();
    //    //mailbody = mailbody + "<br><b>Email/User ID : " + txtEmail.Text.Trim();
    //    //mailbody = mailbody + "<br>Password : " + pwd;
        
    //    mailbody = mailbody + "</b></body></html>";
    //    msg.Subject = "New Project assigned for Survey: "+txtClientName.Text.Trim();
    //    msg.Body = mailbody;

    //    //Smaple 2
    //    //System.Net.NetworkCredential authenticaionInfo = new System.Net.NetworkCredential("binod@mosindia.co.in", "regency");
    //    //SmtpClient SmtpMail = new SmtpClient("smtpout.secureserver.net");
    //    //SmtpMail.UseDefaultCredentials = false;
    //    //SmtpMail.Credentials = authenticaionInfo;


    //    //------------------------------------------------
    //    //Sample 1
    //    // SmtpClient SmtpMail = new SmtpClient("relay-hosting.secureserver.net");
    //    SmtpClient SmtpMail = new SmtpClient("relay-hosting.secureserver.net");
    //    //SmtpClient SmtpMail = new SmtpClient("smtp.socketlabs.com");
    //    //SmtpMail.UseDefaultCredentials = true;
    //    //NetworkCredential NC = new NetworkCredential("jaybinod", "CVtbXIu5RdS0");
    //    //SmtpMail.Credentials = NC;
    //    //--------------------------------------

    //    //SmtpMail.Send(msg);
    //    lblMessage.Text = "Email Confirmation sended to Survey Engineer";
    //    //RefreshData();

    //}

    protected void refreshField()
    {
        //txtEmployeeName.Text = "";
        //txtEmail.Text = "";
        //txtDesignation.Text = "";
        //ddlEmployee.SelectedItem.Text = "(Select Item)";
        //txtCity.Text = "";
        //txtContactPerson.Text = "";
        //txtmobile.Text = "";
        //txtPinCode.Text = "";
        //txtLandline.Text = "";
        //chkAlertNewDealer.Checked = false;
        //chkExistingDealermodification.Checked = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect(ViewState["RefUrl"].ToString());
    }
    protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    {

        SqlCommand cmd = new SqlCommand("Select * from ItemMaster where ItemID=" + ddlItem.SelectedValue, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if(dr.Read())
        {
            //lblUnit.Text = dr["unit"].ToString();
            txtQuantity.Attributes["placeHolder"] = dr["unit"].ToString();

        }
        dr.Close();
        conn.Close();
        txtQuantity.Focus();
    }
    protected void gvFeed_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int ID = Convert.ToInt32(gvFeed.DataKeys[e.RowIndex].Value);
        SqlCommand cmd = new SqlCommand("Delete from SurveyClientItemDetails where ID=" + ID, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        cmd.ExecuteNonQuery();
        CallTempData(Session["cid"].ToString(), int.Parse(ViewState["SurveyID"].ToString()));
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

        SqlCommand cmd = new SqlCommand("Update SurveyClientItemHead set FinishDate=@FinishDate, SubmittedForQuotation=1 where SurveyID=" + surveyID, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        cmd.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, Session["cid"].ToString().Trim().Length)).Value = Session["cid"].ToString().Trim();
        cmd.Parameters.Add(new SqlParameter("@SurveyEngineerID", SqlDbType.NVarChar, Session["userID"].ToString().Trim().Length)).Value = Session["userID"].ToString().Trim();
        cmd.Parameters.Add(new SqlParameter("@FinishDate", SqlDbType.DateTime)).Value = DateTime.Now;
        cmd.ExecuteNonQuery();
        conn.Close();
        lblMessage.Text = "Survey Submitted for Quotation";
        btnFinishSurvey.Enabled = false;

    }

    protected void gvFeed_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvFeed.EditIndex=e.NewEditIndex;
        CallTempData(Session["cid"].ToString(), int.Parse(ViewState["SurveyID"].ToString()));
    }
    
    protected void gvFeed_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvFeed.EditIndex = -1;
        CallTempData(Session["cid"].ToString(), int.Parse(ViewState["SurveyID"].ToString()));
    }
    
    protected void gvFeed_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int sitemid = Convert.ToInt32(gvFeed.DataKeys[e.RowIndex].Value.ToString());

        GridViewRow row = (GridViewRow)gvFeed.Rows[e.RowIndex];
                
        TextBox tqty = (TextBox)row.FindControl("txtQty");
        TextBox tnote = (TextBox)row.FindControl("txtNote");
        try
        {
            double qty = double.Parse(tqty.Text);

        }
        catch
        {
            lblMessage.Text = "Please Input correct quantity";
            return;
        }
        SqlCommand cmdu = new SqlCommand("Update SurveyClientItemDetails set Quantity=@Quantity, Remark=@Remark where ID=" + sitemid, conn);
        cmdu.Parameters.Add(new SqlParameter("@Quantity",SqlDbType.Float)).Value = tqty.Text;
        cmdu.Parameters.Add(new SqlParameter("@Remark",SqlDbType.NVarChar,tnote.Text.Trim().Length)).Value = tnote.Text.Trim();
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        cmdu.ExecuteNonQuery();
        gvFeed.EditIndex = -1;
        CallTempData(Session["cid"].ToString(), int.Parse(ViewState["SurveyID"].ToString()));
        btnFinishSurvey.Enabled = true;
    }
    protected void listGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        ItemMaster();
    }
}