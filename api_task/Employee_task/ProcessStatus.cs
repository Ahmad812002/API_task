using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using System.Web;
using System.Configuration;
using Newtonsoft.Json.Linq;
//Controller

namespace API_task.Employee_task
{
    //[Route("api/my/customendpoint")]
    public class ProcessStatus : ApiController
    {
        public DataAccess data_access = new DataAccess();
        public EmpInfo employee = new EmpInfo();
        public JObject obj = new JObject();


        public JObject Process_response(long National_number)
        {
            
            var employee_inforamtion = Get_employee_info(National_number);
            var Is_Active = Check_activity(employee_inforamtion);

            if (employee_inforamtion.national_number > 0)
            {
                if (Is_Active)
                {
                    final(employee_inforamtion.ID);
                    return obj;
                }
                else
                {
                    obj["status"] = "This employee is not active";
                    return obj;
                }

            }
            else
            {
                JObject obj_message = new JObject();
                obj_message["message"] = "the national number is not exist";
                return obj_message;
            }
            
        }


        //done
        public bool Check_activity(EmpInfo employee)
        {
            if (employee.is_active == true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        [HttpGet]

        //done
        public EmpInfo Get_employee_info(long national_number)
        {


            EmpInfo employee_info = data_access.Get_employee_info(national_number);
            return employee_info;
        }



        public void final(int user_ID)
        {
            List<float> Salaries = new List<float>();

            Salaries = data_access.Get_salaries(user_ID);


            float avg = 0;
            foreach (float item in Salaries)
            {
                avg += item;
            }
            avg /= Salaries.Count();

            Salaries.ToArray();


            float highest = 0;

            if (Salaries.Count() == 1)
            {
                highest = Salaries[0];
            }
            else
            {
                for (var i = 0; i < Salaries.Count() - 1; i++)
                {
                    if (Salaries[i] > highest)
                        highest = Salaries[i];
                }
            }
            obj["highest"] = highest;
            obj["avg"] = avg;

            if (avg > 2000) obj["status"] = "Green";

            else if (avg == 2000) obj["status"] = "Orange";

            else obj["status"] = "Red";

            

        }

        //done

        // there is an error in Get_salaries that doesn't reutrn the data correctly 

        //    public JObject Response_json(int user_ID)
        //    {
        //        List<float> Salaries = new List<float>();

        //        Salaries = data_access.Get_salaries(user_ID);

        //        JObject obj = new JObject();


        //        obj["avg"] = 0;
        //        //avg
        //        float avg = 0;
        //        foreach(float item in Salaries)
        //        {
        //            avg += item;
        //        }
        //        obj["avg"] = avg;
        //        avg /= Salaries.Count();

        //        obj["avg"] = avg;

        //        Salaries.ToArray();
        //        //heighist 

        //        float highest = 0;

        //        if(Salaries.Count() == 1)
        //        {
        //            highest = Salaries[0];
        //        }
        //        else
        //        {
        //            for (var i = 0; i < Salaries.Count() - 1; i++)
        //            {
        //                if (Salaries[i] > highest)
        //                    highest = Salaries[i];
        //            }
        //        }


        //        obj["highest"] = highest;

        //        obj["status"] = Check_status(user_ID);

        //        return obj;
        //    }
        //}
    }
}