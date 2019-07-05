using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 监督评价报告
    /// </summary>
   public static class SubUnitCheckRectifyService
    {
       public static Model.SUBHSSEDB db = Funs.DB;

       /// <summary>
       /// 根据安全监督检查报告id获取监督评价信息
       /// </summary>
       /// <param name="superviseCheckReportId"></param>
       /// <returns></returns>
       public static Model.Supervise_SubUnitCheckRectify GetSubUnitCheckRectifyBySuperviseCheckReportId(string superviseCheckReportId)
       {
           return Funs.DB.Supervise_SubUnitCheckRectify.FirstOrDefault(e => e.SuperviseCheckReportId == superviseCheckReportId);
       }

       /// <summary>
       /// 根据主键获取监督评价信息
       /// </summary>
       /// <param name="subUnitCheckRectifyId"></param>
       /// <returns></returns>
       public static Model.Supervise_SubUnitCheckRectify GetSubUnitCheckRectifyById(string subUnitCheckRectifyId)
       {
           return Funs.DB.Supervise_SubUnitCheckRectify.FirstOrDefault(e => e.SubUnitCheckRectifyId == subUnitCheckRectifyId);
       }

       /// <summary>
       /// 添加监督评价报告
       /// </summary>
       /// <param name="subUnitCheckRectify"></param>
       public static void AddSubUnitCheckRectify(Model.Supervise_SubUnitCheckRectify subUnitCheckRectify)
       {
           Model.SUBHSSEDB db = Funs.DB;
            Model.Supervise_SubUnitCheckRectify newSubUnitCheckRectify = new Model.Supervise_SubUnitCheckRectify
            {
                SubUnitCheckRectifyId = subUnitCheckRectify.SubUnitCheckRectifyId,
                SuperviseCheckReportId = subUnitCheckRectify.SuperviseCheckReportId,
                UnitId = subUnitCheckRectify.UnitId,
                CheckRectType = subUnitCheckRectify.CheckRectType,
                Values1 = subUnitCheckRectify.Values1,
                Values2 = subUnitCheckRectify.Values2,
                Values3 = subUnitCheckRectify.Values3,
                Values4 = subUnitCheckRectify.Values4,
                Values5 = subUnitCheckRectify.Values5,
                Values6 = subUnitCheckRectify.Values6,
                Values7 = subUnitCheckRectify.Values7,
                Values8 = subUnitCheckRectify.Values8,
                AttachUrl = subUnitCheckRectify.AttachUrl,
                UpDateTime = subUnitCheckRectify.UpDateTime,
                CheckEndDate = subUnitCheckRectify.CheckEndDate,
                UpState = subUnitCheckRectify.UpState
            };
            db.Supervise_SubUnitCheckRectify.InsertOnSubmit(newSubUnitCheckRectify);
           db.SubmitChanges();
       }

       /// <summary>
       /// 修改监督评价报告
       /// </summary>
       /// <param name="subUnitCheckRectify"></param>
       public static void UpdateSubUnitCheckRectify(Model.Supervise_SubUnitCheckRectify subUnitCheckRectify)
       {
           Model.SUBHSSEDB db = Funs.DB;
           Model.Supervise_SubUnitCheckRectify newSubUnitCheckRectify = db.Supervise_SubUnitCheckRectify.FirstOrDefault(e => e.SubUnitCheckRectifyId == subUnitCheckRectify.SubUnitCheckRectifyId);
           if (newSubUnitCheckRectify != null)
           {
               newSubUnitCheckRectify.UnitId = subUnitCheckRectify.UnitId;
               newSubUnitCheckRectify.CheckRectType = subUnitCheckRectify.CheckRectType;
               newSubUnitCheckRectify.Values1 = subUnitCheckRectify.Values1;
               newSubUnitCheckRectify.Values2 = subUnitCheckRectify.Values2;
               newSubUnitCheckRectify.Values3 = subUnitCheckRectify.Values3;
               newSubUnitCheckRectify.Values4 = subUnitCheckRectify.Values4;
               newSubUnitCheckRectify.Values5 = subUnitCheckRectify.Values5;
               newSubUnitCheckRectify.Values6 = subUnitCheckRectify.Values6;
               newSubUnitCheckRectify.Values7 = subUnitCheckRectify.Values7;
               newSubUnitCheckRectify.Values8 = subUnitCheckRectify.Values8;
               newSubUnitCheckRectify.AttachUrl = subUnitCheckRectify.AttachUrl;
               newSubUnitCheckRectify.UpDateTime = subUnitCheckRectify.UpDateTime;
               newSubUnitCheckRectify.UpState = subUnitCheckRectify.UpState;
               newSubUnitCheckRectify.CheckEndDate = subUnitCheckRectify.CheckEndDate;
               db.SubmitChanges();
           }
       }
    }
}
