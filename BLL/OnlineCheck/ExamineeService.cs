using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ExamineeService
    {
        /// <summary>
        /// 添加考生
        /// </summary>
        /// <param name="examinee"></param>
        public static void AddExaminee(Model.Edu_Online_Examinee examinee)
        {
            Model.SUBHSSEDB db = Funs.DB;

            Model.Edu_Online_Examinee newExaminee = new Model.Edu_Online_Examinee
            {
                ExamineeId = examinee.ExamineeId,
                UserId = examinee.UserId,
                WorkPostId = examinee.WorkPostId,
                ABVolume = examinee.ABVolume
            };


            db.Edu_Online_Examinee.InsertOnSubmit(newExaminee);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改考生
        /// </summary>
        /// <param name="examinee"></param>
        public static void UpdateExaminee(Model.Edu_Online_Examinee examinee)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Edu_Online_Examinee newExaminee = db.Edu_Online_Examinee.First(e => e.ExamineeId == examinee.ExamineeId);
            newExaminee.UserId = examinee.UserId;
            newExaminee.WorkPostId = examinee.WorkPostId;
            newExaminee.ABVolume = examinee.ABVolume;

            db.SubmitChanges();
        }

        /// <summary>
        /// 修改考生是否考完状态
        /// </summary>
        /// <param name="examineeId"></param>
        public static void UpdateExamineeCheck(string examineeId, int? totalScore,DateTime startTime,DateTime endTime)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Edu_Online_Examinee newExaminee = db.Edu_Online_Examinee.First(e => e.ExamineeId == examineeId);
            newExaminee.IsChecked = true;
            newExaminee.TotalScore = totalScore;
            newExaminee.StartTime = startTime;
            newExaminee.EndTime = endTime;
            db.SubmitChanges();
        }


        /// <summary>
        /// 删除考生
        /// </summary>
        /// <param name="EDU_ID"></param>
        public static void DeleteExaminee(string examineeId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Edu_Online_Examinee del = db.Edu_Online_Examinee.First(e => e.ExamineeId == examineeId);
            db.Edu_Online_Examinee.DeleteOnSubmit(del);
            db.SubmitChanges();
        }

        /// <summary>
        /// 添加考生明细
        /// </summary>
        /// <param name="?"></param>
        public static void AddExamineeDetail(Model.Edu_Online_ExamineeDetail examinee)
        {
            Model.SUBHSSEDB db = Funs.DB;

            Model.Edu_Online_ExamineeDetail detail = new Model.Edu_Online_ExamineeDetail();
            string newKeyID = SQLHelper.GetNewID(typeof(Model.Edu_Online_ExamineeDetail));
            detail.ExamineeDetailId = newKeyID;
            detail.ExamineeId = examinee.ExamineeId;
            detail.TestCode = examinee.TestCode;
            detail.TestType = examinee.TestType;
            detail.Chapter = examinee.Chapter;
            detail.ItemType = examinee.ItemType;
            detail.TestScore = examinee.TestScore;
            detail.TestKey = examinee.TestKey;
            detail.AnswerKey = examinee.AnswerKey;

            db.Edu_Online_ExamineeDetail.InsertOnSubmit(detail);
            db.SubmitChanges();
        }


        /// <summary>
        /// 考生答案修改
        /// </summary>
        /// <param name="examineeId"></param>
        /// <param name="testCode"></param>
        /// <param name="answerKey"></param>
        public static void UpdateExamineeDetail(string examineeId, int? testCode, string answerKey)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Edu_Online_ExamineeDetail update = db.Edu_Online_ExamineeDetail.First(e => e.ExamineeId == examineeId && e.TestCode == testCode);
            update.AnswerKey = answerKey;
            db.SubmitChanges();
        }

        /// <summary>
        /// 删除考生所有明细
        /// </summary>
        /// <param name="examineeId"></param>
        public static void DeleteExamineeDetails(string examineeId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var del = db.Edu_Online_ExamineeDetail.Where(e => e.ExamineeId == examineeId);
            db.Edu_Online_ExamineeDetail.DeleteAllOnSubmit(del);
            db.SubmitChanges();
        }

        /// <summary>
        /// 返回考生答案
        /// </summary>
        /// <param name="examineeId"></param>
        /// <param name="testCode"></param>
        /// <returns></returns>
        public static string  GetExamineeAnswer(string examineeId, int testCode)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Edu_Online_ExamineeDetail update = db.Edu_Online_ExamineeDetail.FirstOrDefault(e => e.ExamineeId == examineeId && e.TestCode == testCode);
            return update.AnswerKey;
        }

        public static List<Model.Edu_Online_ExamineeDetail> GetExamineeDetails(string examineeId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            List<Model.Edu_Online_ExamineeDetail> details = db.Edu_Online_ExamineeDetail.Where(e => e.ExamineeId == examineeId).ToList();
            return details;
        }

        /// <summary>
        /// 判断是否存在明细
        /// </summary>
        /// <param name="examineeId"></param>
        /// <param name="testCode"></param>
        /// <returns></returns>
        public static bool IsExistExamineeDetail(string examineeId, int? testCode)
        {
            Model.SUBHSSEDB db = Funs.DB;
            var q = db.Edu_Online_ExamineeDetail.Where(e => e.ExamineeId == examineeId && e.TestCode == testCode);
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据用户ID获取用户考试信息
        /// </summary>
        /// <param name="examineeId"></param>
        /// <returns></returns>
        public static Model.Edu_Online_Examinee GetExaminee(string userId)
        {
            return Funs.DB.Edu_Online_Examinee.FirstOrDefault(e => e.UserId == userId);
        }

        /// <summary>
        /// 根据考生ID获取用户考试信息
        /// </summary>
        /// <param name="examineeId"></param>
        /// <returns></returns>
        public static Model.Edu_Online_Examinee GetExamineeById(string examineeId)
        {
            return Funs.DB.Edu_Online_Examinee.FirstOrDefault(e => e.ExamineeId == examineeId);
        }       
    }
}
