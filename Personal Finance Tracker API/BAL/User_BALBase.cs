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

        public bool Login(string UserName, string Password)
        {
            User_DALBase dal_user = new User_DALBase();
            if (dal_user.Login(UserName, Password) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
