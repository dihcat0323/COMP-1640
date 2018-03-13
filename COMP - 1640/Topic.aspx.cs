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
    public partial class WebForm4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [System.Web.Services.WebMethod]
        public static TopicEntity LoadData(int currentPage, int pagesize)
        {
            var db = new DataAccess();
            var TopicEntity = new TopicEntity();

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

            TopicEntity.ListTopics = lstTopicUi.Skip(currentPage * pagesize).Take(pagesize).ToList();
            TopicEntity.TotalPage = totalPage;

            return TopicEntity;
        }

        [System.Web.Services.WebMethod]
        public static object TopicClicked(int TopicId)
        {
            Page obj = new Page();
            obj.Session["TopicId"] = TopicId;



            return null;
        }
    }
}