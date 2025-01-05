using Personal_Finance_Tracker_API.DAL;
using Personal_Finance_Tracker_API.Models;

namespace Personal_Finance_Tracker_API.BAL
{
    public class User_BALBase
    {
        #region Sign Up
        public bool SignUp(UserModel user)
        {
            try
            {
                User_DALBase user_DALBase = new User_DALBase();
                if (user_DALBase.SignUp(user))
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

        #region Login
        public LoginModel Login(LoginModel login)
        {
            User_DALBase dal_user = new User_DALBase();
            return dal_user.Login(login);
        }
        #endregion

        #region Change Password Of Specific User
        public bool ChangePassword(ChangePasswordModel cng_password)
        {
            User_DALBase user = new User_DALBase();
            return user.ChangePassword(cng_password);
        }
        #endregion
    }
}
