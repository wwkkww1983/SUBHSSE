using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmitMapper;

namespace BLL
{
    public static class APIPersonService
    {
        #region 获取项目现场人员登录信息
        /// <summary>
        /// 获取项目现场人员登录信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static Model.UserItem PersonLogOn(Model.UserItem userInfo)
        {
            var getUser = from x in Funs.DB.SitePerson_Person
                          join y in Funs.DB.Base_Unit on x.UnitId equals y.UnitId
                          join z in Funs.DB.Base_Project on x.ProjectId equals z.ProjectId
                          join w in Funs.DB.Base_WorkPost on x.WorkPostId equals w.WorkPostId
                          where (x.Telephone == userInfo.Account || x.PersonName == userInfo.Account) && x.Password == Funs.EncryptionPassword(userInfo.Password)
                          && x.InTime <= DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= DateTime.Now) && x.IsUsed == true
                          select new Model.UserItem
                          {
                              UserId = x.PersonId,
                              UserCode = x.CardNo,
                              Password = x.Password,
                              UserName = x.PersonName,
                              UnitId = x.UnitId,
                              LoginProjectId = x.ProjectId,
                              IdentityCard = x.IdentityCard,
                              Account = x.Telephone,
                              UnitName = y.UnitName,
                              LoginProjectName = z.ProjectName,
                              Telephone = x.Telephone,
                              WorkPostId = x.WorkPostId,
                              WorkPostName = w.WorkPostName,
                          };

            return getUser.FirstOrDefault();
        }
        #endregion

        #region 根据personId获取人员信息
        /// <summary>
        /// 根据personId获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static Model.PersonItem getPersonByPersonId(string personId)
        {
            var getPerson = from x in Funs.DB.View_SitePerson_Person
                            where x.PersonId == personId || x.IdentityCard == personId
                            select new Model.PersonItem
                            {
                                PersonId = x.PersonId,
                                CardNo = x.CardNo,
                                PersonName = x.PersonName,
                                Sex = x.Sex,
                                SexName = x.SexName,
                                IdentityCard = x.IdentityCard,
                                Address = x.Address,
                                ProjectId = x.ProjectId,
                                ProjectCode = x.ProjectCode,
                                ProjectName = x.ProjectName,
                                UnitId = x.UnitId,
                                UnitCode = x.UnitCode,
                                UnitName = x.UnitName,
                                TeamGroupId = x.TeamGroupId,
                                TeamGroupName = x.TeamGroupName,
                                WorkPostId = x.WorkPostId,
                                WorkPostName = x.WorkPostName,
                                InTime = string.Format("{0:yyyy-MM-dd}", x.InTime),
                                OutTime = string.Format("{0:yyyy-MM-dd}", x.OutTime),
                                OutResult = x.OutResult,
                                Telephone = x.Telephone,
                                PhotoUrl = x.PhotoUrl,
                                DepartName = x.DepartName,
                                IsUsed = x.IsUsed,
                                IsUsedName = x.IsUsed == false ? "不启用" : "启用",
                                AuditorId = x.AuditorId,
                                AuditorName = x.AuditorName,
                                AuditorDate = string.Format("{0:yyyy-MM-dd}", x.AuditorDate),
                                AttachUrl1 = x.IDCardUrl == null ? Funs.DB.AttachFile.First(z => z.ToKeyId == (x.PersonId + "#1")).AttachUrl.Replace('\\', '/') : x.IDCardUrl.Replace('\\', '/'),
                                AttachUrl2 = Funs.DB.AttachFile.First(z => z.ToKeyId == (x.PersonId + "#2")).AttachUrl.Replace('\\', '/'),
                                AttachUrl3 = Funs.DB.AttachFile.First(z => z.ToKeyId == (x.PersonId + "#3")).AttachUrl.Replace('\\', '/'),
                                AttachUrl4 = Funs.DB.AttachFile.First(z => z.ToKeyId == (x.PersonId + "#4")).AttachUrl.Replace('\\', '/'),
                            };
            return getPerson.FirstOrDefault();
        }
        #endregion

        #region 根据identityCard获取人员信息
        /// <summary>
        /// 根据identityCard获取人员信息
        /// </summary>
        /// <param name="identityCard"></param>
        /// <returns></returns>
        public static Model.PersonItem getPersonByIdentityCard(string identityCard)
        {
            var getPerson = Funs.DB.View_SitePerson_Person.FirstOrDefault(x => x.IdentityCard == identityCard);
            return ObjectMapperManager.DefaultInstance.GetMapper<Model.View_SitePerson_Person, Model.PersonItem>().Map(getPerson);
        }
        #endregion

        #region 根据projectId、unitid获取人员信息
        /// <summary>
        /// 根据projectId、unitid获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static List<Model.PersonItem> getPersonByProjectIdUnitId(string projectId, string unitId)
        {
            var persons = from x in Funs.DB.View_SitePerson_Person
                          where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null) && x.IsUsed == true
                          && x.InTime <= DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= DateTime.Now)
                          orderby x.UnitName, x.PersonName
                          select new Model.PersonItem
                          {
                              PersonId = x.PersonId,
                              CardNo = x.CardNo,
                              PersonName = x.PersonName,
                              SexName = x.SexName,
                              IdentityCard = x.IdentityCard,
                              Address = x.Address,
                              ProjectId = x.ProjectId,
                              ProjectCode = x.ProjectCode,
                              ProjectName = x.ProjectName,
                              UnitId = x.UnitId,
                              UnitCode = x.UnitCode,
                              UnitName = x.UnitName,
                              TeamGroupId = x.TeamGroupId,
                              TeamGroupName = x.TeamGroupName,
                              WorkPostId = x.WorkPostId,
                              WorkPostName = x.WorkPostName,
                              InTime = string.Format("{0:yyyy-MM-dd}", x.InTime),
                              OutTime = string.Format("{0:yyyy-MM-dd}", x.OutTime),
                              OutResult = x.OutResult,
                              Telephone = x.Telephone,
                              PhotoUrl = x.PhotoUrl,
                              DepartName = x.DepartName,
                              WorkAreaId = x.WorkAreaId,
                              WorkAreaName = x.WorkAreaName,
                          };
            return persons.ToList();
        }
        #endregion

        #region 获取在岗、离岗、待审人员列表
        /// <summary>
        /// 获取在岗、离岗、待审人员列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="states">0待审1在岗2离岗</param>
        /// <param name="strUnitId">查询单位</param>
        /// <param name="strWorkPostId">查询岗位</param>
        ///  <param name="strParam">查询条件</param>
        /// <returns></returns>
        public static List<Model.PersonItem> getPersonListByProjectIdStates(string projectId, string unitId, string states, string strUnitId, string strWorkPostId, string strParam)
        {
            var getViews = from x in Funs.DB.SitePerson_Person
                           where x.ProjectId == projectId && (strUnitId == null || x.UnitId == strUnitId)
                           && (strWorkPostId == null || x.WorkPostId == strWorkPostId)
                           select x;
            if (!CommonService.GetIsThisUnit(unitId) || string.IsNullOrEmpty(unitId))
            {
                getViews = getViews.Where(x => x.UnitId == unitId);
            }
            if (states == "0")
            {
                getViews = getViews.Where(x => x.IsUsed == false && !x.AuditorDate.HasValue);
            }
            else if (states == "1")
            {
                getViews = getViews.Where(x => x.IsUsed == true && x.InTime <= DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= DateTime.Now));
            }
            else if (states == "2")
            {
                getViews = getViews.Where(x => x.IsUsed == true && x.OutTime <= DateTime.Now);
            }
            else if (states == "-1")
            {
                getViews = getViews.Where(x => x.IsUsed == false && x.AuditorDate.HasValue);
            }

            if (!string.IsNullOrEmpty(strParam))
            {
                getViews = getViews.Where(x => x.PersonName.Contains(strParam) || x.IdentityCard.Contains(strParam));
            }
            var persons = from x in getViews
                          join y in Funs.DB.Base_Unit on x.UnitId equals y.UnitId
                          orderby y.UnitName, x.PersonName
                          select new Model.PersonItem
                          {
                              PersonId = x.PersonId,
                              CardNo = x.CardNo,
                              PersonName = x.PersonName,
                              SexName = (x.Sex == "2" ? "女" : "男"),
                              IdentityCard = x.IdentityCard,
                              Address = x.Address,
                              ProjectId = x.ProjectId,
                              UnitId = x.UnitId,
                              UnitCode = y.UnitCode,
                              UnitName = y.UnitName,
                              TeamGroupId = x.TeamGroupId,
                              TeamGroupName = Funs.DB.ProjectData_TeamGroup.First(z => z.TeamGroupId == x.TeamGroupId).TeamGroupName,
                              WorkPostId = x.WorkPostId,
                              WorkPostName = Funs.DB.Base_WorkPost.First(z => z.WorkPostId == x.WorkPostId).WorkPostName,
                              InTime = string.Format("{0:yyyy-MM-dd}", x.InTime),
                              OutTime = string.Format("{0:yyyy-MM-dd}", x.OutTime),
                              OutResult = x.OutResult,
                              Telephone = x.Telephone,
                              PhotoUrl = x.PhotoUrl,
                              IsUsed = x.IsUsed,
                              IsUsedName = (x.IsUsed == true ? "启用" : "未启用"),
                              WorkAreaId = x.WorkAreaId,
                              WorkAreaName = Funs.DB.ProjectData_WorkArea.First(z => z.WorkAreaId == x.WorkAreaId).WorkAreaName,
                          };
            return persons.ToList();
        }
        #endregion

        #region 根据培训类型获取项目培训人员信息
        /// <summary>
        /// 根据培训类型获取项目培训人员信息
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <param name="unitIds">培训单位ID</param>
        /// <param name="workPostIds">培训岗位ID</param>
        /// <param name="trainTypeId">培训类型ID</param>
        /// <returns></returns>
        public static List<Model.PersonItem> getTrainingPersonListByTrainTypeId(string projectId, string unitIds, string workPostIds, string trainTypeId)
        {
            List<string> unitIdList = Funs.GetStrListByStr(unitIds, ',');
            var getPersons = from x in Funs.DB.View_SitePerson_Person
                             where x.ProjectId == projectId && unitIdList.Contains(x.UnitId) && x.IsUsed == true
                             && x.InTime <= DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= DateTime.Now)
                             select new Model.PersonItem
                             {
                                 PersonId = x.PersonId,
                                 CardNo = x.CardNo,
                                 PersonName = x.PersonName,
                                 SexName = x.SexName,
                                 IdentityCard = x.IdentityCard,
                                 Address = x.Address,
                                 ProjectId = x.ProjectId,
                                 ProjectCode = x.ProjectCode,
                                 ProjectName = x.ProjectName,
                                 UnitId = x.UnitId,
                                 UnitCode = x.UnitCode,
                                 UnitName = x.UnitName,
                                 TeamGroupId = x.TeamGroupId,
                                 TeamGroupName = x.TeamGroupName,
                                 WorkPostId = x.WorkPostId,
                                 WorkPostName = x.WorkPostName,
                                 InTime = string.Format("{0:yyyy-MM-dd}", x.InTime),
                                 OutTime = string.Format("{0:yyyy-MM-dd}", x.OutTime),
                                 OutResult = x.OutResult,
                                 Telephone = x.Telephone,
                                 PhotoUrl = x.PhotoUrl,
                                 DepartName = x.DepartName,
                             };
            if (!string.IsNullOrEmpty(workPostIds))
            {
                List<string> workPostIdList = Funs.GetStrListByStr(workPostIds, ',');
                getPersons = getPersons.Where(x => workPostIdList.Contains(x.WorkPostId));
            }

            List<Model.PersonItem> getTrainPersonList = new List<Model.PersonItem>();
            var getTrainType = TrainTypeService.GetTrainTypeById(trainTypeId);
            if (getTrainType != null && (!getTrainType.IsRepeat.HasValue || getTrainType.IsRepeat == false))
            {
                foreach (var item in getPersons)
                {
                    var getTrainPersonIdList1 = (from x in Funs.DB.EduTrain_TrainRecordDetail
                                                 join y in Funs.DB.EduTrain_TrainRecord on x.TrainingId equals y.TrainingId
                                                 where y.ProjectId == projectId && y.TrainTypeId == trainTypeId && x.CheckResult == true && x.PersonId == item.PersonId
                                                 select x).FirstOrDefault();
                    if (getTrainPersonIdList1 == null)
                    {
                        var getTrainPersonIdList2 = (from x in Funs.DB.Training_Task
                                                     join y in Funs.DB.Training_Plan on x.PlanId equals y.PlanId
                                                     where y.ProjectId == projectId && y.TrainTypeId == trainTypeId && y.States != "3" && x.UserId == item.PersonId
                                                     select x).FirstOrDefault();
                        if (getTrainPersonIdList2 == null)
                        {
                            getTrainPersonList.Add(item);
                        }
                    }
                }
                return getTrainPersonList;
            }
            else
            {
                return getPersons.ToList();
            }
        }
        #endregion

        #region 人员信息保存方法
        /// <summary>
        /// 人员信息保存方法
        /// </summary>
        /// <param name="person">人员信息</param>
        public static void SaveSitePerson(Model.PersonItem person)
        {
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                Model.SitePerson_Person newPerson = new Model.SitePerson_Person
                {
                    PersonId = person.PersonId,
                    ProjectId = person.ProjectId,
                    CardNo = person.CardNo,
                    PersonName = person.PersonName,
                    IdentityCard = person.IdentityCard,
                    Address = person.Address,
                    UnitId = person.UnitId,
                    OutResult = person.OutResult,
                    Telephone = person.Telephone,
                    PhotoUrl = person.PhotoUrl,
                    IDCardUrl = person.AttachUrl1,
                    InTime = Funs.GetNewDateTime(person.InTime),
                    OutTime = Funs.GetNewDateTime(person.OutTime),
                    AuditorId = person.AuditorId,
                    // AuditorDate = Funs.GetNewDateTime(person.AuditorDate),
                    Sex = person.Sex,
                };
                if (!string.IsNullOrEmpty(person.TeamGroupId))
                {
                    newPerson.TeamGroupId = person.TeamGroupId;
                }
                if (!string.IsNullOrEmpty(person.WorkPostId))
                {
                    newPerson.WorkPostId = person.WorkPostId;
                }
                if (!string.IsNullOrEmpty(person.WorkAreaId))
                {
                    newPerson.WorkAreaId = person.WorkAreaId;
                }
                if (person.IsUsed == true)
                {
                    newPerson.IsUsed = true;
                }
                else
                {
                    newPerson.IsUsed = false;
                }

                var getPerson = db.SitePerson_Person.FirstOrDefault(x => x.IdentityCard == newPerson.IdentityCard || x.PersonId == newPerson.PersonId);
                if (getPerson == null)
                {
                    newPerson.PersonId = SQLHelper.GetNewID(typeof(Model.SitePerson_Person));
                    db.SitePerson_Person.InsertOnSubmit(newPerson);
                    db.SubmitChanges();
                    CodeRecordsService.InsertCodeRecordsByMenuIdProjectIdUnitId(Const.PersonListMenuId, person.ProjectId, person.UnitId, person.PersonId, newPerson.InTime);
                }
                else
                {
                    newPerson.PersonId = getPerson.PersonId;
                    getPerson.ProjectId = person.ProjectId;
                    getPerson.CardNo = person.CardNo;
                    getPerson.PersonName = person.PersonName;
                    getPerson.IdentityCard = person.IdentityCard;
                    getPerson.Address = person.Address;
                    getPerson.UnitId = person.UnitId;
                    getPerson.OutResult = person.OutResult;
                    getPerson.Telephone = person.Telephone;
                    if (!string.IsNullOrEmpty(person.PhotoUrl))
                    {
                        getPerson.PhotoUrl = person.PhotoUrl;
                    }
                    if (!string.IsNullOrEmpty(person.AttachUrl1))
                    {
                        getPerson.IDCardUrl = person.AttachUrl1;
                    }
                    getPerson.InTime = Funs.GetNewDateTime(person.InTime);
                    getPerson.OutTime = Funs.GetNewDateTime(person.OutTime);
                    getPerson.Sex = person.Sex;

                    //getPerson.AuditorDate = Funs.GetNewDateTime(person.AuditorDate);
                    if (!string.IsNullOrEmpty(person.TeamGroupId))
                    {
                        getPerson.TeamGroupId = person.TeamGroupId;
                    }
                    if (!string.IsNullOrEmpty(person.WorkPostId))
                    {
                        getPerson.WorkPostId = person.WorkPostId;
                    }
                    if (!string.IsNullOrEmpty(person.WorkAreaId))
                    {
                        getPerson.WorkAreaId = person.WorkAreaId;
                    }
                    if (getPerson.AuditorDate.HasValue && getPerson.IsUsed == false)
                    {
                        getPerson.AuditorDate = null;
                    }
                    else
                    {
                        getPerson.IsUsed = person.IsUsed;
                        getPerson.AuditorDate = DateTime.Now;
                    }
                    getPerson.AuditorId = person.AuditorId;
                    if (!newPerson.OutTime.HasValue)
                    {
                        getPerson.OutTime = null;
                        getPerson.ExchangeTime = null;
                    }

                    getPerson.ExchangeTime2 = null;
                    db.SubmitChanges();
                }
                if (!string.IsNullOrEmpty(newPerson.PersonId))
                {
                    SaveMeetUrl(newPerson.PersonId, Const.ProjectPersonChangeMenuId, person.AttachUrl1, person.AttachUrl2, person.AttachUrl3, person.AttachUrl4);
                }
            }
        }

        #region 人员附件保存方法
        /// <summary>
        /// 人员附件保存方法
        /// </summary>
        public static void SaveMeetUrl(string personId, string menuId, string url1, string url2, string url3, string url4)
        {
            Model.ToDoItem toDoItem = new Model.ToDoItem
            {
                MenuId = menuId,
                DataId = personId + "#1",
                UrlStr = url1,
            };
            if (!string.IsNullOrEmpty(url1))
            {
                APIUpLoadFileService.SaveAttachUrl(toDoItem);
            }

            toDoItem.DataId = personId + "#2";
            toDoItem.UrlStr = url2;
            if (!string.IsNullOrEmpty(url2))
            {
                APIUpLoadFileService.SaveAttachUrl(toDoItem);
            }

            toDoItem.DataId = personId + "#3";
            toDoItem.UrlStr = url3;
            if (!string.IsNullOrEmpty(url3))
            {
                APIUpLoadFileService.SaveAttachUrl(toDoItem);
            }

            toDoItem.DataId = personId + "#4";
            toDoItem.UrlStr = url4;
            if (!string.IsNullOrEmpty(url4))
            { APIUpLoadFileService.SaveAttachUrl(toDoItem); }
        }
        #endregion
        #endregion

        #region 更新人员附件
        /// <summary>
        /// 更新人员附件
        /// </summary>
        /// <param name="person"></param>
        public static void SaveSitePersonAttachment(Model.PersonItem person)
        {
            var getPerson = Funs.DB.SitePerson_Person.FirstOrDefault(x => x.IdentityCard == person.IdentityCard || x.PersonId == person.PersonId);
            if (getPerson != null)
            {
                if (!string.IsNullOrEmpty(person.PhotoUrl))
                {
                    getPerson.PhotoUrl = person.PhotoUrl;
                }
                if (!string.IsNullOrEmpty(person.AttachUrl1))
                {
                    getPerson.IDCardUrl = person.AttachUrl1;
                }
                Funs.SubmitChanges();
                SaveMeetUrl(getPerson.PersonId, Const.ProjectPersonChangeMenuId, person.AttachUrl1, person.AttachUrl2, person.AttachUrl3, person.AttachUrl4);
            }
        }
        #endregion

        #region 人员离场
        /// <summary>
        /// 人员离场
        /// </summary>
        /// <param name="personId"></param>
        public static void getPersonOut(string personId)
        {
            if (!string.IsNullOrEmpty(personId))
            {
                List<string> getLists = Funs.GetStrListByStr(personId, ',');
                foreach (var item in getLists)
                {
                    var getPerson = Funs.DB.SitePerson_Person.FirstOrDefault(x => x.PersonId == item);
                    if (getPerson != null)
                    {
                        getPerson.OutTime = DateTime.Now;
                        PersonService.UpdatePerson(getPerson);
                    }
                }
            }
        }
        #endregion

        #region 人员出入场
        /// <summary>
        /// 人员出入场
        /// </summary>
        /// <param name="projectId"></param>w
        /// <param name="idCard"></param>
        /// <param name="isIn"></param>
        /// <param name="changeTime"></param>
        public static void getPersonInOut(string projectId, string idCard, int isIn, DateTime changeTime)
        {
            if (!string.IsNullOrEmpty(idCard))
            {
                var getPerson = Funs.DB.SitePerson_Person.FirstOrDefault(x => x.ProjectId == projectId && x.IdentityCard == idCard);
                if (getPerson != null)
                {
                    var getPersonInOut = Funs.DB.SitePerson_PersonInOut.FirstOrDefault(x => x.PersonId == getPerson.PersonId && x.ProjectId == projectId && x.ChangeTime == changeTime);
                    if (getPersonInOut == null)
                    {
                        Model.SitePerson_PersonInOut newInOut = new Model.SitePerson_PersonInOut
                        {
                            PersonInOutId = SQLHelper.GetNewID(),
                            ProjectId = projectId,
                            UnitId = getPerson.UnitId,
                            PersonId = getPerson.PersonId,
                            IsIn = isIn == 1 ? true : false,
                            ChangeTime = changeTime,
                        };
                        Funs.DB.SitePerson_PersonInOut.InsertOnSubmit(newInOut);
                        Funs.SubmitChanges();
                    }
                }
            }
        }
        #endregion

        #region 更新人员数据交换时间
        /// <summary>
        /// 更新人员数据交换时间
        /// </summary>
        /// <param name="personId">人员ID</param>
        public static void getUpdatePersonExchangeTime(string personId, string type)
        {
            var getPerson = PersonService.GetPersonById(personId);
            if (getPerson != null && !string.IsNullOrEmpty(type))
            {
                if (type == "1")
                {
                    getPerson.ExchangeTime2 = DateTime.Now;
                }
                else
                {
                    getPerson.ExchangeTime = DateTime.Now;
                }
                Funs.SubmitChanges();
            }
        }
        #endregion

        #region 获取人员信息出入场记录
        /// <summary>
        /// 获取人员信息出入场记录
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="unitId"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<Model.PersonInOutItem> getPersonInOutList(string projectId, string unitId, string startTime, string endTime)
        {
            DateTime? startTimeD = Funs.GetNewDateTime(startTime);
            DateTime? endTimeD = Funs.GetNewDateTime(endTime);
            var personInOuts = from x in Funs.DB.SitePerson_PersonInOut
                               join y in Funs.DB.SitePerson_Person on x.PersonId equals y.PersonId
                               where x.ProjectId == projectId
                               select new Model.PersonInOutItem
                               {
                                   PersonId = x.PersonId,
                                   PersonName = y.PersonName,
                                   ProjectId = x.ProjectId,
                                   UnitId = y.UnitId,
                                   UnitName = Funs.DB.Base_Unit.First(z => z.UnitId == y.UnitId).UnitName,
                                   WorkPostId = y.WorkPostId,
                                   WorkPostName = Funs.DB.Base_WorkPost.First(z => z.WorkPostId == y.WorkPostId).WorkPostName,
                                   IsIn = x.IsIn,
                                   IsInName = x.IsIn == true ? "进场" : "出场",
                                   ChangeTime = string.Format("{0:yyyy-MM-dd HH:mm}", x.ChangeTime),
                                   ChangeTimeD = x.ChangeTime,
                               };
            if (!string.IsNullOrEmpty(unitId) && !CommonService.GetIsThisUnit(unitId))
            {
                personInOuts = personInOuts.Where(x => x.UnitId == unitId);
            }
            if (startTimeD.HasValue)
            {
                personInOuts = personInOuts.Where(x => x.ChangeTimeD >= startTimeD);
            }
            if (endTimeD.HasValue)
            {
                personInOuts = personInOuts.Where(x => x.ChangeTimeD <= endTimeD);
            }
            return personInOuts.OrderBy(x => x.UnitName).OrderBy(x => x.PersonName).ToList();
        }
        #endregion
    }
}
