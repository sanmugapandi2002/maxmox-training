using  MySql.Data.MySqlClient;
using Simple_Auth_System_project.Model;
using SimpleAuthSystem.Model;
using System.Data.Common;

namespace SimpleAuthSystem.DataAccessLayer
{
    public class AuthDL : IAuthDL
    {
        public readonly IConfiguration _configuration;
        public readonly MySqlConnection _mySqlConnection;

        public AuthDL(IConfiguration configuration)
        {
            _configuration = configuration;

            _mySqlConnection = new MySqlConnection(_configuration["ConnectionStrings:MySqlDBConnectionString"]);
        }

       

        public async Task<SignInResponse> SignIn(SignInRequest request)
        {
            SignInResponse response = new SignInResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {

                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

                string SqlQuery = @"SELECT * 
                                    FROM crudoperation.userdetail
                                    WHERE UserName=@UserName AND PassWord=@PassWord;";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                    sqlCommand.Parameters.AddWithValue("@PassWord", request.Password);
 
                    using (DbDataReader dataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (dataReader.HasRows)
                        {
                            response.Message = "Login Successfully";
                        }
                        else
                        {
                            response.IsSuccess = false;
                            response.Message = "Login Unsuccessfully";
                            return response;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {

            }

            return response;
        }

        public async Task<SignUpResponse> SignUp(SignUpRequest request)
        {
            SignUpResponse response = new SignUpResponse();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                if (_mySqlConnection.State != System.Data.ConnectionState.Open)
                {
                    await _mySqlConnection.OpenAsync();
                }

#pragma warning disable CS8602 // Dereference of a possibly null reference.
                if (!request.Password.Equals(request.ConfirmPassword))
                {
                    response.IsSuccess = false;
                    response.Message = "Password & Confirm Password not Match";
                    return response;
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.

                string SqlQuery = @"INSERT INTO crudoperation.userdetail
                                    (UserName, PassWord) VALUES 
                                    (@UserName, @PassWord)";

                using (MySqlCommand sqlCommand = new MySqlCommand(SqlQuery, _mySqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;

                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue("@UserName", request.UserName);
                    sqlCommand.Parameters.AddWithValue("@PassWord", request.Password);

                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    if (Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Something Went Wrong";
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
 
                
                response.Message = ex.Message;
            }
            finally
            {

            }

            return response;
        }

      
    }

   
}
   