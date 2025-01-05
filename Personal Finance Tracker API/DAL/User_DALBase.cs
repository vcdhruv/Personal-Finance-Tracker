using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Personal_Finance_Tracker_API.Models;
using System.Data;
using System.Data.Common;

namespace Personal_Finance_Tracker_API.DAL
{
    public class User_DALBase : DAL_Helpers
    {
        #region Sign Up Method
        public bool SignUp(UserModel user)
        {

            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Users_SignUp");
                db.AddInParameter(cmd, "@UserName", DbType.String, user.UserName);
                db.AddInParameter(cmd, "@Email", DbType.String, user.Email);
                db.AddInParameter(cmd, "@PasswordHash", DbType.String, PasswordHashing.HashPassword(user.Password));
                if (Convert.ToBoolean(db.ExecuteNonQuery(cmd)) == true) 
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
        #endregion

        #region Login
        public LoginModel Login(LoginModel login) 
        {
            LoginModel user = new LoginModel();
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Users_Login");
                db.AddInParameter(cmd, "@UserName", DbType.String, login.UserName);
                db.AddInParameter(cmd, "@PasswordHash", DbType.String, PasswordHashing.HashPassword(login.Password));
                IDataReader rd = db.ExecuteReader(cmd);
                if(rd != null)
                {
                    while (rd.Read())
                    {
                        user.UserID = (int)rd["UserID"];
                        user.UserName = rd["UserName"].ToString();
                        user.Email = rd["Email"].ToString();
                    }
                }
                return user;
            }
            catch
            {
                return user;
            }
        }
        #endregion

        #region Change Password Of Specific User
        public bool ChangePassword(ChangePasswordModel cng_password)
        {
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Users_ChangePassword");
                db.AddInParameter(cmd, "@Email", DbType.String, cng_password.Email);
                db.AddInParameter(cmd, "@PasswordHash", DbType.String, PasswordHashing.HashPassword(cng_password.PasswordHash));
                return Convert.ToBoolean(db.ExecuteNonQuery(cmd)) == true ? true : false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
