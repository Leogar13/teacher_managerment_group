using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TeacherManagementSystem.Models
{
	public class BoMon
	{
		public List<string> boMon = new List<string>();
		public BoMon()
		{
			var cn = ConfigurationManager.ConnectionStrings["Teacher"].ConnectionString;
			if (boMon.Count > 0)
			{
				boMon.Clear();
			}
			using (var c = new SqlConnection(cn))
			{
				c.Open();
				var TenKhoaQuery = "select TenBoMon from BoMon";
				SqlCommand sqlCommand = new SqlCommand(TenKhoaQuery, c);
				SqlDataReader reader = sqlCommand.ExecuteReader();
				if (reader.HasRows)
				{
					while (reader.Read())
					{
						boMon.Add(reader.GetString(0));
					}
				}
				reader.Close();
			}
		}

	}
}