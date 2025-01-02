using Personal_Finance_Tracker_API.DAL;
using Personal_Finance_Tracker_API.Models;

namespace Personal_Finance_Tracker_API.BAL
{
    public class Report_BALBase
    {
        #region Get Spending Summary
        public List<ReportModel> SpendingSummary(int UserID)
        {
            Report_DALBase report_DALBase = new Report_DALBase();
            return report_DALBase.SpendingSummary(UserID);
        }
        #endregion

        #region Get Savings Progress
        public List<ReportModel> SavingsProgress(int UserID)
        {
            Report_DALBase report_DALBase = new Report_DALBase();
            return report_DALBase.SavingsProgress(UserID);
        }
        #endregion
    }
}
