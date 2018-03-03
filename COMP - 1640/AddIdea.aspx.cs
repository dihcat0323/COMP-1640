using COMP___1640.DAL;
using COMP___1640.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COMP___1640
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                var script = "alert(\"ERROR: You must Login first!!!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadCategories();
            }
        }

        private void LoadCategories()
        {
            var lstCat = new DataAccess().GetAllCategory();
            if (lstCat != null && lstCat.Count > 0)
            {
                foreach (var x in lstCat)
                {
                    var item = new ListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    };
                    ddlCatgeory.Items.Add(item);
                }
            }
        }

        protected void btnAddIdea_Click(object sender, EventArgs e)
        {
            //validation
            var title = txtTitle.Text;
            var content = txtContent.Text;
            //var tags = txtTag.Text;
            if (title.Equals("") || content.Equals(""))
            {
                var script = "alert(\"ERROR: Title and Content are required!!!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return;
            }

            //populate idea to add to DB
            var p = (PersonalDetails)Session["Login"];
            var idea = new Idea
            {
                CategoryId = int.Parse(ddlCatgeory.SelectedItem.Value),
                PersonalId = p.Id,
                Title = title.Replace("'", "\""),
                Details = content.Replace("'", "\""),
                DocumentLink = "", //TODO:
                isAnonymous = ckbAnonymous.Checked ? 1 : 0,
                TotalViews = 0,
                ClosureDate = DateTime.Today, //TODO:
                PostedDate = DateTime.Today//TODO:
            };

            var id = new DataAccess().AddIdea(idea);

            if (id > 0)
            {
                var script = "alert(\"SUCCESS: Idea submitted successfully!!!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);

                Session["IdeaId"] = id;

                Response.Redirect("Post.aspx");
            }
            else
            {
                var script = "alert(\"FAIL: Idea submitted failed!!!\");";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", script, true);
                return;
            }


        }
    }
}