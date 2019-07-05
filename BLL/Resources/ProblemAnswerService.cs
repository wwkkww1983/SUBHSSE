namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class ProblemAnswerService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 获取 问题及答案信息
        /// </summary>
        /// <param name="problemAnswerId"> 问题及答案Id</param>
        /// <returns></returns>
        public static Model.Resources_ProblemAnswer GetProblemAnswerByProblemAnswerId(string problemAnswerId)
        {
            return Funs.DB.Resources_ProblemAnswer.FirstOrDefault(x => x.ProblemAnswerId == problemAnswerId);
        }

        /// <summary>
        /// 增加 问题及答案
        /// </summary>
        /// <param name="ProblemAnswer"></param>
        public static void AddProblemAnswer(Model.Resources_ProblemAnswer ProblemAnswer)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Resources_ProblemAnswer newProblemAnswer = new Model.Resources_ProblemAnswer
            {
                ProblemAnswerId = ProblemAnswer.ProblemAnswerId,
                Problem = ProblemAnswer.Problem,
                ProblemContent = ProblemAnswer.ProblemContent,
                Answer = ProblemAnswer.Answer,
                AnswerContent = ProblemAnswer.AnswerContent,
                CompileDate = System.DateTime.Now
            };
            db.Resources_ProblemAnswer.InsertOnSubmit(newProblemAnswer);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改 问题及答案信息
        /// </summary>
        /// <param name="ProblemAnswerId"></param>
        /// <param name="SignName"></param>
        /// <param name="def"></param>
        public static void UpdateProblemAnswer(Model.Resources_ProblemAnswer ProblemAnswer)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Resources_ProblemAnswer updateProblemAnswer = db.Resources_ProblemAnswer.FirstOrDefault(e => e.ProblemAnswerId == ProblemAnswer.ProblemAnswerId);
            if (updateProblemAnswer != null)
            {
                updateProblemAnswer.Problem = ProblemAnswer.Problem;
                updateProblemAnswer.ProblemContent = ProblemAnswer.ProblemContent;
                updateProblemAnswer.Answer = ProblemAnswer.Answer;
                updateProblemAnswer.AnswerContent = ProblemAnswer.AnswerContent;
                updateProblemAnswer.CompileDate = System.DateTime.Now;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除 问题及答案
        /// </summary>
        /// <param name="ProblemAnswerId"></param>
        public static void DeleteProblemAnswer(string ProblemAnswerId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Resources_ProblemAnswer deleteProblemAnswer = db.Resources_ProblemAnswer.FirstOrDefault(e => e.ProblemAnswerId == ProblemAnswerId);
            if (deleteProblemAnswer != null)
            {
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(deleteProblemAnswer.ProblemAnswerId);
                db.Resources_ProblemAnswer.DeleteOnSubmit(deleteProblemAnswer);
                db.SubmitChanges();
            }
        }
    }
}