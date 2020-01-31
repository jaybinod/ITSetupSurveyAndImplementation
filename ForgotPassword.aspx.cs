using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net.Mail;

public partial class ForgotPassword : System.Web.UI.Page
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["connectionString"]);
    //DataClass DB = new DataClass();
    //PPClass ppc = new PPClass();
    //protected override void OnPreInit(EventArgs e)
    //{
    //    this.MasterPageFile = "~/" + Session["MasterPage"].ToString();
    //    base.OnPreInit(e);
    //} 
    //protected override void OnPreInit(EventArgs e)
    //{
    //    try
    //    {
    //        if (Session["userType"].ToString().Trim() == "Admin")
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
        
    }
    protected void ChangePasswordPushButton_Click(object sender, EventArgs e)
    {
        string userID = "";
        string password = "";
        bool check = IsValidEmailAddress(txtuserEmail.Text.Trim());
        if (check == true)
        {
            SqlCommand cmd = new SqlCommand("Select * from Admin where ltrim(rtrim(email))=@Email", conn);
            cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, txtuserEmail.Text.Trim().Length)).Value = txtuserEmail.Text.Trim();
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                userID = dr["user_name"].ToString().Trim();
                password = dr["password"].ToString().Trim();
            }
            dr.Close();

            string mailbody = "";

            MailMessage msg = new MailMessage();
            msg.BodyEncoding = System.Text.ASCIIEncoding.ASCII;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            //msg.To.Add("binod@mosindia.co.in");
            msg.To.Add(txtuserEmail.Text.Trim());
            //msg.To.Add("shubhangi.shirke@mosindia.biz");
            msg.Bcc.Add("jaybinod@gmail.com");
            msg.Bcc.Add("amangoenka@gmail.com");
            msg.From = new MailAddress("noreply@aifa.com");
            mailbody = "<html>";
            mailbody = mailbody + "<body>";
            mailbody = mailbody + "Dear Sir/Madam,<br><br>Thank you for using forgot user ID/Password service on AIFA.Com. Your login Details are as under<br><br>";
            mailbody = mailbody + "<br><b>Email/User ID : " + userID;
            mailbody = mailbody + "<br>Password : " + password;

            mailbody = mailbody + "</b></body></html>";
            msg.Subject = "Aifa.com Forgot User Id and Password";
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

            SmtpMail.Send(msg);
            lblMessage.Text = "Registration Confirmation sended to supplier Sucessfully";
            //RefreshData();
        }
    }

    protected void SupplierEmail(string user, string pwd)
    {
        
    }
    public bool IsValidEmailAddress(string email)
    {
        try
        {
            var emailChecked = new System.Net.Mail.MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
