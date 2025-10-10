using Azure;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.Common;
using WebApi.Core.Entities;
using WebAPi.DataAccess.Intarfaces;

namespace WebAPi.DataAccess.Repositories
{
    public class PresentationRepository : IPresentationRepository
    {
        private readonly string _connectionString;
        public PresentationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<RepositoryResponse<IEnumerable<Presentations>>> GetAllAsync()
        {
            var presentations = new List<Presentations>();
            var response = new RepositoryResponse<IEnumerable<Presentations>>();
            try
            {
                // establecemos la conexion con la base de datos
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_GetAllPresentations", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            presentations.Add(new Presentations
                            {
                                Id = (int)reader["PresentationId"],
                                Description = reader["PresentationDescription"].ToString(),
                                Quantity = reader["quantity"].ToString()!,
                                UnitMeasure = reader["UnitMeasure"].ToString()!,
                                IsActive = (bool)reader["Isactive"]
                            });
                        }
                    }
                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = presentations;
                    response.OperationStatusCode = returnedValue;

                }
            }
            catch (SqlException ex)
            {
                response.Data = null;
                response.OperationStatusCode = ex.Number;
            }
            return response;
        }

        public async Task<RepositoryResponse<Presentations>> AddAsync(Presentations presentations)
        {
            var presentationReturnted = new Presentations();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_AddPresentations", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Description", presentations.Description);
                    cmd.Parameters.AddWithValue("@Quantity", presentations.Quantity);
                    cmd.Parameters.AddWithValue("@UnitMeasure", presentations.UnitMeasure);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            presentationReturnted.Id = (int)reader["PresentationId"];
                            presentationReturnted.Description = reader["PresentationDescription"].ToString();
                            presentationReturnted.Quantity = reader["quantity"].ToString()!;
                            presentationReturnted.UnitMeasure = reader["UnitMeasure"].ToString()!;
                            presentationReturnted.IsActive = (bool)reader["Isactive"];
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    return new RepositoryResponse<Presentations>
                    {
                        Data = presentationReturnted,
                        OperationStatusCode = returnedValue

                    };
                } 
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<Presentations>
                {
                    Data = null,
                    OperationStatusCode = ex.Number,
                    Message = ex.Message,
                };
            }
            
        }

        public async Task<RepositoryResponse<Presentations>> GetByUnitMeasureAsync(string UnitMeasure)
        {
            var response = new RepositoryResponse<Presentations>();
            Presentations presentations = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("procedimiento almacenado", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UnitMeasure", UnitMeasure);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            presentations = new Presentations
                            {
                                Quantity = Convert.ToString(reader["quantity"]!.ToString()),
                                
                            };
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = presentations;
                    response.OperationStatusCode = returnedValue;

                    return new RepositoryResponse<Presentations>
                    {
                        Data = presentations,
                        OperationStatusCode = returnedValue
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<Presentations>
                {
                    Data = null,
                    OperationStatusCode = ex.Number,
                    Message = ex.Message
                };

            }
        }

    } 
}


