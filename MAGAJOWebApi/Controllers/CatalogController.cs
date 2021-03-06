using System;
using System.Collections.Generic;
using System.Data;
using MAGAJOWebApi.Models;
using MAGAJOWebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MAGAJOWebApi.Controllers {

    [Route("api/[controller]")]
    [EnableCors("AllowOrigin")]
    public class CatalogController : Controller
    {
        public IConfiguration _configuration { get; }

        public CatalogController(IConfiguration configuration) {
            _configuration = configuration;
        }

        public class EntityData {
            public string IEntityID { get; set; }
            public string Itoken { get; set; }
            public FilterData FilterData { get; set; }
            public string SortData { get; set; }
            public string QueryLimits { get; set; }
            public string ColumnData { get; set; }
            public string MGJAPP_ID { get; set; }
        }

        public class FilterData {
            public string condition { get; set; }
            public List<filters> filters { get; set; }
        }

        public class filters
        {
            public string field { get; set; }
            public string[] values { get; set; }
            public string operador { get; set; }
        }

        public class filtrosInsumo {
            public List<filtro> filtros { get; set; }
        }

        public class filtro
        {
            public string valor { get; set; }
        }

        [HttpPost]
        [Route("ObtieneExplosionDatos")]
        public ActionResult Post([FromBody] filtrosInsumo filtros)
        {
            string json = string.Empty;
            List<Parameters> parameters = new List<Parameters>();

            Parameters parameter = new Parameters();
            parameter.Name = "@FILTER";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            var jsonFilter = JsonConvert.SerializeObject(filtros.filtros);
            parameter.Value = jsonFilter;
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
                json = utilities.callSP("GET_EXPLOSION_INSUMOS", parameters, "@IDataOutput");
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

        [HttpPost]
        [Route("GetData")]
        public ActionResult Post([FromBody] EntityData parametros)
        {
            string json = string.Empty;
            List<Parameters> parameters = new List<Parameters>();

            Parameters parameter = new Parameters();
            parameter.Name = "@IEntityID";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = parametros.IEntityID;
            parameter.Size = -1;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@Itoken";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = parametros.Itoken;
            parameter.Size = -1;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@FilterData";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            if (parametros.FilterData != null)
            {
                var jsonFilter = JsonConvert.SerializeObject(parametros.FilterData);
                parameter.Value = jsonFilter;
            }
            else
            {
                parameter.Value = DBNull.Value;
            }
            parameter.Size = -1;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@SortData";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            if (!string.IsNullOrEmpty(parametros.SortData))
                parameter.Value = parametros.SortData;
            else
                parameter.Value = DBNull.Value;
            parameter.Size = -1;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@QueryLimits";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            if (!string.IsNullOrEmpty(parametros.QueryLimits))
                parameter.Value = parametros.QueryLimits;
            else
                parameter.Value = DBNull.Value;
            parameter.Size = -1;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@ColumnData";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            if (!string.IsNullOrEmpty(parametros.ColumnData))
                parameter.Value = parametros.ColumnData;
            else
                parameter.Value = DBNull.Value;
            parameter.Size = -1;
            parameters.Add(parameter);

            /*parameter = new Parameters();
            parameter.Name = "@MGJAPP_ID";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.Char;
            parameter.Value = parametros.MGJAPP_ID;
            parameter.Size = 10;
            parameters.Add(parameter);*/

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
                json = utilities.callSP("MGJENT_GET_ENTITY_DATA", parameters, "@IDataOutput");
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

        [HttpPost]
        [Route("GetModel")]
        public ActionResult GetModel([FromBody] EntityData parametros)
        {
            string json = string.Empty;
            List<Parameters> parameters = new List<Parameters>();

            Parameters parameter = new Parameters();
            parameter.Name = "@MGJAPP_ID";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.Char;
            parameter.Value = parametros.MGJAPP_ID;
            parameter.Size = 10;
            parameters.Add(parameter);

            parameter = new Parameters();
            parameter.Name = "@IEntityID";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = parametros.IEntityID; 
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
                json = utilities.callSP("MGJENT_GET_ENTITY_MODEL", parameters, "@IDataOutput");
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

        public class requi {
            public string parametros { get; set; }
        }

        [HttpPost]
        [Route("SetRequi")]
        public ActionResult SetReqiPost([FromBody] requi parm)
        {
            string json = string.Empty;
            List<Parameters> parameters = new List<Parameters>();

            Parameters parameter = new Parameters();
            parameter.Name = "@JSONREQUI";
            parameter.Direction = ParameterDirection.Input;
            parameter.Type = SqlDbType.VarChar;
            parameter.Value = parm.parametros;
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