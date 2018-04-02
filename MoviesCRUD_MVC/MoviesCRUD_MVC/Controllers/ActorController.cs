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

namespace MoviesCRUD_MVC.Controllers
{
    public class ActorController : Controller
    {
        public ActorController()
        {
            //initializes the connection, so it doesn't keep having to go to the AppSettings
            string connection = ConfigurationManager.ConnectionStrings["DataSource"].ConnectionString;
            _actorDAO = new ActorDAO(connection);
            _movieDAO = new MovieDAO(connection);
        }

        public LoggerPL _Logger = new LoggerPL();

        private readonly ActorDAO _actorDAO;
        private readonly MovieDAO _movieDAO;

        // GET: Actor
        public ActionResult Index()
        {
            return View();
        }

        [SessionChecks("RoleID", 1, 2, 3)]
        [HttpGet]
        public ActionResult ViewAllActors()
        {
            List<ActorPO> actorPOList = new List<ActorPO>();
            ActionResult response = null;

            try
            {
                List<ActorDO> actorDOList = _actorDAO.ViewAllActors();

                foreach (ActorDO actorDO in actorDOList)
                {
                    actorPOList.Add(Mapping.Mapper.ActorDOtoPO(actorDO));
                }
                response = View(actorPOList);
            }
            catch (Exception exception)
            {
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                response = RedirectToAction("Index", "Actor");
            }
            finally
            {

            }
            return response;
        }

        [SessionChecks("RoleID", 1, 2)]
        [HttpGet]
        public ActionResult AddActor()
        {
            return View();
        }

        [SessionChecks("RoleID", 1, 2)]
        [HttpPost]
        public ActionResult AddActor(ActorPO form)
        {
            ActionResult response = null;

            if (ModelState.IsValid)
            {
                try
                {
                    _actorDAO.AddNewActor(Mapping.Mapper.ActorPOtoDO(form));
                    response = RedirectToAction("ViewAllActors", "Actor");
                }
                catch (Exception exception)
                {
                    _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                    if (exception.Data.Contains("Message"))
                    {
                        ModelState.AddModelError("FirstName", exception.Data["Message"].ToString());
                    }

                    response = View(form);
                }
                finally
                {

                }
            }
            else
            {
                response = View(form);
            }
            return response;
        }

        [SessionChecks("RoleID", 1, 2)]
        [HttpGet]
        public ActionResult UpdateActor(int Id)
        {
            ActionResult response = null;

            try
            {
                ActorDO actorDO = _actorDAO.ViewActorByActorId(Id);
                ActorPO actorPO = Mapping.Mapper.ActorDOtoPO(actorDO);
                response = View(actorPO);
            }
            catch (Exception exception)
            {
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                response = RedirectToAction("ActorDetails", "Actor");
            }
            finally
            {

            }
            return response;
        }

        [SessionChecks("RoleID", 1, 2)]
        [HttpPost]
        public ActionResult UpdateActor(ActorPO form)
        {
            ActionResult response = null;

            if (ModelState.IsValid)
            {
                try
                {
                    ActorDO actorDO = Mapping.Mapper.ActorPOtoDO(form);
                    _actorDAO.UpdateActorById(actorDO);
                    response = RedirectToAction("ActorDetails", "Actor", new { Id = form.ActorID });
                }
                catch (Exception exception)
                {
                    _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                    response = RedirectToAction("ViewAllActors", "Actor");
                }
                finally
                {

                }
            }
            else
            {
                response = View(form);
            }
            return response;
        }

        [SessionChecks("RoleID", 1, 2, 3)]
        [HttpGet]
        public ActionResult ActorDetails(int Id)
        {
            ActionResult response = null;
            ActorWithMoviesPO actorWith = new ActorWithMoviesPO();

            try
            {
                List<MovieDO> movies = _movieDAO.ViewMoviesByActorID(Id);

                if (movies != null)
                {
                    foreach (MovieDO movie in movies)
                    {
                        actorWith.Movies.Add(Mapping.Mapper.MovieDOtoPO(movie));
                    }
                }
                else
                {

                }
                ActorDO actorDO = _actorDAO.ViewActorByActorId(Id);
                actorWith.Actor = Mapping.Mapper.ActorDOtoPO(actorDO);
                response = View(actorWith);
            }
            catch (Exception exception)
            {
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                response = RedirectToAction("ViewAllActors", "Actor");
            }
            finally
            {

            }
            return response;
        }

        [SessionChecks("RoleID", 1, 2)]
        [HttpGet]
        public ActionResult DeleteActor(int Id)
        {
            ActionResult response = null;

            try
            {
                _actorDAO.DeleteActorById(Id);
                response = RedirectToAction("ViewAllActors", "Actor");
            }
            catch (Exception exception)
            {
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                response = RedirectToAction("ViewAllActors", "Actor");
            }
            finally
            {

            }
            return response;
        }

        [SessionChecks("RoleID", 1, 2)]
        [HttpPost]
        public ActionResult DeleteActor(ActorPO form)
        {
            ActionResult response = null;

            try
            {
                _actorDAO.DeleteActorById(Mapping.Mapper.ActorPOtoDO(form).ActorID);

                response = RedirectToAction("ViewAllActors", "Actor");
            }
            catch (Exception exception)
            {
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                response = RedirectToAction("ViewAllActors", "Actor");
            }
            finally
            {

            }
            return response;
        }



    }
}