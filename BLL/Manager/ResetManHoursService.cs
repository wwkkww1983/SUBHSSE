using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ResetManHoursService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取人工时清零信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        /// <returns></returns>
        public static Model.Manager_ResetManHours GetResetManHoursByResetManHoursId(string resetManHoursId)
        {
            return (from x in Funs.DB.Manager_ResetManHours where x.ResetManHoursId == resetManHoursId select x).FirstOrDefault();
        }

        public static Model.Manager_ResetManHours GetResetManHoursByAccidentReportId(string accidentReportId)
        {
            return (from x in Funs.DB.Manager_ResetManHours where x.AccidentReportId == accidentReportId select x).FirstOrDefault();
        }

        /// <summary>
        /// 增加人工时清零信息
        /// </summary>
        /// <param name="resetManHours">人工时清零实体</param>
        public static void AddResetManHours(Model.Manager_ResetManHours resetManHours)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Manager_ResetManHours));
            Model.Manager_ResetManHours newResetManHours = new Model.Manager_ResetManHours
            {
                ResetManHoursId = newKeyID,
                ProjectId = resetManHours.ProjectId,
                AccidentTypeId = resetManHours.AccidentTypeId,
                Abstract = resetManHours.Abstract,
                AccidentDate = resetManHours.AccidentDate,
                BeforeManHours = resetManHours.BeforeManHours,
                AccidentReportId = resetManHours.AccidentReportId,
                ProjectManager = resetManHours.ProjectManager,
                HSSEManager = resetManHours.HSSEManager
            };

            db.Manager_ResetManHours.InsertOnSubmit(newResetManHours);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除人工时清零信息
        /// </summary>
        /// <param name="monthReportId">月报告主键</param>
        public static void DeleteResetManHoursByResetManHoursId(string resetManHoursId)
        {
            var q = (from x in db.Manager_ResetManHours where x.ResetManHoursId == resetManHoursId select x).FirstOrDefault();
            db.Manager_ResetManHours.DeleteOnSubmit(q);
            db.SubmitChanges();
        }
    }
}
