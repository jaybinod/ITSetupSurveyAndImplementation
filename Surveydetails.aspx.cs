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

public partial class Surveydetails : System.Web.UI.Page
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
                
                if ((bool)dt.Rows[dt.Rows.Count - 1]["quotationdone"] == true)
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
    //protected void btnSubmit_Click(object sender, EventArgs e)
    //{
    //    if (ddlItem.Text.Length > 0)
    //    {
    //        try
    //        {
    //            double qty = double.Parse(txtQuantity.Text);

    //        }
    //        catch{
    //        lblMessage.Text="Please Input correct quantity";
    //            return;
    //        }
    //        if(double.Parse(txtQuantity.Text)>0)
    //        {
    //            SqlCommand cmdfirst = new SqlCommand("Select count(*) from SurveyClientItemDetails where SurveyID=" + ViewState["SurveyID"], conn);
    //            if (conn.State == ConnectionState.Closed)
    //                conn.Open();
    //            int chkf = (int)cmdfirst.ExecuteScalar();
    //            if (chkf <= 0)
    //            {
    //                SqlCommand cmdupdate = new SqlCommand("Update SurveyClientItemHead set StartDate=@Startdate where SurveyID=" + ViewState["SurveyID"], conn);
    //                cmdupdate.Parameters.Add(new SqlParameter("@Startdate", SqlDbType.Date)).Value = DateTime.Now.ToString("yyyy/MM/dd");
    //                cmdupdate.ExecuteNonQuery();
    //            }
                
    //        //    SqlCommand cmdc = new SqlCommand("Select count(*) from SurveyClientItemDetails where SurveyID=" + ViewState["SurveyID"] + " and ItemID=@ItemID and ClientID=@ClientID and SurveyEngineerID=@SurveyEngineerID", conn);
    //        //    cmdc.Parameters.Add(new SqlParameter("@ItemID", SqlDbType.Int)).Value = ddlItem.SelectedValue;
    //        //    cmdc.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, Session["cid"].ToString().Trim().Length)).Value = Session["cid"].ToString().Trim();
    //        //    cmdc.Parameters.Add(new SqlParameter("@SurveyEngineerID", SqlDbType.NVarChar, Session["userID"].ToString().Trim().Length)).Value = Session["userID"].ToString().Trim();
    //        //    if (conn.State == ConnectionState.Closed)
    //        //        conn.Open();
    //        //    int chk = (int)cmdc.ExecuteScalar();
    //        //    if(chk>0)
    //        //    {
    //        //        lblMessage.Text = "Item Already added for this Survey, Please delete first to add fresh Entry";
    //        //        conn.Close();
    //        //        return;
    //        //    }
    //        //    conn.Close();
    //        ////string EmployeeID = System.Guid.NewGuid().ToString().ToUpper().Trim();
    //            SqlCommand cmd = new SqlCommand("Insert into SurveyClientItemDetails (SurveyID, ItemID, Quantity,ClientID,SurveyEngineerID, Remark, CheckedDate, changestatus) values(@SurveyID, @ItemID, @Quantity,@ClientID,@SurveyEngineerID,@Remark,@CheckedDate,1)", conn);

    //            cmd.Parameters.Add(new SqlParameter("@SurveyID", SqlDbType.Int)).Value = int.Parse(ViewState["SurveyID"].ToString());
    //            cmd.Parameters.Add(new SqlParameter("@ItemID", SqlDbType.Int)).Value = ddlItem.SelectedValue;
    //            cmd.Parameters.Add(new SqlParameter("@Quantity", SqlDbType.Float)).Value = txtQuantity.Text;
    //            cmd.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, Session["cid"].ToString().Trim().Length)).Value = Session["cid"].ToString().Trim();
    //            cmd.Parameters.Add(new SqlParameter("@SurveyEngineerID", SqlDbType.NVarChar, Session["userID"].ToString().Trim().Length)).Value = Session["userID"].ToString().Trim();
    //            cmd.Parameters.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, txtRemark.Text.ToString().Trim().Length)).Value = txtRemark.Text.ToString().Trim();
    //            cmd.Parameters.Add(new SqlParameter("@CheckedDate", SqlDbType.DateTime)).Value = DateTime.Now.ToString("yyyy/MM/dd");

    //        if (conn.State == ConnectionState.Closed)
    //            conn.Open();
    //        cmd.ExecuteNonQuery();
    //        cmd.Parameters.Clear();

    //        conn.Close();
    //        lblMessage.Text = "Item Added in cart successfully";
    //        CallTempData(Session["cid"].ToString(), int.Parse(ViewState["SurveyID"].ToString()));
    //        btnFinishSurvey.Enabled = true;
    //        //EmployeeEmail(Session["cid"].ToString(), ddlEmployee.SelectedValue.ToString().Trim());
    //        //refreshField();
    //        //}
    //        //else
    //        //{
    //        //    lblMessage.Text = "Dealer already Existing";
    //        //    //refreshField();
    //        }
    //    }
    //    else
    //    {
    //        lblMessage.Text = "Item Name should not be Empty";
    //    }


    //}

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
    //protected void ddlItem_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    SqlCommand cmd = new SqlCommand("Select * from ItemMaster where ItemID=" + ddlItem.SelectedValue, conn);
    //    if (conn.State == ConnectionState.Closed)
    //        conn.Open();
    //    SqlDataReader dr = cmd.ExecuteReader();
    //    if(dr.Read())
    //    {
    //        //lblUnit.Text = dr["unit"].ToString();
    //        txtQuantity.Attributes["placeHolder"] = dr["unit"].ToString();

    //    }
    //    dr.Close();
    //    conn.Close();
    //    txtQuantity.Focus();

}