using Movies_DAL;
using Movies_DAL.Models;
using MoviesCRUD_MVC.Customs;
using MoviesCRUD_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;

namespace MoviesCRUD_MVC.Controllers
{
    public class AccountController : Controller
    {
        public AccountController()
        {
            //initializes the connection, so it doesn't keep having to go to the AppSettings
            string connection = ConfigurationManager.ConnectionStrings["DataSource"].ConnectionString;
            _userDAO = new UserDAO(connection);
        }

        public LoggerPL _Logger = new LoggerPL();

        private readonly UserDAO _userDAO;

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginPO form)
        {
            ActionResult response = null;

            if (ModelState.IsValid)
            {
                try
                {
                    UserDO userDO = _userDAO.GetUserByUsername(form.Username);

                    if (userDO == null)
                    {
                        TempData["UserError"] = "Username does not exist";
                        response = View(form);
                    }
                    else
                    {
                        UserPO userPO = Mapping.Mapper.UserDOtoPO(userDO);

                        if (form.Password.Equals(userPO.Password))
                        {
                            Session["Username"] = form.Username;
                            Session["RoleID"] = userPO.RoleID;

                            Session.Timeout = 5;
                            response = RedirectToAction("ViewAllMovies", "Movie");

                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Password does not match any user";
                        }
                    }
                }
                catch (Exception exception)
                {
                    _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                }
                finally
                {

                }
            }
            else
            {

            }
            return response;

        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login", "Account");

        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserPO form)
        {
            ActionResult response = RedirectToAction("ViewAllMovies", "Movie");

            if (ModelState.IsValid)
            {

                try
                {
                    UserDO userDO = _userDAO.GetUserByUsername(form.Username);

                    if (userDO == null)
                    {
                        form.RoleID = 3;
                        userDO = Mapping.Mapper.UserPOtoDO(form);
                        _userDAO.AddUser(userDO);
                        Session["Username"] = form.Username;
                        Session["RoleID"] = 3;
                    }
                    else
                    {
                        TempData["ExistUser"] = "User already exists";
                        response = View(form);
                    }

                }
                catch (Exception exception)
                {
                    _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                }
                finally
                {

                }
            }
            else
            {

            }
            return response;

        }

        private void SetSessionInfo(UserPO info)
        {
            Session["Username"] = info.Username;
            Session["Password"] = info.Password;
            Session.Timeout = 5;
        }

        [SessionChecks("RoleID", 1)]
        [HttpGet]
        public ActionResult ViewAllUsers()
        {
            ActionResult response = null;
            List<UserPO> userPOList = new List<UserPO>();

            try
            {
                List<UserDO> userDOList = _userDAO.ViewAllUsers();

                foreach (UserDO userDO in userDOList)
                {
                    userPOList.Add(Mapping.Mapper.UserDOtoPO(userDO));
                }
                response = View(userPOList);
            }
            catch (Exception exception)
            {
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                response = RedirectToAction("Index", "Account");
            }
            finally
            {

            }
            return response;
        }

        [SessionChecks("RoleID", 1)]
        [HttpGet]
        public ActionResult UpdateUser(string username)
        {
            ActionResult response = null;

            try
            {
                UserDO userDO = _userDAO.GetUserByUsername(username);
                UserPO userPO = Mapping.Mapper.UserDOtoPO(userDO);
                response = View(userPO);
            }
            catch (Exception exception)
            {
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                response = RedirectToAction("UserDetails", "Account");
            }
            finally
            {

            }
            return response;
        }

        [SessionChecks("RoleID", 1)]
        [HttpPost]
        public ActionResult UpdateUser(UserPO form)
        {
            ActionResult response = null;

            if (ModelState.IsValid)
            {
                try
                {
                    UserDO userDO = Mapping.Mapper.UserPOtoDO(form);
                    _userDAO.UpdateUserById(userDO);
                    response = RedirectToAction("UserDetails", "Account", new { Id = form.UserID });
                }
                catch (Exception exception)
                {
                    _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                    response = RedirectToAction("ViewAllUsers", "Account");
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

        [SessionChecks("RoleID", 1)]
        [HttpGet]
        public ActionResult DeleteUser(int Id)
        {
            ActionResult response = null;

            try
            {
                _userDAO.DeleteUserById(Id);
                response = RedirectToAction("ViewAllUsers", "Account");
            }
            catch (Exception exception)
            {
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                response = RedirectToAction("ViewAllUsers", "Account");
            }
            finally
            {

            }
            return response;
        }

        [SessionChecks("RoleID", 1)]
        [HttpGet]
        public ActionResult UserDetails(string username)
        {
            ActionResult response = null;
            UserDO userDO = new UserDO();

            try
            {
                UserPO userPO = Mapping.Mapper.UserDOtoPO(_userDAO.GetUserByUsername(username));

                response = View(userPO);
            }
            catch (Exception exception)
            {
                _Logger.Log("Fatal", exception.Source, exception.TargetSite.ToString(), exception.Message, exception.StackTrace);

                response = RedirectToAction("ViewAllUsers", "Account");
            }
            finally
            {

            }
            return response;
        }

    }
}