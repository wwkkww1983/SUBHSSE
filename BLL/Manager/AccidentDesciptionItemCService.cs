using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace BLL
{
    public class AccidentDesciptionItemCService
    {
        private static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据月报Id获取管理绩效数据统计
        /// </summary>
        /// <param name="monthReportId"></param>
        /// <returns></returns>
        public static List<Model.Manager_Month_AccidentDesciptionItemC> GetAccidentDesciptionItemByMonthReportId(string monthReportId)
        {
            return (from x in Funs.DB.Manager_Month_AccidentDesciptionItemC where x.MonthReportId == monthReportId orderby x.SortIndex select x).ToList();
        }
        /// <summary>
        /// 添加管理绩效数据统计
        /// </summary>
        /// <param name="item"></param>
        public static void AddAccidentDesciptionItem(Model.Manager_Month_AccidentDesciptionItemC item)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_Month_AccidentDesciptionItemC newItem = new Model.Manager_Month_AccidentDesciptionItemC
            {
                AccidentDesItemId = SQLHelper.GetNewID(typeof(Model.Manager_Month_AccidentDesciptionItemC)),
                MonthReportId = item.MonthReportId,
                Matter = item.Matter,
                Datas = item.Datas,
                SortIndex = item.SortIndex
            };
            db.Manager_Month_AccidentDesciptionItemC.InsertOnSubmit(newItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据月报Id删除相关管理绩效数据统计
        /// </summary>
        /// <param name="monthReportId"></param>
        public static void DeleteAccidentDesciptionItemByMonthReportId(string monthReportId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = (from x in db.Manager_Month_AccidentDesciptionItemC where x.MonthReportId == monthReportId select x).ToList();
            if (q != null)
            {
                db.Manager_Month_AccidentDesciptionItemC.DeleteAllOnSubmit(q);
                db.SubmitChanges();
            }
        }

        public static ListItem[] GetMatterList()
        {
            ListItem[] lis = new ListItem[5];
            lis[0] = new ListItem("轻伤人数");
            lis[1] = new ListItem("重伤人数");
            lis[2] = new ListItem("死亡人数");
            lis[3] = new ListItem("直接经济损失");
            lis[4] = new ListItem("事故损失工时数");
            return lis;
        }
    }
}
