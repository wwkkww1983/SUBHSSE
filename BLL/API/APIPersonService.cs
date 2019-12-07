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
                              WorkPostId=x.WorkPostId,
                              WorkPostName=w.WorkPostName,
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
            return  getPerson.FirstOrDefault();
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
           var  getViews = from x in Funs.DB.SitePerson_Person
                        where x.ProjectId == projectId && (strUnitId == null || x.UnitId == strUnitId)
                        && (strWorkPostId == null || x.WorkPostId == strWorkPostId) && (strParam == null || strParam.Contains(x.PersonName) || strParam.Contains(x.IdentityCard))
                        select x;
            if (!CommonService.GetIsThisUnit(unitId))
            {
                getViews = getViews.Where(x => x.UnitId == unitId);
            }
            if (states == "0")
            {
                getViews = getViews.Where(x => x.IsUsed == false);
            }
            else if (states == "1")
            {
                getViews = getViews.Where(x => x.IsUsed == true && x.InTime <= DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= DateTime.Now));
            }
            else if (states == "2")
            {
                getViews = getViews.Where(x => x.IsUsed == true && x.OutTime <= DateTime.Now);
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
            Model.SitePerson_Person newPerson = new Model.SitePerson_Person
            {
                PersonId = person.PersonId,
                ProjectId = person.ProjectId,
                CardNo = person.CardNo,
                PersonName = person.PersonName,
                IdentityCard = person.IdentityCard,
                Address = person.Address,
                UnitId = person.UnitId,
                TeamGroupId = person.TeamGroupId,
                WorkPostId = person.WorkPostId,
                OutResult = person.OutResult,
                Telephone = person.Telephone,
                PhotoUrl = person.PhotoUrl,
                InTime = Funs.GetNewDateTime(person.InTime),
                OutTime = Funs.GetNewDateTime(person.OutTime),
                IsUsed = person.IsUsed,
                Sex = person.Sex,
            };
            
            var getPerson = Funs.DB.SitePerson_Person.FirstOrDefault(x => x.IdentityCard == newPerson.IdentityCard || x.PersonId== newPerson.PersonId);
            if (getPerson == null)
            {
                newPerson.PersonId = SQLHelper.GetNewID(typeof(Model.SitePerson_Person));
                PersonService.AddPerson(newPerson);
            }
            else
            {
                getPerson.ProjectId = person.ProjectId;
                getPerson.CardNo = person.CardNo;
                getPerson.PersonName = person.PersonName;
                getPerson.IdentityCard = person.IdentityCard;
                getPerson.Address = person.Address;
                getPerson.UnitId = person.UnitId;
                getPerson.TeamGroupId = person.TeamGroupId;
                getPerson.WorkPostId = person.WorkPostId;
                getPerson.OutResult = person.OutResult;
                getPerson.Telephone = person.Telephone;
                getPerson.PhotoUrl = person.PhotoUrl;
                getPerson.InTime = Funs.GetNewDateTime(person.InTime);
                getPerson.OutTime = Funs.GetNewDateTime(person.OutTime);
                getPerson.IsUsed = person.IsUsed;
                getPerson.Sex = person.Sex;
                PersonService.UpdatePerson(newPerson);
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
            var getPerson = Funs.DB.SitePerson_Person.FirstOrDefault(x => x.PersonId == personId);
            if (getPerson != null)
            {
                getPerson.OutTime = System.DateTime.Now;
                PersonService.UpdatePerson(getPerson);
            }
        }
        #endregion
    }
}
