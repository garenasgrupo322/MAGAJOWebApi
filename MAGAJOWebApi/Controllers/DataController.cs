using System;
using System.Collections.Generic;
using System.Data;
using MAGAJOWebApi.Models;
using MAGAJOWebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

namespace MAGAJOWebApi.Controllers {

    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class DataController : Controller
    {
        public IConfiguration _configuration { get; }

        public DataController(IConfiguration configuration) {
            _configuration = configuration;
        }

        public class requi {
            public string parametros { get; set; }
        }

        [HttpPost]
        [Route("{IEntityID}")]
        public ActionResult Post(string IEntityID, [FromBody] object parm)
        {

            string jsonPost = JsonConvert.SerializeObject(parm);
            List<Parameters> parameters = new List<Parameters>();

            Parameters parameter = new Parameters();
            parameter.Name = "@IEntityID";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = IEntityID;
            parameter.Size = -1;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@JSONREQUI";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = jsonPost;
            parameter.Size = -1;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@IDataOutput";
            parameter.Direction = ParameterDirection.InputOutput;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = string.Empty;
            parameter.Size = -1;
            parameters.Add(parameter);

            Utilities utilities = new Utilities(_configuration);

            try
            {
                json = utilities.callSP("SET_DATA_REQUISICION", parameters, "@IDataOutput");
                }
            catch (Exception ex)
            {
                return Ok(new
                {
                    success = false,
                    message = ex.Message
                });
            }

            return Ok(json);
        }
    }
}