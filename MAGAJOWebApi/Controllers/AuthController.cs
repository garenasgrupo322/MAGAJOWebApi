using System;
using System.Collections.Generic;
using System.Data;
using MAGAJOWebApi.Models;
using MAGAJOWebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MAGAJOWebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class AuthController : Controller
    {
        public IConfiguration _configuration { get; }

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Route("[action]")]
        public ActionResult GetId([FromForm]string path)
        {
            return Ok(new { 
                id = "ERPZANTE"
            });
        }

        [HttpGet]   
        [Route("ViewLogin/{id}")]
        public ActionResult Get(string id)
        {
            string json = string.Empty;
            List<Parameters> parameters = new List<Parameters>();

            Parameters parameter = new Parameters();
            parameter.Name = "@jsonOutput";
            parameter.Direction = ParameterDirection.InputOutput;
            parameter.Type = SqlDbType.NVarChar;
            parameter.Value = string.Empty;
            parameter.Size = -1;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@MGJAPP_ID";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.Char;
            parameter.Value = id;
            parameter.Size = 10;
            parameters.Add(parameter);

            Utilities utilities = new Utilities(_configuration);

            try
            {
                json = utilities.callSP("MGJSEC_VIEW_LOGIN_GET", parameters, "@jsonOutput");
            } 
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }

            return Ok(json);
        }

        public class loginPost
        {
            public string appId { get; set; }
            public string userName { get; set; }
            public string userPassword { get; set; }
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Post([FromBody] loginPost loginPost)
        {
            string json = string.Empty;
            List<Parameters> parameters = new List<Parameters>();

            Parameters parameter = new Parameters();
            parameter.Name = "@I_TOKEN";
            parameter.Direction = ParameterDirection.InputOutput;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = string.Empty;
            parameter.Size = -1;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@I_USERNAME";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = loginPost.userName;
            parameter.Size = 20;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@I_PASSWORD";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = loginPost.userPassword;
            parameter.Size = 20;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@MGJAPP_ID";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = loginPost.appId;
            parameter.Size = 20;
            parameters.Add(parameter);

            Utilities utilities = new Utilities(_configuration);

            try
            {
                json = utilities.callSP("MGJSEC_AUTHENTICATE", parameters, "@I_TOKEN");
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    success = false,
                    message = ex.Message
                });
            }

            return Ok(new {
                success = true,
                token = json
            });
        }
    }
}
