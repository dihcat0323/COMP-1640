using COMP___1640.DAL;
using COMP___1640.Entity;
using COMP___1640.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace COMP___1640
{
    public partial class Home : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["TopicId"] == null)
            {
                Response.Redirect("Topic.aspx");
            }
            else
            {
                var tpId = -1;
                int.TryParse(Session["TopicId"].ToString(), out tpId);

                if (tpId == -1)
                {
                    Response.Redirect("Topic.aspx");
                }
                else
                {
                    var tp = new DataAccess().GetTopicById(tpId);
                    lblTopicName.Text = tp.Name;
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static IdeaEntity LoadData(int currentPage, int pagesize)
        {
            var db = new DataAccess();
            var ideaEntity = new IdeaEntity();

            var tpId = -1;
            Page obj = new Page();
            int.TryParse(obj.Session["TopicId"].ToString(), out tpId);
            if (tpId == -1)
            {
                return null;
            }

            var lstIdeas = db.GetIdeasByTopic(tpId);
            if (lstIdeas == null || lstIdeas.Count == 0)
            {
                return null;
            }

            var lstIdeaUi = new List<IdeaUI>();
            foreach (var x in lstIdeas)
            {
                var ideaUi = new IdeaUI();
                ideaUi.ideaId = x.Id;
                ideaUi.userName = db.GetUserById(x.PersonalId).Name;
                ideaUi.ideaTitle = x.Title.Replace("\"", "'").Replace(Environment.NewLine, " ");
                ideaUi.ideaContent = x.Details.Replace("\"", "'").Replace(Environment.NewLine, " ");
                ideaUi.postedDate = new Common().CalculateDateRange(x.PostedDate);

                lstIdeaUi.Add(ideaUi);
            }

            var total = lstIdeas.Count;
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

            ideaEntity.ListIdeas = lstIdeaUi.Skip(currentPage * pagesize).Take(pagesize).ToList();
            ideaEntity.TotalPage = totalPage;

            return ideaEntity;
        }

        [System.Web.Services.WebMethod]
        public static object IdeaClicked(int ideaId)
        {
            Page obj = new Page();
            obj.Session["IdeaId"] = ideaId;

            return null;
        }
    }
}