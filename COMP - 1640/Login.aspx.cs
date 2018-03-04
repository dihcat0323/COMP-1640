using COMP___1640.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COMP___1640
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] != null)
            {
                Response.Redirect("Home.aspx");
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
                Response.Redirect("Home.aspx");
            }
            else
            {
                var script = "alert(\"ERROR: email or password invalid!!!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
            }
        }
    }
}