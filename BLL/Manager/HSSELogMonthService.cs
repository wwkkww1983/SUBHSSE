using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class HSSELogMonthService
    {
        /// <summary>
        /// 根据主键获取HSSE日志月
        /// </summary>
        /// <param name="HSSELogMonthId">HSSE日志月主键</param>
        /// <returns></returns>
        public static Model.Manager_HSSELogMonth GetHSSELogMonthByHSSELogMonthId(string HSSELogMonthId)
        {
            return Funs.DB.Manager_HSSELogMonth.FirstOrDefault(x=> x.HSSELogMonthId == HSSELogMonthId);
        }

        /// <summary>
        /// 根据编制人日期项目获取HSSE日志月
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="compileDate">编制日期</param>
        /// <param name="compileMan">编制人</param>
        /// <returns></returns>
        public static Model.Manager_HSSELogMonth GetHSSELogMonthByCompileManDateProjectId(string projectId, DateTime? months, string compileMan)
        {
            return Funs.DB.Manager_HSSELogMonth.FirstOrDefault(x => x.ProjectId == projectId && x.Months.Value.Year == months.Value.Year && x.Months.Value.Month == months.Value.Month && x.CompileMan == compileMan);
        } 
       
        /// <summary>
        /// 增加HSSE日志月
        /// </summary>
        /// <param name="HSSELogMonth">HSSE日志月实体</param>
        public static void AddHSSELogMonth(Model.Manager_HSSELogMonth HSSELogMonth)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_HSSELogMonth newHSSELogMonth = new Model.Manager_HSSELogMonth
            {
                HSSELogMonthId = SQLHelper.GetNewID(typeof(Model.Manager_HSSELogMonth)),
                HSSELogMonthCode = HSSELogMonth.HSSELogMonthCode,
                ProjectId = HSSELogMonth.ProjectId,
                Months = HSSELogMonth.Months,
                CompileMan = HSSELogMonth.CompileMan,
                CompileDate = HSSELogMonth.CompileDate,
                ProjectRange = HSSELogMonth.ProjectRange,
                ManHour = HSSELogMonth.ManHour,
                Rate = HSSELogMonth.Rate,
                RealManHour = HSSELogMonth.RealManHour,
                TotalManHour = HSSELogMonth.TotalManHour,
                Num1 = HSSELogMonth.Num1,
                Num2 = HSSELogMonth.Num2,
                Num3 = HSSELogMonth.Num3,
                Num4 = HSSELogMonth.Num4,
                Num5 = HSSELogMonth.Num5,
                Num6 = HSSELogMonth.Num6,
                Num7 = HSSELogMonth.Num7,
                Num8 = HSSELogMonth.Num8,
                Num9 = HSSELogMonth.Num9,
                Num10 = HSSELogMonth.Num10,
                Num11 = HSSELogMonth.Num11,
                Num12 = HSSELogMonth.Num12,
                Num13 = HSSELogMonth.Num13,
                Num14 = HSSELogMonth.Num14,
                Num15 = HSSELogMonth.Num15
            };
            db.Manager_HSSELogMonth.InsertOnSubmit(newHSSELogMonth);
            db.SubmitChanges();

            ////增加一条编码记录
            BLL.CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(BLL.Const.ProjectHSSELogMonthMenuId, HSSELogMonth.ProjectId, null, HSSELogMonth.HSSELogMonthId, HSSELogMonth.Months);
        }

        /// <summary>
        /// 修改HSSE日志月
        /// </summary>
        /// <param name="HSSELogMonth"></param>
        public static void UpdateHSSELogMonth(Model.Manager_HSSELogMonth HSSELogMonth)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_HSSELogMonth newHSSELogMonth = db.Manager_HSSELogMonth.FirstOrDefault(e => e.HSSELogMonthId == HSSELogMonth.HSSELogMonthId);
            if (newHSSELogMonth != null)
            {
                newHSSELogMonth.HSSELogMonthCode = HSSELogMonth.HSSELogMonthCode;   
                //newHSSELogMonth.Months = HSSELogMonth.Months;
                //newHSSELogMonth.CompileMan = HSSELogMonth.CompileMan;
                newHSSELogMonth.CompileDate = HSSELogMonth.CompileDate;
                newHSSELogMonth.ProjectRange = HSSELogMonth.ProjectRange;
                newHSSELogMonth.ManHour = HSSELogMonth.ManHour;
                newHSSELogMonth.Rate = HSSELogMonth.Rate;
                newHSSELogMonth.RealManHour = HSSELogMonth.RealManHour;
                newHSSELogMonth.TotalManHour = HSSELogMonth.TotalManHour;
                newHSSELogMonth.Num1 = HSSELogMonth.Num1;
                newHSSELogMonth.Num2 = HSSELogMonth.Num2;
                newHSSELogMonth.Num3 = HSSELogMonth.Num3;
                newHSSELogMonth.Num4 = HSSELogMonth.Num4;
                newHSSELogMonth.Num5 = HSSELogMonth.Num5;
                newHSSELogMonth.Num6 = HSSELogMonth.Num6;
                newHSSELogMonth.Num7 = HSSELogMonth.Num7;
                newHSSELogMonth.Num8 = HSSELogMonth.Num8;
                newHSSELogMonth.Num9 = HSSELogMonth.Num9;
                newHSSELogMonth.Num10 = HSSELogMonth.Num10;
                newHSSELogMonth.Num11 = HSSELogMonth.Num11;
                newHSSELogMonth.Num12 = HSSELogMonth.Num12;
                newHSSELogMonth.Num13 = HSSELogMonth.Num13;
                newHSSELogMonth.Num14 = HSSELogMonth.Num14;
                newHSSELogMonth.Num15 = HSSELogMonth.Num15;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除HSSE经理暨HSSE工程师细则
        /// </summary>
        /// <param name="healthId"></param>
        public static void DeleteHSSELogMonthByID(string hsseLogMonthId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_HSSELogMonth hsseLogMonth = db.Manager_HSSELogMonth.FirstOrDefault(e => e.HSSELogMonthId == hsseLogMonthId);
            if (hsseLogMonth != null)
            {
                ///删除编码表记录
                BLL.CodeRecordsService.DeleteCodeRecordsByDataId(hsseLogMonthId);
                //BLL.CommonService.DeleteAttachFileById(healthId);//删除附件
               // BLL.CommonService.DeleteFlowOperateByID(healthId);//删除审核流程
                db.Manager_HSSELogMonth.DeleteOnSubmit(hsseLogMonth);
                db.SubmitChanges();
            }
        }
    }
}
