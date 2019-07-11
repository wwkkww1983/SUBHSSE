using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace BLL
{
    public static class GetDataService
    {
        #region 获取数据 并按类型 插入相应表单
        /// <summary>
        /// 获取数据 并按类型 插入相应表单
        /// </summary>
        public static void AddData()
        {
            Model.SUBHSSEDB db = Funs.DB;
            var getDataList = (from x in db.Sys_DataExchange where x.IsUpdate == false select x).ToList();
            if (getDataList.Count() > 0)
            {
                bool isOk = false;
                foreach (var item in getDataList)
                {
                    JObject obj = JObject.Parse(item.MessageText);
                    string type = obj["Type"].ToString();
                    string code = obj["Code"].ToString();
                    string fromprojectId = obj["DepartId"].ToString();
                    string fromprojectName = "宜都兴发湿法磷酸装置";// obj["DepartName"].ToString();
                    string projectId = string.Empty;
                    ////根据传值项目主键
                    var getProjectByFromProjectId = db.Base_Project.FirstOrDefault(x => x.FromProjectId == fromprojectId);
                    if (getProjectByFromProjectId != null)
                    {
                        projectId = getProjectByFromProjectId.ProjectId;
                    }
                    else
                    {
                        ////根据约定项目名称（项目简称）值判断
                        var getProjectByFromProjectName = db.Base_Project.FirstOrDefault(x => x.ShortName == fromprojectName);
                        if (getProjectByFromProjectName != null)
                        {
                            projectId = getProjectByFromProjectName.ProjectId;
                            getProjectByFromProjectName.FromProjectId = fromprojectId;
                            ProjectService.UpdateProject(getProjectByFromProjectName);
                        }
                    }

                    if (!string.IsNullOrEmpty(projectId))
                    {
                        JArray arr = JArray.Parse(obj["Data"].ToString());
                        if (type == "0")
                        {
                            isOk = AddPerson(projectId, arr);
                        }
                        else if (type == "1")
                        {
                            isOk = AddUnit(projectId, arr);
                        }
                        else if (type == "2")
                        {
                            isOk = AddTrainRecord(projectId, arr);
                        }
                        else if (type == "3")
                        {
                            isOk = AddTrainRecordPerson(projectId, arr);
                        }
                        else if (type == "4")
                        {
                            isOk = AddEduTrain_TrainTest(projectId, arr);
                        }
                        else if (type == "5")
                        {
                            isOk = AddPersonTrainRecord(projectId, arr);
                        }

                        if (isOk) ///更新数据接收状态
                        {
                            item.IsUpdate = true;
                            db.SubmitChanges();
                        }
                    }
                }
            }
        }
        #endregion

        #region 插入人员信息
        /// <summary>
        /// 插入人员信息 0 
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="arr"></param>
        public static bool AddPerson(string projectId, JArray arr)
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
        public static bool AddUnit(string projectId, JArray arr)
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
        public static bool AddTrainRecord(string projectId, JArray arr)
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
                    var getUnit = db.Base_Unit.FirstOrDefault(x => x.UnitName == item["TrainDepart"].ToString());
                    if (getUnit != null)
                    {
                        unitId = getUnit.UnitId;
                    }
                    if (!string.IsNullOrEmpty(unitId) && !string.IsNullOrEmpty(fromRecordId))
                    {
                        Model.EduTrain_TrainRecord newTrainRecord = new Model.EduTrain_TrainRecord
                        {
                            FromRecordId = fromRecordId,
                            TrainingCode = item["DeviceNo"].ToString(),
                            ProjectId = projectId,
                            TrainTitle = item["RecordName"].ToString(),
                            TrainContent = item["TrainContent"].ToString(),
                            TrainStartDate = Funs.GetNewDateTime(item["TrainStartDate"].ToString()),
                            TrainEndDate = Funs.GetNewDateTime(item["TrainEndDate"].ToString()),
                            TeachHour = Funs.GetNewInt(item["CourseDuration"].ToString()),
                            TeachMan = item["TrainPrincipal"].ToString(),
                            Remark = item["TrainDescript"].ToString(),
                            TrainTypeId = trainTypeId,
                            UnitIds = unitId,
                            States = Const.State_0,
                            CompileMan = item["CreateUser"].ToString(),
                            TrainPersonNum = Funs.GetNewInt(item["PersonCount"].ToString()),
                        };

                        var getTrainRecord = db.EduTrain_TrainRecord.FirstOrDefault(x => x.FromRecordId == fromRecordId);
                        if (getTrainRecord == null)
                        {
                            newTrainRecord.TrainingId = SQLHelper.GetNewID(typeof(Model.EduTrain_TrainRecord));
                            EduTrain_TrainRecordService.AddTraining(newTrainRecord);
                        }
                        else
                        {
                            getTrainRecord.TrainTitle = newTrainRecord.TrainTitle;
                            getTrainRecord.TrainContent = newTrainRecord.TrainContent;
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
        public static bool AddTrainRecordPerson(string projectId, JArray arr)
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
                            CheckScore = Funs.GetNewInt(item["Score"].ToString()),
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
        public static bool AddEduTrain_TrainTest(string projectId, JArray arr)
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
        public static bool AddPersonTrainRecord(string projectId, JArray arr)
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
    }
}
