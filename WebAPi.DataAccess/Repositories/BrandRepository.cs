using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Core.Common;
using WebApi.Core.Entities;
using WebAPi.DataAccess.Intarfaces;

namespace WebAPi.DataAccess.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        // Implementacion del metodo GetAllAsync para obtener todas las marcas
        private readonly string _connectionString;
        public BrandRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
        public async Task<RepositoryResponse<IEnumerable<Brands>>> GetAllAsync()
        {
            var brands = new List<Brands>();
            var response = new RepositoryResponse<IEnumerable<Brands>>();
            try
            {
                // establecemos la conexion con la base de datos
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_GETALLBRANDS", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            brands.Add(new Brands
                            {
                                Id = (int)reader["BrandId"],
                                BrandName = reader["BrandName"].ToString()!,
                                Description = reader["BrandDescription"].ToString(),
                                IsActive = (bool)reader["Isactive"]
                            });
                        }
                    }
                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = brands;
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


        public async Task<RepositoryResponse<int>> AddAsync(Brands brand)
        {
            var response = new RepositoryResponse<int>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_REGISTERBRANDS", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BrandName", brand.BrandName);
                    cmd.Parameters.AddWithValue("@BrandDescription", brand.Description ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IsActive", brand.IsActive);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                    await cmd.ExecuteNonQueryAsync();
                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = returnedValue; // Assuming the stored procedure returns the new Brand ID
                    response.OperationStatusCode = 0;
                }
            }
            catch (SqlException ex)
            {
                response.Data = -1; // Indicate failure to add
                response.OperationStatusCode = ex.Number; // SQL error code
            }
            return response;
        }

        public async Task<RepositoryResponse<int>> DeactiveAsync(int id)
        {
            var response = new RepositoryResponse<int>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("USP_DEACTIVATEBRAND", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BrandId", id);

                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                    await cmd.ExecuteNonQueryAsync();
                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);

                    response.Data = returnedValue; // Assuming the stored procedure returns the status of deactivation
                    response.OperationStatusCode = 0;
                }
            }
            catch (SqlException ex)
            {
                response.Data = -1; // Indicate failure to deactivate
                response.OperationStatusCode = ex.Number; // SQL error code
            }
            return response;

        }

        public async Task<RepositoryResponse<Brands>> GetByIdAsync(int id)
        {
            var response = new RepositoryResponse<Brands>();
            Brands brand = null;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand cmd = new SqlCommand("USP_GETBRANDBYID", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BrandId", id);
                    cmd.Parameters.Add("@ReturnValue", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            brand = new Brands
                            {
                                Id = (int)reader["BrandId"],
                                BrandName = reader["BrandName"].ToString()!,
                                Description = reader["BrandDescription"].ToString(),
                                IsActive = (bool)reader["Isactive"]
                            };
                        }
                    }

                    var returnedValue = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
                    response.Data = brand;
                    response.OperationStatusCode = returnedValue;

                    return new RepositoryResponse<Brands>
                    {
                        Data = brand,
                        OperationStatusCode = returnedValue
                    };
                }
            }
            catch (SqlException ex)
            {
                return new RepositoryResponse<Brands>
                {
                    Data = null,
                    OperationStatusCode = ex.Number,
                    Message = ex.Message
                };
                
            }
            
        }
    }
}
