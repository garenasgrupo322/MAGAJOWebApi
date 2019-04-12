using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using MAGAJOWebApi.Models;
using Microsoft.Extensions.Configuration;

namespace MAGAJOWebApi.Utils
{
    public class Utilities
    {

        public IConfiguration _configuration { get; }

        public Utilities(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string callSP(string spName, List<Parameters> parameters, string parameterOutput)
        {
            string result = string.Empty;
            SqlConnection DbConnection = new SqlConnection(_configuration["ConnectionStrings:Connection"]);

            try
            {
                DbConnection.Open();
                SqlCommand command = new SqlCommand(spName, DbConnection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter[] parms = new SqlParameter[parameters.Count];

                int i = 0;
                foreach (Parameters item in parameters)
                {
                    SqlParameter parameter = new SqlParameter();
                    parameter.ParameterName = item.Name;
                    parameter.SqlDbType = item.Type;
                    parameter.Value = item.Value;
                    parameter.Direction = item.Direction;
                    parameter.Size = item.Size;
                    parms[i] = parameter;
                    i++;
                }

                command.Parameters.AddRange(parms);

                command.ExecuteNonQuery();

                result = command.Parameters[parameterOutput].Value.ToString();

            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                DbConnection.Close();
            }


            return result;
        }
    }
}
