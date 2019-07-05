using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
   public static class PersonSpecialtyService
    {
        public static Model.SUBHSSEDB db = Funs.DB;
        /// <summary>
        /// 根据主键获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_PersonSpecialty GetPersonSpecialtyById(string personSpecialtyId)
        {
            return Funs.DB.Base_PersonSpecialty.FirstOrDefault(e => e.PersonSpecialtyId == personSpecialtyId);
        }

        /// <summary>
        /// 根据名称获取信息
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public static Model.Base_PersonSpecialty GetPersonSpecialtyByName(string personSpecialtyName)
        {
            return Funs.DB.Base_PersonSpecialty.FirstOrDefault(e => e.PersonSpecialtyName == personSpecialtyName);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="?"></param>
        public static void AddPersonSpecialty(Model.Base_PersonSpecialty personSpecialty)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_PersonSpecialty newPersonSpecialty = new Model.Base_PersonSpecialty
            {
                PersonSpecialtyId = personSpecialty.PersonSpecialtyId,
                PersonSpecialtyCode = personSpecialty.PersonSpecialtyCode,
                PersonSpecialtyName = personSpecialty.PersonSpecialtyName,
                Remark = personSpecialty.Remark
            };

            db.Base_PersonSpecialty.InsertOnSubmit(newPersonSpecialty);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="teamGroup"></param>
        public static void UpdatePersonSpecialty(Model.Base_PersonSpecialty personSpecialty)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_PersonSpecialty newPersonSpecialty = db.Base_PersonSpecialty.FirstOrDefault(e => e.PersonSpecialtyId == personSpecialty.PersonSpecialtyId);
            if (newPersonSpecialty != null)
            {
                newPersonSpecialty.PersonSpecialtyCode = personSpecialty.PersonSpecialtyCode;
                newPersonSpecialty.PersonSpecialtyName = personSpecialty.PersonSpecialtyName;
                newPersonSpecialty.Remark = personSpecialty.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除信息
        /// </summary>
        /// <param name="personSpecialtyId"></param>
        public static void DeletePersonSpecialtyById(string personSpecialtyId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_PersonSpecialty personSpecialty = db.Base_PersonSpecialty.FirstOrDefault(e => e.PersonSpecialtyId == personSpecialtyId);
            {
                db.Base_PersonSpecialty.DeleteOnSubmit(personSpecialty);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取类别下拉项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_PersonSpecialty> GetPersonSpecialtyList()
        {
            var list = (from x in Funs.DB.Base_PersonSpecialty orderby x.PersonSpecialtyCode select x).ToList();
            return list;
        }

        /// <summary>
        /// 获取专业下拉选项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_PersonSpecialty> GetPersonSpecialtyDropDownList()
        {
            var list = (from x in Funs.DB.Base_PersonSpecialty orderby x.PersonSpecialtyCode select x).ToList();
            return list;
        }
    }
}