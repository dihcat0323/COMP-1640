using COMP___1640.DAL;
using COMP___1640.Entity;
using COMP___1640.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace COMP___1640
{
    public partial class WebForm7 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            else
            {
                //Check if account is Admin role
                var user = (PersonalDetails)Session["Login"];
                var role = new DataAccess().GetRoleById(user.roleId);

                if (role.Name.Contains("Admin"))
                {

                }
                else
                {
                    Response.Redirect("Topic.aspx");
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static TopicEntity LoadData(int currentPage, int pagesize)
        {
            var db = new DataAccess();
            var topicEntity = new TopicEntity();

            var lstTopics = db.GetAllTopics();
            lstTopics.Reverse();

            var lstTopicUi = new List<TopicUI>();
            foreach (var x in lstTopics)
            {
                var tp = new TopicUI();
                tp.Id = x.Id;
                tp.Name = x.Name;
                tp.Details = x.Details.Replace("\"", "'").Replace(Environment.NewLine, " ");
                tp.PostedDate = x.PostedDate.ToString();
                tp.ClosureDate = x.ClosureDate.ToString();
                tp.FinalClosureDate = x.FinalClosureDate.ToString();

                lstTopicUi.Add(tp);
            }

            var total = lstTopics.Count;
            int totalPage;
            if (total > pagesize && total % pagesize == 0)
            {
                totalPage = total / pagesize;
            }
            else if (total > pagesize && total % pagesize != 0)
            {
                totalPage = total / pagesize + 1;
            }
            else
            {
                totalPage = 1;
            }

            topicEntity.ListTopics = lstTopicUi.Skip(currentPage * pagesize).Take(pagesize).ToList();
            topicEntity.TotalPage = totalPage;

            return topicEntity;
        }

        [System.Web.Services.WebMethod]
        public static object TopicClicked(int id)
        {
            var tp = new DataAccess().GetTopicById(id);
            var posted = Convert.ToDateTime(tp.PostedDate);
            var closure = Convert.ToDateTime(tp.ClosureDate);
            var final = Convert.ToDateTime(tp.FinalClosureDate);
            var tpUi = new TopicUI
            {

                Id = tp.Id,
                Name = tp.Name,
                Details = tp.Details,
                PostedDate = posted.ToString("yyyy-MM-dd"),
                ClosureDate = closure.ToString("yyyy-MM-dd"),
                FinalClosureDate = final.ToString("yyyy-MM-dd")
            };

            return tpUi;
        }

        private static string ConvertMonth(string m)
        {
            switch (m)
            {
                case "Jan":
                    return "01";
                case "Feb":
                    return "02";
                case "Mar":
                    return "03";
                case "Apr":
                    return "04";
                case "May":
                    return "05";
                case "Jun":
                    return "06";
                case "Jul":
                    return "07";
                case "Aug":
                    return "08";
                case "Sep":
                    return "09";
                case "Oct":
                    return "10";
                case "Nov":
                    return "11";
                case "Dec":
                    return "12";

            }
            return null;
        }

        [System.Web.Services.WebMethod]
        public static object Client_UpdateTopic(int id, string closure, string final)
        {
            var tp = new Topic
            {
                Id = id,
                ClosureDate = DateTime.Parse(closure),
                FinalClosureDate = DateTime.Parse(final)
            };

            var stt = new DataAccess().UpdateTopic(tp);

            return stt;
        }

        [System.Web.Services.WebMethod]
        public static object Client_AddTopic(string name, string details, string closure, string final)
        {
            var tp = new Topic
            {
                Name = name,
                Details = details,
                PostedDate = DateTime.Today,
                ClosureDate = DateTime.Parse(closure),
                FinalClosureDate = DateTime.Parse(final)
            };

            var stt = new DataAccess().AddTopic(tp);

            return stt;
        }
    }
}