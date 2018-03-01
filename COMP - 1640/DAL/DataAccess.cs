using COMP___1640.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace COMP___1640.DAL
{
    public class DataAccess
    {
        public static SqlConnection Connect()
        {
            //return new SqlConnection(WebConfigurationManager.AppSettings["ConnectionString"]);
            return new SqlConnection("Data Source=.;Initial Catalog=IdeasCampaignManager;Integrated Security=False;User Id=sa;Password=abc@12345;MultipleActiveResultSets=True");
        }

        public PersonalDetails CheckLogIn(string email, string pass)
        {
            var query = string.Format("SELECT * FROM PersonalDetail WHERE p_Email = '{0}' AND p_Pass = '{1}'", email, pass);
            try
            {
                var conn = Connect();
                conn.Open();
                var cmd = new SqlCommand(query, conn);
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    return new PersonalDetails
                    {
                        Id = int.Parse(reader["p_ID"].ToString()),
                        roleId = int.Parse(reader["r_ID"].ToString()),
                        departmentId = int.Parse(reader["dp_ID"].ToString()),
                        Name = reader["p_Name"].ToString(),
                        Email = reader["p_Email"].ToString(),
                        Pass = reader["p_Pass"].ToString(),
                        Details = reader["p_Detail"].ToString()
                    };
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}