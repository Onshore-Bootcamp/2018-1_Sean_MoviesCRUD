using MoviesCRUD_MVC.Models;
using Movies_BLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Movies_DAL.Models;
using Movies_DAL.DAOs;

namespace MoviesCRUD_MVC.Mapping
{
    public class Mapper
    {
        public static MoviePO MovieBOtoPO(MovieBO from)
        {
            MoviePO to = new MoviePO();
            to.MovieID = from.MovieID;
            to.Title = from.Title;
            to.Rating = from.Rating;
            to.Runtime = from.Runtime;
            to.Director = from.Director;
            to.Synopsis = from.Synopsis;

            return to;
        }

        public static MoviePO MovieDOtoPO(MovieDO from)
        {
            MoviePO to = new MoviePO();
            to.MovieID = from.MovieID;
            to.Title = from.Title;
            to.Rating = from.Rating;
            to.Runtime = from.Runtime;
            to.Director = from.Director;
            to.Synopsis = from.Synopsis;

            return to;
        }

        public static MovieBO MoviePOtoBO(MoviePO from)
        {
            MovieBO to = new MovieBO();
            to.MovieID = from.MovieID;
            to.Title = from.Title;
            to.Rating = from.Rating;
            to.Runtime = from.Runtime;
            to.Director = from.Director;
            to.Synopsis = from.Synopsis;

            return to;
        }

        public static MovieDO MoviePOtoDO(MoviePO from)
        {
            MovieDO to = new MovieDO();
            to.MovieID = from.MovieID;
            to.Title = from.Title;
            to.Rating = from.Rating;
            to.Runtime = from.Runtime;
            to.Director = from.Director;
            to.Synopsis = from.Synopsis;

            return to;
        }

        public static ActorPO ActorBOtoPO(ActorBO from)
        {
            ActorPO to = new ActorPO();
            to.ActorID = from.ActorID;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.BirthDate = from.BirthDate;
            to.Bio = from.Bio;
            to.Trivia = from.Trivia;
            to.Quotes = from.Quotes;

            return to;
        }

        public static ActorPO ActorDOtoPO(ActorDO from)
        {
            ActorPO to = new ActorPO();
            to.ActorID = from.ActorID;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.BirthDate = from.BirthDate;
            to.Bio = from.Bio;
            to.Trivia = from.Trivia;
            to.Quotes = from.Quotes;

            return to;
        }

        public static ActorBO ActorPOtoBO(ActorPO from)
        {
            ActorBO to = new ActorBO();
            to.ActorID = from.ActorID;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.BirthDate = from.BirthDate;
            to.Bio = from.Bio;
            to.Trivia = from.Trivia;
            to.Quotes = from.Quotes;

            return to;
        }

        public static ActorDO ActorPOtoDO(ActorPO from)
        {
            ActorDO to = new ActorDO();
            to.ActorID = from.ActorID;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.BirthDate = from.BirthDate;
            to.Bio = from.Bio;
            to.Trivia = from.Trivia;
            to.Quotes = from.Quotes;

            return to;
        }

        public static UserPO UserBOtoPO(UserBO from)
        {
            UserPO to = new UserPO();
            to.UserID = from.UserID;
            to.Username = from.Username;
            to.Password = from.Password;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.Email = from.Email;
            to.RoleID = from.RoleID;

            return to;
        }

        public static UserPO UserDOtoPO(UserDO from)
        {
            UserPO to = new UserPO();
            to.UserID = from.UserID;
            to.Username = from.Username;
            to.Password = from.Password;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.Email = from.Email;
            to.RoleID = from.RoleID;

            return to;
        }

        public static UserBO UserPOtoBO(UserPO from)
        {
            UserBO to = new UserBO();
            to.UserID = from.UserID;
            to.Username = from.Username;
            to.Password = from.Password;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.Email = from.Email;
            to.RoleID = from.RoleID;

            return to;
        }

        public static UserDO UserPOtoDO(UserPO from)
        {
            UserDO to = new UserDO();
            to.UserID = from.UserID;
            to.Username = from.Username;
            to.Password = from.Password;
            to.FirstName = from.FirstName;
            to.LastName = from.LastName;
            to.Email = from.Email;
            to.RoleID = from.RoleID;

            return to;
        }

        public static MovieActorCombo ComboDOtoPO(MovieActorComboDO from)
        {
            MovieActorCombo to = new MovieActorCombo();
            to.ActorId = from.ActorId;
            to.MovieId = from.MovieId;

            return to;
        }

        public static MovieActorComboDO ComboPOtoDO(MovieActorCombo from)
        {
            MovieActorComboDO to = new MovieActorComboDO();
            to.ActorId = from.ActorId;
            to.MovieId = from.MovieId;

            return to;
        }
    }
}