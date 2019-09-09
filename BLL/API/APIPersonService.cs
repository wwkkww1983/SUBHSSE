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
        /// <summary>
        /// 获取用户登录信息
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static Model.UserItem PersonLogOn(Model.UserItem userInfo)
        {
            var getUser = from x in Funs.DB.SitePerson_Person
                          join y in Funs.DB.Base_Unit on x.UnitId equals y.UnitId
                          join z in Funs.DB.Base_Project on x.ProjectId equals z.ProjectId
                          join w in Funs.DB.Base_WorkPost on x.WorkPostId equals w.WorkPostId
                          where x.Telephone == userInfo.Account && x.Password == Funs.EncryptionPassword(userInfo.Password)
                          && x.InTime <= DateTime.Now && (!x.OutTime.HasValue || x.OutTime >= DateTime.Now)
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

        /// <summary>
        /// 根据personId获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static Model.PersonItem getPersonByPersonId(string personId)
        {
            var getPerson = Funs.DB.View_SitePerson_Person.FirstOrDefault(x => x.PersonId == personId);
            return ObjectMapperManager.DefaultInstance.GetMapper<Model.View_SitePerson_Person, Model.PersonItem>().Map(getPerson);
        }

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

        /// <summary>
        /// 根据projectId、unitid获取人员信息
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public static List<Model.PersonItem> getPersonByProjectIdUnitId(string projectId, string unitId)
        {           
           var persons = (from x in Funs.DB.View_SitePerson_Person
                       where x.ProjectId == projectId && (x.UnitId == unitId || unitId == null)
                       orderby x.UnitCode, x.PersonName
                       select x);
            return ObjectMapperManager.DefaultInstance.GetMapper<List<Model.View_SitePerson_Person>, List<Model.PersonItem>>().Map(persons.ToList());
        }
    }
}
