using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using BLL;

namespace FineUIPro.Web.SysManage
{
    public partial class Synchronization : PageBase
    {
        #region 自定义项
        /// <summary>
        /// 本单位
        /// </summary>
        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
            }
        }
        /// <summary>
        /// 本单位
        /// </summary>
        public string UnitName
        {
            get
            {
                return (string)ViewState["UnitName"];
            }
            set
            {
                ViewState["UnitName"] = value;
            }
        }
        #endregion

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var unit = BLL.CommonService.GetIsThisUnit();
                if (unit != null)
                {
                    this.UnitId = unit.UnitId;
                    this.UnitName = unit.UnitName;
                }
            }            
        }

        #region 全选事件
        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAll_Click(object sender, EventArgs e)
        {
            this.CheckedEvent(true);
        }

        /// <summary>
        /// 全不选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUnAll_Click(object sender, EventArgs e)
        {
            this.CheckedEvent(false);
        }

        /// <summary>
        /// 选择方法
        /// </summary>
        /// <param name="isChecked"></param>
        private void CheckedEvent(bool isChecked)
        {
            if (this.TabStrip.ActiveTab == this.formTab)
            {
                this.cbFromVersion.Checked = isChecked;
                this.cbFromUnit.Checked = isChecked;
                this.cbFromUrgeReport.Checked = isChecked;                
                this.cbFromLawRegulation.Checked = isChecked;
                this.cbFromHSSEStandard.Checked = isChecked;
                this.cbFromRulesRegulations.Checked = isChecked;
                this.cbFromManageRule.Checked = isChecked;
                this.cbFromHAZOP.Checked = isChecked;
                this.cbFromAppraise.Checked = isChecked;
                this.cbFromEmergency.Checked = isChecked;
                this.cbFromSpecialScheme.Checked = isChecked;
                this.cbFromTraining.Checked = isChecked;
                this.cbFromTrainingItem.Checked = isChecked;
                this.cbFromTrainTestDB.Checked = isChecked;
                this.cbFromTrainTestDBItem.Checked = isChecked;
                this.cbFromAccidentCase.Checked = isChecked;
                this.cbFromAccidentCaseItem.Checked = isChecked;
                this.cbFromKnowledge.Checked = isChecked;
                this.cbFromKnowledgeItem.Checked = isChecked;
                this.cbFromHazardListType.Checked = isChecked;
                this.cbFromHazardList.Checked = isChecked;
                this.cbFromRectify.Checked = isChecked;
                this.cbFromRectifyItem.Checked = isChecked;
                //this.cbFromExpert.Checked = isChecked;
                this.cbFromCheckRectify.Checked = isChecked;
                this.cbFromCheckInfo_Table8.Checked = isChecked;
                this.cbFromSubUnitReport.Checked = isChecked;
                this.cbFromSubUnitReportItem.Checked = isChecked;

            }
            if (this.TabStrip.ActiveTab == this.toTab)
            {
                this.cbToAccidentCauseReport.Checked = isChecked;
                this.cbToDrillConductedQuarterlyReport.Checked = isChecked;
                this.cbToDrillPlanHalfYearReport.Checked = isChecked;
                this.cbToMillionsMonthlyReport.Checked = isChecked;
                this.cbToSafetyQuarterlyReport.Checked = isChecked;
                this.cbToHAZOP.Checked = isChecked;
                this.cbToAppraise.Checked = isChecked;
                this.cbToEmergency.Checked = isChecked;
                this.cbToSpecialScheme.Checked = isChecked;
                this.cbToLawRegulation.Checked = isChecked;
                this.cbToHSSEStandard.Checked = isChecked;
                this.cbToRulesRegulations.Checked = isChecked;
                this.cbToManageRule.Checked = isChecked;
                this.cbToTrainingItem.Checked = isChecked;
                this.cbToTrainTestDBItem.Checked = isChecked;
                this.cbToAccidentCaseItem.Checked = isChecked;
                this.cbToKnowledgeItem.Checked = isChecked;
                this.cbToHazardList.Checked = isChecked;
                this.cbToRectifyItem.Checked = isChecked;
                //this.cbToExpert.Checked = isChecked;
                this.cbToCheckRectify.Checked = isChecked;
                this.cbToSubUnitReportItem.Checked = isChecked;
                this.cbToHSSEManage.Checked = isChecked;
            }
        }
        #endregion

        #region 同步方法
        /// <summary>
        /// 同步方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnSyn_Click(object sender, EventArgs e)
        {            
            this.SynchData();
            this.CheckedEvent(false);
            ShowNotify("同步完成！", MessageBoxIcon.Success);
        }
        
        /// <summary>
        /// 同步方法
        /// </summary>
        private void SynchData()
        {
            /////创建客户端服务
            var poxy = Web.ServiceProxy.CreateServiceClient();
            #region 从集团公司到企业提取
            
            if (this.cbFromVersion.Checked) ///版本信息
            {
                poxy.GetSys_VersionToSUBCompleted += new EventHandler<BLL.HSSEService.GetSys_VersionToSUBCompletedEventArgs>(poxy_GetSys_VersionToSUBCompleted);
                poxy.GetSys_VersionToSUBAsync();
            }

            if (this.cbFromUnit.Checked) ///单位
            {
                poxy.GetBase_UnitToSUBCompleted += new EventHandler<BLL.HSSEService.GetBase_UnitToSUBCompletedEventArgs>(poxy_GetBase_UnitToSUBCompleted);
                poxy.GetBase_UnitToSUBAsync();
            }
            if (this.cbFromUrgeReport.Checked && !string.IsNullOrEmpty(this.UnitId)) ///催报信息
            {
                poxy.GetInformation_UrgeReportToSUBCompleted += new EventHandler<BLL.HSSEService.GetInformation_UrgeReportToSUBCompletedEventArgs>(poxy_GetInformation_UrgeReportToSUBCompleted);
                poxy.GetInformation_UrgeReportToSUBAsync(this.UnitId);
            }
            if (this.cbFromLawRegulation.Checked) ///法律法规
            {
                poxy.GetLaw_LawRegulationListToSUBCompleted += new EventHandler<BLL.HSSEService.GetLaw_LawRegulationListToSUBCompletedEventArgs>(poxy_GetLaw_LawRegulationListToSUBCompleted);
                poxy.GetLaw_LawRegulationListToSUBAsync();
            }
            if (this.cbFromHSSEStandard.Checked)//标准规范
            {
                poxy.GetLaw_HSSEStandardsListToSUBCompleted += new EventHandler<BLL.HSSEService.GetLaw_HSSEStandardsListToSUBCompletedEventArgs>(poxy_GetLaw_HSSEStandardsListToSUBCompleted);
                poxy.GetLaw_HSSEStandardsListToSUBAsync();
            }
            if (this.cbFromRulesRegulations.Checked)//安全生产规章制度
            {
                poxy.GetLaw_RulesRegulationsToSUBCompleted += new EventHandler<BLL.HSSEService.GetLaw_RulesRegulationsToSUBCompletedEventArgs>(poxy_GetLaw_RulesRegulationsToSUBCompleted);
                poxy.GetLaw_RulesRegulationsToSUBAsync();
            }
            if (this.cbFromManageRule.Checked)//安全管理规定
            {
                poxy.GetLaw_ManageRuleToSUBCompleted += new EventHandler<BLL.HSSEService.GetLaw_ManageRuleToSUBCompletedEventArgs>(poxy_GetLaw_ManageRuleToSUBCompleted);
                poxy.GetLaw_ManageRuleToSUBAsync();
            }
            if (this.cbFromHAZOP.Checked)//HAZOP管理
            {
                poxy.GetTechnique_HAZOPToSUBCompleted += new EventHandler<BLL.HSSEService.GetTechnique_HAZOPToSUBCompletedEventArgs>(poxy_GetTechnique_HAZOPToSUBCompleted);
                poxy.GetTechnique_HAZOPToSUBAsync();
            }
            if (this.cbFromAppraise.Checked)//安全评价
            {
                poxy.GetTechnique_AppraiseToSUBCompleted += new EventHandler<BLL.HSSEService.GetTechnique_AppraiseToSUBCompletedEventArgs>(poxy_GetTechnique_AppraiseToSUBCompleted);
                poxy.GetTechnique_AppraiseToSUBAsync();
            }
            if (this.cbFromEmergency.Checked)//应急预案
            {
                poxy.GetTechnique_EmergencyToSUBCompleted += new EventHandler<BLL.HSSEService.GetTechnique_EmergencyToSUBCompletedEventArgs>(poxy_GetTechnique_EmergencyToSUBCompleted);
                poxy.GetTechnique_EmergencyToSUBAsync();
            }
            if (this.cbToSpecialScheme.Checked)//专项方案
            {
                poxy.GetTechnique_SpecialSchemeToSUBCompleted += new EventHandler<BLL.HSSEService.GetTechnique_SpecialSchemeToSUBCompletedEventArgs>(poxy_GetTechnique_SpecialSchemeToSUBCompleted);
                poxy.GetTechnique_SpecialSchemeToSUBAsync();
            }
            if (this.cbFromTraining.Checked) ///培训教材库类别
            {
                poxy.GetTraining_TrainingListToSUBCompleted += new EventHandler<BLL.HSSEService.GetTraining_TrainingListToSUBCompletedEventArgs>(poxy_GetTraining_TrainingListToSUBCompleted);
                poxy.GetTraining_TrainingListToSUBAsync();
            }
            if (this.cbFromTrainingItem.Checked) ///培训教材库明细
            {
                poxy.GetTraining_TrainingItemListToSUBCompleted += new EventHandler<BLL.HSSEService.GetTraining_TrainingItemListToSUBCompletedEventArgs>(poxy_GetTraining_TrainingItemListToSUBCompleted);
                poxy.GetTraining_TrainingItemListToSUBAsync();
            }
            if (this.cbFromTrainTestDB.Checked) ///安全试题库类别
            {
                poxy.GetTraining_TrainTestDBListToSUBCompleted += new EventHandler<BLL.HSSEService.GetTraining_TrainTestDBListToSUBCompletedEventArgs>(poxy_GetTraining_TrainTestDBListToSUBCompleted);
                poxy.GetTraining_TrainTestDBListToSUBAsync();
            }
            if (this.cbFromTrainTestDBItem.Checked) ///安全试题库明细
            {
                poxy.GetTraining_TrainTestDBItemListToSUBCompleted += new EventHandler<BLL.HSSEService.GetTraining_TrainTestDBItemListToSUBCompletedEventArgs>(poxy_GetTraining_TrainTestDBItemListToSUBCompleted);
                poxy.GetTraining_TrainTestDBItemListToSUBAsync();
            }
            if (this.cbFromAccidentCase.Checked) ///事故案例库类别
            {
                poxy.GetEduTrain_AccidentCaseListToSUBCompleted += new EventHandler<BLL.HSSEService.GetEduTrain_AccidentCaseListToSUBCompletedEventArgs>(poxy_GetEduTrain_AccidentCaseListToSUBCompleted);
                poxy.GetEduTrain_AccidentCaseListToSUBAsync();
            }
            if (this.cbFromAccidentCaseItem.Checked) ///事故案例库明细
            {
                poxy.GetEduTrain_AccidentCaseItemListToSUBCompleted += new EventHandler<BLL.HSSEService.GetEduTrain_AccidentCaseItemListToSUBCompletedEventArgs>(poxy_GetEduTrain_AccidentCaseItemListToSUBCompleted);
                poxy.GetEduTrain_AccidentCaseItemListToSUBAsync();
            }
            if (this.cbFromKnowledge.Checked) ///应知应会库类别
            {
                poxy.GetTraining_KnowledgeListToSUBCompleted += new EventHandler<BLL.HSSEService.GetTraining_KnowledgeListToSUBCompletedEventArgs>(poxy_GetTraining_KnowledgeListToSUBCompleted);
                poxy.GetTraining_KnowledgeListToSUBAsync();
            }
            if (this.cbFromKnowledgeItem.Checked) ///应知应会库明细
            {
                poxy.GetTraining_KnowledgeItemListToSUBCompleted += new EventHandler<BLL.HSSEService.GetTraining_KnowledgeItemListToSUBCompletedEventArgs>(poxy_GetTraining_KnowledgeItemListToSUBCompleted);
                poxy.GetTraining_KnowledgeItemListToSUBAsync();
            }
            if (this.cbFromHazardListType.Checked) ///危险源清单类别
            {
                poxy.GetTechnique_HazardListTypeListToSUBCompleted += new EventHandler<BLL.HSSEService.GetTechnique_HazardListTypeListToSUBCompletedEventArgs>(poxy_GetTechnique_HazardListTypeListToSUBCompleted);
                poxy.GetTechnique_HazardListTypeListToSUBAsync();
            }
            if (this.cbFromHazardList.Checked) ///危险源清单明细
            {
                poxy.GetTechnique_HazardListListToSUBCompleted += new EventHandler<BLL.HSSEService.GetTechnique_HazardListListToSUBCompletedEventArgs>(poxy_GetTechnique_HazardListListToSUBCompleted);
                poxy.GetTechnique_HazardListListToSUBAsync();
            }
            if (this.cbFromRectify.Checked) ///安全隐患类别
            {
                poxy.GetTechnique_RectifyListToSUBCompleted += new EventHandler<BLL.HSSEService.GetTechnique_RectifyListToSUBCompletedEventArgs>(poxy_GetTechnique_RectifyListToSUBCompleted);
                poxy.GetTechnique_RectifyListToSUBAsync();
            }
            if (this.cbFromRectifyItem.Checked) ///安全隐患明细
            {
                poxy.GetTechnique_RectifyItemListToSUBCompleted += new EventHandler<BLL.HSSEService.GetTechnique_RectifyItemListToSUBCompletedEventArgs>(poxy_GetTechnique_RectifyItemListToSUBCompleted);
                poxy.GetTechnique_RectifyItemListToSUBAsync();
            }
            //if (this.cbFromExpert.Checked) ///安全专家
            //{
            //    poxy.GetTechnique_ExpertListToSUBCompleted += new EventHandler<BLL.HSSEService.GetTechnique_ExpertListToSUBCompletedEventArgs>(poxy_GetTechnique_ExpertListToSUBCompleted);
            //    poxy.GetTechnique_ExpertListToSUBAsync();
            //}
            if (this.cbFromCheckRectify.Checked) ///安全监督检查整改
            {
                poxy.GetCheck_CheckRectifyListToSUBCompleted +=new EventHandler<BLL.HSSEService.GetCheck_CheckRectifyListToSUBCompletedEventArgs>(poxy_GetCheck_CheckRectifyListToSUBCompleted);
                poxy.GetCheck_CheckRectifyListToSUBAsync(this.UnitId);
            }
            if (this.cbFromCheckInfo_Table8.Checked) ///安全监督检查报告
            {
                poxy.GetCheck_CheckInfo_Table8ItemListToSUBCompleted += new EventHandler<BLL.HSSEService.GetCheck_CheckInfo_Table8ItemListToSUBCompletedEventArgs>(poxy_GetCheck_CheckInfo_Table8ItemListToSUBCompleted);
                poxy.GetCheck_CheckInfo_Table8ItemListToSUBAsync(this.UnitId);
            }
            if (this.cbFromSubUnitReport.Checked)//企业安全文件上报
            {
                poxy.GetSupervise_SubUnitReportListToSUBCompleted += new EventHandler<BLL.HSSEService.GetSupervise_SubUnitReportListToSUBCompletedEventArgs>(poxy_GetSupervise_SubUnitReportListToSUBCompleted);
                poxy.GetSupervise_SubUnitReportListToSUBAsync();
            }
            if (this.cbFromSubUnitReportItem.Checked)//企业安全文件上报明细
            {
                poxy.GetSupervise_SubUnitReportItemListToSUBCompleted += new EventHandler<BLL.HSSEService.GetSupervise_SubUnitReportItemListToSUBCompletedEventArgs>(poxy_GetSupervise_SubUnitReportItemListToSUBCompleted);
                poxy.GetSupervise_SubUnitReportItemListToSUBAsync(this.UnitId);
            }
            #endregion

            #region 从企业向集团公司上报

            if (!string.IsNullOrEmpty(this.UnitId))
            {
                #region 法律法规从企业上报到集团公司
                if (this.cbToLawRegulation.Checked)
                {
                    poxy.DataInsertLaw_LawRegulationListTableCompleted += new EventHandler<BLL.HSSEService.DataInsertLaw_LawRegulationListTableCompletedEventArgs>(poxy_DataInsertLaw_LawRegulationListTableCompleted);
                    var LawRegulationList = from x in Funs.DB.View_Law_LawRegulationList
                                            join y in Funs.DB.AttachFile on x.LawRegulationId equals y.ToKeyId
                                            where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                                            select new BLL.HSSEService.Law_LawRegulationList
                                            {
                                                LawRegulationId = x.LawRegulationId,
                                                ApprovalDate = x.ApprovalDate,                                               
                                                CompileDate = x.CompileDate,
                                                CompileMan = x.CompileMan,
                                                Description = x.Description,
                                                EffectiveDate = x.EffectiveDate,
                                                LawRegulationCode = x.LawRegulationCode,
                                                LawRegulationName = x.LawRegulationName,
                                                UnitId = this.UnitId,
                                                IsPass = null,
                                                
                                                AttachFileId = y.AttachFileId,
                                                ToKeyId = y.ToKeyId,
                                                AttachSource = y.AttachSource,
                                                AttachUrl = y.AttachUrl,
                                                ////附件转为字节传送
                                                FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                                                LawsRegulationsTypeId = x.LawsRegulationsTypeId,
                                                LawsRegulationsTypeCode = x.LawsRegulationsTypeCode,
                                                LawsRegulationsTypeName = x.LawsRegulationsTypeName,
                                            };
                    poxy.DataInsertLaw_LawRegulationListTableAsync(LawRegulationList.ToList());
                }
                #endregion

                #region 标准规范从企业上报到集团公司
                if (this.cbToHSSEStandard.Checked)
                {

                    poxy.DataInsertLaw_HSSEStandardsListTableCompleted += new EventHandler<BLL.HSSEService.DataInsertLaw_HSSEStandardsListTableCompletedEventArgs>(poxy_DataInsertLaw_HSSEStandardsListTableCompleted);
                    var HSSEStandardsList = from x in Funs.DB.View_HSSEStandardsList
                                            join y in Funs.DB.AttachFile on x.StandardId equals y.ToKeyId
                                            where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                                            select new BLL.HSSEService.Law_HSSEStandardsList
                                            {
                                                StandardId = x.StandardId,
                                                StandardGrade = x.StandardGrade,
                                                StandardNo = x.StandardNo,
                                                StandardName = x.StandardName,
                                                TypeId = x.TypeId,
                                                TypeCode = x.TypeCode,
                                                TypeName = x.TypeName,
                                                IsSelected1 = x.IsSelected1,
                                                IsSelected2 = x.IsSelected2,
                                                IsSelected3 = x.IsSelected3,
                                                IsSelected4 = x.IsSelected4,
                                                IsSelected5 = x.IsSelected5,
                                                IsSelected6 = x.IsSelected6,
                                                IsSelected7 = x.IsSelected7,
                                                IsSelected8 = x.IsSelected8,
                                                IsSelected9 = x.IsSelected9,
                                                IsSelected10 = x.IsSelected10,
                                                IsSelected11 = x.IsSelected11,
                                                IsSelected12 = x.IsSelected12,
                                                IsSelected13 = x.IsSelected13,
                                                IsSelected14 = x.IsSelected14,
                                                IsSelected15 = x.IsSelected15,
                                                IsSelected16 = x.IsSelected16,
                                                IsSelected17 = x.IsSelected17,
                                                IsSelected18 = x.IsSelected18,
                                                IsSelected19 = x.IsSelected19,
                                                IsSelected20 = x.IsSelected20,
                                                IsSelected21 = x.IsSelected21,
                                                IsSelected22 = x.IsSelected22,
                                                IsSelected23 = x.IsSelected23,
                                                IsSelected24 = x.IsSelected24,
                                                IsSelected25 = x.IsSelected25,
                                                IsSelected90 = x.IsSelected90,
                                                CompileMan = x.CompileMan,
                                                CompileDate = x.CompileDate,
                                                IsPass = null,
                                                UnitId = this.UnitId,
                                                AttachFileId = y.AttachFileId,
                                                ToKeyId = y.ToKeyId,
                                                AttachSource = y.AttachSource,
                                                AttachUrl = y.AttachUrl,
                                                ////附件转为字节传送
                                                FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                                            };
                    poxy.DataInsertLaw_HSSEStandardsListTableAsync(HSSEStandardsList.ToList());
                }
                #endregion

                #region 安全生产规章制度从企业上报到集团公司
                if (this.cbToRulesRegulations.Checked)
                {

                    poxy.DataInsertLaw_RulesRegulationsTableCompleted += new EventHandler<BLL.HSSEService.DataInsertLaw_RulesRegulationsTableCompletedEventArgs>(poxy_DataInsertLaw_RulesRegulationsTableCompleted);
                    var RulesRegulations = from x in Funs.DB.View_Law_RulesRegulations
                                           join y in Funs.DB.AttachFile on x.RulesRegulationsId equals y.ToKeyId
                                           where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                                           select new BLL.HSSEService.Law_RulesRegulations
                                           {
                                               RulesRegulationsId = x.RulesRegulationsId,
                                               RulesRegulationsCode = x.RulesRegulationsCode,
                                               RulesRegulationsName = x.RulesRegulationsName,
                                               RulesRegulationsTypeId = x.RulesRegulationsTypeId,
                                               RulesRegulationsTypeCode = x.RulesRegulationsTypeCode,
                                               RulesRegulationsTypeName = x.RulesRegulationsTypeName,
                                               CustomDate = x.CustomDate,
                                               ApplicableScope = x.ApplicableScope,
                                               Remark = x.Remark,
                                               CompileMan = x.CompileMan,
                                               CompileDate = x.CompileDate,
                                               IsPass = null,
                                               UnitId = this.UnitId,
                                               AttachFileId = y.AttachFileId,
                                               ToKeyId = y.ToKeyId,
                                               AttachSource = y.AttachSource,
                                               AttachUrl = y.AttachUrl,
                                               ////附件转为字节传送
                                               FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),
                                           };
                    poxy.DataInsertLaw_RulesRegulationsTableAsync(RulesRegulations.ToList());
                }
                #endregion

                #region 安全管理规定从企业上报到集团公司
                if (this.cbToManageRule.Checked)
                {
                    poxy.DataInsertLaw_ManageRuleTableCompleted += new EventHandler<BLL.HSSEService.DataInsertLaw_ManageRuleTableCompletedEventArgs>(poxy_DataInsertLaw_ManageRuleTableCompleted);
                    var manageRule = from x in Funs.DB.View_Law_ManageRule
                                     join y in Funs.DB.AttachFile on x.ManageRuleId equals y.ToKeyId
                                     where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                                     select new BLL.HSSEService.Law_ManageRule
                                     {
                                         ManageRuleId = x.ManageRuleId,
                                         ManageRuleCode = x.ManageRuleCode,
                                         ManageRuleName = x.ManageRuleName,
                                         ManageRuleTypeId = x.ManageRuleTypeId,
                                         VersionNo = x.VersionNo,
                                         CompileMan = x.CompileMan,
                                         CompileDate = x.CompileDate,
                                         Remark = x.Remark,
                                         IsPass = null,
                                         UnitId = this.UnitId,
                                         AttachFileId = y.AttachFileId,
                                         ToKeyId = y.ToKeyId,
                                         AttachSource = y.AttachSource,
                                         AttachUrl = y.AttachUrl,
                                         ////附件转为字节传送
                                         FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                                     };
                    poxy.DataInsertLaw_ManageRuleTableAsync(manageRule.ToList());
                }
                #endregion

                #region 职工伤亡事故原因分析报表从企业上报到集团公司
                if (this.cbToAccidentCauseReport.Checked)
                {
                    poxy.DataInsertInformation_AccidentCauseReportTableCompleted += new EventHandler<BLL.HSSEService.DataInsertInformation_AccidentCauseReportTableCompletedEventArgs>(poxy_DataInsertInformation_AccidentCauseReportTableCompleted);
                    var report = from x in Funs.DB.Information_AccidentCauseReport
                                 where x.UpState == BLL.Const.UpState_2
                                 select new BLL.HSSEService.Information_AccidentCauseReport
                                 {
                                     AccidentCauseReportId = x.AccidentCauseReportId,
                                     UnitId = this.UnitId,
                                     AccidentCauseReportCode = x.AccidentCauseReportCode,
                                     Year = x.Year,
                                     Month = x.Month,
                                     DeathAccident = x.DeathAccident,
                                     DeathToll = x.DeathToll,
                                     InjuredAccident = x.InjuredAccident,
                                     InjuredToll = x.InjuredToll,
                                     MinorWoundAccident = x.MinorWoundAccident,
                                     MinorWoundToll = x.MinorWoundToll,
                                     AverageTotalHours = x.AverageTotalHours,
                                     AverageManHours = x.AverageManHours,
                                     TotalLossMan = x.TotalLossMan,
                                     LastMonthLossHoursTotal = x.LastMonthLossHoursTotal,
                                     KnockOffTotal = x.KnockOffTotal,
                                     DirectLoss = x.DirectLoss,
                                     IndirectLosses = x.IndirectLosses,
                                     TotalLoss = x.TotalLoss,
                                     TotalLossTime = x.TotalLossTime,
                                     FillCompanyPersonCharge = x.FillCompanyPersonCharge,
                                     TabPeople = x.TabPeople,
                                     AuditPerson = x.AuditPerson,
                                     FillingDate = x.FillingDate,
                                 };

                    var reportItem = from x in Funs.DB.Information_AccidentCauseReportItem
                                     join y in Funs.DB.Information_AccidentCauseReport
                                     on x.AccidentCauseReportId equals y.AccidentCauseReportId
                                     where y.UpState == BLL.Const.UpState_2
                                     select new BLL.HSSEService.Information_AccidentCauseReportItem
                                     {
                                         AccidentCauseReportItemId = x.AccidentCauseReportItemId,
                                         AccidentCauseReportId = x.AccidentCauseReportId,
                                         AccidentType = x.AccidentType,
                                         TotalDeath = x.TotalDeath,
                                         TotalInjuries = x.TotalInjuries,
                                         TotalMinorInjuries = x.TotalMinorInjuries,
                                         Death1 = x.Death1,
                                         Injuries1 = x.Injuries1,
                                         MinorInjuries1 = x.MinorInjuries1,
                                         Death2 = x.Death2,
                                         Injuries2 = x.Injuries2,
                                         MinorInjuries2 = x.MinorInjuries2,
                                         Death3 = x.Death3,
                                         Injuries3 = x.Injuries3,
                                         MinorInjuries3 = x.MinorInjuries3,
                                         Death4 = x.Death4,
                                         Injuries4 = x.Injuries4,
                                         MinorInjuries4 = x.MinorInjuries4,
                                         Death5 = x.Death5,
                                         Injuries5 = x.Injuries5,
                                         MinorInjuries5 = x.MinorInjuries5,
                                         Death6 = x.Death6,
                                         Injuries6 = x.Injuries6,
                                         MinorInjuries6 = x.MinorInjuries6,
                                         Death7 = x.Death7,
                                         Injuries7 = x.Injuries7,
                                         MinorInjuries7 = x.MinorInjuries7,
                                         Death8 = x.Death8,
                                         Injuries8 = x.Injuries8,
                                         MinorInjuries8 = x.MinorInjuries8,
                                         Death9 = x.Death9,
                                         Injuries9 = x.Injuries9,
                                         MinorInjuries9 = x.MinorInjuries9,
                                         Death10 = x.Death10,
                                         Injuries10 = x.Injuries10,
                                         MinorInjuries10 = x.MinorInjuries10,
                                         Death11 = x.Death11,
                                         Injuries11 = x.Injuries11,
                                         MinorInjuries11 = x.MinorInjuries11,
                                     };
                    poxy.DataInsertInformation_AccidentCauseReportTableAsync(report.ToList(), reportItem.ToList());
                }
                #endregion

                #region 应急演练开展情况季报表从企业上报到集团公司
                if (this.cbToDrillConductedQuarterlyReport.Checked)
                {
                    poxy.DataInsertInformation_DrillConductedQuarterlyReportTableCompleted += new EventHandler<BLL.HSSEService.DataInsertInformation_DrillConductedQuarterlyReportTableCompletedEventArgs>(poxy_DataInsertInformation_DrillConductedQuarterlyReportTableCompleted);
                    var report = from x in Funs.DB.Information_DrillConductedQuarterlyReport
                                 where x.UpState == BLL.Const.UpState_2
                                 select new BLL.HSSEService.Information_DrillConductedQuarterlyReport
                                 {
                                     DrillConductedQuarterlyReportId = x.DrillConductedQuarterlyReportId,
                                     UnitId = this.UnitId,
                                     ReportDate = x.ReportDate,
                                     Quarter = x.Quarter,
                                     YearId = x.YearId,
                                     CompileMan = x.CompileMan,
                                 };

                    var reportItem = from x in Funs.DB.Information_DrillConductedQuarterlyReportItem
                                     join y in Funs.DB.Information_DrillConductedQuarterlyReport
                                     on x.DrillConductedQuarterlyReportId equals y.DrillConductedQuarterlyReportId
                                     where y.UpState == BLL.Const.UpState_2
                                     select new BLL.HSSEService.Information_DrillConductedQuarterlyReportItem
                                     {
                                         DrillConductedQuarterlyReportItemId = x.DrillConductedQuarterlyReportItemId,
                                         DrillConductedQuarterlyReportId = x.DrillConductedQuarterlyReportId,
                                         IndustryType = x.IndustryType,
                                         TotalConductCount = x.TotalConductCount,
                                         TotalPeopleCount = x.TotalPeopleCount,
                                         TotalInvestment = x.TotalInvestment,
                                         HQConductCount = x.HQConductCount,
                                         HQPeopleCount = x.HQPeopleCount,
                                         HQInvestment = x.HQInvestment,
                                         BasicConductCount = x.BasicConductCount,
                                         BasicPeopleCount = x.BasicPeopleCount,
                                         BasicInvestment = x.BasicInvestment,
                                         ComprehensivePractice = x.ComprehensivePractice,
                                         CPScene = x.CPScene,
                                         CPDesktop = x.CPDesktop,
                                         SpecialDrill = x.SpecialDrill,
                                         SDScene = x.SDScene,
                                         SDDesktop = x.SDDesktop,
                                         SortIndex = x.SortIndex,
                                     };
                    poxy.DataInsertInformation_DrillConductedQuarterlyReportTableAsync(report.ToList(), reportItem.ToList());
                }
                #endregion

                #region 应急演练工作计划半年报表从企业上报到集团公司
                if (this.cbToDrillPlanHalfYearReport.Checked)
                {
                    poxy.DataInsertInformation_DrillPlanHalfYearReportTableCompleted += new EventHandler<BLL.HSSEService.DataInsertInformation_DrillPlanHalfYearReportTableCompletedEventArgs>(poxy_DataInsertInformation_DrillPlanHalfYearReportTableCompleted);
                    var report = from x in Funs.DB.Information_DrillPlanHalfYearReport
                                 where x.UpState == BLL.Const.UpState_2
                                 select new BLL.HSSEService.Information_DrillPlanHalfYearReport
                                 {
                                     DrillPlanHalfYearReportId = x.DrillPlanHalfYearReportId,
                                     UnitId = this.UnitId,
                                     CompileMan = x.CompileMan,
                                     CompileDate = x.CompileDate,
                                     YearId = x.YearId,
                                     HalfYearId = x.HalfYearId,
                                     Telephone = x.Telephone,
                                 };

                    var reportItem = from x in Funs.DB.Information_DrillPlanHalfYearReportItem
                                     join y in Funs.DB.Information_DrillPlanHalfYearReport
                                     on x.DrillPlanHalfYearReportId equals y.DrillPlanHalfYearReportId
                                     where y.UpState == BLL.Const.UpState_2
                                     select new BLL.HSSEService.Information_DrillPlanHalfYearReportItem
                                     {
                                         DrillPlanHalfYearReportItemId = x.DrillPlanHalfYearReportItemId,
                                         DrillPlanHalfYearReportId = x.DrillPlanHalfYearReportId,
                                         DrillPlanName = x.DrillPlanName,
                                         OrganizationUnit = x.OrganizationUnit,
                                         DrillPlanDate = x.DrillPlanDate,
                                         AccidentScene = x.AccidentScene,
                                         ExerciseWay = x.ExerciseWay,
                                         SortIndex = x.SortIndex,
                                     };
                    poxy.DataInsertInformation_DrillPlanHalfYearReportTableAsync(report.ToList(), reportItem.ToList());
                }
                #endregion

                #region 百万工时安全统计月报表从企业上报到集团公司
                if (this.cbToMillionsMonthlyReport.Checked)
                {
                    poxy.DataInsertInformation_MillionsMonthlyReportTableCompleted += new EventHandler<BLL.HSSEService.DataInsertInformation_MillionsMonthlyReportTableCompletedEventArgs>(poxy_DataInsertInformation_MillionsMonthlyReportTableCompleted);
                    var report = from x in Funs.DB.Information_MillionsMonthlyReport
                                 where x.UpState == BLL.Const.UpState_2
                                 select new BLL.HSSEService.Information_MillionsMonthlyReport
                                 {
                                     MillionsMonthlyReportId = x.MillionsMonthlyReportId,
                                     UnitId = this.UnitId,
                                     Year = x.Year,
                                     Month = x.Month,
                                     FillingMan = x.FillingMan,
                                     FillingDate = x.FillingDate,
                                     DutyPerson = x.DutyPerson,
                                     RecordableIncidentRate = x.RecordableIncidentRate,
                                     LostTimeRate = x.LostTimeRate,
                                     LostTimeInjuryRate = x.LostTimeInjuryRate,
                                     DeathAccidentFrequency = x.DeathAccidentFrequency,
                                     AccidentMortality = x.AccidentMortality,
                                 };

                    var reportItem = from x in Funs.DB.Information_MillionsMonthlyReportItem
                                     join y in Funs.DB.Information_MillionsMonthlyReport
                                     on x.MillionsMonthlyReportId equals y.MillionsMonthlyReportId
                                     where y.UpState == BLL.Const.UpState_2
                                     select new BLL.HSSEService.Information_MillionsMonthlyReportItem
                                     {
                                         MillionsMonthlyReportItemId = x.MillionsMonthlyReportItemId,
                                         MillionsMonthlyReportId = x.MillionsMonthlyReportId,
                                         SortIndex = x.SortIndex,
                                         Affiliation = x.Affiliation,
                                         Name = x.Name,
                                         PostPersonNum = x.PostPersonNum,
                                         SnapPersonNum = x.SnapPersonNum,
                                         ContractorNum = x.ContractorNum,
                                         SumPersonNum = x.SumPersonNum,
                                         TotalWorkNum = x.TotalWorkNum,
                                         SeriousInjuriesNum = x.SeriousInjuriesNum,
                                         SeriousInjuriesPersonNum = x.SeriousInjuriesPersonNum,
                                         SeriousInjuriesLossHour = x.SeriousInjuriesLossHour,
                                         MinorAccidentNum = x.MinorAccidentNum,
                                         MinorAccidentPersonNum = x.MinorAccidentPersonNum,
                                         MinorAccidentLossHour = x.MinorAccidentLossHour,
                                         OtherAccidentNum = x.OtherAccidentNum,
                                         OtherAccidentPersonNum = x.OtherAccidentPersonNum,
                                         OtherAccidentLossHour = x.OtherAccidentLossHour,
                                         RestrictedWorkPersonNum = x.RestrictedWorkPersonNum,
                                         RestrictedWorkLossHour = x.RestrictedWorkLossHour,
                                         MedicalTreatmentPersonNum = x.MedicalTreatmentPersonNum,
                                         MedicalTreatmentLossHour = x.MedicalTreatmentLossHour,
                                         FireNum = x.FireNum,
                                         ExplosionNum = x.ExplosionNum,
                                         TrafficNum = x.TrafficNum,
                                         EquipmentNum = x.EquipmentNum,
                                         QualityNum = x.QualityNum,
                                         OtherNum = x.OtherNum,
                                         FirstAidDressingsNum = x.FirstAidDressingsNum,
                                         AttemptedEventNum = x.AttemptedEventNum,
                                         LossDayNum = x.LossDayNum,
                                     };
                    poxy.DataInsertInformation_MillionsMonthlyReportTableAsync(report.ToList(), reportItem.ToList());
                }
                #endregion

                #region 安全生产数据季报从企业上报到集团公司
                if (this.cbToSafetyQuarterlyReport.Checked)
                {
                    poxy.DataInsertInformation_SafetyQuarterlyReportTableCompleted += new EventHandler<BLL.HSSEService.DataInsertInformation_SafetyQuarterlyReportTableCompletedEventArgs>(poxy_DataInsertInformation_SafetyQuarterlyReportTableCompleted);
                    var report = from x in Funs.DB.Information_SafetyQuarterlyReport
                                 where x.UpState == BLL.Const.UpState_2
                                 select new BLL.HSSEService.Information_SafetyQuarterlyReport
                                 {
                                     SafetyQuarterlyReportId = x.SafetyQuarterlyReportId,
                                     UnitId = this.UnitId,
                                     YearId = x.YearId,
                                     Quarters = x.Quarters,
                                     TotalInWorkHours = x.TotalInWorkHours,
                                     TotalInWorkHoursRemark = x.TotalInWorkHoursRemark,
                                     TotalOutWorkHours = x.TotalOutWorkHours,
                                     TotalOutWorkHoursRemark = x.TotalOutWorkHoursRemark,
                                     WorkHoursLossRate = x.WorkHoursLossRate,
                                     WorkHoursLossRateRemark = x.WorkHoursLossRateRemark,
                                     WorkHoursAccuracy = x.WorkHoursAccuracy,
                                     WorkHoursAccuracyRemark = x.WorkHoursAccuracyRemark,
                                     MainBusinessIncome = x.MainBusinessIncome,
                                     MainBusinessIncomeRemark = x.MainBusinessIncomeRemark,
                                     ConstructionRevenue = x.ConstructionRevenue,
                                     ConstructionRevenueRemark = x.ConstructionRevenueRemark,
                                     UnitTimeIncome = x.UnitTimeIncome,
                                     UnitTimeIncomeRemark = x.UnitTimeIncomeRemark,
                                     BillionsOutputMortality = x.BillionsOutputMortality,
                                     BillionsOutputMortalityRemark = x.BillionsOutputMortalityRemark,
                                     MajorFireAccident = x.MajorFireAccident,
                                     MajorFireAccidentRemark = x.MajorFireAccidentRemark,
                                     MajorEquipAccident = x.MajorEquipAccident,
                                     MajorEquipAccidentRemark = x.MajorEquipAccidentRemark,
                                     AccidentFrequency = x.AccidentFrequency,
                                     AccidentFrequencyRemark = x.AccidentFrequencyRemark,
                                     SeriousInjuryAccident = x.SeriousInjuryAccident,
                                     SeriousInjuryAccidentRemark = x.SeriousInjuryAccidentRemark,
                                     FireAccident = x.FireAccident,
                                     FireAccidentRemark = x.FireAccidentRemark,
                                     EquipmentAccident = x.EquipmentAccident,
                                     EquipmentAccidentRemark = x.EquipmentAccidentRemark,
                                     PoisoningAndInjuries = x.PoisoningAndInjuries,
                                     PoisoningAndInjuriesRemark = x.PoisoningAndInjuriesRemark,
                                     ProductionSafetyInTotal = x.ProductionSafetyInTotal,
                                     ProductionSafetyInTotalRemark = x.ProductionSafetyInTotalRemark,
                                     ProtectionInput = x.ProtectionInput,
                                     ProtectionInputRemark = x.ProtectionInputRemark,
                                     LaboAndHealthIn = x.LaboAndHealthIn,
                                     LaborAndHealthInRemark = x.LaborAndHealthInRemark,
                                     TechnologyProgressIn = x.TechnologyProgressIn,
                                     TechnologyProgressInRemark = x.TechnologyProgressInRemark,
                                     EducationTrainIn = x.EducationTrainIn,
                                     EducationTrainInRemark = x.EducationTrainInRemark,
                                     ProjectCostRate = x.ProjectCostRate,
                                     ProjectCostRateRemark = x.ProjectCostRateRemark,
                                     ProductionInput = x.ProductionInput,
                                     ProductionInputRemark = x.ProductionInputRemark,
                                     Revenue = x.Revenue,
                                     RevenueRemark = x.RevenueRemark,
                                     FullTimeMan = x.FullTimeMan,
                                     FullTimeManRemark = x.FullTimeManRemark,
                                     FullTimeManAttachUrl = x.FullTimeManAttachUrl,
                                     PMMan = x.PMMan,
                                     PMManRemark = x.PMManRemark,
                                     PMManAttachUrl = x.PMManAttachUrl,
                                     CorporateDirectorEdu = x.CorporateDirectorEdu,
                                     CorporateDirectorEduRemark = x.CorporateDirectorEduRemark,
                                     ProjectLeaderEdu = x.ProjectLeaderEdu,
                                     ProjectLeaderEduRemark = x.ProjectLeaderEduRemark,
                                     FullTimeEdu = x.FullTimeEdu,
                                     FullTimeEduRemark = x.FullTimeEduRemark,
                                     ThreeKidsEduRate = x.ThreeKidsEduRate,
                                     ThreeKidsEduRateRemark = x.ThreeKidsEduRateRemark,
                                     UplinReportRate = x.UplinReportRate,
                                     UplinReportRateRemark = x.UplinReportRateRemark,
                                     Remarks = x.Remarks,
                                     CompileMan = x.CompileMan,
                                     ////附件转为字节传送
                                     FullTimeManAttachUrlFileContext = FileStructService.GetFileStructByAttachUrl(x.FullTimeManAttachUrl),
                                     PMManAttachUrlFileContext = FileStructService.GetFileStructByAttachUrl(x.PMManAttachUrl),
                                     KeyEquipmentTotal = x.KeyEquipmentTotal,
                                     KeyEquipmentTotalRemark = x.KeyEquipmentTotalRemark,
                                     KeyEquipmentReportCount = x.KeyEquipmentReportCount,
                                     KeyEquipmentReportCountRemark = x.KeyEquipmentReportCountRemark,
                                     ChemicalAreaProjectCount = x.ChemicalAreaProjectCount,
                                     ChemicalAreaProjectCountRemark = x.ChemicalAreaProjectCountRemark,
                                     HarmfulMediumCoverCount = x.HarmfulMediumCoverCount,
                                     HarmfulMediumCoverCountRemark = x.HarmfulMediumCoverCountRemark,
                                     HarmfulMediumCoverRate = x.HarmfulMediumCoverRate,
                                     HarmfulMediumCoverRateRemark = x.HarmfulMediumCoverRateRemark
                                 };
                    poxy.DataInsertInformation_SafetyQuarterlyReportTableAsync(report.ToList());
                }
                #endregion

                #region HAZOP管理从企业上报到集团公司
                if (this.cbToHAZOP.Checked)
                {
                    poxy.DataInsertTechnique_HAZOPTableCompleted += new EventHandler<BLL.HSSEService.DataInsertTechnique_HAZOPTableCompletedEventArgs>(poxy_DataInsertTechnique_HAZOPTableCompleted);
                    var hazop = from x in Funs.DB.View_Technique_HAZOP
                                join y in Funs.DB.AttachFile on x.HAZOPId equals y.ToKeyId
                                where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                                select new BLL.HSSEService.Technique_HAZOP
                                {
                                    HAZOPId = x.HAZOPId,
                                    UnitId = this.UnitId,
                                    Abstract = x.Abstract,
                                    HAZOPDate = x.HAZOPDate,
                                    HAZOPTitle = x.HAZOPTitle,
                                    CompileMan = x.CompileMan,
                                    CompileDate = x.CompileDate,
                                    IsPass = null,
                                    AttachFileId = y.AttachFileId,
                                    ToKeyId = y.ToKeyId,
                                    AttachSource = y.AttachSource,
                                    AttachUrl = y.AttachUrl,
                                    ////附件转为字节传送
                                    FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                                };
                    poxy.DataInsertTechnique_HAZOPTableAsync(hazop.ToList());
                }
                #endregion

                #region 安全评价从企业上报到集团公司
                if (this.cbToAppraise.Checked)
                {
                    poxy.DataInsertTechnique_AppraiseTableCompleted += new EventHandler<BLL.HSSEService.DataInsertTechnique_AppraiseTableCompletedEventArgs>(poxy_DataInsertTechnique_AppraiseTableCompleted);
                    var appraise = from x in Funs.DB.View_Technique_Appraise
                                   join y in Funs.DB.AttachFile on x.AppraiseId equals y.ToKeyId
                                   where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                                   select new BLL.HSSEService.Technique_Appraise
                                   {
                                       AppraiseId = x.AppraiseId,
                                       AppraiseCode = x.AppraiseCode,
                                       AppraiseTitle = x.AppraiseTitle,
                                       Abstract = x.Abstract,
                                       AppraiseDate = x.AppraiseDate,
                                       ArrangementPerson = x.ArrangementPerson,
                                       ArrangementDate = x.ArrangementDate,
                                       CompileMan = x.CompileMan,
                                       CompileDate = x.CompileDate,
                                       UnitId = this.UnitId,
                                       IsPass = null,
                                       AttachFileId = y.AttachFileId,
                                       ToKeyId = y.ToKeyId,
                                       AttachSource = y.AttachSource,
                                       AttachUrl = y.AttachUrl,
                                       ////附件转为字节传送
                                       FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                                   };
                    poxy.DataInsertTechnique_AppraiseTableAsync(appraise.ToList());
                }
                #endregion

                #region 应急预案从企业上报到集团公司
                if (this.cbToEmergency.Checked)
                {
                    poxy.DataInsertTechnique_EmergencyTableCompleted += new EventHandler<BLL.HSSEService.DataInsertTechnique_EmergencyTableCompletedEventArgs>(poxy_DataInsertTechnique_EmergencyTableCompleted);
                    var emergency = from x in Funs.DB.View_Technique_Emergency
                                    join y in Funs.DB.AttachFile on x.EmergencyId equals y.ToKeyId
                                    where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                                    select new BLL.HSSEService.Technique_Emergency
                                    {
                                        EmergencyId = x.EmergencyId,
                                        EmergencyTypeId = x.EmergencyTypeId,
                                        EmergencyCode = x.EmergencyCode,
                                        EmergencyName = x.EmergencyName,
                                        Summary = x.Summary,
                                        Remark = x.Remark,
                                        CompileMan = x.CompileMan,
                                        CompileDate = x.CompileDate,
                                        IsPass = null,
                                        UnitId = this.UnitId,

                                        AttachFileId = y.AttachFileId,
                                        ToKeyId = y.ToKeyId,
                                        AttachSource = y.AttachSource,
                                        AttachUrl = y.AttachUrl,
                                        ////附件转为字节传送
                                        FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                                    };
                    poxy.DataInsertTechnique_EmergencyTableAsync(emergency.ToList());
                }
                #endregion

                #region 专项方案从企业上报到集团公司
                if (this.cbToSpecialScheme.Checked)
                {
                    poxy.DataInsertTechnique_SpecialSchemeTableCompleted += new EventHandler<BLL.HSSEService.DataInsertTechnique_SpecialSchemeTableCompletedEventArgs>(poxy_DataInsertTechnique_SpecialSchemeTableCompleted);
                    var specialScheme = from x in Funs.DB.View_Technique_SpecialScheme
                                        join y in Funs.DB.AttachFile on x.SpecialSchemeId equals y.ToKeyId
                                        where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4) && (x.IsBuild == false || x.IsBuild == null)
                                        select new BLL.HSSEService.Technique_SpecialScheme
                                        {
                                            SpecialSchemeId = x.SpecialSchemeId,
                                            SpecialSchemeTypeId = x.SpecialSchemeTypeId,
                                            SpecialSchemeCode = x.SpecialSchemeCode,
                                            SpecialSchemeName = x.SpecialSchemeName,
                                            UnitId = this.UnitId,
                                            CompileMan = x.CompileMan,
                                            CompileDate = x.CompileDate,
                                            Summary = x.Summary,
                                            IsPass = null,
                                            AttachFileId = y.AttachFileId,
                                            ToKeyId = y.ToKeyId,
                                            AttachSource = y.AttachSource,
                                            AttachUrl = y.AttachUrl,
                                            ////附件转为字节传送
                                            FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                                        };
                    poxy.DataInsertTechnique_SpecialSchemeTableAsync(specialScheme.ToList());

                }
                #endregion

                #region 培训教材明细从企业上报到集团公司
                if (this.cbToTrainingItem.Checked)
                {

                    poxy.DataInsertTraining_TrainingItemTableCompleted += new EventHandler<BLL.HSSEService.DataInsertTraining_TrainingItemTableCompletedEventArgs>(poxy_DataInsertTraining_TrainingItemTableCompleted);
                    var TrainingItemList = from x in Funs.DB.Training_TrainingItem
                                           join y in Funs.DB.AttachFile on x.TrainingItemId equals y.ToKeyId
                                           where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                                           select new BLL.HSSEService.Training_TrainingItem
                                           {
                                               TrainingItemId = x.TrainingItemId,
                                               TrainingId = x.TrainingId,
                                               TrainingItemCode = x.TrainingItemCode,
                                               TrainingItemName = x.TrainingItemName,
                                               VersionNum = x.VersionNum,
                                               ApproveState = x.ApproveState,
                                               ResourcesFrom = x.ResourcesFrom,
                                               CompileMan = x.CompileMan,
                                               CompileDate = x.CompileDate,
                                               ResourcesFromType = x.ResourcesFromType,
                                               UnitId = this.UnitId,
                                               IsPass = null,
                                               AttachFileId = y.AttachFileId,
                                               ToKeyId = y.ToKeyId,
                                               AttachSource = y.AttachSource,
                                               AttachUrl = y.AttachUrl,
                                               ////附件转为字节传送
                                               FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),
                                           };
                    poxy.DataInsertTraining_TrainingItemTableAsync(TrainingItemList.ToList());
                }
                #endregion

                #region 安全试题明细从企业上报到集团公司
                if (this.cbToTrainTestDBItem.Checked)
                {
                    poxy.DataInsertTraining_TrainTestDBItemTableCompleted += new EventHandler<BLL.HSSEService.DataInsertTraining_TrainTestDBItemTableCompletedEventArgs>(poxy_DataInsertTraining_TrainTestDBItemTableCompleted);
                    var TrainTestDBItemList = from x in Funs.DB.Training_TrainTestDBItem
                                              join y in Funs.DB.AttachFile on x.TrainTestItemId equals y.ToKeyId
                                              where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                                              select new BLL.HSSEService.Training_TrainTestDBItem
                                              {
                                                  TrainTestItemId = x.TrainTestItemId,
                                                  TrainTestId = x.TrainTestId,
                                                  TrainTestItemCode = x.TrainTestItemCode,
                                                  TraiinTestItemName = x.TraiinTestItemName,
                                                  CompileMan = x.CompileMan,
                                                  CompileDate = x.CompileDate,
                                                  UnitId = this.UnitId,
                                                  IsPass = null,

                                                  AttachFileId = y.AttachFileId,
                                                  ToKeyId = y.ToKeyId,
                                                  AttachSource = y.AttachSource,
                                                  AttachUrl = y.AttachUrl,
                                                  ////附件转为字节传送
                                                  FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),
                                              };
                    poxy.DataInsertTraining_TrainTestDBItemTableAsync(TrainTestDBItemList.ToList());
                }
                #endregion

                #region 事故案例明细从企业上报到集团公司
                if (this.cbToAccidentCaseItem.Checked)
                {

                    poxy.DataInsertEduTrain_AccidentCaseItemTableCompleted += new EventHandler<BLL.HSSEService.DataInsertEduTrain_AccidentCaseItemTableCompletedEventArgs>(poxy_DataInsertEduTrain_AccidentCaseItemTableCompleted);
                    var AccidentCaseItemList = from x in Funs.DB.EduTrain_AccidentCaseItem
                                               where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                                               select new BLL.HSSEService.EduTrain_AccidentCaseItem
                                               {
                                                   AccidentCaseItemId = x.AccidentCaseItemId,
                                                   AccidentCaseId = x.AccidentCaseId,
                                                   Activities = x.Activities,
                                                   AccidentName = x.AccidentName,
                                                   AccidentProfiles = x.AccidentProfiles,
                                                   AccidentReview = x.AccidentReview,
                                                   CompileMan = x.CompileMan,
                                                   CompileDate = x.CompileDate,
                                                   UnitId = this.UnitId,
                                                   IsPass = null,
                                               };
                    poxy.DataInsertEduTrain_AccidentCaseItemTableAsync(AccidentCaseItemList.ToList());
                }
                #endregion

                #region 应知应会明细从企业上报到集团公司
                if (this.cbToKnowledgeItem.Checked)
                {
                    poxy.DataInsertTraining_KnowledgeItemTableCompleted += new EventHandler<BLL.HSSEService.DataInsertTraining_KnowledgeItemTableCompletedEventArgs>(poxy_DataInsertTraining_KnowledgeItemTableCompleted);
                    var TrainingItemList = from x in Funs.DB.Training_KnowledgeItem
                                           where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                                           select new BLL.HSSEService.Training_KnowledgeItem
                                           {
                                               KnowledgeItemId = x.KnowledgeItemId,
                                               KnowledgeId = x.KnowledgeId,
                                               KnowledgeItemCode = x.KnowledgeItemCode,
                                               KnowledgeItemName = x.KnowledgeItemName,
                                               Remark = x.Remark,
                                               CompileMan = x.CompileMan,
                                               CompileDate = x.CompileDate,
                                               UnitId = this.UnitId,
                                               IsPass = null,
                                           };
                    poxy.DataInsertTraining_KnowledgeItemTableAsync(TrainingItemList.ToList());
                }
                #endregion

                #region 危险源清单明细从企业上报到集团公司
                if (this.cbToHazardList.Checked)
                {
                    poxy.DataInsertTechnique_HazardListTableCompleted += new EventHandler<BLL.HSSEService.DataInsertTechnique_HazardListTableCompletedEventArgs>(poxy_DataInsertTechnique_HazardListTableCompleted);
                    var hazardListList = from x in Funs.DB.Technique_HazardList
                                         where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                                         select new BLL.HSSEService.Technique_HazardList
                                         {
                                             HazardId = x.HazardId,
                                             HazardListTypeId = x.HazardListTypeId,
                                             HazardCode = x.HazardCode,
                                             HazardItems = x.HazardItems,
                                             DefectsType = x.DefectsType,
                                             MayLeadAccidents = x.MayLeadAccidents,
                                             HelperMethod = x.HelperMethod,
                                             HazardJudge_L = x.HazardJudge_L,
                                             HazardJudge_E = x.HazardJudge_E,
                                             HazardJudge_C = x.HazardJudge_C,
                                             HazardJudge_D = x.HazardJudge_D,
                                             HazardLevel = x.HazardLevel,
                                             ControlMeasures = x.ControlMeasures,
                                             CompileMan = x.CompileMan,
                                             CompileDate = x.CompileDate,
                                             UnitId = this.UnitId,
                                             IsPass = null,
                                         };
                    poxy.DataInsertTechnique_HazardListTableAsync(hazardListList.ToList());
                }
                #endregion

                #region 安全隐患明细从企业上报到集团公司
                if (this.cbToRectifyItem.Checked)
                {
                    poxy.DataInsertTechnique_RectifyItemTableCompleted += new EventHandler<BLL.HSSEService.DataInsertTechnique_RectifyItemTableCompletedEventArgs>(poxy_DataInsertTechnique_RectifyItemTableCompleted);
                    var rectifyItemList = from x in Funs.DB.Technique_RectifyItem
                                          where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                                          select new BLL.HSSEService.Technique_RectifyItem
                                          {
                                              RectifyItemId = x.RectifyItemId,
                                              RectifyId = x.RectifyId,
                                              HazardSourcePoint = x.HazardSourcePoint,
                                              RiskAnalysis = x.RiskAnalysis,
                                              RiskPrevention = x.RiskPrevention,
                                              SimilarRisk = x.SimilarRisk,
                                              CompileMan = x.CompileMan,
                                              CompileDate = x.CompileDate,
                                              UnitId = this.UnitId,
                                              IsPass = null,
                                          };
                    poxy.DataInsertTechnique_RectifyItemTableAsync(rectifyItemList.ToList());
                }
                #endregion

                #region 安全专家从企业上报到集团公司
                //if (this.cbToExpert.Checked)
                //{
                //    poxy.DataInsertTechnique_ExpertTableCompleted += new EventHandler<BLL.HSSEService.DataInsertTechnique_ExpertTableCompletedEventArgs>(poxy_DataInsertTechnique_ExpertTableCompleted);
                //    var expertList = from x in Funs.DB.View_Expert
                //                     join y in Funs.DB.AttachFile on x.ExpertId equals y.ToKeyId
                //                     where x.IsPass == true && (x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4)
                //                     select new BLL.HSSEService.Technique_Expert
                //                     {
                //                         ExpertId = x.ExpertId,
                //                         ExpertCode = x.ExpertCode,
                //                         ExpertName = x.ExpertName,
                //                         Sex = x.SexStr,
                //                         Birthday = x.Birthday,
                //                         Age = x.Age,
                //                         UnitName = x.UnitName,
                //                         Marriage = x.Marriage,
                //                         Nation = x.Nation,
                //                         IdentityCard = x.IdentityCard,
                //                         Email = x.Email,
                //                         Telephone = x.Telephone,
                //                         Education = x.Education,
                //                         Hometown = x.Hometown,
                //                         UnitId = this.UnitId,
                //                         ExpertTypeId = x.ExpertTypeId,
                //                         PersonSpecialtyId = x.PersonSpecialtyId,
                //                         PostTitleId = x.PostTitleId,
                //                         Performance = x.Performance,
                //                         EffectiveDate = x.EffectiveDate,
                //                         CompileMan = x.CompileMan,
                //                         CompileDate = x.CompileDate,
                //                         //AttachUrl = x.AttachUrl,
                //                         PhotoUrl = x.PhotoUrl,
                //                         ExpertTypeName = x.ExpertTypeName,
                //                         ExpertTypeCode = x.ExpertTypeCode,
                //                         PersonSpecialtyName = x.PersonSpecialtyName,
                //                         PersonSpecialtyCode = x.PersonSpecialtyCode,
                //                         PostTitleName = x.PostTitleName,
                //                         PostTitleCode = x.PostTitleCode,
                //                         //AttachUrlFileContext = BLL.FileStructService.GetFileStructByAttachUrl(x.AttachUrl),
                //                         AttachFileId = y.AttachFileId,
                //                         ToKeyId = y.ToKeyId,
                //                         AttachSource = y.AttachSource,
                //                         AttachUrl = y.AttachUrl,
                //                         ////附件转为字节传送
                //                         AttachUrlFileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),
                //                         PhotoUrlFileContext = BLL.FileStructService.GetFileStructByAttachUrl(x.PhotoUrl),
                //                         IsPass = null,
                //                     };
                //    poxy.DataInsertTechnique_ExpertTableAsync(expertList.ToList());
                //}
                #endregion

                #region 安全监督检查整改从企业上报到集团公司
                if (this.cbToCheckRectify.Checked)
                {
                    poxy.DataInsertCheck_CheckRectifyTableCompleted += new EventHandler<BLL.HSSEService.DataInsertCheck_CheckRectifyTableCompletedEventArgs>(poxy_DataInsertCheck_CheckRectifyTableCompleted);
                    var rectify = from x in Funs.DB.View_CheckRectifyListFromSUB
                                  where x.RealEndDate.HasValue && x.HandleState == "2"
                                  select new BLL.HSSEService.Check_CheckRectify
                                  {
                                      CheckRectifyId = x.CheckRectifyId,
                                      CheckRectifyCode = x.CheckRectifyCode,
                                      ProjectId = x.ProjectId,
                                      UnitId = x.UnitId,
                                      CheckDate = x.CheckDate,
                                      IssueMan = x.IssueMan,
                                      IssueDate = x.IssueDate,                                     
                                      HandleState = x.HandleState,
                                      CheckRectifyItemId = x.CheckRectifyItemId,                                      
                                      ConfirmMan = x.ConfirmMan,
                                      ConfirmDate = x.ConfirmDate,
                                      OrderEndDate = x.OrderEndDate,
                                      OrderEndPerson = x.OrderEndPerson,
                                      RealEndDate = x.RealEndDate,
                                      Verification =x.Verification,

                                      AttachFileId = x.AttachFileId2,
                                      ToKeyId = x.ToKeyId2,
                                      AttachSource = x.AttachSource2,
                                      AttachUrl = x.AttachUrl2,

                                      ////附件转为字节传送
                                      FileContext = FileStructService.GetMoreFileStructByAttachUrl(x.AttachUrl2),
                                  };

                    poxy.DataInsertCheck_CheckRectifyTableAsync(rectify.ToList());
                }
                #endregion

                #region 企业安全文件上报从企业上报到集团公司
                if (this.cbToSubUnitReportItem.Checked)
                {
                    /////创建客户端服务
                    poxy = Web.ServiceProxy.CreateServiceClient();
                    poxy.DataInsertSupervise_SubUnitReportItemItemTableCompleted += new EventHandler<BLL.HSSEService.DataInsertSupervise_SubUnitReportItemItemTableCompletedEventArgs>(poxy_DataInsertSupervise_SubUnitReportItemTableCompleted);
                    var subUnitReport = from x in Funs.DB.View_Supervise_SubUnitReportItem
                                        join y in Funs.DB.AttachFile on x.SubUnitReportItemId equals y.ToKeyId
                                        where x.UpState == BLL.Const.UpState_2 || x.UpState == BLL.Const.UpState_4 || x.UpState == null
                                        select new BLL.HSSEService.Supervise_SubUnitReportItem
                                        {
                                            SubUnitReportItemId=x.SubUnitReportItemId,
                                            ReportTitle = x.ReportTitle,
                                            ReportContent = x.ReportContent,
                                            ReportDate = x.ReportDate,
                                            State = x.State,


                                            AttachFileId = y.AttachFileId,
                                            ToKeyId = y.ToKeyId,
                                            AttachSource = y.AttachSource,
                                            AttachUrl = y.AttachUrl,
                                            ////附件转为字节传送
                                            FileContext = FileStructService.GetMoreFileStructByAttachUrl(y.AttachUrl),

                                        };
                    poxy.DataInsertSupervise_SubUnitReportItemItemTableAsync(subUnitReport.ToList());
                }
                #endregion
                
                #region 安全管理机构从企业上报到集团公司
                if (this.cbToHSSEManage.Checked)
                {
                    /////创建客户端服务
                    poxy = Web.ServiceProxy.CreateServiceClient();
                    poxy.DataInsertHSSESystem_HSSEManageItemTableCompleted+=new EventHandler<BLL.HSSEService.DataInsertHSSESystem_HSSEManageItemTableCompletedEventArgs>(poxy_DataInsertHSSESystem_HSSEManageItemTableCompleted);
                    var subUnitReport = from x in Funs.DB.HSSESystem_HSSEManageItem
                                        join y in Funs.DB.HSSESystem_HSSEManage on x.HSSEManageId equals y.HSSEManageId
                                        where y.HSSEManageName == this.UnitName
                                        select new BLL.HSSEService.HSSESystem_HSSEManageItem
                                        {
                                            HSSEManageName = y.HSSEManageName,
                                            HSSEManageItemId = x.HSSEManageItemId,
                                            Post = x.Post,
                                            Names = x.Names,
                                            Telephone = x.Telephone,
                                            MobilePhone = x.MobilePhone,
                                            EMail = x.EMail,
                                            Duty = x.Duty,
                                            SortIndex = x.SortIndex,
                                        };
                    poxy.DataInsertHSSESystem_HSSEManageItemTableAsync(subUnitReport.ToList());
                }
                #endregion
            }
            #endregion           
        }
        #endregion

        #region 数据从集团公司提取
        #region 版本信息从集团公司提取
        /// <summary>
        /// 版本信息从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetSys_VersionToSUBCompleted(object sender, BLL.HSSEService.GetSys_VersionToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var versionItems = e.Result;
                if (versionItems.Count() > 0)
                {
                    count = versionItems.Count();
                    foreach (var item in versionItems)
                    {
                        var version = Funs.DB.Sys_Version.FirstOrDefault(x => x.VersionId == item.VersionId);
                        if (version == null)
                        {
                            Model.Sys_Version newVersion = new Model.Sys_Version
                            {
                                VersionId = item.VersionId,
                                VersionName = item.VersionName,
                                VersionDate = item.VersionDate,
                                CompileMan = item.CompileMan,
                                AttachUrl = item.AttachUrl,
                                IsSub = item.IsSub
                            };
                            Funs.DB.Sys_Version.InsertOnSubmit(newVersion);
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }

            SetLog("版本信息", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion        

        #region 单位信息从集团公司提取
        /// <summary>
        /// 单位信息从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetBase_UnitToSUBCompleted(object sender, BLL.HSSEService.GetBase_UnitToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var unitItems = e.Result;
                if (unitItems.Count() > 0)
                {
                    count = unitItems.Count();
                    foreach (var item in unitItems)
                    {
                        if (!string.IsNullOrEmpty(item.UnitTypeId))
                        {
                            var unitType = BLL.UnitTypeService.GetUnitTypeById(item.UnitTypeId);
                            if (unitType == null)
                            {
                                Model.Base_UnitType newUnitType = new Model.Base_UnitType
                                {
                                    UnitTypeId = item.UnitTypeId,
                                    UnitTypeCode = item.UnitTypeCode,
                                    UnitTypeName = item.UnitTypeName
                                };
                                Funs.DB.Base_UnitType.InsertOnSubmit(newUnitType);
                                Funs.DB.SubmitChanges();
                            }
                        }
                        var unit = BLL.UnitService.GetUnitByUnitId(item.UnitId);
                        if (unit == null)
                        {
                            Model.Base_Unit newUnit = new Model.Base_Unit
                            {
                                UnitId = item.UnitId,
                                UnitName = item.UnitName,
                                UnitCode = item.UnitCode,
                                UnitTypeId = item.UnitTypeId,
                                ProjectRange = item.ProjectRange,
                                Corporate = item.Corporate,
                                Address = item.Address,
                                Telephone = item.Telephone,
                                Fax = item.Fax,
                                IsBuild = true
                            };
                            Funs.DB.Base_Unit.InsertOnSubmit(newUnit);
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }

            SetLog("单位信息", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion        

        #region 催报信息从集团公司提取到企业
        /// <summary>
        /// 催报信息从集团公司提取到企业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetInformation_UrgeReportToSUBCompleted(object sender, BLL.HSSEService.GetInformation_UrgeReportToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var UrgeReportItems = e.Result;
                if (UrgeReportItems.Count() > 0)
                {
                    count = UrgeReportItems.Count();
                    foreach (var item in UrgeReportItems)
                    {
                        var urg = Funs.DB.Information_UrgeReport.FirstOrDefault(x => x.UrgeReportId == item.UrgeReportId);
                        if (urg == null)
                        {
                            Model.Information_UrgeReport newUrgeReport = new Model.Information_UrgeReport
                            {
                                UrgeReportId = item.UrgeReportId,
                                UnitId = item.UnitId,
                                ReprotType = item.ReprotType,
                                YearId = item.YearId,
                                MonthId = item.MonthId,
                                QuarterId = item.QuarterId,
                                HalfYearId = item.HalfYearId,
                                UrgeDate = item.UrgeDate,
                                IsComplete = null,
                                IsCancel = item.IsCancel
                            };
                            Funs.DB.Information_UrgeReport.InsertOnSubmit(newUrgeReport);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            urg.IsCancel = item.IsCancel;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }

            SetLog("催报信息", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 法律法规信息从集团公司提取
        /// <summary>
        /// 法律法规从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetLaw_LawRegulationListToSUBCompleted(object sender, BLL.HSSEService.GetLaw_LawRegulationListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var law = e.Result;
                if (law.Count() > 0)
                {
                    count = law.Count();
                    foreach (var item in law)
                    {                       
                        var type = BLL.LawsRegulationsTypeService.GetLawsRegulationsTypeById(item.LawsRegulationsTypeId);
                        if (!string.IsNullOrEmpty(item.LawsRegulationsTypeId) && type == null)
                        {
                            Model.Base_LawsRegulationsType new_LawsRegulationsType = new Model.Base_LawsRegulationsType
                            {
                                Id = item.LawsRegulationsTypeId,
                                Code = item.LawsRegulationsTypeCode,
                                Name = item.LawsRegulationsTypeName
                            };
                            Funs.DB.Base_LawsRegulationsType.InsertOnSubmit(new_LawsRegulationsType);
                            Funs.DB.SubmitChanges();
                        }

                        var newlaw = BLL.LawRegulationListService.GetLawRegulationListById(item.LawRegulationId);
                        if (newlaw == null)
                        {
                            Model.Law_LawRegulationList newLawRegulationList = new Model.Law_LawRegulationList
                            {
                                LawRegulationId = item.LawRegulationId,
                                LawsRegulationsTypeId = item.LawsRegulationsTypeId,
                                ApprovalDate = item.ApprovalDate,
                                //newLawRegulationList.AuditDate = item.AuditDate;
                                //newLawRegulationList.AuditMan = item.AuditMan;
                                CompileDate = item.CompileDate,
                                CompileMan = item.CompileMan,
                                Description = item.Description,
                                EffectiveDate = item.EffectiveDate,
                                IsPass = true,
                                LawRegulationCode = item.LawRegulationCode,
                                LawRegulationName = item.LawRegulationName,
                                UnitId = item.UnitId,
                                IsBuild = true
                            };
                            Funs.DB.Law_LawRegulationList.InsertOnSubmit(newLawRegulationList);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            //newlaw.LawRegulationId = item.LawRegulationId;
                            newlaw.LawsRegulationsTypeId = item.LawsRegulationsTypeId;
                            newlaw.ApprovalDate = item.ApprovalDate;
                            //newlaw.AuditDate = item.AuditDate;
                            //newlaw.AuditMan = item.AuditMan;
                            newlaw.CompileDate = item.CompileDate;
                            newlaw.CompileMan = item.CompileMan;
                            newlaw.Description = item.Description;
                            newlaw.EffectiveDate = item.EffectiveDate;
                            newlaw.IsPass = true;
                            newlaw.LawRegulationCode = item.LawRegulationCode;
                            newlaw.LawRegulationName = item.LawRegulationName;
                            newlaw.UnitId = item.UnitId;
                            newlaw.IsBuild = true;                            
                            Funs.DB.SubmitChanges();
                        }

                        ////上传附件
                        BLL.FileInsertService.InsertAttachFile(item.AttachFileId, item.LawRegulationId, item.AttachSource, item.AttachUrl, item.FileContext);
                    }
                }
            }

            SetLog("法律法规", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 标准规范从集团公司提取
        /// <summary>
        /// 标准规范
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetLaw_HSSEStandardsListToSUBCompleted(object sender, BLL.HSSEService.GetLaw_HSSEStandardsListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var standards = e.Result;
                if (standards.Count() > 0)
                {
                    count = standards.Count();
                    foreach (var item in standards)
                    {
                        if (!string.IsNullOrEmpty(item.TypeId))
                        {
                            var type = BLL.HSSEStandardListTypeService.GetHSSEStandardListType(item.TypeId);
                            if (type == null)
                            {
                                Model.Base_HSSEStandardListType newHSSEStandardListType = new Model.Base_HSSEStandardListType
                                {
                                    TypeId = item.TypeId,
                                    TypeCode = item.TypeCode,
                                    TypeName = item.TypeName
                                };

                                Funs.DB.Base_HSSEStandardListType.InsertOnSubmit(newHSSEStandardListType);
                                Funs.DB.SubmitChanges();
                            }
                        }
                        var newStandard = BLL.HSSEStandardsListService.GetHSSEStandardsListByHSSEStandardsListId(item.StandardId);
                        if (newStandard == null)
                        {
                            Model.Law_HSSEStandardsList newHSSEStandardsList = new Model.Law_HSSEStandardsList
                            {
                                StandardId = item.StandardId,
                                StandardGrade = item.StandardGrade,
                                StandardNo = item.StandardNo,
                                StandardName = item.StandardName,
                                TypeId = item.TypeId,

                                IsSelected1 = item.IsSelected1,
                                IsSelected2 = item.IsSelected2,
                                IsSelected3 = item.IsSelected3,
                                IsSelected4 = item.IsSelected4,
                                IsSelected5 = item.IsSelected5,
                                IsSelected6 = item.IsSelected6,
                                IsSelected7 = item.IsSelected7,
                                IsSelected8 = item.IsSelected8,
                                IsSelected9 = item.IsSelected9,
                                IsSelected10 = item.IsSelected10,
                                IsSelected11 = item.IsSelected11,
                                IsSelected12 = item.IsSelected12,
                                IsSelected13 = item.IsSelected13,
                                IsSelected14 = item.IsSelected14,
                                IsSelected15 = item.IsSelected15,
                                IsSelected16 = item.IsSelected16,
                                IsSelected17 = item.IsSelected17,
                                IsSelected18 = item.IsSelected18,
                                IsSelected19 = item.IsSelected19,
                                IsSelected20 = item.IsSelected20,
                                IsSelected21 = item.IsSelected21,
                                IsSelected22 = item.IsSelected22,
                                IsSelected23 = item.IsSelected23,
                                IsSelected24 = item.IsSelected24,
                                IsSelected25 = item.IsSelected25,
                                IsSelected90 = item.IsSelected90,
                                CompileMan = item.CompileMan,
                                CompileDate = item.CompileDate,
                                IsPass = true,
                                UnitId = item.UnitId,
                                IsBuild = true
                            };
                            Funs.DB.Law_HSSEStandardsList.InsertOnSubmit(newHSSEStandardsList);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            newStandard.StandardGrade = item.StandardGrade;
                            newStandard.StandardNo = item.StandardNo;
                            newStandard.StandardName = item.StandardName;
                            newStandard.TypeId = item.TypeId;
                            
                            newStandard.IsSelected1 = item.IsSelected1;
                            newStandard.IsSelected2 = item.IsSelected2;
                            newStandard.IsSelected3 = item.IsSelected3;
                            newStandard.IsSelected4 = item.IsSelected4;
                            newStandard.IsSelected5 = item.IsSelected5;
                            newStandard.IsSelected6 = item.IsSelected6;
                            newStandard.IsSelected7 = item.IsSelected7;
                            newStandard.IsSelected8 = item.IsSelected8;
                            newStandard.IsSelected9 = item.IsSelected9;
                            newStandard.IsSelected10 = item.IsSelected10;
                            newStandard.IsSelected11 = item.IsSelected11;
                            newStandard.IsSelected12 = item.IsSelected12;
                            newStandard.IsSelected13 = item.IsSelected13;
                            newStandard.IsSelected14 = item.IsSelected14;
                            newStandard.IsSelected15 = item.IsSelected15;
                            newStandard.IsSelected16 = item.IsSelected16;
                            newStandard.IsSelected17 = item.IsSelected17;
                            newStandard.IsSelected18 = item.IsSelected18;
                            newStandard.IsSelected19 = item.IsSelected19;
                            newStandard.IsSelected20 = item.IsSelected20;
                            newStandard.IsSelected21 = item.IsSelected21;
                            newStandard.IsSelected22 = item.IsSelected22;
                            newStandard.IsSelected23 = item.IsSelected23;
                            newStandard.IsSelected90 = item.IsSelected90;
                            newStandard.CompileMan = item.CompileMan;
                            newStandard.CompileDate = item.CompileDate;
                            newStandard.IsPass = true;
                            newStandard.UnitId = item.UnitId;
                            newStandard.IsBuild = true;
                            Funs.DB.SubmitChanges();
                        }
                        ////上传附件
                        BLL.FileInsertService.InsertAttachFile(item.AttachFileId, item.StandardId, item.AttachSource, item.AttachUrl, item.FileContext);

                    }
                }
            }

            SetLog("标准规范", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }

        #endregion

        #region 安全生产规章制度从集团公司提取
        /// <summary>
        /// 安全生产规章制度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetLaw_RulesRegulationsToSUBCompleted(object sender, BLL.HSSEService.GetLaw_RulesRegulationsToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var rulesRegulations = e.Result;
                if (rulesRegulations.Count() > 0)
                {
                    count = rulesRegulations.Count();
                    foreach (var item in rulesRegulations)
                    {                       
                        if (!string.IsNullOrEmpty(item.RulesRegulationsTypeId))
                        {
                            var type = BLL.RulesRegulationsTypeService.GetRulesRegulationsTypeById(item.RulesRegulationsTypeId);
                            if (type == null)
                            {
                                Model.Base_RulesRegulationsType newRulesRegulationsType = new Model.Base_RulesRegulationsType
                                {
                                    RulesRegulationsTypeId = item.RulesRegulationsTypeId,
                                    RulesRegulationsTypeCode = item.RulesRegulationsTypeCode,
                                    RulesRegulationsTypeName = item.RulesRegulationsTypeName
                                };

                                Funs.DB.Base_RulesRegulationsType.InsertOnSubmit(newRulesRegulationsType);
                                Funs.DB.SubmitChanges();
                            }
                        }
                        var RulesR = BLL.RulesRegulationsService.GetRulesRegulationsById(item.RulesRegulationsId);
                        if (RulesR == null)
                        {
                            Model.Law_RulesRegulations newRulesRegulations = new Model.Law_RulesRegulations
                            {
                                RulesRegulationsId = item.RulesRegulationsId,
                                RulesRegulationsCode = item.RulesRegulationsCode,
                                RulesRegulationsName = item.RulesRegulationsName,
                                RulesRegulationsTypeId = item.RulesRegulationsTypeId,
                                CustomDate = item.CustomDate,
                                ApplicableScope = item.ApplicableScope,
                                Remark = item.Remark,
                                CompileMan = item.CompileMan,
                                CompileDate = item.CompileDate,
                                IsPass = true,
                                UnitId = item.UnitId,
                                IsBuild = true
                            };
                            Funs.DB.Law_RulesRegulations.InsertOnSubmit(newRulesRegulations);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            RulesR.RulesRegulationsCode = item.RulesRegulationsCode;
                            RulesR.RulesRegulationsName = item.RulesRegulationsName;
                            RulesR.RulesRegulationsTypeId = item.RulesRegulationsTypeId;
                            RulesR.CustomDate = item.CustomDate;
                            RulesR.ApplicableScope = item.ApplicableScope;
                            RulesR.Remark = item.Remark;
                            RulesR.CompileMan = item.CompileMan;
                            RulesR.CompileDate = item.CompileDate;
                            RulesR.IsPass = true;
                            RulesR.UnitId = item.UnitId;
                            RulesR.IsBuild = true;
                            Funs.DB.SubmitChanges();
                        }
                        ////上传附件
                        BLL.FileInsertService.InsertAttachFile(item.AttachFileId, item.RulesRegulationsId, item.AttachSource, item.AttachUrl, item.FileContext);
                    }
                }
            }

            SetLog("安全生产规章制度", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }

        #endregion

        #region 安全管理规定从集团公司提取
        /// <summary>
        /// 安全管理规定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetLaw_ManageRuleToSUBCompleted(object sender, BLL.HSSEService.GetLaw_ManageRuleToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var manageRule = e.Result;
                if (manageRule.Count() > 0)
                {
                    count = manageRule.Count();
                    foreach (var item in manageRule)
                    {
                       
                        if (!string.IsNullOrEmpty(item.ManageRuleTypeId))
                        {
                            var type = BLL.ManageRuleTypeService.GetManageRuleTypeById(item.ManageRuleTypeId);
                            if (type == null)
                            {
                                Model.Base_ManageRuleType newManageRuleType = new Model.Base_ManageRuleType
                                {
                                    ManageRuleTypeId = item.ManageRuleTypeId,
                                    ManageRuleTypeCode = item.ManageRuleTypeCode,
                                    ManageRuleTypeName = item.ManageRuleTypeName
                                };

                                Funs.DB.Base_ManageRuleType.InsertOnSubmit(newManageRuleType);
                                Funs.DB.SubmitChanges();
                            }
                        }
                        var RulesR = BLL.ManageRuleService.GetManageRuleById(item.ManageRuleId);
                        if (RulesR == null)
                        {
                            Model.Law_ManageRule newManageRule = new Model.Law_ManageRule
                            {
                                ManageRuleId = item.ManageRuleId,
                                ManageRuleCode = item.ManageRuleCode,
                                ManageRuleName = item.ManageRuleName,
                                ManageRuleTypeId = item.ManageRuleTypeId,
                                VersionNo = item.VersionNo,
                                CompileMan = item.CompileMan,
                                CompileDate = item.CompileDate,
                                Remark = item.Remark,
                                IsPass = true,
                                UnitId = item.UnitId,
                                IsBuild = true
                            };
                            Funs.DB.Law_ManageRule.InsertOnSubmit(newManageRule);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            RulesR.ManageRuleCode = item.ManageRuleCode;
                            RulesR.ManageRuleName = item.ManageRuleName;
                            RulesR.ManageRuleTypeId = item.ManageRuleTypeId;
                            RulesR.VersionNo = item.VersionNo;
                            RulesR.CompileMan = item.CompileMan;
                            RulesR.CompileDate = item.CompileDate;
                            RulesR.Remark = item.Remark;
                            RulesR.IsPass = true;
                            RulesR.UnitId = item.UnitId;
                            RulesR.IsBuild = true;
                            Funs.DB.SubmitChanges();
                        }
                        ////上传附件
                        BLL.FileInsertService.InsertAttachFile(item.AttachFileId, item.ManageRuleId, item.AttachSource, item.AttachUrl, item.FileContext);
                    }
                }
            }

            SetLog("安全管理规定", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }

        #endregion

        #region HAZOP管理从集团公司提取
        /// <summary>
        /// HAZOP管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetTechnique_HAZOPToSUBCompleted(object sender, BLL.HSSEService.GetTechnique_HAZOPToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var hazop = e.Result;
                if (hazop.Count() > 0)
                {
                    count = hazop.Count();
                    foreach (var item in hazop)
                    {                        
                        var H = BLL.HAZOPService.GetHAZOPById(item.HAZOPId);
                        if (H == null)
                        {
                            Model.Technique_HAZOP newHAZOP = new Model.Technique_HAZOP
                            {
                                HAZOPId = item.HAZOPId,
                                UnitId = item.UnitId,
                                Abstract = item.Abstract,
                                HAZOPDate = item.HAZOPDate,
                                HAZOPTitle = item.HAZOPTitle,
                                CompileMan = item.CompileMan,
                                CompileDate = item.CompileDate,
                                IsPass = true,
                                IsBuild = true
                            };
                            Funs.DB.Technique_HAZOP.InsertOnSubmit(newHAZOP);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            H.UnitId = item.UnitId;
                            H.Abstract = item.Abstract;
                            H.HAZOPDate = item.HAZOPDate;
                            H.HAZOPTitle = item.HAZOPTitle;
                            H.CompileMan = item.CompileMan;
                            H.CompileDate = item.CompileDate;
                            H.IsPass = true;
                            H.IsBuild = true;

                            Funs.DB.SubmitChanges();
                        }
                        ////上传附件
                        BLL.FileInsertService.InsertAttachFile(item.AttachFileId, item.HAZOPId, item.AttachSource, item.AttachUrl, item.FileContext);
                    }
                }
            }

            SetLog("HAZOP管理", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }

        #endregion

        #region 管理评价从集团公司提取
        /// <summary>
        /// 管理评价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetTechnique_AppraiseToSUBCompleted(object sender, BLL.HSSEService.GetTechnique_AppraiseToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var appraise = e.Result;
                if (appraise.Count() > 0)
                {
                    count = appraise.Count();
                    foreach (var item in appraise)
                    {
                        var H = BLL.AppraiseService.GetAppraiseById(item.AppraiseId);
                        if (H == null)
                        {
                            Model.Technique_Appraise newAppraise = new Model.Technique_Appraise
                            {
                                AppraiseId = item.AppraiseId,
                                AppraiseCode = item.AppraiseCode,
                                AppraiseTitle = item.AppraiseTitle,
                                Abstract = item.Abstract,
                                AppraiseDate = item.AppraiseDate,
                                ArrangementPerson = item.ArrangementPerson,
                                ArrangementDate = item.ArrangementDate,
                                CompileMan = item.CompileMan,
                                CompileDate = item.CompileDate,
                                UnitId = item.UnitId,
                                IsPass = true,
                                IsBuild = true
                            };
                            Funs.DB.Technique_Appraise.InsertOnSubmit(newAppraise);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            H.AppraiseId = item.AppraiseId;
                            H.AppraiseCode = item.AppraiseCode;
                            H.AppraiseTitle = item.AppraiseTitle;
                            H.Abstract = item.Abstract;
                            H.AppraiseDate = item.AppraiseDate;
                            H.ArrangementPerson = item.ArrangementPerson;
                            H.ArrangementDate = item.ArrangementDate;
                            H.CompileMan = item.CompileMan;
                            H.CompileDate = item.CompileDate;
                            H.UnitId = item.UnitId;
                            H.IsPass = true;
                            H.IsBuild = true;
                            Funs.DB.SubmitChanges();
                        }
                        ////上传附件
                        BLL.FileInsertService.InsertAttachFile(item.AttachFileId, item.AppraiseId, item.AttachSource, item.AttachUrl, item.FileContext);
                    }
                }
            }

            SetLog("安全评价", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }

        #endregion

        #region 应急预案从集团公司提取
        /// <summary>
        /// 应急预案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetTechnique_EmergencyToSUBCompleted(object sender, BLL.HSSEService.GetTechnique_EmergencyToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var emergency = e.Result;
                if (emergency.Count() > 0)
                {
                    count = emergency.Count();
                    foreach (var item in emergency)
                    {
                        if (!string.IsNullOrEmpty(item.EmergencyTypeId))
                        {
                            var type = BLL.EmergencyTypeService.GetEmergencyTypeById(item.EmergencyTypeId);
                            if (type == null)
                            {
                                Model.Base_EmergencyType newEmergencyType = new Model.Base_EmergencyType
                                {
                                    EmergencyTypeId = item.EmergencyTypeId,
                                    EmergencyTypeCode = item.EmergencyTypeCode,
                                    EmergencyTypeName = item.EmergencyTypeName
                                };

                                Funs.DB.Base_EmergencyType.InsertOnSubmit(newEmergencyType);
                                Funs.DB.SubmitChanges();
                            }
                        }
                        var RulesR = BLL.EmergencyService.GetEmergencyListById(item.EmergencyId);
                        if (RulesR == null)
                        {
                            Model.Technique_Emergency newEmergency = new Model.Technique_Emergency
                            {
                                EmergencyId = item.EmergencyId,
                                EmergencyTypeId = item.EmergencyTypeId,
                                EmergencyCode = item.EmergencyCode,
                                EmergencyName = item.EmergencyName,
                                Summary = item.Summary,
                                Remark = item.Remark,
                                CompileMan = item.CompileMan,
                                CompileDate = item.CompileDate,
                                IsPass = true,
                                UnitId = item.UnitId,
                                IsBuild = true
                            };
                            Funs.DB.Technique_Emergency.InsertOnSubmit(newEmergency);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            RulesR.EmergencyTypeId = item.EmergencyTypeId;
                            RulesR.EmergencyCode = item.EmergencyCode;
                            RulesR.EmergencyName = item.EmergencyName;
                            RulesR.Summary = item.Summary;
                            RulesR.Remark = item.Remark;
                            RulesR.CompileMan = item.CompileMan;
                            RulesR.CompileDate = item.CompileDate;
                            RulesR.IsPass = true;
                            RulesR.UnitId = item.UnitId;
                            RulesR.IsBuild = true;
                            Funs.DB.SubmitChanges();
                        }
                        ////上传附件
                        BLL.FileInsertService.InsertAttachFile(item.AttachFileId, item.EmergencyTypeId, item.AttachSource, item.AttachUrl, item.FileContext);
                    }
                }
            }

            SetLog("应急预案", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }

        #endregion

        #region 专项方案从集团公司提取
        /// <summary>
        /// 专项方案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetTechnique_SpecialSchemeToSUBCompleted(object sender, BLL.HSSEService.GetTechnique_SpecialSchemeToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var specialScheme = e.Result;
                if (specialScheme.Count() > 0)
                {
                    count = specialScheme.Count();
                    foreach (var item in specialScheme)
                    {
                        if (!string.IsNullOrEmpty(item.SpecialSchemeTypeId))
                        {
                            var type = BLL.SpecialSchemeTypeService.GetSpecialSchemeTypeById(item.SpecialSchemeTypeId);
                            if (type == null)
                            {
                                Model.Base_SpecialSchemeType newSpecialSchemeType = new Model.Base_SpecialSchemeType
                                {
                                    SpecialSchemeTypeId = item.SpecialSchemeTypeId,
                                    SpecialSchemeTypeCode = item.SpecialSchemeTypeCode,
                                    SpecialSchemeTypeName = item.SpecialSchemeTypeName
                                };

                                Funs.DB.Base_SpecialSchemeType.InsertOnSubmit(newSpecialSchemeType);
                                Funs.DB.SubmitChanges();
                            }
                        }
                        var RulesR = BLL.SpecialSchemeService.GetSpecialSchemeListById(item.SpecialSchemeId);
                        if (RulesR == null)
                        {
                            Model.Technique_SpecialScheme newSpecialScheme = new Model.Technique_SpecialScheme
                            {
                                SpecialSchemeId = item.SpecialSchemeId,
                                SpecialSchemeTypeId = item.SpecialSchemeTypeId,
                                SpecialSchemeCode = item.SpecialSchemeCode,
                                SpecialSchemeName = item.SpecialSchemeName,
                                UnitId = item.UnitId,
                                CompileMan = item.CompileMan,
                                CompileDate = item.CompileDate,
                                Summary = item.Summary,
                                IsPass = true,
                                IsBuild = true
                            };
                            Funs.DB.Technique_SpecialScheme.InsertOnSubmit(newSpecialScheme);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            RulesR.SpecialSchemeId = item.SpecialSchemeId;
                            RulesR.SpecialSchemeTypeId = item.SpecialSchemeTypeId;
                            RulesR.SpecialSchemeCode = item.SpecialSchemeCode;
                            RulesR.SpecialSchemeName = item.SpecialSchemeName;
                            RulesR.UnitId = item.UnitId;
                            RulesR.CompileMan = item.CompileMan;
                            RulesR.CompileDate = item.CompileDate;
                            RulesR.Summary = item.Summary;
                            RulesR.IsPass = true;
                            RulesR.IsBuild = true;

                            Funs.DB.SubmitChanges();
                        }
                        ////上传附件
                        BLL.FileInsertService.InsertAttachFile(item.AttachFileId, item.SpecialSchemeId, item.AttachSource, item.AttachUrl, item.FileContext);
                    }
                }
            }

            SetLog("专项方案", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }

        #endregion

        #region 培训教材库类别信息从集团公司提取
        /// <summary>
        /// 培训教材库类别从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetTraining_TrainingListToSUBCompleted(object sender, BLL.HSSEService.GetTraining_TrainingListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var training = e.Result;
                if (training.Count() > 0)
                {
                    count = training.Count();
                    foreach (var item in training)
                    {
                        var newTraining = BLL.TrainingService.GetTrainingByTrainingId(item.TrainingId);
                        if (newTraining == null)
                        {
                            Model.Training_Training newTraining1 = new Model.Training_Training
                            {
                                TrainingId = item.TrainingId,
                                TrainingCode = item.TrainingCode,
                                TrainingName = item.TrainingName,
                                SupTrainingId = item.SupTrainingId,
                                IsEndLever = item.IsEndLever,
                                IsBuild = true
                            };
                            Funs.DB.Training_Training.InsertOnSubmit(newTraining1);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            newTraining.TrainingCode = item.TrainingCode;
                            newTraining.TrainingName = item.TrainingName;
                            newTraining.SupTrainingId = item.SupTrainingId;
                            newTraining.IsEndLever = item.IsEndLever;
                            newTraining.IsBuild = true;

                            Funs.DB.SubmitChanges();
                        }

                    }
                }
            }

            SetLog("培训教材库类别", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 培训教材库明细信息从集团公司提取
        /// <summary>
        /// 培训教材库明细从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetTraining_TrainingItemListToSUBCompleted(object sender, BLL.HSSEService.GetTraining_TrainingItemListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var items = e.Result;
                if (items.Count() > 0)
                {
                    count = items.Count();
                    foreach (var item in items)
                    {
                        var newItem = BLL.TrainingItemService.GetTrainingItemByTrainingItemId(item.TrainingItemId);
                        if (newItem == null)
                        {
                            var Training = BLL.TrainingService.GetTrainingByTrainingId(item.TrainingId);
                            if (Training != null)
                            {
                                Model.Training_TrainingItem newItem1 = new Model.Training_TrainingItem
                                {
                                    TrainingItemId = item.TrainingItemId,
                                    TrainingId = item.TrainingId,
                                    TrainingItemCode = item.TrainingItemCode,
                                    TrainingItemName = item.TrainingItemName,
                                    VersionNum = item.VersionNum,
                                    ApproveState = item.ApproveState,
                                    ResourcesFrom = item.ResourcesFrom,
                                    CompileMan = item.CompileMan,
                                    CompileDate = item.CompileDate,
                                    ResourcesFromType = item.ResourcesFromType,
                                    UnitId = item.UnitId,
                                    IsPass = true,
                                    AttachUrl = item.AttachUrl
                                };
                                Funs.DB.Training_TrainingItem.InsertOnSubmit(newItem1);
                                Funs.DB.SubmitChanges();
                            }
                        }
                        else
                        {
                            newItem.TrainingItemCode = item.TrainingItemCode;
                            newItem.TrainingItemName = item.TrainingItemName;
                            newItem.VersionNum = item.VersionNum;
                            newItem.ApproveState = item.ApproveState;
                            newItem.ResourcesFrom = item.ResourcesFrom;
                            newItem.CompileMan = item.CompileMan;
                            newItem.CompileDate = item.CompileDate;
                            newItem.ResourcesFromType = item.ResourcesFromType;
                            newItem.UnitId = item.UnitId;
                            newItem.IsPass = true;
                            newItem.AttachUrl = item.AttachUrl;
                            Funs.DB.SubmitChanges();
                        }
                        if (item.FileContext != null)
                        {
                            ////上传附件
                            BLL.FileInsertService.InsertAttachFile(item.AttachFileId, item.TrainingItemId, item.AttachSource, item.AttachUrl, item.FileContext);
                        }
                    }
                }
            }

            SetLog("培训教材库明细", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 安全试题库类别信息从集团公司提取
        /// <summary>
        /// 安全试题库类别从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetTraining_TrainTestDBListToSUBCompleted(object sender, BLL.HSSEService.GetTraining_TrainTestDBListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var trainTestDB = e.Result;
                if (trainTestDB.Count() > 0)
                {
                    count = trainTestDB.Count();
                    foreach (var item in trainTestDB)
                    {
                        var newTrainTestDB = BLL.TrainTestDBService.GetTrainTestDBById(item.TrainTestId);
                        if (newTrainTestDB == null)
                        {
                            Model.Training_TrainTestDB newTrainTestDB1 = new Model.Training_TrainTestDB
                            {
                                TrainTestId = item.TrainTestId,
                                TrainTestCode = item.TrainTestCode,
                                TrainTestName = item.TrainTestName,
                                SupTrainTestId = item.SupTrainTestId,
                                IsEndLever = item.IsEndLever,
                                IsBuild = true
                            };
                            Funs.DB.Training_TrainTestDB.InsertOnSubmit(newTrainTestDB1);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            newTrainTestDB.TrainTestCode = item.TrainTestCode;
                            newTrainTestDB.TrainTestName = item.TrainTestName;
                            newTrainTestDB.SupTrainTestId = item.SupTrainTestId;
                            newTrainTestDB.IsEndLever = item.IsEndLever;
                            newTrainTestDB.IsBuild = true;

                            Funs.DB.SubmitChanges();
                        }

                    }
                }
            }

            SetLog("安全试题库类别", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 安全试题库明细信息从集团公司提取
        /// <summary>
        /// 安全试题库明细从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetTraining_TrainTestDBItemListToSUBCompleted(object sender, BLL.HSSEService.GetTraining_TrainTestDBItemListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var items = e.Result;
                if (items.Count() > 0)
                {
                    count = items.Count();
                    foreach (var item in items)
                    {
                        var newItem = BLL.TrainTestDBItemService.GetTrainTestDBItemById(item.TrainTestItemId);
                        if (newItem == null)
                        {
                            var TrainTest = BLL.TrainTestDBService.GetTrainTestDBById(item.TrainTestId);
                            if (TrainTest != null)
                            {
                                Model.Training_TrainTestDBItem newItem1 = new Model.Training_TrainTestDBItem
                                {
                                    TrainTestItemId = item.TrainTestItemId,
                                    TrainTestId = item.TrainTestId,
                                    TrainTestItemCode = item.TrainTestItemCode,
                                    TraiinTestItemName = item.TraiinTestItemName,
                                    CompileMan = item.CompileMan,
                                    CompileDate = item.CompileDate,
                                    UnitId = item.UnitId,
                                    IsPass = true
                                };
                                Funs.DB.Training_TrainTestDBItem.InsertOnSubmit(newItem1);
                                Funs.DB.SubmitChanges();
                            }
                        }
                        else
                        {                           
                            newItem.TrainTestId = item.TrainTestId;
                            newItem.TrainTestItemCode = item.TrainTestItemCode;
                            newItem.TraiinTestItemName = item.TraiinTestItemName;
                            newItem.CompileMan = item.CompileMan;
                            newItem.CompileDate = item.CompileDate;
                            newItem.UnitId = item.UnitId;
                            newItem.IsPass = true;

                            Funs.DB.SubmitChanges();
                        }
                        ////上传附件
                        BLL.FileInsertService.InsertAttachFile(item.AttachFileId, item.TrainTestId, item.AttachSource, item.AttachUrl, item.FileContext);
                    }
                }
            }

            SetLog("安全试题库明细", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 事故案例库类别信息从集团公司提取
        /// <summary>
        /// 事故案例库类别从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetEduTrain_AccidentCaseListToSUBCompleted(object sender, BLL.HSSEService.GetEduTrain_AccidentCaseListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var accidentCaseList = e.Result;
                if (accidentCaseList.Count() > 0)
                {
                    count = accidentCaseList.Count();
                    foreach (var item in accidentCaseList)
                    {
                        var newAccidentCase = BLL.AccidentCaseService.GetAccidentCaseById(item.AccidentCaseId);
                        if (newAccidentCase == null)
                        {
                            Model.EduTrain_AccidentCase newAccidentCase1 = new Model.EduTrain_AccidentCase
                            {
                                AccidentCaseId = item.AccidentCaseId,
                                AccidentCaseCode = item.AccidentCaseCode,
                                AccidentCaseName = item.AccidentCaseName,
                                SupAccidentCaseId = item.SupAccidentCaseId,
                                IsEndLever = item.IsEndLever,
                                IsBuild = true
                            };
                            Funs.DB.EduTrain_AccidentCase.InsertOnSubmit(newAccidentCase1);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            newAccidentCase.AccidentCaseCode = item.AccidentCaseCode;
                            newAccidentCase.AccidentCaseName = item.AccidentCaseName;
                            newAccidentCase.SupAccidentCaseId = item.SupAccidentCaseId;
                            newAccidentCase.IsEndLever = item.IsEndLever;
                            newAccidentCase.IsBuild = true;

                            Funs.DB.SubmitChanges();
                        }

                    }
                }
            }

            SetLog("事故案例库类别", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 事故案例库明细信息从集团公司提取
        /// <summary>
        /// 事故案例库明细从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetEduTrain_AccidentCaseItemListToSUBCompleted(object sender, BLL.HSSEService.GetEduTrain_AccidentCaseItemListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var items = e.Result;
                if (items.Count() > 0)
                {
                    count = items.Count();
                    foreach (var item in items)
                    {
                        var newItem = BLL.AccidentCaseItemService.GetAccidentCaseItemById(item.AccidentCaseItemId);
                        if (newItem == null)
                        {
                            var AccidentCase = BLL.AccidentCaseService.GetAccidentCaseById(item.AccidentCaseId);
                            if (AccidentCase != null)
                            {
                                Model.EduTrain_AccidentCaseItem newItem1 = new Model.EduTrain_AccidentCaseItem
                                {
                                    AccidentCaseItemId = item.AccidentCaseItemId,
                                    AccidentCaseId = item.AccidentCaseId,
                                    Activities = item.Activities,
                                    AccidentName = item.AccidentName,
                                    AccidentProfiles = item.AccidentProfiles,
                                    AccidentReview = item.AccidentReview,
                                    CompileMan = item.CompileMan,
                                    CompileDate = item.CompileDate,
                                    UnitId = item.UnitId,
                                    IsPass = true
                                };
                                Funs.DB.EduTrain_AccidentCaseItem.InsertOnSubmit(newItem1);
                                Funs.DB.SubmitChanges();
                            }
                        }
                        else
                        {
                            newItem.AccidentCaseId = item.AccidentCaseId;
                            newItem.Activities = item.Activities;
                            newItem.AccidentName = item.AccidentName;
                            newItem.AccidentProfiles = item.AccidentProfiles;
                            newItem.AccidentReview = item.AccidentReview;
                            newItem.CompileMan = item.CompileMan;
                            newItem.CompileDate = item.CompileDate;
                            newItem.UnitId = item.UnitId;
                            newItem.IsPass = true;

                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }

            SetLog("事故案例库明细", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 应知应会库类别信息从集团公司提取
        /// <summary>
        /// 应知应会库类别从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetTraining_KnowledgeListToSUBCompleted(object sender, BLL.HSSEService.GetTraining_KnowledgeListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var knowledgeList = e.Result;
                if (knowledgeList.Count() > 0)
                {
                    count = knowledgeList.Count();
                    foreach (var item in knowledgeList)
                    {
                        var newKnowledge = BLL.KnowledgeService.GetKnowLedgeById(item.KnowledgeId);
                        if (newKnowledge == null)
                        {
                            Model.Training_Knowledge newKnowledge1 = new Model.Training_Knowledge
                            {
                                KnowledgeId = item.KnowledgeId,
                                KnowledgeCode = item.KnowledgeCode,
                                KnowledgeName = item.KnowledgeName,
                                SupKnowledgeId = item.SupKnowledgeId,
                                IsEndLever = item.IsEndLever,
                                IsBuild = true
                            };
                            Funs.DB.Training_Knowledge.InsertOnSubmit(newKnowledge1);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            newKnowledge.KnowledgeCode = item.KnowledgeCode;
                            newKnowledge.KnowledgeName = item.KnowledgeName;
                            newKnowledge.SupKnowledgeId = item.SupKnowledgeId;
                            newKnowledge.IsEndLever = item.IsEndLever;
                            newKnowledge.IsBuild = true;

                            Funs.DB.SubmitChanges();
                        }

                    }
                }
            }

            SetLog("应知应会库类别", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 应知应会库明细信息从集团公司提取
        /// <summary>
        /// 应知应会库明细从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetTraining_KnowledgeItemListToSUBCompleted(object sender, BLL.HSSEService.GetTraining_KnowledgeItemListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var items = e.Result;
                if (items.Count() > 0)
                {
                    count = items.Count();
                    foreach (var item in items)
                    {
                        var newItem = BLL.KnowledgeItemService.GetKnowledgeItemById(item.KnowledgeItemId);
                        if (newItem == null)
                        {
                            var Knowledge = BLL.KnowledgeService.GetKnowLedgeById(item.KnowledgeId);
                            if (Knowledge != null)
                            {
                                Model.Training_KnowledgeItem newItem1 = new Model.Training_KnowledgeItem
                                {
                                    KnowledgeItemId = item.KnowledgeItemId,
                                    KnowledgeId = item.KnowledgeId,
                                    KnowledgeItemCode = item.KnowledgeItemCode,
                                    KnowledgeItemName = item.KnowledgeItemName,
                                    Remark = item.Remark,
                                    CompileMan = item.CompileMan,
                                    CompileDate = item.CompileDate,
                                    UnitId = item.UnitId,
                                    IsPass = true
                                };
                                Funs.DB.Training_KnowledgeItem.InsertOnSubmit(newItem1);
                                Funs.DB.SubmitChanges();
                            }
                        }
                        else
                        {
                            newItem.KnowledgeId = item.KnowledgeId;
                            newItem.KnowledgeItemCode = item.KnowledgeItemCode;
                            newItem.KnowledgeItemName = item.KnowledgeItemName;
                            newItem.Remark = item.Remark;
                            newItem.CompileMan = item.CompileMan;
                            newItem.CompileDate = item.CompileDate;
                            newItem.UnitId = item.UnitId;
                            newItem.IsPass = true;

                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }

            SetLog("应知应会库明细", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 危险源清单类别信息从集团公司提取
        /// <summary>
        /// 危险源清单类别从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetTechnique_HazardListTypeListToSUBCompleted(object sender, BLL.HSSEService.GetTechnique_HazardListTypeListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var hazardListTypeList = e.Result;
                if (hazardListTypeList.Count() > 0)
                {
                    count = hazardListTypeList.Count();
                    foreach (var item in hazardListTypeList)
                    {
                        var newHazardListType = BLL.HazardListTypeService.GetHazardListTypeById(item.HazardListTypeId);
                        if (newHazardListType == null)
                        {
                            Model.Technique_HazardListType newHazardListType1 = new Model.Technique_HazardListType
                            {
                                HazardListTypeId = item.HazardListTypeId,
                                HazardListTypeCode = item.HazardListTypeCode,
                                HazardListTypeName = item.HazardListTypeName,
                                SupHazardListTypeId = item.SupHazardListTypeId,
                                IsEndLevel = item.IsEndLevel,
                                IsBuild = true
                            };
                            Funs.DB.Technique_HazardListType.InsertOnSubmit(newHazardListType1);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            newHazardListType.HazardListTypeCode = item.HazardListTypeCode;
                            newHazardListType.HazardListTypeName = item.HazardListTypeName;
                            newHazardListType.SupHazardListTypeId = item.SupHazardListTypeId;
                            newHazardListType.IsEndLevel = item.IsEndLevel;
                            newHazardListType.IsBuild = true;

                            Funs.DB.SubmitChanges();
                        }

                    }
                }
            }

            SetLog("危险源清单类别", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 危险源清单明细信息从集团公司提取
        /// <summary>
        /// 危险源清单明细从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetTechnique_HazardListListToSUBCompleted(object sender, BLL.HSSEService.GetTechnique_HazardListListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var items = e.Result;
                if (items.Count() > 0)
                {
                    count = items.Count();
                    foreach (var item in items)
                    {
                        var newItem = BLL.HazardListService.GetHazardListById(item.HazardId);
                        if (newItem == null)
                        {
                            var newHazardListType = BLL.HazardListTypeService.GetHazardListTypeById(item.HazardListTypeId);
                            if (newHazardListType != null)
                            {
                                Model.Technique_HazardList newItem1 = new Model.Technique_HazardList
                                {
                                    HazardId = item.HazardId,
                                    HazardListTypeId = item.HazardListTypeId,
                                    HazardCode = item.HazardCode,
                                    HazardItems = item.HazardItems,
                                    DefectsType = item.DefectsType,
                                    MayLeadAccidents = item.MayLeadAccidents,
                                    HelperMethod = item.HelperMethod,
                                    HazardJudge_L = item.HazardJudge_L,
                                    HazardJudge_E = item.HazardJudge_E,
                                    HazardJudge_C = item.HazardJudge_C,
                                    HazardJudge_D = item.HazardJudge_D,
                                    HazardLevel = item.HazardLevel,
                                    ControlMeasures = item.ControlMeasures,
                                    CompileMan = item.CompileMan,
                                    CompileDate = item.CompileDate,
                                    UnitId = item.UnitId,
                                    IsPass = true
                                };
                                Funs.DB.Technique_HazardList.InsertOnSubmit(newItem1);
                                Funs.DB.SubmitChanges();
                            }
                        }
                        else
                        {
                            newItem.HazardListTypeId = item.HazardListTypeId;
                            newItem.HazardCode = item.HazardCode;
                            newItem.HazardItems = item.HazardItems;
                            newItem.DefectsType = item.DefectsType;
                            newItem.MayLeadAccidents = item.MayLeadAccidents;
                            newItem.HelperMethod = item.HelperMethod;
                            newItem.HazardJudge_L = item.HazardJudge_L;
                            newItem.HazardJudge_E = item.HazardJudge_E;
                            newItem.HazardJudge_C = item.HazardJudge_C;
                            newItem.HazardJudge_D = item.HazardJudge_D;
                            newItem.HazardLevel = item.HazardLevel;
                            newItem.ControlMeasures = item.ControlMeasures;
                            newItem.CompileMan = item.CompileMan;
                            newItem.CompileDate = item.CompileDate;
                            newItem.UnitId = item.UnitId;
                            newItem.IsPass = true;

                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }

            SetLog("危险源清单明细", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 安全隐患类别信息从集团公司提取
        /// <summary>
        /// 安全隐患类别从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetTechnique_RectifyListToSUBCompleted(object sender, BLL.HSSEService.GetTechnique_RectifyListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var rectifyList = e.Result;
                if (rectifyList.Count() > 0)
                {
                    count = rectifyList.Count();
                    foreach (var item in rectifyList)
                    {
                        var newRectify = BLL.RectifyService.GetRectifyById(item.RectifyId);
                        if (newRectify == null)
                        {
                            Model.Technique_Rectify newRectify1 = new Model.Technique_Rectify
                            {
                                RectifyId = item.RectifyId,
                                RectifyCode = item.RectifyCode,
                                RectifyName = item.RectifyName,
                                SupRectifyId = item.SupRectifyId,
                                IsEndLever = item.IsEndLever,
                                IsBuild = true
                            };
                            Funs.DB.Technique_Rectify.InsertOnSubmit(newRectify1);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            newRectify.RectifyCode = item.RectifyCode;
                            newRectify.RectifyName = item.RectifyName;
                            newRectify.SupRectifyId = item.SupRectifyId;
                            newRectify.IsEndLever = item.IsEndLever;
                            newRectify.IsBuild = true;

                            Funs.DB.SubmitChanges();
                        }

                    }
                }
            }

            SetLog("安全隐患类别", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 安全隐患明细信息从集团公司提取
        /// <summary>
        /// 安全隐患明细从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetTechnique_RectifyItemListToSUBCompleted(object sender, BLL.HSSEService.GetTechnique_RectifyItemListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var items = e.Result;
                if (items.Count() > 0)
                {
                    count = items.Count();
                    foreach (var item in items)
                    {
                        var newItem = BLL.RectifyItemService.GetRectifyItemById(item.RectifyItemId);
                        if (newItem == null)
                        {
                            var newRectify = BLL.RectifyService.GetRectifyById(item.RectifyId);
                            if (newRectify != null)
                            {
                                Model.Technique_RectifyItem newItem1 = new Model.Technique_RectifyItem
                                {
                                    RectifyItemId = item.RectifyItemId,
                                    RectifyId = item.RectifyId,
                                    HazardSourcePoint = item.HazardSourcePoint,
                                    RiskAnalysis = item.RiskAnalysis,
                                    RiskPrevention = item.RiskPrevention,
                                    SimilarRisk = item.SimilarRisk,
                                    CompileMan = item.CompileMan,
                                    CompileDate = item.CompileDate,
                                    UnitId = item.UnitId,
                                    IsPass = true
                                };
                                Funs.DB.Technique_RectifyItem.InsertOnSubmit(newItem1);
                                Funs.DB.SubmitChanges();
                            }
                        }
                        else
                        {
                            newItem.RectifyId = item.RectifyId;
                            newItem.HazardSourcePoint = item.HazardSourcePoint;
                            newItem.RiskAnalysis = item.RiskAnalysis;
                            newItem.RiskPrevention = item.RiskPrevention;
                            newItem.SimilarRisk = item.SimilarRisk;
                            newItem.CompileMan = item.CompileMan;
                            newItem.CompileDate = item.CompileDate;
                            newItem.UnitId = item.UnitId;
                            newItem.IsPass = true;

                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }

            SetLog("安全隐患明细", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion
        
        #region 安全监督检查整改信息从集团公司提取
        /// <summary>
        /// 安全监督检查整改从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetCheck_CheckRectifyListToSUBCompleted(object sender, BLL.HSSEService.GetCheck_CheckRectifyListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                List<string> ids = new List<string>();
                var items = e.Result;
                if (items.Count() > 0)
                {
                    count = items.Count();
                    foreach (var item in items)
                    {
                        if (!ids.Contains(item.CheckRectifyId))
                        {
                            var newRectify = BLL.CheckRectifyService.GetCheckRectifyByCheckRectifyId(item.CheckRectifyId);
                            if (newRectify == null)
                            {
                                ids.Add(item.CheckRectifyId);
                                Model.Check_CheckRectify newCheckRectify = new Model.Check_CheckRectify
                                {
                                    CheckRectifyId = item.CheckRectifyId,
                                    CheckRectifyCode = item.CheckRectifyCode,
                                    ProjectId = item.ProjectId,
                                    UnitId = item.UnitId,
                                    CheckDate = item.CheckDate,
                                    IssueMan = item.IssueMan,
                                    IssueDate = item.IssueDate,
                                    HandleState = item.HandleState
                                };
                                Funs.DB.Check_CheckRectify.InsertOnSubmit(newCheckRectify);
                                Funs.DB.SubmitChanges();

                                //获取对应主表主键的明细集合
                                var table5Items = items.Where(x => x.CheckRectifyId == item.CheckRectifyId);
                                foreach (var newItem in table5Items)
                                {
                                    var oldItem5 = Funs.DB.Check_CheckInfo_Table5Item.FirstOrDefault(x => x.ID == newItem.Table5ItemId);
                                    if (oldItem5 == null)
                                    {
                                        Model.Check_CheckInfo_Table5Item newCheckRectifyItem = new Model.Check_CheckInfo_Table5Item
                                        {
                                            ID = newItem.Table5ItemId,
                                            SortIndex = newItem.SortIndex,
                                            WorkType = newItem.WorkType,
                                            DangerPoint = newItem.DangerPoint,
                                            RiskExists = newItem.RiskExists,
                                            IsProject = newItem.IsProject,
                                            CheckMan = newItem.CheckMan,
                                            SubjectUnitMan = newItem.SubjectUnitMan
                                        };
                                        Funs.DB.Check_CheckInfo_Table5Item.InsertOnSubmit(newCheckRectifyItem);
                                        Funs.DB.SubmitChanges();

                                        ////上传附件
                                        if (!string.IsNullOrEmpty(newItem.AttachFileId))
                                        {
                                            BLL.FileInsertService.InsertAttachFile(newItem.AttachFileId, newItem.Table5ItemId, newItem.AttachSource, newItem.AttachUrl, newItem.FileContext);
                                        }
                                    }

                                    var oldItem = BLL.CheckRectifyItemService.GetCheckRectifyItemByCheckRectifyItemId(newItem.CheckRectifyItemId);
                                    if (oldItem == null)
                                    {
                                        Model.Check_CheckRectifyItem newCheckRectifyItem = new Model.Check_CheckRectifyItem
                                        {
                                            CheckRectifyItemId = newItem.CheckRectifyItemId,
                                            CheckRectifyId = newItem.CheckRectifyId,
                                            Table5ItemId = newItem.Table5ItemId,
                                            ConfirmMan = newItem.ConfirmMan,
                                            ConfirmDate = newItem.ConfirmDate,
                                            OrderEndDate = newItem.OrderEndDate,
                                            OrderEndPerson = newItem.OrderEndPerson,
                                            RealEndDate = newItem.RealEndDate
                                        };
                                        Funs.DB.Check_CheckRectifyItem.InsertOnSubmit(newCheckRectifyItem);
                                        Funs.DB.SubmitChanges();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            SetLog("集团检查整改", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion
        
        #region 安全监督检查报告信息从集团公司提取
        /// <summary>
        /// 安全监督检查报告从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetCheck_CheckInfo_Table8ItemListToSUBCompleted(object sender, BLL.HSSEService.GetCheck_CheckInfo_Table8ItemListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                List<string> ids = new List<string>();
                var items = e.Result;
                if (items.Count() > 0)
                {
                    count = items.Count();
                    foreach (var item in items)
                    {
                        if (!ids.Contains(item.CheckInfoId))
                        {
                            var updateCheckInfo = Funs.DB.Check_CheckInfo.FirstOrDefault(x => x.CheckInfoId == item.CheckInfoId);
                            if (updateCheckInfo == null)
                            {
                                ids.Add(item.CheckInfoId);
                                Model.Check_CheckInfo newCheckInfo = new Model.Check_CheckInfo
                                {
                                    CheckInfoId = item.CheckInfoId,
                                    CheckTypeName = item.CheckTypeName,
                                    SubjectUnitId = item.SubjectUnitId,
                                    SubjectUnitAdd = item.SubjectUnitAdd,
                                    SubjectUnitMan = item.SubjectUnitMan,
                                    SubjectUnitTel = item.SubjectUnitTel,
                                    CheckStartTime = item.CheckStartTime,
                                    CheckEndTime = item.CheckEndTime,
                                    SubjectObject = item.SubjectObject
                                };
                                Funs.DB.Check_CheckInfo.InsertOnSubmit(newCheckInfo);
                                Funs.DB.SubmitChanges();
                            }
                            else
                            {
                                updateCheckInfo.CheckInfoId = item.CheckInfoId;
                                updateCheckInfo.CheckTypeName = item.CheckTypeName;
                                updateCheckInfo.SubjectUnitId = item.SubjectUnitId;
                                updateCheckInfo.SubjectUnitAdd = item.SubjectUnitAdd;
                                updateCheckInfo.SubjectUnitMan = item.SubjectUnitMan;
                                updateCheckInfo.SubjectUnitTel = item.SubjectUnitTel;
                                updateCheckInfo.CheckStartTime = item.CheckStartTime;
                                updateCheckInfo.CheckEndTime = item.CheckEndTime;
                                updateCheckInfo.SubjectObject = item.SubjectObject;                               
                                Funs.DB.SubmitChanges();
                            }
                        }
                        
                        var updateTable8 = Funs.DB.Check_CheckInfo_Table8.FirstOrDefault(x => x.CheckItemId == item.CheckItemId);
                        if (updateTable8 == null)
                        {
                            Model.Check_CheckInfo_Table8 newTable8 = new Model.Check_CheckInfo_Table8
                            {
                                CheckItemId = item.CheckItemId,
                                CheckInfoId = item.CheckInfoId,
                                Values1 = item.Values1,
                                Values2 = item.Values2,
                                Values3 = item.Values3,
                                Values4 = item.Values4,
                                Values5 = item.Values5,
                                Values6 = item.Values6,
                                Values7 = item.Values7,
                                Values8 = item.Values8
                            };
                            Funs.DB.Check_CheckInfo_Table8.InsertOnSubmit(newTable8);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            updateTable8.Values1 = item.Values1;
                            updateTable8.Values2 = item.Values2;
                            updateTable8.Values3 = item.Values3;
                            updateTable8.Values4 = item.Values4;
                            updateTable8.Values5 = item.Values5;
                            updateTable8.Values6 = item.Values6;
                            updateTable8.Values7 = item.Values7;
                            updateTable8.Values8 = item.Values8;
                            Funs.DB.SubmitChanges();
                        }
                        
                        var updateTable8Item = Funs.DB.Check_CheckInfo_Table8Item.FirstOrDefault(x => x.ID == item.ID);
                        if (updateTable8Item == null)
                        {
                            Model.Check_CheckInfo_Table8Item newTable8Item = new Model.Check_CheckInfo_Table8Item
                            {
                                ID = item.ID,
                                CheckInfoId = item.CheckInfoId,
                                Name = item.Name,
                                Sex = item.Sex,
                                UnitName = item.UnitName,
                                PostName = item.PostName,
                                WorkTitle = item.WorkTitle,
                                CheckPostName = item.CheckPostName,
                                CheckDate = item.CheckDate,
                                SortIndex = item.SortIndex
                            };
                            Funs.DB.Check_CheckInfo_Table8Item.InsertOnSubmit(newTable8Item);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {                            
                            updateTable8Item.ID = item.ID;
                            updateTable8Item.CheckInfoId = item.CheckInfoId;
                            updateTable8Item.Name = item.Name;
                            updateTable8Item.Sex = item.Sex;
                            updateTable8Item.UnitName = item.UnitName;
                            updateTable8Item.PostName = item.PostName;
                            updateTable8Item.WorkTitle = item.WorkTitle;
                            updateTable8Item.CheckPostName = item.CheckPostName;
                            updateTable8Item.CheckDate = item.CheckDate;
                            updateTable8Item.SortIndex = item.SortIndex;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }
            SetLog("集团检查报告", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 企业安全从集团公司提取
        /// <summary>
        /// 企业安全从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetSupervise_SubUnitReportListToSUBCompleted(object sender, BLL.HSSEService.GetSupervise_SubUnitReportListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var subUnitReportList = e.Result;
                if (subUnitReportList.Count() > 0)
                {
                    count = subUnitReportList.Count();
                    foreach (var item in subUnitReportList)
                    {
                        var newSubUnitReport = BLL.SubUnitReportService.GetSubUnitReportById(item.SubUnitReportId);
                        if (newSubUnitReport == null)
                        {
                            Model.Supervise_SubUnitReport newSubUnitReport1 = new Model.Supervise_SubUnitReport
                            {
                                SubUnitReportId = item.SubUnitReportId,
                                SubUnitReportCode = item.SubUnitReportCode,
                                SubUnitReportName = item.SubUnitReportName,
                                SupSubUnitReportId = item.SupSubUnitReportId,
                                IsEndLever = item.IsEndLever
                            };
                            Funs.DB.Supervise_SubUnitReport.InsertOnSubmit(newSubUnitReport1);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            newSubUnitReport.SubUnitReportCode = item.SubUnitReportCode;
                            newSubUnitReport.SubUnitReportName = item.SubUnitReportName;
                            newSubUnitReport.SupSubUnitReportId = item.SupSubUnitReportId;
                            newSubUnitReport.IsEndLever = item.IsEndLever;

                            Funs.DB.SubmitChanges();
                        }

                    }
                }
            }
            SetLog("企业安全文件上报", BLL.Const.BtnDownload, count.ToString(), e.Error);
        }
        #endregion

        #region 企业安全文件上报明细从集团公司提取
        /// <summary>
        /// 企业安全文件上报明细从集团公司提取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_GetSupervise_SubUnitReportItemListToSUBCompleted(object sender, BLL.HSSEService.GetSupervise_SubUnitReportItemListToSUBCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null && e.Result != null)
            {
                var items = e.Result;
                if (items.Count() > 0)
                {
                    count = items.Count();
                    foreach (var item in items)
                    {
                        var newItem = BLL.SubUnitReportItemService.GetSubUnitReportItemById(item.SubUnitReportItemId);
                        if (newItem == null)
                        {
                            var newSubUnitReport = BLL.SubUnitReportService.GetSubUnitReportById(item.SubUnitReportId);
                            if (newSubUnitReport != null)
                            {
                                Model.Supervise_SubUnitReportItem newItem1 = new Model.Supervise_SubUnitReportItem
                                {
                                    SubUnitReportItemId = item.SubUnitReportItemId,
                                    SubUnitReportId = item.SubUnitReportId,
                                    UnitId = item.UnitId,
                                    PlanReortDate = item.PlanReortDate,
                                    State = item.State
                                };
                                Funs.DB.Supervise_SubUnitReportItem.InsertOnSubmit(newItem1);
                                Funs.DB.SubmitChanges();
                            }
                        }
                        else
                        {
                            newItem.SubUnitReportId = item.SubUnitReportId;
                            newItem.UnitId = item.UnitId;
                            newItem.PlanReortDate = item.PlanReortDate;
                            newItem.State = item.State;

                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }

            SetLog("企业安全文件上报明细", BLL.Const.BtnDownload, count.ToString(), e.Error);           
        }
        #endregion
        #endregion

        #region 从企业向集团公司上报
        #region 法律法规上报到集团公司
        /// <summary>
        /// 法律法规上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertLaw_LawRegulationListTableCompleted(object sender, BLL.HSSEService.DataInsertLaw_LawRegulationListTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var law = BLL.LawRegulationListService.GetLawRegulationListById(item);
                    if (law != null)
                    {
                        law.UpState = BLL.Const.UpState_3;
                        BLL.LawRegulationListService.UpdateLawRegulationList(law);
                    }
                }
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("法律法规", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 标准规范上报到集团公司
        /// <summary>
        /// 标准规范上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertLaw_HSSEStandardsListTableCompleted(object sender, BLL.HSSEService.DataInsertLaw_HSSEStandardsListTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var standardsList = BLL.HSSEStandardsListService.GetHSSEStandardsListByHSSEStandardsListId(item);
                    if (standardsList != null)
                    {
                        standardsList.UpState = BLL.Const.UpState_3;
                        BLL.HSSEStandardsListService.UpdateHSSEStandardsList(standardsList);
                    }
                }
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("标准规范", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 安全生产规章制度上报到集团公司
        /// <summary>
        /// 安全生产规章制度上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertLaw_RulesRegulationsTableCompleted(object sender, BLL.HSSEService.DataInsertLaw_RulesRegulationsTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var rulesRegulations = BLL.RulesRegulationsService.GetRulesRegulationsById(item);
                    if (rulesRegulations != null)
                    {
                        rulesRegulations.UpState = BLL.Const.UpState_3;
                        BLL.RulesRegulationsService.UpdateRulesRegulations(rulesRegulations);
                    }
                }
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("安全生产规章制度", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 管理规定上报到集团公司
        /// <summary>
        /// 管理规定上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertLaw_ManageRuleTableCompleted(object sender, BLL.HSSEService.DataInsertLaw_ManageRuleTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var manageRule = BLL.ManageRuleService.GetManageRuleById(item);
                    if (manageRule != null)
                    {
                        manageRule.UpState = BLL.Const.UpState_3;
                        BLL.ManageRuleService.UpdateManageRule(manageRule);
                    }
                }
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("安全管理规定", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 职工伤亡事故原因分析报表从企业上报到集团公司
        /// <summary>
        /// 职工伤亡事故原因分析报表从企业上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertInformation_AccidentCauseReportTableCompleted(object sender, BLL.HSSEService.DataInsertInformation_AccidentCauseReportTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var report = BLL.AccidentCauseReportService.GetAccidentCauseReportByAccidentCauseReportId(item);
                    if (report != null)
                    {
                        report.UpState = BLL.Const.UpState_3;
                        BLL.AccidentCauseReportService.UpdateAccidentCauseReport(report);

                        ////更新催报信息 
                        var urgeReport = Funs.DB.Information_UrgeReport.FirstOrDefault(x => x.UnitId == report.UnitId && x.ReprotType == BLL.Const.ReportType_2 && x.YearId == report.Year.ToString() && x.MonthId == report.Month.ToString());
                        if (urgeReport != null)
                        {
                            urgeReport.IsComplete = true;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("职工伤亡事故原因分析报表", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 应急演练开展情况季报表从企业上报到集团公司
        /// <summary>
        /// 应急演练开展情况季报表从企业上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertInformation_DrillConductedQuarterlyReportTableCompleted(object sender, BLL.HSSEService.DataInsertInformation_DrillConductedQuarterlyReportTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var report = BLL.DrillConductedQuarterlyReportService.GetDrillConductedQuarterlyReportById(item);
                    if (report != null)
                    {
                        report.UpState = BLL.Const.UpState_3;
                        BLL.DrillConductedQuarterlyReportService.UpdateDrillConductedQuarterlyReport(report);

                        ////更新催报信息 
                        var urgeReport = Funs.DB.Information_UrgeReport.FirstOrDefault(x => x.UnitId == report.UnitId && x.ReprotType == BLL.Const.ReportType_4 && x.YearId == report.YearId.ToString() && x.QuarterId == report.Quarter.ToString());
                        if (urgeReport != null)
                        {
                            urgeReport.IsComplete = true;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("应急演练开展情况季报表", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 应急演练工作计划半年报表从企业上报到集团公司
        /// <summary>
        /// 应急演练工作计划半年报表从企业上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertInformation_DrillPlanHalfYearReportTableCompleted(object sender, BLL.HSSEService.DataInsertInformation_DrillPlanHalfYearReportTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var report = BLL.DrillPlanHalfYearReportService.GetDrillPlanHalfYearReportById(item);
                    if (report != null)
                    {
                        report.UpState = BLL.Const.UpState_3;
                        BLL.DrillPlanHalfYearReportService.UpdateDrillPlanHalfYearReport(report);

                        ////更新催报信息 
                        var urgeReport = Funs.DB.Information_UrgeReport.FirstOrDefault(x => x.UnitId == report.UnitId && x.ReprotType == BLL.Const.ReportType_5 && x.YearId == report.YearId.ToString() && x.HalfYearId == report.HalfYearId.ToString());
                        if (urgeReport != null)
                        {
                            urgeReport.IsComplete = true;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("应急演练工作计划半年报表", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 百万工时安全统计月报表从企业上报到集团公司
        /// <summary>
        /// 百万工时安全统计月报表从企业上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertInformation_MillionsMonthlyReportTableCompleted(object sender, BLL.HSSEService.DataInsertInformation_MillionsMonthlyReportTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var report = BLL.MillionsMonthlyReportService.GetMillionsMonthlyReportByMillionsMonthlyReportId(item);
                    if (report != null)
                    {
                        report.UpState = BLL.Const.UpState_3;
                        BLL.MillionsMonthlyReportService.UpdateMillionsMonthlyReport(report);

                        ////更新催报信息 
                        var urgeReport = Funs.DB.Information_UrgeReport.FirstOrDefault(x => x.UnitId == report.UnitId && x.ReprotType == BLL.Const.ReportType_1 && x.YearId == report.Year.ToString() && x.MonthId == report.Month.ToString());
                        if (urgeReport != null)
                        {
                            urgeReport.IsComplete = true;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("百万工时安全统计月报表", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 安全生产数据季报从企业上报到集团公司
        /// <summary>
        /// 安全生产数据季报从企业上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertInformation_SafetyQuarterlyReportTableCompleted(object sender, BLL.HSSEService.DataInsertInformation_SafetyQuarterlyReportTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var report = BLL.SafetyQuarterlyReportService.GetSafetyQuarterlyReportById(item);
                    if (report != null)
                    {
                        report.UpState = BLL.Const.UpState_3;
                        BLL.SafetyQuarterlyReportService.UpdateSafetyQuarterlyReport(report);

                        ////更新催报信息 
                        var urgeReport = Funs.DB.Information_UrgeReport.FirstOrDefault(x => x.UnitId == report.UnitId && x.ReprotType == BLL.Const.ReportType_3 && x.YearId == report.YearId.ToString() && x.QuarterId == report.Quarters.ToString());
                        if (urgeReport != null)
                        {
                            urgeReport.IsComplete = true;
                            Funs.DB.SubmitChanges();
                        }
                    }
                }              
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("安全生产数据季报", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region HAZOP管理上报到集团公司
        /// <summary>
        /// HAZOP管理上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_HAZOPTableCompleted(object sender, BLL.HSSEService.DataInsertTechnique_HAZOPTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var hazop = BLL.HAZOPService.GetHAZOPById(item);
                    if (hazop != null)
                    {
                        hazop.UpState = BLL.Const.UpState_3;
                        BLL.HAZOPService.UpdateHAZOP(hazop);
                    }
                }
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("HAZOP管理", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 安全评价上报到集团公司
        /// <summary>
        /// 安全评价上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_AppraiseTableCompleted(object sender, BLL.HSSEService.DataInsertTechnique_AppraiseTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var appraise = BLL.AppraiseService.GetAppraiseById(item);
                    if (appraise != null)
                    {
                        appraise.UpState = BLL.Const.UpState_3;
                        BLL.AppraiseService.UpdateAppraise(appraise);
                    }
                }
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("安全评价", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 应急预案上报到集团公司
        /// <summary>
        /// 应急预案上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_EmergencyTableCompleted(object sender, BLL.HSSEService.DataInsertTechnique_EmergencyTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var emergency = BLL.EmergencyService.GetEmergencyListById(item);
                    if (emergency != null)
                    {
                        emergency.UpState = BLL.Const.UpState_3;
                        BLL.EmergencyService.UpdateEmergencyList(emergency);
                    }
                }
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("应急预案", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 专项方案上报到集团公司
        /// <summary>
        /// 专项方案上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_SpecialSchemeTableCompleted(object sender, BLL.HSSEService.DataInsertTechnique_SpecialSchemeTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var specialScheme = BLL.SpecialSchemeService.GetSpecialSchemeListById(item);
                    if (specialScheme != null)
                    {
                        specialScheme.UpState = BLL.Const.UpState_3;
                        BLL.SpecialSchemeService.UpdateSpecialSchemeList(specialScheme);
                    }
                }
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("专项方案", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 培训教材明细从企业上报到集团公司
        /// <summary>
        /// 培训教材明细从企业上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTraining_TrainingItemTableCompleted(object sender, BLL.HSSEService.DataInsertTraining_TrainingItemTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var trainingItem = BLL.TrainingItemService.GetTrainingItemByTrainingItemId(item);
                    if (trainingItem != null)
                    {
                        trainingItem.UpState = BLL.Const.UpState_3;
                        BLL.TrainingItemService.UpdateTrainingItemIsPass(trainingItem);
                    }
                }
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("培训教材明细", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 安全试题明细从企业上报到集团公司
        /// <summary>
        /// 安全试题明细从企业上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTraining_TrainTestDBItemTableCompleted(object sender, BLL.HSSEService.DataInsertTraining_TrainTestDBItemTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var trainTestDBItem = BLL.TrainTestDBItemService.GetTrainTestDBItemById(item);
                    if (trainTestDBItem != null)
                    {
                        trainTestDBItem.UpState = BLL.Const.UpState_3;
                        BLL.TrainTestDBItemService.UpdateTrainTestDBItemIsPass(trainTestDBItem);
                    }
                }
                
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("安全试题明细", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 事故案例明细从企业上报到集团公司
        /// <summary>
        /// 事故案例明细从企业上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertEduTrain_AccidentCaseItemTableCompleted(object sender, BLL.HSSEService.DataInsertEduTrain_AccidentCaseItemTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var accidentCaseItem = BLL.AccidentCaseItemService.GetAccidentCaseItemById(item);
                    if (accidentCaseItem != null)
                    {
                        accidentCaseItem.UpState = BLL.Const.UpState_3;
                        BLL.AccidentCaseItemService.UpdateAccidentCaseItemIsPass(accidentCaseItem);
                    }
                }
            }

            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("事故案例明细", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 应知应会明细从企业上报到集团公司
        /// <summary>
        /// 应知应会明细从企业上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTraining_KnowledgeItemTableCompleted(object sender, BLL.HSSEService.DataInsertTraining_KnowledgeItemTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var knowledgeItem = BLL.KnowledgeItemService.GetKnowledgeItemById(item);
                    if (knowledgeItem != null)
                    {
                        knowledgeItem.UpState = BLL.Const.UpState_3;
                        BLL.KnowledgeItemService.UpdateKnowledgeItemIsPass(knowledgeItem);
                    }
                }
            }

            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("应知应会明细", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 危险源清单明细从企业上报到集团公司
        /// <summary>
        /// 危险源清单明细从企业上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_HazardListTableCompleted(object sender, BLL.HSSEService.DataInsertTechnique_HazardListTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var hazardList = BLL.HazardListService.GetHazardListById(item);
                    if (hazardList != null)
                    {
                        hazardList.UpState = BLL.Const.UpState_3;
                        BLL.HazardListService.UpdateHazardListIsPass(hazardList);
                    }
                }
            }

            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("危险源清单明细", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 安全隐患明细从企业上报到集团公司
        /// <summary>
        /// 安全隐患明细从企业上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertTechnique_RectifyItemTableCompleted(object sender, BLL.HSSEService.DataInsertTechnique_RectifyItemTableCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                var idList = e.Result;
                foreach (var item in idList)
                {
                    var rectifyItem = BLL.RectifyItemService.GetRectifyItemById(item);
                    if (rectifyItem != null)
                    {
                        rectifyItem.UpState = BLL.Const.UpState_3;
                        BLL.RectifyItemService.UpdateRectifyItemIsPass(rectifyItem);
                    }
                }
            }
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("安全隐患明细", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 安全专家上报到集团公司
        ///// <summary>
        ///// 安全专家上报到集团公司
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void poxy_DataInsertTechnique_ExpertTableCompleted(object sender, BLL.HSSEService.DataInsertTechnique_ExpertTableCompletedEventArgs e)
        //{
        //    if (e.Error == null)
        //    {
        //        var idList = e.Result;
        //        foreach (var item in idList)
        //        {
        //            var expert = BLL.ExpertService.GetExpertById(item);
        //            if (expert != null)
        //            {
        //                expert.UpState = BLL.Const.UpState_3;
        //                BLL.ExpertService.UpdateExpertIsPass(expert);
        //            }
        //        }
        //        BLL.LogService.AddSys_Log(this.CurrUser,, "【安全专家】上报到集团公司" + idList.Count.ToString() + "条数据；");
        //    }
        //    else
        //    {
        //        BLL.LogService.AddSys_Log(this.CurrUser,, "【安全专家】上报到集团公司失败；");
        //    }
        //}
        #endregion

        #region 安全监督检查整改上报到集团公司
        /// <summary>
        /// 安全监督检查整改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertCheck_CheckRectifyTableCompleted(object sender, BLL.HSSEService.DataInsertCheck_CheckRectifyTableCompletedEventArgs e)
        {           
            int count = 0;
            if (e.Error == null)
            {
                var idList = e.Result;
                count = e.Result.Count;
                foreach (var item in idList)
                {
                    var getCheckRectify = BLL.CheckRectifyService.GetCheckRectifyByCheckRectifyId(item);
                    if (getCheckRectify != null)
                    {
                        var itesm = Funs.DB.Check_CheckRectifyItem.FirstOrDefault(x => x.CheckRectifyId == item && !x.RealEndDate.HasValue);
                        if (itesm == null)
                        {
                            getCheckRectify.HandleState = BLL.Const.State_3;    //已签发但未完成
                            BLL.CheckRectifyService.UpdateCheckRectify(getCheckRectify);
                        }
                    }
                }
            }

            SetLog("集团检查整改", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 企业安全文件上报明细
        /// <summary>
        /// 企业安全文件上报明细上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertSupervise_SubUnitReportItemTableCompleted(object sender, BLL.HSSEService.DataInsertSupervise_SubUnitReportItemItemTableCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null)
            {
                var idList = e.Result;
                count = e.Result.Count;
                foreach (var item in idList)
                {
                    var subUnitReportItem = BLL.SubUnitReportItemService.GetSubUnitReportItemById(item);
                    if (subUnitReportItem != null)
                    {
                        subUnitReportItem.UpState = BLL.Const.UpState_3;
                        subUnitReportItem.State = BLL.Const.UpState_3;
                        BLL.SubUnitReportItemService.UpdateSubUnitReportItem(subUnitReportItem);
                    }
                }
            }

            SetLog("企业安全文件上报", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion

        #region 安全管理机构
        /// <summary>
        /// 安全管理机构上报到集团公司
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void poxy_DataInsertHSSESystem_HSSEManageItemTableCompleted(object sender, BLL.HSSEService.DataInsertHSSESystem_HSSEManageItemTableCompletedEventArgs e)
        {
            int count = 0;
            if (e.Error == null)
            {
                count = e.Result.Count;
            }

            SetLog("安全管理机构", BLL.Const.BtnUploadResources, count.ToString(), e.Error);
        }
        #endregion
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="count"></param>
        /// <param name="type"></param>
        /// <param name="error"></param>
        private void SetLog(string pageName,string count,string type, System.Exception error)
        {
            if (type == BLL.Const.BtnDownload)
            {
                if (error == null)
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, "【" + pageName + "】从集团提取" + count + "条数据；", string.Empty, BLL.Const.SynchronizationMenuId, BLL.Const.BtnDownload);
                }
                else
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, "【" + pageName + "】从集团提取失败；", string.Empty, BLL.Const.SynchronizationMenuId, BLL.Const.BtnDownload);
                }
            }
            else
            {
                if (error == null)
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, "【" + pageName + "】从集团提取" + count + "条数据；", string.Empty, BLL.Const.SynchronizationMenuId, BLL.Const.BtnUploadResources);
                }
                else
                {
                    BLL.LogService.AddSys_Log(this.CurrUser, "【" + pageName + "】从集团提取失败；", string.Empty, BLL.Const.SynchronizationMenuId, BLL.Const.BtnUploadResources);
                }
            }
        }

        //#region 启动监视器 系统启动5分钟
        ///// <summary>
        ///// 监视组件
        ///// </summary>
        //private static System.Timers.Timer messageTimer;

        ///// <summary>
        ///// 启动监视器,不一定能成功，根据系统设置决定对监视器执行的操作 系统启动5分钟
        ///// </summary>
        //public void StartMonitor()
        //{
        //    int adTimeJ = 1; /// 5分钟
        //    if (messageTimer != null)
        //    {
        //        messageTimer.Stop();
        //        messageTimer.Dispose();
        //        messageTimer = null;
        //    }

        //    messageTimer = new System.Timers.Timer
        //    {
        //        AutoReset = true
        //    };
        //    messageTimer.Elapsed += new ElapsedEventHandler(AdUserInProcess);

        //    messageTimer.Interval = 6000 * adTimeJ;
        //    messageTimer.Start();

        //}

        ///// <summary>
        ///// 流程确认 定时执行 系统启动5分钟
        ///// </summary>
        ///// <param name="sender">Timer组件</param>
        ///// <param name="e">事件参数</param>
        //private  void AdUserInProcess(object sender, ElapsedEventArgs e)
        //{
        //    if (messageTimer != null)
        //    {
        //        messageTimer.Stop();
        //    }
          
        //    ///自动同步      
        //    this.SynchDataTime();         
        //    if (messageTimer != null)
        //    {
        //        messageTimer.Dispose();
        //        messageTimer = null;
        //    }
        //}
        //#endregion

        //#region 启动监视器 定时23:00执行
        ///// <summary>
        ///// 监视组件
        ///// </summary>
        //private static System.Timers.Timer messageTimerEve;

        ///// <summary>
        ///// 启动监视器,不一定能成功，根据系统设置决定对监视器执行的操作 定时
        ///// </summary>
        //public  void StartMonitorEve()
        //{
        //    if (messageTimerEve != null)
        //    {
        //        messageTimerEve.Stop();
        //        messageTimerEve.Dispose();
        //        messageTimerEve = null;
        //    }

        //    messageTimerEve = new System.Timers.Timer
        //    {
        //        AutoReset = true
        //    };
        //    messageTimerEve.Elapsed += new ElapsedEventHandler(ColligateFormConfirmProcessEve);
        //    messageTimerEve.Interval = GetMessageTimerEveNextInterval();
        //    messageTimerEve.Start();
        //}

        ///// <summary>
        /////  流程确认 定时执行 定时00:05 执行
        ///// </summary>
        ///// <param name="sender">Timer组件</param>
        ///// <param name="e">事件参数</param>
        //private void ColligateFormConfirmProcessEve(object sender, ElapsedEventArgs e)
        //{
        //    try
        //    {
        //        if (messageTimerEve != null)
        //        {
        //            messageTimerEve.Stop();
        //        }
                       
        //        ///自动同步    
        //        this.SynchDataTime();         
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrLogInfo.WriteLog("定时执行失败！", ex);
        //    }
        //    finally
        //    {
        //        messageTimerEve.Interval = GetMessageTimerEveNextInterval();
        //        messageTimerEve.Start();
        //    }
        //}

        ///// <summary>
        ///// 计算MessageTimerEve定时器的执行间隔
        ///// </summary>
        ///// <returns>执行间隔</returns>
        //private static double GetMessageTimerEveNextInterval()
        //{
        //    double returnValue = 0;
        //    TimeSpan curentTime = DateTime.Now.TimeOfDay;
        //    int hour = 23;
        //    //if (!String.IsNullOrEmpty(Funs.AdTimeD))
        //    //{
        //    //    hour = int.Parse(Funs.AdTimeD);
        //    //}
        //    TimeSpan triggerTime = new TimeSpan(hour, 00, 0);
        //    if (curentTime > triggerTime)
        //    {
        //        // 超过了执行时间
        //        returnValue = (new TimeSpan(23, 59, 59) - curentTime + triggerTime.Add(new TimeSpan(0, 0, 1))).TotalMilliseconds;
        //    }
        //    else
        //    {
        //        returnValue = (triggerTime - curentTime).TotalMilliseconds;
        //    }

        //    if (returnValue <= 0)
        //    {
        //        // 误差纠正
        //        returnValue = 1;
        //    }

        //    return returnValue;
        //}
        //#endregion    
   
        //#region 自动
        ///// <summary>
        ///// 同步方法
        ///// </summary>
        //private void SynchDataTime()
        //{
        //    /////创建客户端服务
        //    var poxy = Web.ServiceProxy.CreateServiceClient();
        //    ///版本信息
        //    poxy.GetSys_VersionToSUBCompleted += new EventHandler<BLL.HSSEService.GetSys_VersionToSUBCompletedEventArgs>(poxy_GetSys_VersionToSUBCompleted);
        //    poxy.GetSys_VersionToSUBAsync();
        //    ///催报信息
        //    poxy.GetInformation_UrgeReportToSUBCompleted += new EventHandler<BLL.HSSEService.GetInformation_UrgeReportToSUBCompletedEventArgs>(poxy_GetInformation_UrgeReportToSUBCompleted);
        //    poxy.GetInformation_UrgeReportToSUBAsync(this.UnitId);
        //    ///安全监督检查整改
        //    poxy.GetCheck_CheckRectifyListToSUBCompleted += new EventHandler<BLL.HSSEService.GetCheck_CheckRectifyListToSUBCompletedEventArgs>(poxy_GetCheck_CheckRectifyListToSUBCompleted);
        //    poxy.GetCheck_CheckRectifyListToSUBAsync(this.UnitId);
        //    ///安全监督检查报告
        //    poxy.GetCheck_CheckInfo_Table8ItemListToSUBCompleted += new EventHandler<BLL.HSSEService.GetCheck_CheckInfo_Table8ItemListToSUBCompletedEventArgs>(poxy_GetCheck_CheckInfo_Table8ItemListToSUBCompleted);
        //    poxy.GetCheck_CheckInfo_Table8ItemListToSUBAsync(this.UnitId);
        //    ////企业安全文件上报
        //    poxy.GetSupervise_SubUnitReportListToSUBCompleted += new EventHandler<BLL.HSSEService.GetSupervise_SubUnitReportListToSUBCompletedEventArgs>(poxy_GetSupervise_SubUnitReportListToSUBCompleted);
        //    poxy.GetSupervise_SubUnitReportListToSUBAsync();
        //    ///企业安全文件上报明细
        //    poxy.GetSupervise_SubUnitReportItemListToSUBCompleted += new EventHandler<BLL.HSSEService.GetSupervise_SubUnitReportItemListToSUBCompletedEventArgs>(poxy_GetSupervise_SubUnitReportItemListToSUBCompleted);
        //    poxy.GetSupervise_SubUnitReportItemListToSUBAsync(this.UnitId);      
        //}
        //#endregion
    }
}