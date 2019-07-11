using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 模拟考试记录明细
    /// </summary>
    public static class ModelTestRecordItemService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据模拟考试记录Id获取明细信息
        /// </summary>
        /// <param name="ModelTestRecordItemId"></param>
        /// <returns></returns>
        public static Model.Training_ModelTestRecordItem GetModelTestRecordItemModelTestRecordItemId(string ModelTestRecordItemId)
        {
            return db.Training_ModelTestRecordItem.FirstOrDefault(e => e.ModelTestRecordItemId == ModelTestRecordItemId);
        }

        /// <summary>
        /// 根据培训ID删除所有相关明细信息
        /// </summary>
        /// <param name="planId"></param>
        public static void DeleteModelTestRecordItemmByModelTestRecordItemId(string ModelTestRecordItemId)
        {
            var ModelTestRecordItem = db.Training_ModelTestRecordItem.FirstOrDefault(x => x.ModelTestRecordItemId == ModelTestRecordItemId);
            if (ModelTestRecordItem != null)
            {
                db.Training_ModelTestRecordItem.DeleteOnSubmit(ModelTestRecordItem);
                db.SubmitChanges();
            }
        }
    }
}
