using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 行政管理检查类别
    /// </summary>
    public static class CheckTypeSetService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取行政管理检查类别
        /// </summary>
        /// <param name="checkTypeId"></param>
        /// <returns></returns>
        public static Model.Administrative_CheckTypeSet GetCheckTypeSetByCheckTypeCode(string checkTypeCode)
        {
            return Funs.DB.Administrative_CheckTypeSet.FirstOrDefault(e => e.CheckTypeCode == checkTypeCode);
        }

        /// <summary>
        /// 根据上级id获取所有类别
        /// </summary>
        /// <param name="supCheckTypeId"></param>
        /// <returns></returns>
        public static List<Model.Administrative_CheckTypeSet> GetCheckTypeSetBySupCheckTypeCode(string supCheckTypeCode)
        {
            return (from x in Funs.DB.Administrative_CheckTypeSet where x.SupCheckTypeCode == supCheckTypeCode select x).ToList();
        }

        /// <summary>
        /// 添加行政管理检查类别
        /// </summary>
        /// <param name="checkTypeSet"></param>
        public static void AddCheckTypeSet(Model.Administrative_CheckTypeSet checkTypeSet)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_CheckTypeSet newCheckTypeSet = new Model.Administrative_CheckTypeSet
            {
                CheckTypeCode = checkTypeSet.CheckTypeCode,
                CheckTypeContent = checkTypeSet.CheckTypeContent,
                SupCheckTypeCode = checkTypeSet.SupCheckTypeCode,
                SortIndex = checkTypeSet.SortIndex,
                IsEndLevel = checkTypeSet.IsEndLevel
            };
            db.Administrative_CheckTypeSet.InsertOnSubmit(newCheckTypeSet);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改行政管理检查类别
        /// </summary>
        /// <param name="checkTypeSet"></param>
        public static void UpdateCheckTypeSet(Model.Administrative_CheckTypeSet checkTypeSet)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_CheckTypeSet newCheckTypeSet = db.Administrative_CheckTypeSet.FirstOrDefault(e => e.CheckTypeCode == checkTypeSet.CheckTypeCode);
            if (newCheckTypeSet != null)
            {
                newCheckTypeSet.CheckTypeContent = checkTypeSet.CheckTypeContent;
                newCheckTypeSet.SupCheckTypeCode = checkTypeSet.SupCheckTypeCode;
                newCheckTypeSet.SortIndex = checkTypeSet.SortIndex;
                newCheckTypeSet.IsEndLevel = checkTypeSet.IsEndLevel;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除行政管理检查类别
        /// </summary>
        /// <param name="checkTypeId"></param>
        public static void DeleteCheckTypeSetById(string checkTypeCode)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Administrative_CheckTypeSet checkTypeSet = db.Administrative_CheckTypeSet.FirstOrDefault(e => e.CheckTypeCode == checkTypeCode);
            if (checkTypeSet != null)
            {
                db.Administrative_CheckTypeSet.DeleteOnSubmit(checkTypeSet);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取下拉列表选择项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Administrative_CheckTypeSet> GetCheckTypeSetList()
        {
            return (from x in Funs.DB.Administrative_CheckTypeSet where x.SupCheckTypeCode == "0" select x).ToList();
        }

        /// <summary>
        /// 根据上级Id查询所有检查类别
        /// </summary>
        /// <param name="supCheckTypeCode">上级Id</param>
        /// <returns>检查类别的集合</returns>
        public static IEnumerable GetCheckTypeSetsBySupCheckTypeCode(string supCheckTypeCode, string manageCheckId)
        {
            return from x in db.Administrative_CheckTypeSet
                   where x.SupCheckTypeCode == supCheckTypeCode
                   orderby x.SortIndex
                   select new
                   {
                       x.CheckTypeCode,
                       x.CheckTypeContent,
                       x.SupCheckTypeCode,
                       x.SortIndex,
                       x.IsEndLevel,
                       IsCheck = (from y in db.Administrative_ManageCheckItem where x.CheckTypeCode == y.CheckTypeCode && y.ManageCheckId == manageCheckId select y.IsCheck).First()
                       == null ? true : (from y in db.Administrative_ManageCheckItem where x.CheckTypeCode == y.CheckTypeCode && y.ManageCheckId == manageCheckId select y.IsCheck).First(),
                   };
        }

        /// <summary>
        /// 是否末级
        /// </summary>
        /// <param name="checkItemSetId">检查类别主键</param>
        /// <returns>布尔值</returns>
        public static bool IsEndLevel(string checkTypeCode)
        {
            if (checkTypeCode == "0")
            {
                return false;
            }
            else
            {
                Model.Administrative_CheckTypeSet checkTypeSet = Funs.DB.Administrative_CheckTypeSet.FirstOrDefault(e => e.CheckTypeCode == checkTypeCode);
                return Convert.ToBoolean(checkTypeSet.IsEndLevel);
            }
        }

        /// <summary>
        /// 根据上级Id查询所有检查类别的数量
        /// </summary>
        /// <param name="supCheckTypeCode">上级Id</param>
        /// <returns>检查类别的数量</returns>
        public static int GetCheckTypeSetCountBySupCheckTypeCode(string supCheckTypeCode)
        {
            var q = (from x in Funs.DB.Administrative_CheckTypeSet where x.SupCheckTypeCode == supCheckTypeCode select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据上级Id查询所有检查类别主键列的值
        /// </summary>
        /// <param name="supCheckTypeCode">上级Id</param>
        /// <returns>检查类别主键列值的集合</returns>
        public static List<string> GetCheckTypeCodesBySupCheckTypeCode(string supCheckTypeCode)
        {
            return (from x in Funs.DB.Administrative_CheckTypeSet where x.SupCheckTypeCode == supCheckTypeCode select x.CheckTypeCode).ToList();
        }
    }
}