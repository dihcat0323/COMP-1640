using COMP___1640.DAL;
using System;
using System.Web.UI;

namespace COMP___1640
{
    public partial class WebForm1 : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                Response.Redirect("Topic.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Text;
            if (email.Equals("") || password.Equals(""))
            {
                var script = "alert(\"ERROR: Both email and password are required fields!!!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return;
            }

            var da = new DataAccess();
            var p = da.CheckLogIn(email, password);

            if (p != null)
            {
                //save account info to session
                Session["Login"] = p;
                
                //redirect to Home
                Response.Redirect("Topic.aspx");
            }
            else
            {
                var script = "alert(\"ERROR: Email or password invalid!!!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }
    }
}
