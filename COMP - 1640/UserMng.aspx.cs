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
    public partial class WebForm8 : System.Web.UI.Page
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
        public static UserEntity LoadData(int currentPage, int pagesize)
        {
            var db = new DataAccess();
            var topicEntity = new UserEntity();

            var lstUsers = db.GetAllUsers();
            lstUsers.Reverse();

            var lstUserUi = new List<UserUI>();
            foreach (var x in lstUsers)
            {
                var u = new UserUI();
                u.Id = x.Id;
                u.roleName = new DataAccess().GetRoleById(x.roleId).Name;
                u.departmentName = new DataAccess().GetDepartmentById(x.departmentId).Name;
                u.Name = x.Name;
                u.Email = x.Email;
                u.Pass = x.Pass;
                u.Details = x.Details;

                lstUserUi.Add(u);
            }

            var total = lstUserUi.Count;
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

            topicEntity.ListUsers = lstUserUi.Skip(currentPage * pagesize).Take(pagesize).ToList();
            topicEntity.ListRoles = new DataAccess().GetAllRoles();
            topicEntity.ListDepartment = new DataAccess().GetAllDepartments();
            topicEntity.TotalPage = totalPage;

            return topicEntity;
        }

        [System.Web.Services.WebMethod]
        public static object UserClicked(int id)
        {
            var u = new DataAccess().GetUserById(id);

            return u;
        }

        [System.Web.Services.WebMethod]
        public static object Client_UpdateUser(int id, int rId, int dpId, string name, string email, string pass, string details)
        {
            var tp = new PersonalDetails
            {
                Id = id,
                roleId = rId,
                departmentId = dpId,
                Name = name,
                Email = email,
                Pass = pass,
                Details = details
            };

            var stt = new DataAccess().UpdateUser(tp);

            return stt;
        }

        [System.Web.Services.WebMethod]
        public static object Client_AddUser(int rId, int dpId, string name, string email, string pass, string details)
        {
            var tp = new PersonalDetails
            {
                roleId = rId,
                departmentId = dpId,
                Name = name,
                Email = email,
                Pass = pass,
                Details = details
            };

            var stt = new DataAccess().AddUser(tp);

            return stt;
        }
    }
}