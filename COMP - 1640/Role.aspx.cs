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
    public partial class WebForm5 : System.Web.UI.Page
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

                if (role.Name.Contains("Manager") && role.Name.Contains("QA"))
                {

                }
                else
                {
                    Response.Redirect("Topic.aspx");
                }
            }
        }

        [System.Web.Services.WebMethod]
        public static RoleEntity LoadData(int currentPage, int pagesize)
        {
            var db = new DataAccess();
            var roleEntity = new RoleEntity();

            var lstRoles = db.GetAllRoles();
            lstRoles.Reverse();

            var lstRoleUi = new List<Role>();
            foreach (var x in lstRoles)
            {
                var r = new Role();
                r.Id = x.Id;
                r.Name = x.Name;
                r.Description = x.Description.Replace("\"", "'").Replace(Environment.NewLine, " ");

                lstRoleUi.Add(r);
            }
            roleEntity.ListRoles = lstRoleUi;

            return roleEntity;
        }

        [System.Web.Services.WebMethod]
        public static object RoleClicked(int roleId)
        {
            var role = new DataAccess().GetRoleById(roleId);

            return role;
        }

        [System.Web.Services.WebMethod]
        public static object Client_AddRole(string name, string description)
        {
            var role = new Role
            {
                Name = name,
                Description = description
            };

            var stt = new DataAccess().AddRole(role);

            return stt;
        }

        [System.Web.Services.WebMethod]
        public static object Client_UpdateRole(int id, string name, string description)
        {
            var role = new Role
            {
                Id = id,
                Name = name,
                Description = description
            };

            var stt = new DataAccess().UpdateRole(role);

            return stt;
        }
    }
}