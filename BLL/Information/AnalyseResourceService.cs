using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class AnalyseResourceService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        public static List<Model.SpResourceCollection> GetListResourceCollection(List<string> unitIdList, string userName, DateTime? startTime, DateTime? endTime)
        {            
            var resourceCollections = new List<Model.SpResourceCollection>();
            var units = BLL.UnitService.GetUnitDropDownList();
            if (unitIdList.Count()>0)
            {
                units = units.Where(x => unitIdList.Contains(x.UnitId)).ToList();
            }
            ///本单位
            string thisUnitId = string.Empty;
            var unitThis = BLL.CommonService.GetIsThisUnit();
            if (unitThis != null)
            {
                thisUnitId = unitThis.UnitId;
            }
            if (units.Count() > 0)
            {
                foreach (var unitItem in units)
                {
                    List<string> userNameList = new List<string>();
                    ////法律法规
                    var LawRegulationList = from x in Funs.DB.Law_LawRegulationList
                                            where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                            select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        LawRegulationList = LawRegulationList.Where(x => x.CompileMan.Contains(userName));
                    }
                    ////取上传人 姓名列表
                    var nameList1 = LawRegulationList.Select(x => x.CompileMan).Distinct();
                    if (nameList1.Count() > 0)
                    {
                        userNameList.AddRange(nameList1);
                    }

                    ////标准规范
                    var HSSEStandardsList = from x in Funs.DB.Law_HSSEStandardsList
                                            where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                            select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        HSSEStandardsList = HSSEStandardsList.Where(x => x.CompileMan.Contains(userName));
                    }
                    ////取上传人 姓名列表
                    var nameList2 = HSSEStandardsList.Select(x => x.CompileMan).Distinct();
                    if (nameList2.Count() > 0)
                    {
                        userNameList.AddRange(nameList2);
                    }
                    ////规章制度
                    var RulesRegulations = from x in Funs.DB.Law_RulesRegulations
                                            where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                            select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        RulesRegulations = RulesRegulations.Where(x => x.CompileMan.Contains(userName));
                    }
                    var nameList3 = RulesRegulations.Select(x => x.CompileMan).Distinct();
                    if (nameList3.Count() > 0)
                    {
                        userNameList.AddRange(nameList3);
                    }
                    ////管理规定
                    var ManageRule = from x in Funs.DB.Law_ManageRule
                                           where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                           select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        ManageRule = ManageRule.Where(x => x.CompileMan.Contains(userName));
                    }
                    var nameList4 = ManageRule.Select(x => x.CompileMan).Distinct();
                    if (nameList4.Count() > 0)
                    {
                        userNameList.AddRange(nameList4);
                    }

                    ////培训教材
                    var  TrainingItem = from x in Funs.DB.Training_TrainingItem
                                     where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                     select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        TrainingItem = TrainingItem.Where(x => x.CompileMan.Contains(userName));
                    }
                    var nameList5 = TrainingItem.Select(x => x.CompileMan).Distinct();
                    if (nameList5.Count() > 0)
                    {
                        userNameList.AddRange(nameList5);
                    }
                    ////安全试题库
                    var TrainTestDBItem = from x in Funs.DB.Training_TrainTestDBItem
                                     where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                     select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        TrainTestDBItem = TrainTestDBItem.Where(x => x.CompileMan.Contains(userName));
                    }
                    var nameList6 = TrainTestDBItem.Select(x => x.CompileMan).Distinct();
                    if (nameList6.Count() > 0)
                    {
                        userNameList.AddRange(nameList6);
                    }
                    ////事故案例库
                    var AccidentCaseItem = from x in Funs.DB.EduTrain_AccidentCaseItem
                                     where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                     select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        AccidentCaseItem = AccidentCaseItem.Where(x => x.CompileMan.Contains(userName));
                    }
                    ////取上传人 姓名列表
                    var nameList7 = AccidentCaseItem.Select(x => x.CompileMan).Distinct();
                    if (nameList7.Count() > 0)
                    {
                        userNameList.AddRange(nameList7);
                    }
                    ////应知应会库
                    var KnowledgeDB = from x in Funs.DB.Training_KnowledgeItem
                                     where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                     select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        KnowledgeDB = KnowledgeDB.Where(x => x.CompileMan.Contains(userName));
                    }
                    ////取上传人 姓名列表
                    var nameList8 = KnowledgeDB.Select(x => x.CompileMan).Distinct();
                    if (nameList8.Count() > 0)
                    {
                        userNameList.AddRange(nameList8);
                    }
                    ////危险源
                    var HazardList = from x in Funs.DB.Technique_HazardList
                                     where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                     select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        HazardList = HazardList.Where(x => x.CompileMan.Contains(userName));
                    }
                    ////取上传人 姓名列表
                    var nameList9 = HazardList.Select(x => x.CompileMan).Distinct();
                    if (nameList9.Count() > 0)
                    {
                        userNameList.AddRange(nameList9);
                    }

                    ////安全隐患
                    var Rectify = from x in Funs.DB.Technique_RectifyItem
                                     where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                     select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        Rectify = Rectify.Where(x => x.CompileMan.Contains(userName));
                    }
                    ////取上传人 姓名列表
                    var nameList10 = Rectify.Select(x => x.CompileMan).Distinct();
                    if (nameList10.Count() > 0)
                    {
                        userNameList.AddRange(nameList10);
                    }
                    ////HAZOP管理
                    var HAZOP = from x in Funs.DB.Technique_HAZOP
                                     where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                     select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        HAZOP = HAZOP.Where(x => x.CompileMan.Contains(userName));
                    }
                    ////取上传人 姓名列表
                    var nameList11 = HAZOP.Select(x => x.CompileMan).Distinct();
                    if (nameList11.Count() > 0)
                    {
                        userNameList.AddRange(nameList11);
                    }

                    ////AppraiseCount管理
                    var Appraise = from x in Funs.DB.Technique_Appraise
                                where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        Appraise = Appraise.Where(x => x.CompileMan.Contains(userName));
                    }
                    ////取上传人 姓名列表
                    var nameListAppraise = Appraise.Select(x => x.CompileMan).Distinct();
                    if (nameListAppraise.Count() > 0)
                    {
                        userNameList.AddRange(nameListAppraise);
                    }
                    ////安全专家
                    var Expert = from x in Funs.DB.Technique_Expert
                                     where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                     select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        Expert = Expert.Where(x => x.CompileMan.Contains(userName));
                    }
                    ////取上传人 姓名列表
                    var nameList12 = Expert.Select(x => x.CompileMan).Distinct();
                    if (nameList12.Count() > 0)
                    {
                        userNameList.AddRange(nameList12);
                    }

                    ////应急预案
                    var Emergency = from x in Funs.DB.Technique_Emergency
                                     where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                     select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        Emergency = Emergency.Where(x => x.CompileMan.Contains(userName));
                    }
                    ////取上传人 姓名列表
                    var nameList13 = Emergency.Select(x => x.CompileMan).Distinct();
                    if (nameList13.Count() > 0)
                    {
                        userNameList.AddRange(nameList13);
                    }

                    ////专项方案
                    var SpecialScheme = from x in Funs.DB.Technique_SpecialScheme
                                     where (x.UnitId == unitItem.UnitId || (x.UnitId == null && unitItem.UnitId == thisUnitId)) && (!startTime.HasValue || x.CompileDate >= startTime) && (!endTime.HasValue || x.CompileDate <= endTime)
                                     select x;
                    if (!string.IsNullOrEmpty(userName))
                    {
                        SpecialScheme = SpecialScheme.Where(x => x.CompileMan.Contains(userName));
                    }
                    ////取上传人 姓名列表
                    var nameList14 = SpecialScheme.Select(x => x.CompileMan).Distinct();
                    if (nameList14.Count() > 0)
                    {
                        userNameList.AddRange(nameList14);
                    }

                    if (userNameList.Count() > 0)
                    {
                        userNameList = userNameList.Distinct().ToList();
                        foreach (var userItem in userNameList)
                        {
                            Model.SpResourceCollection spItem = new Model.SpResourceCollection
                            {
                                UnitName = unitItem.UnitName,
                                UserName = userItem,

                                ////法律法规
                                LawRegulationCount = LawRegulationList.Where(x => x.CompileMan == userItem).Count()
                            };
                            int LawRegulationPass = LawRegulationList.Where(x => x.CompileMan == userItem && x.IsPass== true).Count();
                            spItem.TotalCount += spItem.LawRegulationCount;
                            spItem.TotalUsedCount += LawRegulationPass;

                            ////标准规范
                            spItem.HSSEStandardListCount = HSSEStandardsList.Where(x => x.CompileMan == userItem).Count();
                            int HSSEStandardListPass = HSSEStandardsList.Where(x => x.CompileMan == userItem && x.IsPass == true).Count();
                            spItem.TotalCount += spItem.HSSEStandardListCount;
                            spItem.TotalUsedCount += HSSEStandardListPass;

                            ////规章制度
                            spItem.RulesRegulationsCount = RulesRegulations.Where(x => x.CompileMan == userItem).Count();
                            int RulesRegulationsPass = RulesRegulations.Where(x => x.CompileMan == userItem && x.IsPass == true).Count();
                            spItem.TotalCount += spItem.RulesRegulationsCount;
                            spItem.TotalUsedCount += RulesRegulationsPass;

                            ////管理规定
                            spItem.ManageRuleCount = ManageRule.Where(x => x.CompileMan == userItem).Count();
                            int ManageRulePass = ManageRule.Where(x => x.CompileMan == userItem && x.IsPass == true).Count();
                            spItem.TotalCount += spItem.ManageRuleCount;
                            spItem.TotalUsedCount += ManageRulePass;

                            ////培训教材
                            spItem.TrainDBCount = TrainingItem.Where(x => x.CompileMan == userItem).Count();
                            int TrainingItemPass = TrainingItem.Where(x => x.CompileMan == userItem && x.IsPass == true).Count();
                            spItem.TotalCount += spItem.TrainDBCount;
                            spItem.TotalUsedCount += TrainingItemPass;

                            ////安全试题库
                            spItem.TrainTestDBCount = TrainTestDBItem.Where(x => x.CompileMan == userItem).Count();
                            int TrainTestDBItemPass = TrainTestDBItem.Where(x => x.CompileMan == userItem && x.IsPass == true).Count();
                            spItem.TotalCount += spItem.TrainTestDBCount;
                            spItem.TotalUsedCount += TrainTestDBItemPass;

                            ////事故案例库
                            spItem.AccidentCaseCount = AccidentCaseItem.Where(x => x.CompileMan == userItem).Count();
                            int AccidentCaseItemPass = AccidentCaseItem.Where(x => x.CompileMan == userItem && x.IsPass == true).Count();
                            spItem.TotalCount += spItem.AccidentCaseCount;
                            spItem.TotalUsedCount += AccidentCaseItemPass;

                            ////应知应会库
                            spItem.KnowledgeDBCount = KnowledgeDB.Where(x => x.CompileMan == userItem).Count();
                            int KnowledgeDBPass = KnowledgeDB.Where(x => x.CompileMan == userItem && x.IsPass == true).Count();
                            spItem.TotalCount += spItem.KnowledgeDBCount;
                            spItem.TotalUsedCount += KnowledgeDBPass;

                            ////危险源
                            spItem.HazardListCount = HazardList.Where(x => x.CompileMan == userItem).Count();
                            int HazardListPass = HazardList.Where(x => x.CompileMan == userItem && x.IsPass == true).Count();
                            spItem.TotalCount += spItem.HazardListCount;
                            spItem.TotalUsedCount += HazardListPass;

                            ////安全隐患
                            spItem.RectifyCount = Rectify.Where(x => x.CompileMan == userItem).Count();
                            int RectifyPass = Rectify.Where(x => x.CompileMan == userItem && x.IsPass == true).Count();
                            spItem.TotalCount += spItem.RectifyCount;
                            spItem.TotalUsedCount += RectifyPass;

                            ////HAZOP管理
                            spItem.HAZOPCount = HAZOP.Where(x => x.CompileMan == userItem).Count();
                            int HAZOPPass = HAZOP.Where(x => x.CompileMan == userItem && x.IsPass == true).Count();
                            spItem.TotalCount += spItem.HAZOPCount;
                            spItem.TotalUsedCount += HAZOPPass;


                            ////AppraiseCount管理
                            spItem.AppraiseCount = Appraise.Where(x => x.CompileMan == userItem).Count();
                            int AppraisePass = Appraise.Where(x => x.CompileMan == userItem && x.IsPass == true).Count();
                            spItem.TotalCount += spItem.AppraiseCount;
                            spItem.TotalUsedCount += AppraisePass;

                            ////安全专家
                            spItem.ExpertCount = Expert.Where(x => x.CompileMan == userItem).Count();
                            int ExpertPass = Expert.Where(x => x.CompileMan == userItem && x.IsPass == true).Count();
                            spItem.TotalCount += spItem.ExpertCount;
                            spItem.TotalUsedCount += ExpertPass;

                            ////应急预案
                            spItem.EmergencyCount = Emergency.Where(x => x.CompileMan == userItem).Count();
                            int EmergencyPass = Emergency.Where(x => x.CompileMan == userItem && x.IsPass == true).Count();
                            spItem.TotalCount += spItem.EmergencyCount;
                            spItem.TotalUsedCount += EmergencyPass;

                            ////专项方案
                            spItem.SpecialSchemeCount = SpecialScheme.Where(x => x.CompileMan == userItem).Count();
                            int SpecialSchemePass = SpecialScheme.Where(x => x.CompileMan == userItem && x.IsPass == true).Count();
                            spItem.TotalCount += spItem.SpecialSchemeCount;
                            spItem.TotalUsedCount += SpecialSchemePass;


                            string rate = string.Empty;
                            if (spItem.TotalCount > 0)
                            {
                                decimal totalUsedRate = Convert.ToDecimal(spItem.TotalUsedCount) / Convert.ToDecimal(spItem.TotalCount);
                                totalUsedRate = Math.Round(totalUsedRate * 100, 2, MidpointRounding.AwayFromZero);
                                if (totalUsedRate == 1)
                                {
                                    rate = "100.00";
                                }
                                else
                                {
                                    rate = totalUsedRate.ToString();
                                }
                            }
                            else
                            {
                                rate = "0";
                            }
                            spItem.TotalUsedRate = rate + "%";
                            resourceCollections.Add(spItem);
                        }
                    }
                    else
                    {
                        Model.SpResourceCollection spResourceCollection = new Model.SpResourceCollection
                        {
                            UnitName = unitItem.UnitName
                        };
                        resourceCollections.Add(spResourceCollection);
                    }
                }
            }

            return resourceCollections;
        }
    }
}
