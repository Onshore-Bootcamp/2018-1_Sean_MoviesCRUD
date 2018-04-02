using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Movies_DAL;
using Movies_DAL.Models;

namespace Movies_DAL
{
    public class UserDAO
    {
        public UserDAO(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        private readonly string _ConnectionString = ConfigurationManager.ConnectionStrings["DataSource"].ConnectionString;

        LoggerDAL _Logger = new LoggerDAL();

        public List<UserDO> ViewAllUsers()
        {
            List<UserDO> userList = new List<UserDO>();

            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataAdapter adapter = null;
            DataTable userTable = new DataTable();

            try
            {
                connectionToSql = new SqlConnection(_ConnectionString);
                storedProcedure = new SqlCommand("OBTAIN_USERS", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                connectionToSql.Open();

                adapter = new SqlDataAdapter(storedProcedure);
                adapter.Fill(userTable);

                foreach (DataRow row in userTable.Rows)
                {
                    UserDO userDO = new UserDO();

                    userDO.UserID = int.Parse(row["UserID"].ToString());
                    userDO.Username = row["Username"].ToString().Trim();
                    userDO.Password = row["Password"].ToString().Trim();
                    userDO.FirstName = row["FirstName"].ToString().Trim();
                    userDO.LastName = row["LastName"].ToString().Trim();
                    userDO.RoleID = int.Parse(row["RoleID"].ToString());
                    userDO.Email = row["Email"].ToString().Trim();

                    userList.Add(userDO);
                }
            }
            catch (Exception exception)
            {
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                throw exception;
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
                else
                {

                }

                if (adapter != null)
                {
                    adapter.Dispose();
                }
                else
                {

                }
            }
            return userList;
        }

        public UserDO GetUserByUsername(string username)
        {
            UserDO userDO = null;
            SqlConnection connectioToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataAdapter adapter = null;
            DataTable usersTable = new DataTable();

            try
            {
                connectioToSql = new SqlConnection(_ConnectionString);
                storedProcedure = new SqlCommand("OBTAIN_USER_BY_USERNAME", connectioToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@Username", username);

                connectioToSql.Open();
                adapter = new SqlDataAdapter(storedProcedure);
                adapter.Fill(usersTable);

                if (usersTable.Rows.Count > 0)
                {
                    DataRow row = usersTable.Rows[0];
                    userDO = new UserDO();

                    userDO.UserID = int.Parse(row["UserID"].ToString());
                    userDO.Username = (row["Username"].ToString().Trim());
                    userDO.Password = (row["Password"].ToString().Trim());
                    userDO.FirstName = (row["FirstName"].ToString().Trim());
                    userDO.LastName = (row["LastName"].ToString().Trim());
                    userDO.RoleID = int.Parse(row["RoleID"].ToString().Trim());
                    userDO.Email = (row["Email"].ToString().Trim());
                }
                else
                {

                }
            }
            catch (Exception exception)
            {
                _Logger.Log("fatal", exception.Source, exception.TargetSite.Name, exception.Message, exception.StackTrace);
                throw exception;
            }
            finally
            {
                if (connectioToSql != null)
                {
                    connectioToSql.Close();
                    connectioToSql.Dispose();
                }
                else
                {

                }

                if (adapter != null)
                {
                    adapter.Dispose();
                }
                else
                {

                }
            }
            return userDO;
        }
        public LoggerDAL Logger = new LoggerDAL();

        public void AddUser(UserDO userDO)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_ConnectionString);
                storedProcedure = new SqlCommand("ADD_USER", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@Username", userDO.Username);
                storedProcedure.Parameters.AddWithValue("@Password", userDO.Password);
                storedProcedure.Parameters.AddWithValue("@FirstName", userDO.FirstName);
                storedProcedure.Parameters.AddWithValue("@LastName", userDO.LastName);
                storedProcedure.Parameters.AddWithValue("@RoleID", 3);
                storedProcedure.Parameters.AddWithValue("@Email", userDO.Email);

                connectionToSql.Open();

                storedProcedure.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);
                throw exception;
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
                else
                {

                }
            }
        }

        public void UpdateUserById(UserDO userDO)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_ConnectionString);
                storedProcedure = new SqlCommand("UPDATE_USER_BY_ID", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@Username", userDO.Username);
                storedProcedure.Parameters.AddWithValue("@Password", userDO.Password);
                storedProcedure.Parameters.AddWithValue("@FirstName", userDO.FirstName);
                storedProcedure.Parameters.AddWithValue("@LastName", userDO.LastName);
                storedProcedure.Parameters.AddWithValue("@Role", userDO.RoleID);
                storedProcedure.Parameters.AddWithValue("@Email", userDO.Email);

                connectionToSql.Open();

                storedProcedure.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);
                throw exception;
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
                else
                {

                }
            }
        }

        public void DeleteUserById(int IdInput)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_ConnectionString);
                storedProcedure = new SqlCommand("DELETE_USER_BY_ID", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("UserID", IdInput);

                connectionToSql.Open();

                storedProcedure.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                throw exception;
            }
            finally
            {
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
                else
                {

                }
            }
        }

       

    }
}
