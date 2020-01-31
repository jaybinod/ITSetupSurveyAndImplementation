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

public partial class SurveyVerifyPage : System.Web.UI.Page
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
                    string surveytype = "";
                    try
                    {
                        surveytype = Request.QueryString["t"].ToString();
                    }
                    catch
                    {
                        surveytype = "";
                    }
                    if(surveytype=="n")
                    {
                        string query = "Select count(*) from SurveyClientItemHead SCIH where SCIH.quotationdone=0 and SCIH.ClientID='"+Session["cid"]+"' and SCIH.SurveyEngineerID='"+Session["userID"]+"'";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        if (conn.State == ConnectionState.Closed)
                            conn.Open();
                        int chk = (int)cmd.ExecuteScalar();
                        conn.Close();

                        if(chk>0)
                        {
                            lblMessage.Text = "Another Survey already open for this Client. Quotation not yet done, You can update in same Survey.";
                            lblMessage1.Text = "Do you want to create New Survey?";
                        }
                        else
                        {
                            lblMessage.Text = "Do you want to create New Survey?";
                        }
                    }
                }
            }
            catch
            {
                Response.Redirect("default.aspx");
            }
        }
    }

    
    protected void btnNo_Click(object sender, EventArgs e)
    {
            Response.Redirect("HomeSurvey.aspx");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SqlCommand cmdi = new SqlCommand("Insert Into surveyClientItemHead (ClientID,SurveyEngineerID,StartDate,ProjectStatus,QuotationDone) output inserted.SurveyID values(@ClientID,@SurveyEngineerID,@StartDate,0,0)", conn);
        cmdi.Parameters.Add(new SqlParameter("@ClientID", SqlDbType.NVarChar, Session["cid"].ToString().Trim().Length)).Value = Session["cid"].ToString().Trim();
        cmdi.Parameters.Add(new SqlParameter("@SurveyEngineerID", SqlDbType.NVarChar, Session["userID"].ToString().Trim().Length)).Value = Session["userID"].ToString().Trim();
        cmdi.Parameters.Add(new SqlParameter("@StartDate", SqlDbType.DateTime)).Value = DateTime.Now.ToString("yyyy/MM/dd");
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        int surveyid = (int)cmdi.ExecuteScalar();
        conn.Close();
        string url = "SurveyPage.aspx?id=" + Session["cid"] + "&sid=" + surveyid;
        Response.Redirect(url);
    }
}