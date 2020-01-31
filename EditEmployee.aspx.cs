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
    //protected override void OnPreInit(EventArgs e)
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
        if (!IsPostBack)
        {
            try
            {
                if ((bool)Session["Validate"] == true)
                {
                    string eid=Request.QueryString["id"].ToString();
                    Session["eid"] = eid;
                    Department();
                    CallData(eid);
                }
            }
            catch
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    protected void Department()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("Select * from Departmentmaster order by department", conn);
        //if (conn.State == ConnectionState.Closed)
        //    conn.Open();
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        conn.Close();
        ddlDepartment.DataSource = dt;
        ddlDepartment.DataBind();
    }

    protected void CallData(string EmployeeID)
    {
        SqlCommand cmd = new SqlCommand("Select * from EmployeeUserMaster where UserID='" + EmployeeID+"'", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if(dr.Read())
        {
            txtEmployeeName.Text = dr["EmployeeName"].ToString().Trim();
            txtDesignation.Text = dr["Designation"].ToString().Trim();
            txtEmail.Text = dr["EmailID"].ToString().Trim();
            ddlDepartment.SelectedValue = dr["DepartmentID"].ToString().Trim();
            txtmobile.Text = dr["Mobile"].ToString().Trim();
            chkActive.Checked = (bool)dr["Active"];
        //    txtEmail.Text = dr["Email"].ToString().Trim();
        //    if((bool)dr["NewDealerAdded"]==true)
        //        ddlNewDealer.Text = "Yes";
        //    else
        //        ddlNewDealer.Text = "No";
        //    if ((bool)dr["ExistingDealerModification"] == true)
        //        ddlExistingModification.Text = "Yes";
        //    else
        //        ddlExistingModification.Text = "No";

        }
        conn.Close();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("dashboard.aspx");
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool email = false;
        try
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(txtEmail.Text);
            email = true;
        }
        catch {
            email = false;
        }
        if (email == true)
        {
            SqlCommand fmdfinde = new SqlCommand("Select count(*) from EmployeeUserMaster where upper(ltrim(rtrim(EmailID)))=@Email and UserID!='" + Session["eid"] + "'", conn);
            fmdfinde.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, txtEmail.Text.Trim().Length)).Value = txtEmail.Text.Trim().ToUpper();

            if (conn.State == ConnectionState.Closed)
                conn.Open();
            int chkd = (int)fmdfinde.ExecuteScalar();
            conn.Close();
            if (chkd > 0)
            {
                lblMessage.Text = "Mentioned Email ID already registered for another user";
            }
            else
            {
                if (txtEmployeeName.Text.Length > 0)
                {
                    
                        string EmployeeID = System.Guid.NewGuid().ToString().ToUpper().Trim();
                        SqlCommand cmd = new SqlCommand("update [dbo].[EmployeeUserMaster] set [EmployeeName]=@EmployeeName, [Designation]=@Designation, [DepartmentID]=@DepartmentID, [UserType]=@UserType, [UserName]=@UserName, [EmailID]=@Email, [Mobile]=@Mobile, [Active]=@Active where UserID='"+Session["eid"]+"'", conn);
                        
                        //cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.NVarChar, EmployeeID.Trim().Length)).Value = EmployeeID.Trim();
                        cmd.Parameters.Add(new SqlParameter("@EmployeeName", SqlDbType.NVarChar, txtEmployeeName.Text.Trim().Length)).Value = txtEmployeeName.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Designation", SqlDbType.NVarChar, txtDesignation.Text.Trim().Length)).Value = txtDesignation.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@DepartmentID", SqlDbType.Int)).Value = ddlDepartment.SelectedValue;
                        cmd.Parameters.Add(new SqlParameter("@UserType", SqlDbType.NVarChar, 4)).Value = "User";
                        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, txtEmail.Text.Trim().Length)).Value = txtEmail.Text.Trim();
                        //cmd.Parameters.Add(new SqlParameter("@password", SqlDbType.NVarChar, 8)).Value = EmployeeID.Substring(0,8);
                        //cmd.Parameters.Add(new SqlParameter("@Contact", SqlDbType.NVarChar, txtContactPerson.Text.Trim().Length)).Value = txtContactPerson.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Mobile", SqlDbType.NVarChar, txtmobile.Text.Trim().Length)).Value = txtmobile.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, txtEmail.Text.Trim().Length)).Value = txtEmail.Text.Trim();
                        cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.Bit)).Value = chkActive.Checked;    
                    
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();    
                    cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        conn.Close();
                        lblMessage.Text = "Employee Updated sucessfully";
                        //EmployeeEmail(EmployeeID.Substring(0, 8));
                        refreshField();
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
        }
        else
        {
            lblMessage.Text = "Please enter valid email address";
        }
        //Response.Redirect("dashboard.aspx");
    }

    protected void EmployeeEmail(string pwd)
    {
        string mailbody = "";

        MailMessage msg = new MailMessage();
        msg.BodyEncoding = System.Text.ASCIIEncoding.ASCII;
        msg.IsBodyHtml = true;
        msg.Priority = MailPriority.Normal;
        //msg.To.Add("binod@mosindia.co.in");
        msg.To.Add(txtEmail.Text.Trim());
        
        msg.Bcc.Add("jaybinod@gmail.com");
        //msg.Bcc.Add("amangoenka@gmail.com");
        msg.From = new MailAddress("noreply@infrasol.com");
        mailbody = "<html>";
        mailbody = mailbody + "<body>";
        mailbody = mailbody + "Dear "+txtEmployeeName.Text.Trim()+",<br><br>Your Details are as under with login ID and password<br><br>";
        mailbody = mailbody + "Designation : " + txtDesignation.Text.Trim();
        mailbody = mailbody + "<br>Deprtment : " + ddlDepartment.SelectedItem.Text.Trim();
        //mailbody = mailbody + "<br>Address : " + txtAddress.Text.Trim();
        //mailbody = mailbody + "<br>City : " + txtCity.Text.Trim();
        //mailbody = mailbody + "<br>State : " + txtState.Text.Trim();
        //mailbody = mailbody + "<br>Postal Code : " + txtPinCode.Text.Trim();
        mailbody = mailbody + "<br>Mobile : " + txtmobile.Text.Trim();
        mailbody = mailbody + "<br><b>Email/User ID : " + txtEmail.Text.Trim();
        mailbody = mailbody + "<br>Password : " + pwd;
        
        mailbody = mailbody + "</b></body></html>";
        msg.Subject = "Login Details";
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
        lblMessage.Text = "Registration Confirmation sended to Employee Email Address";
        //RefreshData();

    }

    protected void refreshField()
    {
        txtEmployeeName.Text = "";
        txtEmail.Text = "";
        txtDesignation.Text = "";
        ddlDepartment.SelectedItem.Text = "(Select)";
        //txtCity.Text = "";
        //txtContactPerson.Text = "";
        txtmobile.Text = "";
        //txtPinCode.Text = "";
        //txtLandline.Text = "";
        //chkAlertNewDealer.Checked = false;
        //chkExistingDealermodification.Checked = false;
    }
}