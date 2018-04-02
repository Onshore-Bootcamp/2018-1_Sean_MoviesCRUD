using Movies_DAL;
using Movies_DAL.DAOs;
using Movies_DAL.Models;
using MoviesCRUD_MVC.Customs;
using MoviesCRUD_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Movies_BLL;

namespace MoviesCRUD_MVC.Controllers
{
    public class MovieController : Controller
    {
        public MovieController()
        {
            //initializes the connection, so it doesn't keep having to go to the AppSettings
            string connection = ConfigurationManager.ConnectionStrings["DataSource"].ConnectionString;
            _movieDAO = new MovieDAO(connection);
            _actorDAO = new ActorDAO(connection);
        }

        public LoggerPL _Logger = new LoggerPL();

        private readonly MovieDAO _movieDAO;
        private readonly ActorDAO _actorDAO;

        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }

        //Checks session for 3 separate permissions
        [SessionChecks("RoleID", 1, 2, 3)]
        [HttpGet]
        public ActionResult ViewAllMovies()
        {
            //Setting response to null
            ActionResult response = null;
            //Instantiating a new moviePO list
            List<MoviePO> moviePOList = new List<MoviePO>();

            try
            {
                //Equalling my list to my viewall method
                List<MovieDO> movieDOList = _movieDAO.ViewAllMovies();

                foreach (MovieDO movieDO in movieDOList)
                {
                    //Adding the movieDO to the PO list
                    moviePOList.Add(Mapping.Mapper.MovieDOtoPO(movieDO));
                }
                //Setting response to see the list
                response = View(moviePOList);
            }
            catch (Exception exception)
            {
                //If there's an error, it's logged
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);
                //Making sure there's a fallback location in case something goes bad
                response = RedirectToAction("Index", "Movie");
            }
            finally
            {

            }
            //Show whichever response happened
            return response;
        }

        [SessionChecks("RoleID", 1, 2)]
        [HttpGet]
        public ActionResult AddMovie()
        {
            return View();
        }

        [SessionChecks("RoleID", 1, 2)]
        [HttpPost]
        public ActionResult AddMovie(MoviePO form)
        {
            //Starting response is null
            ActionResult response = null;

            if (ModelState.IsValid)
            {
                try
                {
                    //Using the addmovie method, we pass in the object form to be filled out
                    _movieDAO.AddNewMovie(Mapping.Mapper.MoviePOtoDO(form));
                    //After it's filled out, it goes to viewing all movies
                    response = RedirectToAction("ViewAllMovies", "Movie");
                }
                catch (Exception exception)
                {
                    //If there's an error, it's logged
                    _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);
                    //Making sure there's a fallback location in case something goes bad
                    response = RedirectToAction("ViewAllMovies", "Movie");
                }
                finally
                {

                }
            }
            else
            {
                //If the modelstate isn't good, it goes back to the form to be filled
                response = View(form);
            }
            //Show whichever response happened
            return response;
        }

        [SessionChecks("RoleID", 1, 2)]
        [HttpGet]
        public ActionResult AddActorToMovie(int movieID)
        {
            //Setting response to null
            ActionResult response = null;

            try
            {
                //Instantiating my movie/actor object
                MovieActorCombo movieWith = new MovieActorCombo();
                //Setting an actor list, for my dropdown, that only shows those that are NOT in said movie...after passing in the movieID
                List<ActorDO> ActorList = _actorDAO.ActorsNotInMovie(movieID);

                foreach (ActorDO actorDO in ActorList)
                {
                    //Mapping the actorDO to PO
                    ActorPO actorPO = Mapping.Mapper.ActorDOtoPO(actorDO);

                    //Dropdown
                    SelectListItem ActorName = new SelectListItem
                    {
                        //Shows the actor's first and last name
                        Text = actorPO.FullName,
                        Value = actorPO.ActorID.ToString()
                    };
                    //End result of the dropdown, showing all the necessary actors
                    movieWith.ActorDropDown.Add(ActorName);
                }

                //Setting the movieID of a blank movie, to a movie including actors
                movieWith.MovieId = movieID;

                //Return view, to go to the actual view that ties an actor to a movie.
                response = View(movieWith);
            }
            catch (Exception exception)
            {
                //Logs if there's an issue
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                //Making sure there's a fallback location in case something goes bad
                response = RedirectToAction("MovieDetails", "Movie");
            }
            finally
            {

            }
            //Show whichever response happened
            return response;
        }

        [SessionChecks("RoleID", 1, 2)]
        [HttpPost]
        public ActionResult AddActorToMovie(MovieActorCombo combo)
        {
            //Setting the response to null
            ActionResult response = null;

            if (ModelState.IsValid)
            {
                try
                {
                    //Setting the movie/actor combo to DO from PO
                    MovieActorComboDO comboDO = Mapping.Mapper.ComboPOtoDO(combo);
                    //Passing in the DO combo to the add actor to movie method
                    _movieDAO.AddActorToMovie(comboDO);
                }
                catch (Exception exception)
                {
                    //Logs if there's an issue
                    _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                    //Setting view, to go to the actual view that ties an actor to a movie.
                    response = View(combo);
                }
                finally
                {
                    //Setting response to view all the movies
                    response = RedirectToAction("ViewAllMovies", "Movie");
                }
            }
            else
            {
                //Setting response to view the movie you're currently on
                response = View(combo);
            }
            //Show whichever response happened
            return response;
        }

        [SessionChecks("RoleID", 1, 2)]
        [HttpGet]
        public ActionResult UpdateMovie(int Id)
        {
            //Setting response to null
            ActionResult response = null;

            try
            {
                //Getting specific movie, by passing in the ID
                MovieDO movieDO = _movieDAO.ViewMovieByMovieId(Id);
                //MApping the movieDO to PO
                MoviePO moviePO = Mapping.Mapper.MovieDOtoPO(movieDO);

                //Setting response to the view to see the called upon movie
                response = View(moviePO);
            }
            catch (Exception exception)
            {
                //Logs if there's an issue
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                //Setting response to see the movie details regardless
                response = RedirectToAction("MovieDetails", "Movie");
            }
            finally
            {

            }
            //Show whichever response happened
            return response;
        }

        [SessionChecks("RoleID", 1, 2)]
        [HttpPost]
        public ActionResult UpdateMovie(MoviePO form)
        {
            //Setting response to null
            ActionResult response = null;

            if (ModelState.IsValid)
            {
                try
                {
                    //Passing in the form to be updated to the DO
                    MovieDO movieDO = Mapping.Mapper.MoviePOtoDO(form);
                    //Passing in the movie to be updated via the update movie method
                    _movieDAO.UpdateMovieById(movieDO);

                    //Setting response to view the updated movie
                    response = RedirectToAction("MovieDetails", "Movie", new { Id = form.MovieID });
                }
                catch (Exception exception)
                {
                    //Logs if there's an exception
                    _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                    //Setting response to view all movies if there's a problem
                    response = RedirectToAction("ViewAllMovies", "Movie");
                }
                finally
                {

                }
            }
            else
            {
                //If modelstate is a no go, take them back to the form to try again
                response = View(form);
            }
            //Show whichever response happened
            return response;
        }

        [SessionChecks("RoleID", 1, 2, 3)]
        [HttpGet]
        public ActionResult MovieDetails(int Id)
        {
            //Setting response to null
            ActionResult response = null;
            //Instantiating my movie/actor combo object
            MovieWithActorsPO movieWith = new MovieWithActorsPO();

            try
            {
                foreach (ActorDO actor in _actorDAO.ViewActorsByMovieID(Id))
                {
                    //Passing in the actor from DO to PO to the moviewith combo
                    movieWith.Actors.Add(Mapping.Mapper.ActorDOtoPO(actor));
                }
                //Getting the details from the movieID that was passed
                MovieDO movieDO = _movieDAO.ViewMovieByMovieId(Id);
                //Mapping the Movie DO to PO to the movie/actor combo
                movieWith.Movie = Mapping.Mapper.MovieDOtoPO(movieDO);
                //Setting response to view the movie details and actors
                response = View(movieWith);
            }
            catch (Exception exception)
            {
                //Logs if there's an exception
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                //Takes you to all movies if there's an exception
                response = RedirectToAction("ViewAllMovies", "Movie");
            }
            finally
            {

            }
            //Show whichever response happened
            return response;
        }

        [SessionChecks("RoleID", 1, 2)]
        [HttpGet]
        public ActionResult DeleteMovie(int Id)
        {
            //Setting response to null
            ActionResult response = null;

            try
            {
                //Passing in the movieID to the delete method
                _movieDAO.DeleteMovieByID(Id);
                //Setting response to view all after said and done
                response = RedirectToAction("ViewAllMovies", "Movie");
            }
            catch (Exception exception)
            {
                //Logs  if an exception
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                //Setting response to view all after said and done
                response = RedirectToAction("ViewAllMovies", "Movie");
            }
            finally
            {

            }
            //Show whichever response happened
            return response;
        }

        [SessionChecks("RoleID", 1, 2)]
        [HttpGet]
        public ActionResult DeleteActorInMovie(int MovieId, int ActorId)
        {
            //Setting response to null
            ActionResult response = null;

            try
            {
                //Passing in the movie and actor ID to the delete actor from movie method
                _actorDAO.DeleteActorByMovieId(MovieId, ActorId);
                response = RedirectToAction("MovieDetails", "Movie", new { Id = MovieId });
            }
            catch (Exception exception)
            {
                //Logs if an exception
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                //Setting response to view all movies
                response = RedirectToAction("MovieDetails", "Movie", new { Id = MovieId });
            }
            finally
            {

            }
            //Show whichever response happened
            return response;
        }
    }
}