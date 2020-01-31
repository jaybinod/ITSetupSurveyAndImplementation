using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class MarketingTab : System.Web.UI.UserControl
{
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);

    protected void Page_Load(object sender, EventArgs e)
    {
        GetStatus();
    }

    protected void GetStatus()
    {
        SqlCommand cmd = new SqlCommand("Select count(*) from surveyclientItemHead SCIH, Clientmaster CM where SCIH.ClientID=CM.ClientID and SCIH.Projectstatus=0", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        int wip = (int)cmd.ExecuteScalar();
        lblwip.Text = wip.ToString();
        conn.Close();

        cmd = new SqlCommand("Select count(*) from surveyclientItemHead SCIH, Clientmaster CM where SCIH.ClientID=CM.ClientID and SCIH.Projectstatus=1", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        int complete = (int)cmd.ExecuteScalar();
        lblCompleted.Text = complete.ToString();
        conn.Close();

        SqlCommand cmdc = new SqlCommand("Select count(*) from Clientmaster CM", conn);
        if (conn.State == ConnectionState.Closed)
            conn.Open();
        int loc = (int)cmdc.ExecuteScalar();
        lblLoc.Text = loc.ToString();
        conn.Close();
    }
}