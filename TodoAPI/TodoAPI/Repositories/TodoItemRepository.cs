using Oracle.ManagedDataAccess.Client;
using System.Data;
using TodoApi.Models;
using TodoAPI.Definitions;

namespace TodoAPI.Repositories
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string? connString;

        public TodoItemRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            connString = _configuration.GetConnectionString("OracleDbConnection");
        }
        public async Task<TodoItem> GetTodoItem(decimal id)
        {
            var todoItem = new TodoItem();
            using (OracleConnection conn = new OracleConnection(connString))
            {
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "PBLHO.PKG_TODO_API.P_Get_TodoItem";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_ITEMID", OracleDbType.Decimal).Value = id;
                        cmd.Parameters.Add("O_ITEM_C", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            todoItem = new()
                            {
                                Id = (decimal)reader["ITEMID"],
                                Name = reader["ITEMNAME"].ToString(),
                                IsComplete = reader["ISCOMPLETE"].ToString() == "C" ? true : false,
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
            return Task.FromResult(todoItem).Result;
        }

        public async Task<List<TodoItem>> GetTodoItems()
        {
            var todoItems = new List<TodoItem>();
            var todoItem = new TodoItem();
            using (OracleConnection conn = new OracleConnection(connString))
            {
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "PBLHO.PKG_TODO_API.P_Get_TodoItems";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("O_ITEMS_C", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                        OracleDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            todoItem = new()
                            {
                                Id = (decimal)reader["ITEMID"],
                                Name = reader["ITEMNAME"].ToString(),
                                IsComplete = reader["ISCOMPLETE"].ToString() == "Y" ? true : false,
                            };
                            todoItems.Add(todoItem);
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
            return Task.FromResult(todoItems).Result;
        }

        public async Task<bool> CreateTodoItem(TodoItem item)
        {
            var status = false;
            var todoItem = new TodoItem();
            using (OracleConnection conn = new OracleConnection(connString))
            {
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "PBLHO.PKG_TODO_API.P_Create_TodoItem";
                        cmd.CommandType = CommandType.StoredProcedure;                        
                        cmd.Parameters.Add("P_ITEMNAME", OracleDbType.Varchar2).Value = item.Name;
                        cmd.Parameters.Add("P_ISCOMPLETE", OracleDbType.Char).Value = item.IsComplete == true ? "Y" : "N";
                        cmd.Parameters.Add("O_STATUS", OracleDbType.Char, 1).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        status = cmd.Parameters["O_STATUS"].Value.ToString() == "C" ? true : false;
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
            return Task.FromResult(status).Result;
        }

        public async Task<bool> UpdateTodoItem(TodoItem item)
        {
            var status = false;
            var todoItem = new TodoItem();
            using (OracleConnection conn = new OracleConnection(connString))
            {
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "PBLHO.PKG_TODO_API.P_Update_TodoItem";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_ITEMID", OracleDbType.Decimal).Value = item.Id;
                        cmd.Parameters.Add("P_ITEMNAME", OracleDbType.Varchar2).Value = item.Name;
                        cmd.Parameters.Add("P_ISCOMPLETE", OracleDbType.Char).Value = item.IsComplete == true ? "Y" : "N";
                        cmd.Parameters.Add("O_STATUS", OracleDbType.Char, 1).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        status = cmd.Parameters["O_STATUS"].Value.ToString() == "U" ? true : false;
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
            return Task.FromResult(status).Result;
        }

        public async Task<bool> DeleteTodoItem(decimal id)
        {
            var status = false;
            var todoItem = new TodoItem();
            using (OracleConnection conn = new OracleConnection(connString))
            {
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    try
                    {
                        conn.Open();
                        cmd.BindByName = true;

                        cmd.CommandText = "PBLHO.PKG_TODO_API.P_Delete_TodoItem";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("P_ITEMID", OracleDbType.Decimal).Value = id;
                        cmd.Parameters.Add("O_STATUS", OracleDbType.Char, 1).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();
                        status = cmd.Parameters["O_STATUS"].Value.ToString() == "D" ? true : false;
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
            return Task.FromResult(status).Result;
        }

    }
}
