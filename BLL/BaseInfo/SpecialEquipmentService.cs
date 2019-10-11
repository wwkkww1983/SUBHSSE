using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 机具设备
    /// </summary>
    public static class SpecialEquipmentService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取机具设备
        /// </summary>
        /// <param name="specialEquipmentId"></param>
        /// <returns></returns>
        public static Model.Base_SpecialEquipment GetSpecialEquipmentById(string specialEquipmentId)
        {
            return Funs.DB.Base_SpecialEquipment.FirstOrDefault(e => e.SpecialEquipmentId == specialEquipmentId);
        }

        /// <summary>
        /// 添加机具设备
        /// </summary>
        /// <param name="specialEquipment"></param>
        public static void AddSpecialEquipment(Model.Base_SpecialEquipment specialEquipment)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_SpecialEquipment newSpecialEquipment = new Model.Base_SpecialEquipment
            {
                SpecialEquipmentId = specialEquipment.SpecialEquipmentId,
                SpecialEquipmentCode = specialEquipment.SpecialEquipmentCode,
                SpecialEquipmentName = specialEquipment.SpecialEquipmentName,
                Remark = specialEquipment.Remark,
                IsSpecial = specialEquipment.IsSpecial
            };
            db.Base_SpecialEquipment.InsertOnSubmit(newSpecialEquipment);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改机具设备
        /// </summary>
        /// <param name="specialEquipment"></param>
        public static void UpdateSpecialEquipment(Model.Base_SpecialEquipment specialEquipment)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_SpecialEquipment newSpecialEquipment = db.Base_SpecialEquipment.FirstOrDefault(e => e.SpecialEquipmentId == specialEquipment.SpecialEquipmentId);
            if (newSpecialEquipment != null)
            {
                newSpecialEquipment.SpecialEquipmentCode = specialEquipment.SpecialEquipmentCode;
                newSpecialEquipment.SpecialEquipmentName = specialEquipment.SpecialEquipmentName;
                newSpecialEquipment.Remark = specialEquipment.Remark;
                newSpecialEquipment.IsSpecial = specialEquipment.IsSpecial;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除机具设备
        /// </summary>
        /// <param name="specialEquipmentId"></param>
        public static void DeleteSpecialEquipmentById(string specialEquipmentId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Base_SpecialEquipment specialEquipment = db.Base_SpecialEquipment.FirstOrDefault(e => e.SpecialEquipmentId == specialEquipmentId);
            if (specialEquipment != null)
            {
                db.Base_SpecialEquipment.DeleteOnSubmit(specialEquipment);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 获取机具设备列表
        /// </summary>
        /// <returns></returns>
        public static List<Model.Base_SpecialEquipment> GetSpecialEquipmentList(bool isSpecial)
        {
            return (from x in Funs.DB.Base_SpecialEquipment where x.IsSpecial == isSpecial
                    orderby x.SpecialEquipmentCode select x).ToList();
        }

        /// <summary>
        /// 用户下拉框
        /// </summary>
        /// <param name="dropName">下拉框名字</param>
        /// <param name="projectId">项目id</param>
        /// <param name="isShowPlease">是否显示请选择</param>
        public static void InitSpecialEquipmentDropDownList(FineUIPro.DropDownList dropName, bool isSpecial, bool isShowPlease)
        {
            dropName.DataValueField = "SpecialEquipmentId";
            dropName.DataTextField = "SpecialEquipmentName";
            dropName.DataSource = GetSpecialEquipmentList(isSpecial);
            dropName.DataBind();
            if (isShowPlease)
            {
                Funs.FineUIPleaseSelect(dropName);
            }
        }
    }
}