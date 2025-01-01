using Personal_Finance_Tracker_API.DAL;
using Personal_Finance_Tracker_API.Models;

namespace Personal_Finance_Tracker_API.BAL
{
    public class User_BALBase
    {
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
    }
}
