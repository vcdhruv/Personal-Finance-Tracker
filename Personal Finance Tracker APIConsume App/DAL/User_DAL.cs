using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using System.Data;
using System.Data.Common;

namespace Personal_Finance_Tracker_APIConsume_App.DAL
{
    public class User_DAL : DAL_Helpers
    {
        public bool IsUserPresentOrNot(string email)
        {
            SqlDatabase db = new SqlDatabase(ConnStr);
            DbCommand cmd = db.GetStoredProcCommand("PR_Users_IsPresentOrNot");
            db.AddInParameter(cmd, "@Email", DbType.String, email);
            int result = (int)db.ExecuteScalar(cmd);
            return result > 0 ? true : false; 
        }
    }
}
