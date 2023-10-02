//using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace API_task.Employee_task
{
    public class DataAccess
    {

        private readonly string _connection;
        public EmpInfo employee { get; private set; }
        public DataAccess()
        {
            _connection = ConfigurationManager.ConnectionStrings["databaseConnection"].ConnectionString;
        }


        [HttpGet]

        //done
        public List<float> Get_salaries(int user_ID)
        {

            List<float> salaries_list = new List<float>();

            using (SqlConnection connection = new SqlConnection(_connection))
            {
                connection.Open();
                string query = "SELECT salary From Salaries WHERE user_ID = @user_ID";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@user_ID", user_ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal salary = reader.GetDecimal(reader.GetOrdinal("salary"));
                            salaries_list.Add((float)salary);
                        }
                    }
                }
            }

            return salaries_list;
        }





        public EmpInfo Get_employee_info(long national_number)
        {
            EmpInfo employee = null;

            using (SqlConnection sql_connecetion = new SqlConnection(_connection))
            {
                sql_connecetion.Open();

                string query = "SELECT * FROM Users WHERE national_number = @national_number";

                using (SqlCommand command = new SqlCommand(query, sql_connecetion))
                {
                    command.Parameters.AddWithValue("@national_number", national_number);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employee = new EmpInfo
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                user_name = reader["user_name"].ToString(),
                                email = reader["email"].ToString(),
                                phone = reader["phone"].ToString(),
                                is_active = reader.GetBoolean(reader.GetOrdinal("is_active")),
                                national_number = Convert.ToInt64(reader["national_number"])
                            };
                        }
                    }
                }

                // Check if employee is still null (not found in the database)
                if (employee == null)
                {
                    return new EmpInfo
                    {
                        user_name = "Employee not found for the provided national number",
                        email = "",
                        phone = "",
                        ID = 0,
                        status="",
                        is_active = false,
                        national_number = 0
                    };
                }
            }

            return employee;
        }



        //public List<int> Get_employee_nationtal_number()
        //{
        //    List<int> employee_national_number_list = new List<int>();
        //    try
        //    {
        //        using (SqlConnection sql_connecetion = new SqlConnection(_connection))
        //        {
        //            sql_connecetion.Open();
        //            string query = "SELECT * FROM Users WHERE national_number = @receivedNationalNumber";

        //            using (SqlCommand command = new SqlCommand(query, sql_connecetion))
        //            {
        //                using (SqlDataReader reader = command.ExecuteReader())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        EmpInfo employee = new EmpInfo
        //                        {
        //                            national_number = Convert.ToInt32(reader["National Number"])
        //                        };
        //                        employee_national_number_list.Add(employee.national_number);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (SqlException)
        //    {

        //    }
        //    catch (Exception)
        //    {

        //    }

        //    return employee_national_number_list;
        //}



        //public List<EmpInfo> Get_employee_info_by_nationtal_number(int national_number)
        //{
        //    List<EmpInfo> employee_info_by_national_number = new List<EmpInfo>();
        //    using (SqlConnection sql_connecetion = new SqlConnection(_connection))
        //    {
        //        sql_connecetion.Open();
        //        string query = "SELECT * NationalNumber FROM Users WHERE NationaNumber = @receivedNationalNumber ";

        //        using (SqlCommand command = new SqlCommand(query, sql_connecetion))
        //        {
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                if (reader.Read())
        //                {
        //                    while (reader.Read())
        //                    {
        //                        EmpInfo employee = new EmpInfo
        //                        {
        //                            ID = Convert.ToInt32(reader["ID"]),
        //                            user_name = reader["User Name"].ToString(),
        //                            email = reader["Email"].ToString(),
        //                            phone = reader["Phone Number"].ToString(),
        //                            is_active = reader.GetBoolean(reader.GetOrdinal("is Active")),
        //                            national_number = Convert.ToInt32(reader["National Number"])
        //                        };
        //                        employee_info_by_national_number.Add(employee);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return employee_info_by_national_number;
        //}
    }
}