using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Movies_DAL.Models;
using System.Data.SqlClient;
using System.Data;

namespace Movies_DAL
{
    public class MovieDAO
    {
        //Setting connectionstring
        public MovieDAO(string connectionString)
        {
            _ConnectionString = connectionString;
        }

        private readonly string _ConnectionString = ConfigurationManager.ConnectionStrings["DataSource"].ConnectionString;

        //Instantiating a logger
        public LoggerDAL _Logger = new LoggerDAL();

        //Using list to view all my movies
        public List<MovieDO> ViewAllMovies()
        {
            //Instantiating my list
            List<MovieDO> movieList = new List<MovieDO>();
            //Instantiating my table for movies
            DataTable movieTable = new DataTable();

            //Setting all to null
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataAdapter adapter = null;

            try
            {
                //Instantiating connection
                connectionToSql = new SqlConnection(_ConnectionString);
                //Instantiating my sqlcommand to get my procedure
                storedProcedure = new SqlCommand("OBTAIN_MOVIES", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                //Establishing connection to sql
                connectionToSql.Open();

                //Creating my adapter
                adapter = new SqlDataAdapter(storedProcedure);
                //Using my datatable to fill the adapter
                adapter.Fill(movieTable);

                foreach (DataRow row in movieTable.Rows)
                {
                    //Instantiating my movie object
                    MovieDO movieDO = new MovieDO();

                    //Parsing the movieID to string
                    movieDO.MovieID = int.Parse(row["MovieID"].ToString());
                    //Taking the title of the object and puting it in the table
                    movieDO.Title = row["Title"].ToString().Trim();
                    //Taking the rating of the object and puting it in the table
                    movieDO.Rating = row["Rating"].ToString().Trim();
                    //Parsing the runtime to string, to be put in my datatable
                    movieDO.Runtime = int.Parse(row["Runtime"].ToString());
                    //Taking the director of the object and puting it in the table
                    movieDO.Director = row["Director"].ToString().Trim();
                    //Taking the description of the object and puting it in the table
                    movieDO.Synopsis = row["Synopsis"].ToString().Trim();
                    //This is where all of the above is added to the table
                    movieList.Add(movieDO);
                }
            }
            catch (Exception exception)
            {
                //If there's an error, it is logged
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);
                
                throw exception;
            }
            finally
            {
                //If there was a connection, close it and get rid of it
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
                else
                {

                }
                //Getting rid of adapter now
                if (adapter != null)
                {
                    adapter.Dispose();
                }
                else
                {

                }
            }
            //Showing the table, using what was put in
            return movieList;
        }

        //Adding a movie method
        public void AddNewMovie(MovieDO movieDO)
        {
            //Setting to null
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                //Instantiating connection
                connectionToSql = new SqlConnection(_ConnectionString);
                //Instantiating my sqlcommand to get my procedure
                storedProcedure = new SqlCommand("ADD_MOVIE", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                //Adding the properties of the object to the properties in the database
                storedProcedure.Parameters.AddWithValue("@Title", movieDO.Title);
                storedProcedure.Parameters.AddWithValue("@Rating", movieDO.Rating);
                storedProcedure.Parameters.AddWithValue("@Runtime", movieDO.Runtime);
                storedProcedure.Parameters.AddWithValue("@Director", movieDO.Director);
                storedProcedure.Parameters.AddWithValue("@Synopsis", movieDO.Synopsis);

                //Opening the connection
                connectionToSql.Open();

                //Not getting anything back
                storedProcedure.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                //If there's an exception, it is logged and thrown here
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                throw exception;
            }
            finally
            {
                //If there was a connection to sql, it is closed and disposed here
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

        //Movie details method
        public MovieDO ViewMovieByMovieId(int IdInput)
        {
            //Setting all to null
            MovieDO movieDO = null;
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataAdapter adapter = null;
            DataTable movieTable = new DataTable();

            try
            {
                //Instantiating connection to sql
                connectionToSql = new SqlConnection(_ConnectionString);
                //Instantiating my sqlcommand to get my procedure
                storedProcedure = new SqlCommand("OBTAIN_MOVIE_BY_MOVIE_ID", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                //Throwing the movieID into the procedure to get the details
                storedProcedure.Parameters.AddWithValue("@MovieID", IdInput);

                //Opening connection
                connectionToSql.Open();

                //Creating my adapter
                adapter = new SqlDataAdapter(storedProcedure);
                //Using my datatable to fill the adapter
                adapter.Fill(movieTable);

                if (movieTable.Rows.Count > 0)
                {
                    //Setting up my table
                    DataRow row = movieTable.Rows[0];
                    //Instantiating my object
                    movieDO = new MovieDO();

                    //Using info from the database, the properties are put into the table
                    movieDO.MovieID = int.Parse(row["MovieID"].ToString());
                    movieDO.Title = row["Title"].ToString().Trim();
                    movieDO.Rating = row["Rating"].ToString().Trim();
                    movieDO.Runtime = int.Parse(row["Runtime"].ToString());
                    movieDO.Director = row["Director"].ToString().Trim();
                    movieDO.Synopsis = row["Synopsis"].ToString().Trim();
                }
                else
                {

                }
            }
            catch (Exception exception)
            {
                //If there's an error, it is logged and thrown
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                throw exception;
            }
            finally
            {
                //If there was a connection, close it and get rid of it
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
                else
                {

                }

                //Getting rid of adapter
                if (adapter != null)
                {
                    adapter.Dispose();
                }
                else
                {

                }
            }
            //Showing the finished movie
            return movieDO;
        }

        //Seeing movies an actor has been in...method
        public List<MovieDO> ViewMoviesByActorID(int IdInput)
        {
            //Setting all to null
            List<MovieDO> movieList = new List<MovieDO>();
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;
            SqlDataAdapter adapter = null;
            DataTable movieTable = new DataTable();

            try
            {
                //Instantiating connection to sql
                connectionToSql = new SqlConnection(_ConnectionString);
                //Instantiating my sqlcommand to get my procedure
                storedProcedure = new SqlCommand("OBTAIN_MOVIE_BY_ACTOR_ID", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                //Throwing the actorID into the procedure to get the details
                storedProcedure.Parameters.AddWithValue("@ActorID", IdInput);

                //Opening connection
                connectionToSql.Open();

                //Instantiating my adapter
                adapter = new SqlDataAdapter(storedProcedure);
                var counter = adapter.Fill(movieTable);

                foreach (DataRow firstRow in movieTable.Rows)
                {
                    MovieDO movieDO = new MovieDO();

                    if (int.TryParse(firstRow["MovieID"].ToString(), out counter))
                    {
                        movieDO.MovieID = int.Parse(firstRow["MovieID"].ToString());
                        movieDO.Title = firstRow["Title"].ToString().Trim();
                        movieDO.Rating = firstRow["Rating"].ToString().Trim();
                        movieDO.Runtime = int.Parse(firstRow["Runtime"].ToString());
                    }
                    else
                    {

                    }

                    movieDO.Director = firstRow["Director"].ToString().Trim();
                    movieDO.Synopsis = firstRow["Synopsis"].ToString().Trim();

                    //Adding the individual movie to the list
                    movieList.Add(movieDO);
                }

            }
            catch (Exception exception)
            {
                //If there's an error, it is logged and thrown
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                throw exception;
            }
            finally
            {
                //If there was a connection to sql, it is closed and disposed here
                if (connectionToSql != null)
                {
                    connectionToSql.Close();
                    connectionToSql.Dispose();
                }
                else
                {

                }

                //Getting rid of adapter
                if (adapter != null)
                {
                    adapter.Dispose();
                }
                else
                {

                }
            }
            //Showing the table, using what was put in
            return movieList;
        }

        //Update specific movie method
        public void UpdateMovieById(MovieDO movieDO)
        {
            //Setting to null
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                //Instantiating the sqlconnection
                connectionToSql = new SqlConnection(_ConnectionString);
                //Instantiating my sqlcommand to get my procedure
                storedProcedure = new SqlCommand("UPDATE_MOVIE_BY_ID", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                //Using info from the database, the properties are put into the table
                storedProcedure.Parameters.AddWithValue("@MovieID", movieDO.MovieID);
                storedProcedure.Parameters.AddWithValue("@Title", movieDO.Title);
                storedProcedure.Parameters.AddWithValue("@Rating", movieDO.Rating);
                storedProcedure.Parameters.AddWithValue("@Runtime", movieDO.Runtime);
                storedProcedure.Parameters.AddWithValue("@Director", movieDO.Director);
                storedProcedure.Parameters.AddWithValue("@Synopsis", movieDO.Synopsis);

                //Opening the connection
                connectionToSql.Open();

                //Not getting anything back
                storedProcedure.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                //If there's an error, it is logged and thrown
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                throw exception;
            }
            finally
            {
                //If there was a connection, close it and get rid of it
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

        //Delete movie method
        public void DeleteMovieByID(int IdInput)
        {
            //Setting to null
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                //Instantiating sqlconnection
                connectionToSql = new SqlConnection(_ConnectionString);
                //Instantiating my sqlcommand to get my procedure
                storedProcedure = new SqlCommand("DELETE_MOVIE_BY_ID", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                //Throwing the movieID into the procedure to get the details
                storedProcedure.Parameters.AddWithValue("MovieID", IdInput);

                //Opening the sql connection
                connectionToSql.Open();

                //Not getting anything back
                storedProcedure.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                //If there's an error, it's logged and thrown
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                throw exception;
            }
            finally
            {
                //If there was a connection, it's closed and disposed here
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

        //Adding an actor to a movie method
        public void AddActorToMovie(MovieActorComboDO comboDO)
        {
            //Setting connection and command to null
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                //Instantiating the connection
                connectionToSql = new SqlConnection(_ConnectionString);
                //Instantiating my sqlcommand to get my procedure
                storedProcedure = new SqlCommand("ADD_ACTOR_TO_MOVIE", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                //Throwing the movie and actorID into the procedure to get the details
                storedProcedure.Parameters.AddWithValue("@MovieID", comboDO.MovieId);
                storedProcedure.Parameters.AddWithValue("@ActorID", comboDO.ActorId);

                //Open connection
                connectionToSql.Open();

                //Not getting anything back
                storedProcedure.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                //Log and throw exception if there's an error
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                throw exception;
            }
            finally
            {
                //If there's a connection, close and dispose
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

        //Average runtime method
        public void AvgRuntime()
        {
            //Setting to null
            SqlConnection connectionToSql = null;
            SqlCommand storedProcedure = null;

            try
            {
                //Instantiating connection
                connectionToSql = new SqlConnection(_ConnectionString);
                //Instantiating my sqlcommand to get my procedure
                storedProcedure = new SqlCommand("AVERAGE", connectionToSql);
                storedProcedure.CommandType = CommandType.StoredProcedure;

                //Opening connection
                connectionToSql.Open();

                //Not getting anything back
                storedProcedure.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                //Log and throw exception if there's an error
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                throw exception;
            }
            finally
            {
                //If there's a connection, close and dispose
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
