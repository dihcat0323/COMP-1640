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
    public partial class WebForm9 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                //Check if account is Admin category
                var user = (PersonalDetails)Session["Login"];
                var r = new DataAccess().GetRoleById(user.roleId);

                if (r.Name.Contains("Manager") && r.Name.Contains("QA"))
                {
                    AnonymousPercentage();
                }
                else
                {
                    Response.Redirect("Topic.aspx");
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static List<TopicUI> TotalIdeasPerTopic()
        {
            var lstTp = new List<TopicUI>();
            var topics = new DataAccess().GetAllTopics();

            foreach (var x in topics)
            {
                var tp = new TopicUI();
                tp.Name = x.Name;
                tp.TotalIdeas = new DataAccess().CountIdeasByTopic(x.Id);
                double totalIdeas = new DataAccess().TotalIdeas();
                double topicIdeas = tp.TotalIdeas;
                tp.Percentages = ((topicIdeas / totalIdeas) * 100).ToString();
                tp.Percentages = tp.Percentages.Length > 5 ? tp.Percentages.Substring(0, 5) : tp.Percentages;
                lstTp.Add(tp);
            }

            return lstTp;
        }

        [System.Web.Services.WebMethod]
        public static List<Department> DepartmentStatistics()
        {
            var lstDep = new DataAccess().DepartmentStatistics();

            if (lstDep != null)
            {
                for (int i = 0; i < lstDep.Count; i++)
                {
                    if (lstDep[i].TotalIdeas == null)
                        lstDep[i].TotalIdeas = "0";
                    if (lstDep[i].IdeaPercentages == null)
                        lstDep[i].IdeaPercentages = "0";
                }

                return lstDep;
            }

            return null;
        }

        public void AnonymousPercentage()
        {
            var anonymous = new DataAccess().AnonymousPercentages();
            if (anonymous.Count == 3)
            {
                var totalIdeas = new DataAccess().TotalIdeas();
                lblTotalIdeas.Text = totalIdeas.ToString();
                lblTotalCmts.Text = new DataAccess().TotalCmts().ToString();
                lblIdeas.Text = anonymous[0] + " %";
                lblCmts.Text = anonymous[1] + " %";
                lblIdeas2.Text = anonymous[2] + " %";
            }
        }
    }
}