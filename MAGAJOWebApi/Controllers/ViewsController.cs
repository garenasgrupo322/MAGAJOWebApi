using System;
using System.Collections.Generic;
using System.Data;
using MAGAJOWebApi.Models;
using MAGAJOWebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;

namespace MAGAJOWebApi.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class ViewsController : Controller
    {
        public IConfiguration _configuration { get; }

        public ViewsController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        [HttpGet("{idApp}/{idView}")]
        public ActionResult GetSecurity(string idApp, string idView) {
            string json = string.Empty;
            List<Parameters> parameters = new List<Parameters>();

            Parameters parameter = new Parameters();
            parameter.Name = "@JSonReturn";
            parameter.Direction = ParameterDirection.InputOutput;
            parameter.Type = SqlDbType.NVarChar;
            parameter.Value = string.Empty;
            parameter.Size = -1;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@UserId";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = idApp;
            parameter.Size = 100;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@RoleId";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = idView;
            parameter.Size = 100;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@ApplicationId";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = idView;
            parameter.Size = 100;
            parameters.Add(parameter);

            Utilities utilities = new Utilities(_configuration);

            try
            {
                json = utilities.callSP("MGJSEC_ROLE_GET_VIEWS", parameters, "@JSonReturn");
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

        [HttpGet("{idApp}/{idView}")] 
        public ActionResult Get(string idApp, string idView)
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
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = idApp;
            parameter.Size = 50;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@MGJAPP_VIEW_ID";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = idView;
            parameter.Size = 50;
            parameters.Add(parameter);

            Utilities utilities = new Utilities(_configuration);

            try
            {
                json = utilities.callSP("MGJVIEW_GET", parameters, "@jsonOutput");
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
        
    }
}