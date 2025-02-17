using AuthenticationAPI.Definitions;
using AuthenticationAPI.Models;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace AuthenticationAPI.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IConfiguration _configuration;

        public AuthRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<User> GetUserByUsername(String? username)
        {
            var connString = _configuration.GetConnectionString("OracleDbConnection");
            User user = new User();           
            using (OracleConnection conn = new OracleConnection(connString))
            {
                using (OracleCommand cmd =  conn.CreateCommand())
                {
                    cmd.CommandText = "PBLHO.PKG_REGISTERED_USERS.P_Get_Registered_User";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("P_USERNAME", OracleDbType.Varchar2).Value = username;
                    cmd.Parameters.Add("O_USER_C", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    cmd.BindByName = true;
                    try
                    {
                        conn.Open();
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            user = new()
                            {
                                Username = reader["USERNAME"].ToString(),
                                Password = reader["PASSWORD"].ToString(),
                            };
                        }

                        cmd.Dispose();
                    }

                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            return Task.FromResult(user);
        }
    }
}
