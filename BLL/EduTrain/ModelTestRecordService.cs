using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 模拟考试记录
    /// </summary>
    public static class ModelTestRecordService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取模拟考试记录
        /// </summary>
        /// <param name="ModelTestRecordId"></param>
        /// <returns></returns>
        public static Model.Training_ModelTestRecord GetModelTestRecordById(string ModelTestRecordId)
        {
            return db.Training_ModelTestRecord.FirstOrDefault(e => e.ModelTestRecordId == ModelTestRecordId);
        }

        /// <summary>
        /// 根据主键删除培训计划信息
        /// </summary>
        /// <param name="planId"></param>
        public static void DeleteModelTestRecordByModelTestRecordId(string ModelTestRecordId)
        {
            var ModelTestRecord = db.Training_ModelTestRecord.FirstOrDefault(e => e.ModelTestRecordId == ModelTestRecordId);
            if (ModelTestRecord != null)
            {
                var ModelTestRecordItem = from x in db.Training_ModelTestRecordItem where x.ModelTestRecordId == ModelTestRecordId select x;
                if (ModelTestRecordItem.Count() > 0)
                {
                    foreach (var item in ModelTestRecordItem)
                    {
                        BLL.ModelTestRecordItemService.DeleteModelTestRecordItemmByModelTestRecordItemId(item.ModelTestRecordItemId);
                    }
                }
                db.Training_ModelTestRecord.DeleteOnSubmit(ModelTestRecord);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 更新没有结束时间且超时的考试记录
        /// </summary>
        public static void UpdateTestEndTimeNull()
        {
            var testRecord = from x in Funs.DB.Training_ModelTestRecord
                             where !x.TestEndTime.HasValue && x.TestStartTime.Value.AddMinutes(90) < DateTime.Now
                             select x;
            if (testRecord.Count() > 0)
            {
                foreach (var item in testRecord)
                {
                    item.TestEndTime = item.TestStartTime.Value.AddMinutes(100);
                    if (!item.TestScores.HasValue)
                    {
                        item.TestScores = 0;
                    }
                    Funs.DB.SubmitChanges();
                }
            }
        }
    }
}
