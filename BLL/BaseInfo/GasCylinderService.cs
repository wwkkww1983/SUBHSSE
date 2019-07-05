using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 气瓶类型
    /// </summary>
    public static class GasCylinderService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取气瓶类型
        /// </summary>
        /// <param name="gasCylinderId"></param>
        /// <returns></returns>
        public static Model.Base_GasCylinder GetGasCylinderById(string gasCylinderId)
        {
            return Funs.DB.Base_GasCylinder.FirstOrDefault(e => e.GasCylinderId == gasCylinderId);
        }

        /// <summary>
        /// 添加气瓶类型
        /// </summary>
        /// <param name="gasCylinder"></param>
        public static void AddGasCylinder(Model.Base_GasCylinder gasCylinder)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_GasCylinder newGasCylinder = new Model.Base_GasCylinder
            {
                GasCylinderId = gasCylinder.GasCylinderId,
                GasCylinderCode = gasCylinder.GasCylinderCode,
                GasCylinderName = gasCylinder.GasCylinderName,
                Remark = gasCylinder.Remark
            };
            db.Base_GasCylinder.InsertOnSubmit(newGasCylinder);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改气瓶类型
        /// </summary>
        /// <param name="gasCylinder"></param>
        public static void UpdateGasCylinder(Model.Base_GasCylinder gasCylinder)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_GasCylinder newGasCylinder = db.Base_GasCylinder.FirstOrDefault(e => e.GasCylinderId == gasCylinder.GasCylinderId);
            if (newGasCylinder != null)
            {
                newGasCylinder.GasCylinderCode = gasCylinder.GasCylinderCode;
                newGasCylinder.GasCylinderName = gasCylinder.GasCylinderName;
                newGasCylinder.Remark = gasCylinder.Remark;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除气瓶类型
        /// </summary>
        /// <param name="gasCylinderId"></param>
        public static void DeleteGasCylinderById(string gasCylinderId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_GasCylinder gasCylinder = db.Base_GasCylinder.FirstOrDefault(e => e.GasCylinderId == gasCylinderId);
            if (gasCylinder != null)
            {
                db.Base_GasCylinder.DeleteOnSubmit(gasCylinder);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取气瓶类型下拉选择项
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_GasCylinder> GetGasCylinderList()
        {
            return (from x in Funs.DB.Base_GasCylinder orderby x.GasCylinderCode select x).ToList();
        }
    }
}
