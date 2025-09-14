using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class UsersController : ApiController
    {
        private readonly string connStr = System.Configuration.ConfigurationManager
                .ConnectionStrings["MyDBConnection"].ConnectionString;
        [HttpGet]
        public IEnumerable<User> Get()  //return list of user objects
        {
            var users = new List<User>();

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Username, Email FROM Users", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = (int)reader["Id"],
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString()
                    });
                }
            }

            return users;
        }
        [HttpGet]
        public IHttpActionResult GetById(int id) //id parameter naming should match the router in WebApiConfig.cs
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT Id, Username, Email FROM Users WHERE Id = @id", conn); //used @id instead of concatenation because it will be vulnerable to sql injecting
                cmd.Parameters.AddWithValue("@id", id); //replace @id with the value of the C# variable id

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var user = new User
                        {
                            Id = (int)reader["Id"],
                            Username = reader["Username"].ToString(), 
                            Email = reader["Email"].ToString()
                        };
                        return Ok(user);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
        }
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Delete From Users Where Id=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                int rowsDeleted = cmd.ExecuteNonQuery();
                if (rowsDeleted > 0)
                {
                    return Ok("User deleted successfully");
                }
                else
                {
                    return NotFound();
                }
            }
        }
        [HttpPost]
        public IHttpActionResult CreateUser(User user)
        {
            if (user == null)
            {
                return BadRequest("Invalid user data");
            }

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Users (Username, Email) VALUES (@username,@email); SELECT SCOPE_IDENTITY();", con);

                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@email", user.Email);

                // Get the new Id from SQL
                int newId = Convert.ToInt32(cmd.ExecuteScalar()); // executeScaler reads the first value of first row returned which is id, convert it into c# integer
                user.Id = newId; // assign it to the model

                return Created($"api/users/{newId}", user);
            }
        }
        [HttpPut]
        public IHttpActionResult updateUser(int id, User user) 
        {
            if (string.IsNullOrEmpty(user.Username))
            {
                return BadRequest("Invalid user data");
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE USERS SET Username=@username WHERE Id=@id;", conn);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@id", id);
                int rowsUpdate = cmd.ExecuteNonQuery();
                if (rowsUpdate > 0)
                {
                    return Ok("User updated successfully"); 
                }
                else
                {
                    return NotFound();
                }
            }
            
        }
    }
}
