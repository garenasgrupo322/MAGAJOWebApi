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
    public class MenuController : Controller
    {
        public IConfiguration _configuration { get; }

        public MenuController(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        
        [HttpGet]   
        [Route("ViewMenu/{id}")]
        public ActionResult GetMenu(string id)
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
                json = utilities.callSP("MGJSEC_VIEW_MENU_GET", parameters, "@jsonOutput");
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