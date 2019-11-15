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

        /// <summary>
        /// 人员信息保存方法
        /// </summary>
        /// <param name="person">人员信息</param>
        public static void SaveSitePerson(Model.PersonItem person)
        {
            Model.SitePerson_Person newPerson = new Model.SitePerson_Person
            {
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
            
            var getPerson = Funs.DB.SitePerson_Person.FirstOrDefault(x => x.IdentityCard == newPerson.IdentityCard);
            if (getPerson == null)
            {
                newPerson.PersonId = SQLHelper.GetNewID(typeof(Model.SitePerson_Person));
                PersonService.AddPerson(newPerson);
            }
            else
            {
                newPerson.PersonId = getPerson.PersonId;
                PersonService.UpdatePerson(newPerson);
            }
        }       
    }
}
