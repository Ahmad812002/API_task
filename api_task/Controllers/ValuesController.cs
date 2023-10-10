using API_task.Employee_task;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API_task.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet]
        [Route("api/Values/{National_number}")]
        //public JObject Get(long National_number)
        //{
        //    ProcessStatus process = new ProcessStatus();
        //    var result = process.Process_response(National_number.ToString());
        //    if (National_number == 0) // Adjust this condition as needed
        //    {
        //        return new JObject(
        //            new JProperty("Error", "the national number is invalid")
        //            );
        //    }

        //    return result;
        //}

        public JObject Get(string National_number)
        {
            if (!string.IsNullOrWhiteSpace(National_number) && !long.TryParse(National_number, out _))
            {
                return new JObject(new JProperty("Error", "The national number is not a valid numeric value."));
            }

            ProcessStatus process = new ProcessStatus();
            var result = process.Process_response(National_number);

            if (National_number == "0") // Adjust this condition as needed
            {
                return new JObject(new JProperty("Error", "The national number is invalid"));
            }

            return result;
        }


        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
