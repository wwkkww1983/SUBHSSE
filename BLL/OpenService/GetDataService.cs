using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace BLL
{
    public static class GetDataService
    {
        #region 与博晟培训考试接口
        #region 与博晟培训考试接口 获取数据 并按类型 插入相应表单
        /// <summary>
        /// 获取数据 并按类型 插入相应表单
        /// </summary>
        public static void AddData()
        {
            var getDataList = (from x in Funs.DB.Sys_DataExchange where x.IsUpdate == false select x).ToList();
            if (getDataList.Count() > 0)
            {
                var getDataList0 = getDataList.Where(x => x.MessageText.Contains("\"Type\":0")).ToList();
                var getDataList1 = getDataList.Where(x => x.MessageText.Contains("\"Type\":1")).ToList();
                var getDataList2 = getDataList.Where(x => x.MessageText.Contains("\"Type\":2")).ToList();
                var getDataList3 = getDataList.Where(x => x.MessageText.Contains("\"Type\":3")).ToList();
                var getDataList4 = getDataList.Where(x => x.MessageText.Contains("\"Type\":4")).ToList();
                var getDataList5 = getDataList.Where(x => x.MessageText.Contains("\"Type\":5")).ToList();

                AddDataItem(getDataList1);
                AddDataItem(getDataList0);
                AddDataItem(getDataList2);
                AddDataItem(getDataList3);
                AddDataItem(getDataList4);
                AddDataItem(getDataList5);
            }
        }
        #endregion

        #region 与博晟培训考试接口数据插入明细方法
        /// <summary>
        ///  与博晟培训考试接口数据插入明细方法
        /// </summary>
        /// <param name="getDataExchange"></param>
        private static void AddDataItem(List<Model.Sys_DataExchange> getDataExchange)
        {
            bool isOk = false;
            foreach (var item in getDataExchange)
            {
                JObject obj = JObject.Parse(item.MessageText);
                string type = obj["Type"].ToString();
                string code = obj["Code"].ToString();
                string fromprojectId = obj["DepartId"].ToString();
                JArray arr = JArray.Parse(obj["Data"].ToString());
                string projectId = string.Empty;
                ////根据传值项目主键
                var getProjectByFromProjectId = Funs.DB.Base_Project.FirstOrDefault(x => x.FromProjectId == fromprojectId);
                if (getProjectByFromProjectId != null)
                {
                    projectId = getProjectByFromProjectId.ProjectId;
                }
                else
                {
                    if (type == "1")
                    {
                        projectId = AddProject(arr);
                    }
                }

                if (!string.IsNullOrEmpty(projectId))
                {
                    var sysUser = BLL.UserService.GetUserByUserId(BLL.Const.sysglyId);
                    if (sysUser != null)
                    {
                        sysUser.LoginProjectId = projectId;
                    }

                    if (type == "0")
                    {
                        isOk = AddPerson(projectId, arr, sysUser);
                    }
                    else if (type == "1")
                    {
                        isOk = AddUnit(projectId, arr, sysUser);
                    }
                    else if (type == "2")
                    {
                        isOk = AddTrainRecord(projectId, arr, sysUser);
                    }
                    else if (type == "3")
                    {
                        isOk = AddTrainRecordPerson(projectId, arr, sysUser);
                    }
                    else if (type == "4")
                    {
                        isOk = AddEduTrain_TrainTest(projectId, arr, sysUser);
                    }
                    else if (type == "5")
                    {
                        isOk = AddPersonTrainRecord(projectId, arr, sysUser);
                    }

                    if (isOk) ///更新数据接收状态
                    {
                        item.IsUpdate = true;
                        Funs.DB.SubmitChanges();
                    }
                }
            }
        }
        #endregion

        #region 插入信息-项目信息
        /// <summary>
        /// 插入信息-项目信息
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="arr"></param>
        public static string AddProject(JArray arr)
        {
            Model.SUBHSSEDB db = Funs.DB;
            string projectId = string.Empty;
            try
            {
                foreach (var item in arr)
                {
                    string fromUnitId = item["ID"].ToString();
                    string departName = item["DepartName"].ToString(); ///单位名称
                    string departSir = item["DepartSir"].ToString(); ///单位级别 0：非项目部 1：项目部级 2：项目部下级单位  
                    if (!string.IsNullOrEmpty(fromUnitId) && !string.IsNullOrEmpty(departName) && departSir == "1")
                    {
                        var getProjectByFromProjectId = db.Base_Project.FirstOrDefault(x => x.FromProjectId == fromUnitId);
                        if (getProjectByFromProjectId == null)
                        {
                            string projectCode = item["ProjectCode"].ToString();
                            var getProjectByFromProjectName = db.Base_Project.FirstOrDefault(x => x.ProjectCode == projectCode);
                            if (getProjectByFromProjectName != null)
                            {
                                projectId = getProjectByFromProjectName.ProjectId;
                                getProjectByFromProjectName.FromProjectId = fromUnitId;
                                ProjectService.UpdateProject(getProjectByFromProjectName);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrLogInfo.WriteLog(string.Empty, ex);
            }
            return projectId;
        }
        #endregion

        #region 插入人员信息
        /// <summary>
        /// 插入人员信息 0 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="arr"></param>
        public static bool AddPerson(string projectId, JArray arr, Model.Sys_User user)
        {
            Model.SUBHSSEDB db = Funs.DB;
            bool isOk = true;
            try
            {
                foreach (var item in arr)
                {
                    ////单位
                    string unitId = null;
                    var getUnit = db.Base_Unit.FirstOrDefault(x => x.FromUnitId == item["DepartId"].ToString());
                    if (getUnit != null)
                    {
                        unitId = getUnit.UnitId;
                    }

                    ///区域
                    string workAreaId = null;
                    var getWorkArea = db.ProjectData_WorkArea.FirstOrDefault(x => x.ProjectId == projectId && x.WorkAreaName == item["BuildArea"].ToString());
                    if (getWorkArea != null)
                    {
                        workAreaId = getWorkArea.WorkAreaId;
                    }
                    ///岗位
                    string workPostId = null;
                    var getWorkPost = db.Base_WorkPost.FirstOrDefault(x => x.WorkPostName == item["Station"].ToString());
                    if (getWorkPost != null)
                    {
                        workPostId = getWorkPost.WorkPostId;
                    }
                    DateTime? inTime = Funs.GetNewDateTime(item["EntranceDate"].ToString());
                    DateTime? outTime = Funs.GetNewDateTime(item["LeaveDate"].ToString());
                    if (outTime < inTime)
                    {
                        outTime = null;
                    }
                    string IdentifyID = item["IdentifyID"].ToString();
                    if (!string.IsNullOrEmpty(IdentifyID))
                    {
                        Model.SitePerson_Person newPerson = new Model.SitePerson_Person
                        {
                            FromPersonId = item["ID"].ToString(),
                            CardNo = item["JobNumber"].ToString(),
                            PersonName = item["Name"].ToString(),
                            Sex = item["Sex"].ToString(),
                            IdentityCard = IdentifyID,
                            Address = item["Address"].ToString(),
                            ProjectId = projectId,
                            UnitId = unitId, /////映射取值
                            //TeamGroupId = person.TeamGroupId,
                            WorkAreaId = workAreaId, /////关联映射取值
                            WorkPostId = workPostId, /////关联映射取值
                            InTime = inTime,
                            OutTime = outTime,
                            Telephone = item["ContactTel"].ToString(),
                            IsUsed = true,
                            IsCardUsed = true,
                        };

                        var getPerson = db.SitePerson_Person.FirstOrDefault(x => x.ProjectId == projectId && x.IdentityCard == IdentifyID);
                        if (getPerson == null)
                        {
                            newPerson.PersonId = SQLHelper.GetNewID(typeof(Model.SitePerson_Person));
                            PersonService.AddPerson(newPerson);
                            BLL.LogService.AddSys_Log(user, newPerson.PersonName, newPerson.PersonId, BLL.Const.PersonListMenuId, BLL.Const.BtnAdd);
                        }
                        else
                        {
                            getPerson.FromPersonId = newPerson.FromPersonId;
                            getPerson.InTime = newPerson.InTime;
                            getPerson.OutTime = newPerson.OutTime;
                            getPerson.Telephone = newPerson.Telephone;
                            if (!string.IsNullOrEmpty(unitId))
                            {
                                getPerson.UnitId = unitId;
                            }
                            if (!string.IsNullOrEmpty(workAreaId))
                            {
                                getPerson.WorkAreaId = workAreaId;
                            }
                            if (!string.IsNullOrEmpty(workPostId))
                            {
                                getPerson.WorkPostId = workPostId;
                            }
                            PersonService.UpdatePerson(getPerson);
                            BLL.LogService.AddSys_Log(user, getPerson.PersonName, getPerson.PersonId, BLL.Const.PersonListMenuId, BLL.Const.BtnModify);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isOk = false;
                ErrLogInfo.WriteLog(string.Empty, ex);
            }

            return isOk;
        }
        #endregion

        #region 插入信息-单位信息 1
        /// <summary>
        /// 插入信息-单位信息 1
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="arr"></param>
        public static bool AddUnit(string projectId, JArray arr,Model.Sys_User user)
        {
            Model.SUBHSSEDB db = Funs.DB;
            bool isOk = true;
            try
            {
                foreach (var item in arr)
                {
                    string fromUnitId = item["ID"].ToString();
                    string departName = item["DepartName"].ToString(); ///单位名称
                    string departSir = item["DepartSir"].ToString(); ///单位级别 0：非项目部 1：项目部级 2：项目部下级单位
                    ////单位类型
                    string unitTypeId = null;
                    var getUnitType = db.Base_UnitType.FirstOrDefault(x => x.UnitTypeName == item["DepartType"].ToString());
                    if (getUnitType != null)
                    {
                        unitTypeId = getUnitType.UnitTypeId;
                    }

                    if (!string.IsNullOrEmpty(fromUnitId) && !string.IsNullOrEmpty(departName) && departSir != "1")
                    {
                        if (!string.IsNullOrEmpty(projectId))
                        {
                            Model.Base_Unit newUnit = new Model.Base_Unit
                            {
                                FromUnitId = fromUnitId,
                                UnitCode = item["DepartCode"].ToString(),
                                UnitName = departName,
                                UnitTypeId = unitTypeId,
                                Corporate = item["Charge"].ToString(),
                                Telephone = item["Phone"].ToString(),
                                IsHide = false,
                                DataSources = projectId,
                            };

                            var getUnit = db.Base_Unit.FirstOrDefault(x => x.FromUnitId == fromUnitId);
                            if (getUnit == null)
                            {
                                var getUnitByName = db.Base_Unit.FirstOrDefault(x => x.UnitName == departName);
                                if (getUnitByName != null)
                                {
                                    newUnit.UnitId = getUnitByName.UnitId;
                                    getUnitByName.FromUnitId = fromUnitId;
                                    db.SubmitChanges();
                                }
                                else
                                {
                                    newUnit.UnitId = SQLHelper.GetNewID(typeof(Model.Base_Unit));
                                    UnitService.AddUnit(newUnit);
                                }
                            }
                            else
                            {
                                newUnit.UnitId = getUnit.UnitId;
                            }

                            var pUnit = db.Project_ProjectUnit.FirstOrDefault(x => x.ProjectId == projectId && x.UnitId == newUnit.UnitId);
                            if (pUnit == null)
                            {
                                Model.Project_ProjectUnit newProjectUnit = new Model.Project_ProjectUnit
                                {
                                    ProjectId = projectId,
                                    UnitId = newUnit.UnitId,
                                    UnitType = Const.ProjectUnitType_2,
                                };

                                ProjectUnitService.AddProjectUnit(newProjectUnit);
                                BLL.LogService.AddSys_Log(user, null, newProjectUnit.ProjectUnitId, BLL.Const.ProjectUnitMenuId, BLL.Const.BtnModify);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                isOk = false;
                ErrLogInfo.WriteLog(string.Empty, ex);
            }
            return isOk;
        }
        #endregion

        #region 插入培训记录 2
        /// <summary>
        /// 插入培训记录 2
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="arr"></param>
        public static bool AddTrainRecord(string projectId, JArray arr, Model.Sys_User user)
        {
            Model.SUBHSSEDB db = Funs.DB;
            bool isOk = true;
            try
            {
                foreach (var item in arr)
                {
                    string fromRecordId = item["ID"].ToString();
                    string trainTypeId = null;  ////培训类型
                    var getTrainType = db.Base_TrainType.FirstOrDefault(x => x.TrainTypeName == item["TrainType"].ToString());
                    if (getTrainType != null)
                    {
                        trainTypeId = getTrainType.TrainTypeId;
                    }
                    string unitId = null;
                    if (!string.IsNullOrEmpty(item["TrainDepart"].ToString()))
                    {
                        var lists = Funs.GetStrListByStr(item["TrainDepart"].ToString(), ',');
                        if (lists.Count() > 0)
                        {
                            foreach (var itemList in lists)
                            {
                                var getUnit = db.Base_Unit.FirstOrDefault(x => x.UnitName == itemList);
                                if (getUnit != null)
                                {
                                    if (string.IsNullOrEmpty(unitId))
                                    {
                                        unitId = getUnit.UnitId;
                                    }
                                    else
                                    {
                                        unitId += ("," + getUnit.UnitId);
                                    }
                                }
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(unitId) && !string.IsNullOrEmpty(fromRecordId))
                    {
                        Model.EduTrain_TrainRecord newTrainRecord = new Model.EduTrain_TrainRecord
                        {
                            FromRecordId = fromRecordId,

                            ProjectId = projectId,
                            TrainTitle = item["RecordName"].ToString(),
                            TrainContent = item["TrainContent"].ToString(),
                            TrainStartDate = Funs.GetNewDateTime(item["TrainStartDate"].ToString()),
                            TrainEndDate = Funs.GetNewDateTime(item["TrainEndDate"].ToString()),
                            TeachHour = Funs.GetNewDecimalOrZero(item["TrainPeriod"].ToString()),
                            TeachMan = item["TrainPrincipal"].ToString(),
                            Remark = item["TrainDescript"].ToString(),
                            TrainTypeId = trainTypeId,
                            UnitIds = unitId,
                            States = Const.State_0,
                            CompileMan = item["CreateUser"].ToString(),
                            TrainPersonNum = Funs.GetNewInt(item["PersonCount"].ToString()),
                        };

                        newTrainRecord.TrainingCode = Funs.GetNewFileName(newTrainRecord.TrainStartDate);
                        var getTrainRecord = Funs.DB.EduTrain_TrainRecord.FirstOrDefault(x => x.FromRecordId == fromRecordId);
                        if (getTrainRecord == null)
                        {
                            newTrainRecord.TrainingId = SQLHelper.GetNewID(typeof(Model.EduTrain_TrainRecord));
                            EduTrain_TrainRecordService.AddTraining(newTrainRecord);
                            BLL.LogService.AddSys_Log(user, newTrainRecord.TrainingCode, newTrainRecord.TrainingId, BLL.Const.ProjectTrainRecordMenuId, BLL.Const.BtnAdd);
                        }
                        else
                        {
                            getTrainRecord.TrainingCode = newTrainRecord.TrainingCode;
                            getTrainRecord.TrainTitle = newTrainRecord.TrainTitle;
                            getTrainRecord.TrainContent = newTrainRecord.TrainContent;
                            getTrainRecord.UnitIds = newTrainRecord.UnitIds;
                            getTrainRecord.TrainStartDate = newTrainRecord.TrainStartDate;
                            if (newTrainRecord.TrainEndDate.HasValue)
                            {
                                getTrainRecord.TrainEndDate = newTrainRecord.TrainEndDate;
                            }
                            else
                            {
                                getTrainRecord.TrainEndDate = newTrainRecord.TrainStartDate;
                            }
                            getTrainRecord.TeachHour = newTrainRecord.TeachHour;
                            getTrainRecord.TeachMan = newTrainRecord.TeachMan;
                            getTrainRecord.TeachAddress = newTrainRecord.TeachAddress;

                            getTrainRecord.Remark = newTrainRecord.Remark;
                            EduTrain_TrainRecordService.UpdateTraining(getTrainRecord);
                            BLL.LogService.AddSys_Log(user, getTrainRecord.TrainingCode, getTrainRecord.TrainingId, BLL.Const.ProjectTrainRecordMenuId, BLL.Const.BtnModify);
                        }
                    }
                    else
                    {
                        isOk = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isOk = false;
                ErrLogInfo.WriteLog(string.Empty, ex);
            }
            return isOk;
        }
        #endregion

        #region 插入培训人员 3
        /// <summary>
        /// 插入培训人员 3
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="arr"></param>
        public static bool AddTrainRecordPerson(string projectId, JArray arr, Model.Sys_User user)
        {
            Model.SUBHSSEDB db = Funs.DB;
            bool isOk = true;
            try
            {
                foreach (var item in arr)
                {
                    string trainingId = null;  ////培训记录ID
                    var getTrainRecord = db.EduTrain_TrainRecord.FirstOrDefault(x => x.FromRecordId == item["RecordId"].ToString());
                    if (getTrainRecord != null)
                    {
                        trainingId = getTrainRecord.TrainingId;
                    }

                    string personId = null; ///人员信息ID
                    var getPerson = db.SitePerson_Person.FirstOrDefault(x => x.IdentityCard == item["IdentifyId"].ToString());
                    if (getPerson != null)
                    {
                        personId = getPerson.PersonId;
                    }
                    bool checkResult = false;
                    if (item["IsPass"].ToString() == "1")
                    {
                        checkResult = true;
                    }
                    if (!string.IsNullOrEmpty(trainingId) && !string.IsNullOrEmpty(personId))
                    {
                        Model.EduTrain_TrainRecordDetail newTrainRecordDetail = new Model.EduTrain_TrainRecordDetail
                        {
                            TrainingId = trainingId,
                            PersonId = personId,
                            CheckScore = Funs.GetNewDecimal(item["Score"].ToString()),
                            CheckResult = checkResult,
                        };

                        var getTrainRecordDetail = db.EduTrain_TrainRecordDetail.FirstOrDefault(x => x.TrainingId == trainingId && x.PersonId == personId);
                        if (getTrainRecordDetail == null)
                        {
                            EduTrain_TrainRecordDetailService.AddTrainDetail(newTrainRecordDetail);
                        }
                        else
                        {
                            getTrainRecordDetail.CheckScore = newTrainRecordDetail.CheckScore;
                            getTrainRecordDetail.CheckResult = newTrainRecordDetail.CheckResult;
                            EduTrain_TrainRecordDetailService.UpdateTrainDetail(getTrainRecordDetail);
                        }
                    }
                    else
                    {
                        isOk = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isOk = false;
                ErrLogInfo.WriteLog(string.Empty, ex);
            }
            return isOk;
        }
        #endregion

        #region 插入试卷 4
        /// <summary>
        /// 插入试卷 4
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="arr"></param>
        public static bool AddEduTrain_TrainTest(string projectId, JArray arr, Model.Sys_User user)
        {
            Model.SUBHSSEDB db = Funs.DB;
            bool isOk = true;
            try
            {
                foreach (var item in arr)
                {
                    string trainingId = null;  ////培训记录ID
                    var getTrainRecord = db.EduTrain_TrainRecord.FirstOrDefault(x => x.FromRecordId == item["RecordId"].ToString());
                    if (getTrainRecord != null)
                    {
                        trainingId = getTrainRecord.TrainingId;
                    }

                    string trainTestId = item["ID"].ToString();
                    if (!string.IsNullOrEmpty(trainingId) && !string.IsNullOrEmpty(trainTestId))
                    {
                        Model.EduTrain_TrainTest newTrainTest = new Model.EduTrain_TrainTest
                        {
                            TrainTestId = trainTestId,
                            TrainingId = trainingId,
                            ExamNo = item["ExamNo"].ToString(),
                            GroupNo = item["GroupNo"].ToString(),
                            CourseID = item["CourseID"].ToString(),
                            COrder = Funs.GetNewInt(item["COrder"].ToString()),
                            QsnCode = item["QsnCode"].ToString(),
                            QsnId = item["QsnId"].ToString(),
                            QsnContent = item["QsnContent"].ToString(),
                            QsnFileName = item["QsnFileName"].ToString(),
                            QsnAnswer = item["QsnAnswer"].ToString(),
                            QsnCategory = item["QsnCategory"].ToString(),
                            QsnKind = item["QsnKind"].ToString(),
                            Description = item["Description"].ToString(),
                            QsnImportant = item["QsnImportant"].ToString(),
                            Analysis = item["Analysis"].ToString(),
                            UploadTime = Funs.GetNewDateTime(item["UploadTime"].ToString()),
                        };

                        var getTrainRecordDetail = db.EduTrain_TrainTest.FirstOrDefault(x => x.TrainTestId == trainTestId);
                        if (getTrainRecordDetail == null)
                        {
                            EduTrain_TrainTestService.AddTrainTest(newTrainTest);
                        }
                        else
                        {
                            newTrainTest.ExamNo = newTrainTest.ExamNo;
                            newTrainTest.GroupNo = newTrainTest.GroupNo;
                            newTrainTest.CourseID = newTrainTest.CourseID;
                            newTrainTest.COrder = newTrainTest.COrder;
                            newTrainTest.QsnCode = newTrainTest.QsnCode;
                            newTrainTest.QsnId = newTrainTest.QsnId;
                            newTrainTest.QsnContent = newTrainTest.QsnContent;
                            newTrainTest.QsnFileName = newTrainTest.QsnFileName;
                            newTrainTest.QsnAnswer = newTrainTest.QsnAnswer;
                            newTrainTest.QsnCategory = newTrainTest.QsnCategory;
                            newTrainTest.QsnKind = newTrainTest.QsnKind;
                            newTrainTest.Description = newTrainTest.Description;
                            newTrainTest.QsnImportant = newTrainTest.QsnImportant;
                            newTrainTest.Analysis = newTrainTest.Analysis;
                            newTrainTest.UploadTime = newTrainTest.UploadTime;
                            db.SubmitChanges();
                        }
                    }
                    else
                    {
                        isOk = false;
                    }
                }
        }
            catch (Exception ex)
            {
                isOk = false;
                ErrLogInfo.WriteLog(string.Empty, ex);
            }
            return isOk;
        }
        #endregion

        #region 插入人员培训记录 5
        /// <summary>
        /// 插入人员培训记录 5
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="arr"></param>
        public static bool AddPersonTrainRecord(string projectId, JArray arr, Model.Sys_User user)
        {
            Model.SUBHSSEDB db = Funs.DB;
            bool isOk = true;
            try
            {
                foreach (var item in arr)
                {
                    string id = item["ID"].ToString();
                    if (!string.IsNullOrEmpty(id))
                    {
                        bool isPass = false;
                        if (item["IsPass"].ToString() == "1")
                        {
                            isPass = true;
                        }

                        Model.EduTrain_TrainPersonRecord newTrainPersonRecord = new Model.EduTrain_TrainPersonRecord
                        {
                            ID = id,
                            ProjectId = projectId,
                            EmpName = item["EmpName"].ToString(),
                            IdentifyId = item["IdentifyId"].ToString(),
                            CategoryName = item["CategoryName"].ToString(),
                            RecordId = item["RecordId"].ToString(),
                            DepartId = item["DepartId"].ToString(),
                            DepartName = item["DepartName"].ToString(),
                            TrainPeriod = item["TrainPeriod"].ToString(),
                            TotalScore = item["TotalScore"].ToString(),
                            PassScore = item["PassScore"].ToString(),
                            Score = item["Score"].ToString(),
                            IsPass = isPass,
                            GroupNo = item["GroupNo"].ToString(),
                            ExamNo = item["ExamNo"].ToString(),
                            ExamCount = item["ExamCount"].ToString(),
                            DeviceNo = item["DeviceNo"].ToString(),
                            OwnerDepartId = item["OwnerDepartId"].ToString(),
                            Answers = item["Answers"].ToString(),
                            RecordName = item["RecordName"].ToString(),
                            TrainType = item["TrainType"].ToString(),
                            PaperMode = item["PaperMode"].ToString(),
                            TrainMode = item["TrainMode"].ToString(),
                            TrainPrincipal = item["TrainPrincipal"].ToString(),
                            TrainStartDate = Funs.GetNewDateTime(item["TrainStartDate"].ToString()),
                            TrainEndDate = Funs.GetNewDateTime(item["TrainEndDate"].ToString()),
                            TrainContent = item["TrainContent"].ToString(),
                            TrainDescript = item["TrainDescript"].ToString(),
                        };

                        var getnewTrainPersonRecord = db.EduTrain_TrainPersonRecord.FirstOrDefault(x => x.ID == id);
                        if (getnewTrainPersonRecord == null)
                        {
                            db.EduTrain_TrainPersonRecord.InsertOnSubmit(newTrainPersonRecord);
                            db.SubmitChanges();
                        }
                        else
                        {
                            db.SubmitChanges();
                        }
                    }
                    else
                    {
                        isOk = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isOk = false;
                ErrLogInfo.WriteLog(string.Empty, ex);
            }
            return isOk;
        }
        #endregion
        #endregion

        #region 培训计划提交后 按照培训任务 生成培训人员的培训教材明细
        /// <summary>
        /// 生成培训人员的培训教材明细
        /// </summary>
        /// <param name="taskId"></param>
        public static void CreateTrainingTaskItemByTaskId(string taskId)
        {           
            /////查找未生成教材明细的 培训任务
            var getTasks = from x in Funs.DB.Training_Task
                           where x.States == Const.State_0 && (x.TaskId == taskId || taskId == null)
                           select x;
            if (getTasks.Count() > 0)
            {
                foreach (var item in getTasks)
                {
                    var getPerson = PersonService.GetPersonById(item.UserId);
                    if (getPerson != null)
                    {
                        ////获取计划下 人员培训教材明细
                        var getDataList = Funs.DB.Sp_GetTraining_TaskItemTraining(item.PlanId, getPerson.WorkPostId);
                        foreach (var dataItem in getDataList)
                        {
                            Model.Training_TaskItem newTaskItem = new Model.Training_TaskItem
                            {
                                TaskId = item.TaskId,
                                PlanId = item.PlanId,
                                PersonId = item.UserId,
                                TrainingItemCode = dataItem.TrainingItemCode,
                                TrainingItemName = dataItem.TrainingItemName,
                                AttachUrl = dataItem.AttachUrl,
                            };

                            var getTaskItem = Funs.DB.Training_TaskItem.FirstOrDefault(x => x.TaskId == item.TaskId && x.TrainingItemName == newTaskItem.TrainingItemName && x.AttachUrl == newTaskItem.AttachUrl);
                            if (getTaskItem == null)
                            {
                                newTaskItem.TaskItemId = SQLHelper.GetNewID();
                                Funs.DB.Training_TaskItem.InsertOnSubmit(newTaskItem);
                                Funs.DB.SubmitChanges();
                            }
                        }
                    }

                    ////更新培训任务
                    item.States = Const.State_1;
                    Funs.DB.SubmitChanges();
                }
            }
        }
        #endregion

        #region 自动结束考试
        /// <summary>
        ///  自动结束考试
        /// </summary>
        public static void UpdateTestPlanStates()
        {
            Model.SUBHSSEDB db = Funs.DB;
            var getTestPlans = from x in db.Training_TestPlan
                               where x.States == "2" && x.TestEndTime.AddMinutes(x.Duration) < DateTime.Now
                               select x;
            if (getTestPlans.Count() > 0)
            {
                foreach (var item in getTestPlans)
                {
                    APITestPlanService.SubmitTest(item);
                    item.States = "3";
                    db.SubmitChanges();
                }
            }

            var getTrainingTestRecords = from x in db.Training_TestRecord
                                         where x.TestStartTime.Value.AddMinutes(x.Duration) < DateTime.Now 
                                         && (!x.TestEndTime.HasValue || !x.TestScores.HasValue)
                                         select x;
            foreach (var itemRecord in getTrainingTestRecords)
            {
                itemRecord.TestEndTime = itemRecord.TestStartTime.Value.AddMinutes(itemRecord.Duration);
                itemRecord.TestScores = db.Training_TestRecordItem.Where(x => x.TestRecordId == itemRecord.TestRecordId).Sum(x => x.SubjectScore) ?? 0;
                TestRecordService.UpdateTestRecord(itemRecord);
            }
        }
        #endregion

        #region 自动校正出入场人数及工时
        /// <summary>
        ///  自动校正出入场人数及工时
        /// </summary>
        public static void CorrectingPersonInOutNumber(string projectId)
        {
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                var getProjects = (from x in db.Base_Project
                                          where x.ProjectState == null || x.ProjectState == BLL.Const.ProjectState_1
                                          orderby x.ProjectCode descending
                                          select x).ToList();
                if (!string.IsNullOrEmpty(projectId))
                {
                    getProjects = getProjects.Where(x => x.ProjectId == projectId).ToList();
                }
                foreach (var projectItem in getProjects)
                {
                    var getPersonInOutNumber = db.SitePerson_PersonInOutNumber.FirstOrDefault(x => x.ProjectId == projectItem.ProjectId
                                         && x.InOutDate.Year == DateTime.Now.Year && x.InOutDate.Month == DateTime.Now.Month && x.InOutDate.Day == DateTime.Now.Day);
                    if (getPersonInOutNumber == null)
                    {
                        //// 现场人员数
                        int SitePersonNum = 0;
                        var getAllPersonList = from x in db.SitePerson_PersonInOut
                                               where x.ProjectId == projectItem.ProjectId
                                               select x;
                        var getAllPersonInOuts = from x in getAllPersonList
                                                 join y in db.SitePerson_Person on x.PersonId equals y.PersonId
                                                 where y.IsUsed == true && (!y.OutTime.HasValue || y.OutTime >= DateTime.Now)
                                                 select x;
                        if (getAllPersonList.Count() > 0)
                        {
                            if (getAllPersonInOuts.Count() > 0)
                            {
                                var getIn = getAllPersonInOuts.Where(x => x.IsIn == true);
                                List<string> getPersonIds = new List<string>();
                                foreach (var item in getIn)
                                {
                                    var getMax = getAllPersonInOuts.FirstOrDefault(x => x.PersonId == item.PersonId && x.IsIn == false && x.ChangeTime >= item.ChangeTime);
                                    if (getMax == null)
                                    {
                                        if (getPersonIds.Count() == 0 || !getPersonIds.Contains(item.PersonId))
                                        {
                                            getPersonIds.Add(item.PersonId);
                                            SitePersonNum = SitePersonNum + 1;
                                        }
                                    }
                                }
                            }
                            //// 获取工时                  
                            int SafeHours = 0;
                            var getPersonOutTimes = from x in getAllPersonInOuts
                                                    where x.IsIn == false && x.ChangeTime <= DateTime.Now
                                                    select x;
                            foreach (var item in getPersonOutTimes)
                            {
                                var getInTimes = from x in getAllPersonInOuts
                                                 where x.IsIn == true && x.ChangeTime < item.ChangeTime
                                                 orderby x.ChangeTime descending
                                                 select x;
                                if (getInTimes.Count() > 0)
                                {
                                    var maxInT = getInTimes.FirstOrDefault();
                                    if (maxInT != null && maxInT.ChangeTime.HasValue)
                                    {
                                        SafeHours += Convert.ToInt32((item.ChangeTime - maxInT.ChangeTime).Value.TotalHours);
                                    }
                                }
                            }

                            Model.SitePerson_PersonInOutNumber newNum = new Model.SitePerson_PersonInOutNumber
                            {
                                PersonInOutNumberId = SQLHelper.GetNewID(),
                                ProjectId = projectItem.ProjectId,
                                InOutDate = DateTime.Now,
                                PersonNum = SitePersonNum,
                                WorkHours = SafeHours,
                            };

                            db.SitePerson_PersonInOutNumber.InsertOnSubmit(newNum);
                            db.SubmitChanges();
                        }
                    }
                }
            }
        }
        #endregion

        #region 自动批量生成人员二维码
        /// <summary>
        ///  自动校正出入场人数及工时
        /// </summary>
        public static void CreateQRCode()
        {
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                var getPersons = from x in db.SitePerson_Person
                                 where x.IdentityCard != null && x.QRCodeAttachUrl == null
                                 select x;
                if (getPersons.Count() > 0)
                {
                    foreach (var item in getPersons)
                    {
                        string url = CreateQRCodeService.CreateCode_Simple("person$" + item.IdentityCard);
                        if (!string.IsNullOrEmpty(url))
                        {
                            item.QRCodeAttachUrl = url;
                            db.SubmitChanges();
                        }
                    }
                }
            }
        }
        #endregion
    }
}
