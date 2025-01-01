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
        public bool Login(string UserName , string Password) 
        {
            try
            {
                SqlDatabase db = new SqlDatabase(connStr);
                DbCommand cmd = db.GetStoredProcCommand("API_Users_Login");
                db.AddInParameter(cmd, "@UserName", DbType.String, UserName);
                db.AddInParameter(cmd, "@PasswordHash", DbType.String, PasswordHashing.HashPassword(Password));
                if (Convert.ToBoolean(db.ExecuteNonQuery(cmd)))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
