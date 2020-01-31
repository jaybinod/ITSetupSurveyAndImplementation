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

public partial class EditEmployee : System.Web.UI.Page
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
                    string sid=Request.QueryString["id"].ToString();
                    Session["sid"] = sid;
                    EngineerEmployee();
                    ddlEmployee.SelectedValue = "";
                    CallData(sid);
                }
            }
            catch
            {
                Response.Redirect("default.aspx");
            }
        }
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
        dt.Rows[dt.Rows.Count - 1]["EmployeeName"] = "(Select)";
        DataView dv = dt.DefaultView;
        dv.Sort = "EmployeeName asc";
        DataTable sortedDT = dv.ToTable();

        ddlEmployee.DataSource = sortedDT;
        ddlEmployee.DataBind();
    }

    protected void CallData(string SurveyID)
    {
        string query = "Select *, (Select EmployeeName from EmployeeUserMaster where UserID=SCIH.SurveyEngineerID) SurveyEngineer from ClientMaster CM, ClientServiceMaster CSM, SurveyClientItemHead SCIH where SCIH.ClientID=CM.ClientID and CM.ClientID=CSM.ClientID and SCIH.SurveyID='" + SurveyID + "'";

        SqlCommand cmd = new SqlCommand(query, conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if(dr.Read())
        {
            txtSurveyID.Text = dr["SurveyID"].ToString();
            txtSubmittedDate.Text = DateTime.Parse(dr["SubmittedDate"].ToString()).ToString("dd-MMM-yyyy");
            txtClientName.Text = dr["ClientName"].ToString().Trim();

            ddlEmployee.SelectedValue = dr["SurveyEngineerID"].ToString().Trim();
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
        if (ddlEmployee.Text.Length > 0)
        {

            //string EmployeeID = System.Guid.NewGuid().ToString().ToUpper().Trim();
            SqlCommand cmd = new SqlCommand("update [dbo].[SurveyClientItemHead] set [SurveyEngineerID]=@SurveyEngineerID where SurveyID=" + Session["sid"], conn);

            cmd.Parameters.Add(new SqlParameter("@SurveyEngineerID", SqlDbType.NVarChar, ddlEmployee.SelectedValue.ToString().Trim().Length)).Value = ddlEmployee.SelectedValue.ToString().Trim();
            
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();

            conn.Close();
            lblMessage.Text = "Project Assigned to Survey Engineer Sucessfully";
            EmployeeEmail(Session["sid"].ToString(),ddlEmployee.SelectedValue.ToString().Trim());
            //refreshField();
            //}
            //else
            //{
            //    lblMessage.Text = "Dealer already Existing";
            //    //refreshField();
            //}
        }
        else
        {
            lblMessage.Text = "Employee Name should not be Empty";
        }


    }

    protected void EmployeeEmail(string ClientID, string EngineerID)
    {
        string mailbody = "";

        MailMessage msg = new MailMessage();
        msg.BodyEncoding = System.Text.ASCIIEncoding.ASCII;
        msg.IsBodyHtml = true;
        msg.Priority = MailPriority.Normal;
        //msg.To.Add("binod@mosindia.co.in");
        //msg.To.Add(txtEmail.Text.Trim());
        
        msg.Bcc.Add("jaybinod@gmail.com");
        //msg.Bcc.Add("amangoenka@gmail.com");
        msg.From = new MailAddress("noreply@infrasol.com");
        mailbody = "<html>";
        mailbody = mailbody + "<body>";
        mailbody = mailbody + "Dear "+ddlEmployee.SelectedItem.Text.Trim()+",<br><br>"+Session["EmployeeName"].ToString()+", has assigned you new client "+txtClientName.Text.Trim()+" for survey.<br><br>Please call on below mentioned number and proceed for survey<br><br>";
        //mailbody = mailbody + "Designation : " + txtDesignation.Text.Trim();
        //mailbody = mailbody + "<br>Deprtment : " + ddlDepartment.SelectedItem.Text.Trim();
        ////mailbody = mailbody + "<br>Address : " + txtAddress.Text.Trim();
        //mailbody = mailbody + "<br>City : " + txtCity.Text.Trim();
        //mailbody = mailbody + "<br>State : " + txtState.Text.Trim();
        //mailbody = mailbody + "<br>Postal Code : " + txtPinCode.Text.Trim();
        //mailbody = mailbody + "<br>Mobile : " + txtmobile.Text.Trim();
        //mailbody = mailbody + "<br><b>Email/User ID : " + txtEmail.Text.Trim();
        //mailbody = mailbody + "<br>Password : " + pwd;
        
        mailbody = mailbody + "</b></body></html>";
        msg.Subject = "New Project assigned for Survey: "+txtClientName.Text.Trim();
        msg.Body = mailbody;

        //Smaple 2
        //System.Net.NetworkCredential authenticaionInfo = new System.Net.NetworkCredential("binod@mosindia.co.in", "regency");
        //SmtpClient SmtpMail = new SmtpClient("smtpout.secureserver.net");
        //SmtpMail.UseDefaultCredentials = false;
        //SmtpMail.Credentials = authenticaionInfo;


        //------------------------------------------------
        //Sample 1
        // SmtpClient SmtpMail = new SmtpClient("relay-hosting.secureserver.net");
        SmtpClient SmtpMail = new SmtpClient("relay-hosting.secureserver.net");
        //SmtpClient SmtpMail = new SmtpClient("smtp.socketlabs.com");
        //SmtpMail.UseDefaultCredentials = true;
        //NetworkCredential NC = new NetworkCredential("jaybinod", "CVtbXIu5RdS0");
        //SmtpMail.Credentials = NC;
        //--------------------------------------

        //SmtpMail.Send(msg);
        lblMessage.Text = "Email Confirmation sended to Survey Engineer";
        //RefreshData();

    }

    protected void refreshField()
    {
        //txtEmployeeName.Text = "";
        //txtEmail.Text = "";
        //txtDesignation.Text = "";
        ddlEmployee.SelectedItem.Text = "(Select)";
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
}