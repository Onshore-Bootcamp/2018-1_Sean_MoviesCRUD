using Movies_DAL.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies_DAL.DAOs
{
    public class ActorDAO
    {
        public ActorDAO(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        private readonly string _ConnectionString = ConfigurationManager.ConnectionStrings["DataSource"].ConnectionString;

        public LoggerDAL _Logger = new LoggerDAL();

        public List<ActorDO> ViewAllActors()
        {
            List<ActorDO> actorList = new List<ActorDO>();
            DataTable actorsTable = new DataTable();

            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataAdapter adapter = null;

            try
            {
                connectionToSql = new SqlConnection(_ConnectionString);
                storedProcedure = new SqlCommand("OBTAIN_ALL_ACTORS", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                connectionToSql.Open();

                adapter = new SqlDataAdapter(storedProcedure);
                adapter.Fill(actorsTable);

                foreach (DataRow row in actorsTable.Rows)
                {
                    ActorDO actorDO = new ActorDO();

                    actorDO.ActorID = int.Parse(row["ActorID"].ToString());
                    actorDO.FirstName = row["FirstName"].ToString().Trim();
                    actorDO.LastName = row["LastName"].ToString().Trim();
                    
                    if (DateTime.TryParse(row["BirthDate"].ToString(), out DateTime BirthDate))
                    {
                        actorDO.BirthDate = BirthDate;
                    }
                    else
                    {

                    }

                    actorDO.Bio = row["Bio"].ToString().Trim();
                    actorDO.Trivia = row["Trivia"].ToString().Trim();
                    actorDO.Quotes = row["Quotes"].ToString().Trim();

                    actorList.Add(actorDO);
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
            return actorList;
        }

        public void AddNewActor(ActorDO actorDO)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_ConnectionString);
                storedProcedure = new SqlCommand("ADD_ACTOR", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("FirstName", actorDO.FirstName);
                storedProcedure.Parameters.AddWithValue("LastName", actorDO.LastName);
                storedProcedure.Parameters.AddWithValue("BirthDate", actorDO.BirthDate);
                storedProcedure.Parameters.AddWithValue("Bio", actorDO.Bio);
                storedProcedure.Parameters.AddWithValue("Trivia", actorDO.Trivia);
                storedProcedure.Parameters.AddWithValue("Quotes", actorDO.Quotes);

                connectionToSql.Open();

                storedProcedure.ExecuteNonQuery();
            }
            catch (SqlException exception)
            {
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                if (exception.Number == 2601 || exception.Number == 2627)
                {
                    exception.Data["Message"] = "Duplicate actor";
                }
                else
                {

                }

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

        public ActorDO ViewActorByActorId(int IdInput)
        {
            ActorDO actorDO = null;
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataAdapter adapter = null;
            DataTable actorTable = new DataTable();

            try
            {
                connectionToSql = new SqlConnection(_ConnectionString);
                storedProcedure = new SqlCommand("OBTAIN_ACTOR_BY_ACTOR_ID", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@ActorID", IdInput);

                connectionToSql.Open();

                adapter = new SqlDataAdapter(storedProcedure);
                adapter.Fill(actorTable);

                if (actorTable.Rows.Count > 0)
                {
                    DataRow row = actorTable.Rows[0];
                    actorDO = new ActorDO();

                    actorDO.ActorID = int.Parse(row["ActorID"].ToString());
                    actorDO.FirstName = row["FirstName"].ToString().Trim();
                    actorDO.LastName = row["LastName"].ToString().Trim();

                    if (DateTime.TryParse(row["BirthDate"].ToString(), out DateTime birthDate))
                    {
                        actorDO.BirthDate = birthDate;
                    }
                    else
                    {

                    }

                    actorDO.Bio = row["Bio"].ToString().Trim();
                    actorDO.Trivia = row["Trivia"].ToString().Trim();
                    actorDO.Quotes = row["Quotes"].ToString().Trim();
                }
                else
                {

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
            }
            return actorDO;
        }

        public List<ActorDO> ViewActorsByMovieID(int IdInput)
        {
            List<ActorDO> actorList = new List<ActorDO>();
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataAdapter adapter = null;
            DataTable actorTable = new DataTable();

            try
            {
                connectionToSql = new SqlConnection(_ConnectionString);
                storedProcedure = new SqlCommand("OBTAIN_ACTOR_BY_MOVIE_ID", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@MovieID", IdInput);

                connectionToSql.Open();

                adapter = new SqlDataAdapter(storedProcedure);
                adapter.Fill(actorTable);

                foreach (DataRow firstRow in actorTable.Rows)
                {
                    ActorDO actorDO = new ActorDO();

                    actorDO.ActorID = int.Parse(firstRow["ActorID"].ToString());
                    actorDO.FirstName = firstRow["FirstName"].ToString().Trim();
                    actorDO.LastName = firstRow["LastName"].ToString().Trim();

                    if (DateTime.TryParse(firstRow["BirthDate"].ToString(), out DateTime birthDate))
                    {
                        actorDO.BirthDate = birthDate;
                    }
                    else
                    {

                    }

                    actorDO.Bio = firstRow["Bio"].ToString().Trim();
                    actorDO.Trivia = firstRow["Trivia"].ToString().Trim();
                    actorDO.Quotes = firstRow["Quotes"].ToString().Trim();

                    actorList.Add(actorDO);
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

                if (storedProcedure != null)
                {
                    storedProcedure.Dispose();
                }
                else
                {

                }
            }
            return actorList;
        }

        public void UpdateActorById(ActorDO actorDO)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_ConnectionString);
                storedProcedure = new SqlCommand("UPDATE_ACTOR_BY_ID", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@ActorID", actorDO.ActorID);
                storedProcedure.Parameters.AddWithValue("@FirstName", actorDO.FirstName);
                storedProcedure.Parameters.AddWithValue("@LastName", actorDO.LastName);
                storedProcedure.Parameters.AddWithValue("@BirthDate", actorDO.BirthDate);
                storedProcedure.Parameters.AddWithValue("@Bio", actorDO.Bio);
                storedProcedure.Parameters.AddWithValue("@Trivia", actorDO.Trivia);
                storedProcedure.Parameters.AddWithValue("@Quotes", actorDO.Quotes);

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

        public void DeleteActorById(int IdInput)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_ConnectionString);
                storedProcedure = new SqlCommand("DELETE_ACTOR_BY_ID", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("ActorID", IdInput);

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

        public void DeleteActorByMovieId(int MovieId, int ActorId)
        {
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                connectionToSql = new SqlConnection(_ConnectionString);
                storedProcedure = new SqlCommand("DELETE_ACTOR_BY_MOVIE_ID", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("MovieID", MovieId);
                storedProcedure.Parameters.AddWithValue("ActorID", ActorId);

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

        public List<ActorDO> ActorsNotInMovie(int movieID)
        {
            List<ActorDO> actorList = new List<ActorDO>();
            DataTable actorTable = new DataTable();

            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataAdapter adapter = null;

            try
            {
                connectionToSql = new SqlConnection(_ConnectionString);
                storedProcedure = new SqlCommand("OBTAIN_ACTORS_NOT_IN_MOVIE", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                storedProcedure.Parameters.AddWithValue("@MovieID", movieID);

                connectionToSql.Open();

                adapter = new SqlDataAdapter(storedProcedure);
                adapter.Fill(actorTable);

                foreach (DataRow row in actorTable.Rows)
                {
                    ActorDO actorDO = new ActorDO();

                    actorDO.ActorID = int.Parse(row["ActorID"].ToString());
                    actorDO.FirstName = row["FirstName"].ToString().Trim();
                    actorDO.LastName = row["LastName"].ToString().Trim();

                    //if (DateTime.TryParse(row["BirthDate"].ToString(), out DateTime BirthDate))
                    //{
                    //    actorDO.BirthDate = BirthDate;
                    //}
                    //
                    //actorDO.Bio = row["Bio"].ToString().Trim();
                    //actorDO.Trivia = row["Trivia"].ToString().Trim();
                    //actorDO.Quotes = row["Quotes"].ToString().Trim();

                    actorList.Add(actorDO);
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
            return actorList;
        }

    }
}
