using COMP___1640.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace COMP___1640.Entity
{
    public class UserEntity
    {
        public int TotalPage { get; set; }
        public List<UserUI> ListUsers { get; set; }

        public List<Role> ListRoles { get; set; }
        public List<Department> ListDepartment { get; set; }
    }
}