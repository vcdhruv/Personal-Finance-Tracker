﻿namespace Personal_Finance_Tracker_APIConsume_App.BAL
{
    public class CV
    {
        private static IHttpContextAccessor _httpContextAccessor;

        static CV()
        {
            _httpContextAccessor = new HttpContextAccessor();
        }

        public static string? UserName()
        {
            string? UserName = null;
            if (_httpContextAccessor.HttpContext.Session.GetString("UserName") != null)
            {
                UserName = _httpContextAccessor.HttpContext.Session.GetString("UserName").ToString();
            }
            return UserName;
        }
        public static int? UserID()
        {
            int? UserID = null;
            if (_httpContextAccessor.HttpContext.Session.GetString("UserID") != null)
            {
                UserID = Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("UserID"));
            }
            return UserID;
        }
        public static string? Email()
        {
            string? Email = null;
            if (_httpContextAccessor.HttpContext.Session.GetString("Email") != null)
            {
                Email = _httpContextAccessor.HttpContext.Session.GetString("Email").ToString();
            }
            return Email;
        }
        public static string? Token()
        {
            string? Token = null;
            if (_httpContextAccessor.HttpContext.Session.GetString("Token") != null)
            {
                Token = _httpContextAccessor.HttpContext.Session.GetString("Token").ToString();
            }
            return Token;
        }
    }
}
