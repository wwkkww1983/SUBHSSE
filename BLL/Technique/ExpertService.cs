using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    public class ExpertService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据安全专家Id获取事故案例明细
        /// </summary>
        /// <param name="expertId"></param>
        /// <returns></returns>
        public static Model.Technique_Expert GetExpertById(string expertId)
        {
            return Funs.DB.Technique_Expert.FirstOrDefault(e => e.ExpertId == expertId);
        }

        /// <summary>
        /// 根据整理人获取安全专家信息
        /// </summary>
        /// <param name="compileMan"></param>
        /// <returns></returns>
        public static List<Model.Technique_Expert> GetExpertByCompileMan(string compileMan)
        {
            return (from x in Funs.DB.Technique_Expert where x.CompileMan == compileMan select x).ToList();
        }

        /// <summary>
        /// 增加安全专家信息
        /// </summary>
        /// <param name="item"></param>
        public static void AddExpert(Model.Technique_Expert item)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Technique_Expert newItem = new Model.Technique_Expert
            {
                ExpertId = item.ExpertId,
                ExpertCode = item.ExpertCode,
                ExpertName = item.ExpertName,
                Sex = item.Sex,
                Birthday = item.Birthday,
                Age = item.Age,
                Marriage = item.Marriage,
                Nation = item.Nation,
                IdentityCard = item.IdentityCard,
                Email = item.Email,
                Telephone = item.Telephone,
                Education = item.Education,
                Hometown = item.Hometown,
                UnitId = item.UnitId,
                ExpertTypeId = item.ExpertTypeId,
                PersonSpecialtyId = item.PersonSpecialtyId,
                PostTitleId = item.PostTitleId,
                Performance = item.Performance,
                EffectiveDate = item.EffectiveDate,
                CompileMan = item.CompileMan,
                CompileDate = item.CompileDate,
                AttachUrl = item.AttachUrl,
                PhotoUrl = item.PhotoUrl,
                IsPass = item.IsPass,
                UnitName = item.UnitName,
                UpState = item.UpState
            };
            db.Technique_Expert.InsertOnSubmit(newItem);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改安全专家信息
        /// </summary>
        /// <param name="item"></param>
        public static void UpdateExpert(Model.Technique_Expert item)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Technique_Expert newItem = db.Technique_Expert.FirstOrDefault(e => e.ExpertId == item.ExpertId);
            if (newItem != null)
            {
                newItem.ExpertCode = item.ExpertCode;
                newItem.ExpertName = item.ExpertName;
                newItem.Sex = item.Sex;
                newItem.Birthday = item.Birthday;
                newItem.Age = item.Age;
                newItem.Marriage = item.Marriage;
                newItem.Nation = item.Nation;
                newItem.IdentityCard = item.IdentityCard;
                newItem.Email = item.Email;
                newItem.Telephone = item.Telephone;
                newItem.Education = item.Education;
                newItem.Hometown = item.Hometown;
                newItem.UnitId = item.UnitId;
                newItem.ExpertTypeId = item.ExpertTypeId;
                newItem.PersonSpecialtyId = item.PersonSpecialtyId;
                newItem.PostTitleId = item.PostTitleId;
                newItem.Performance = item.Performance;
                newItem.EffectiveDate = item.EffectiveDate;
                newItem.CompileMan = item.CompileMan;
                newItem.CompileDate = item.CompileDate;
                newItem.AttachUrl = item.AttachUrl;
                newItem.PhotoUrl = item.PhotoUrl;
                newItem.UnitName = item.UnitName;
                newItem.UpState = item.UpState;
                db.SubmitChanges();
            }
        }
        
        /// <summary>
        /// 修改安全专家信息 是否采用
        /// </summary>
        /// <param name="item"></param>
        public static void UpdateExpertIsPass(Model.Technique_Expert item)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Technique_Expert newItem = db.Technique_Expert.FirstOrDefault(e => e.ExpertId == item.ExpertId);
            if (newItem != null)
            {
                newItem.AuditMan = item.AuditMan;
                newItem.AuditDate = item.AuditDate;
                newItem.IsPass = item.IsPass;
                newItem.UpState = item.UpState;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据主键删除安全专家信息
        /// </summary>
        /// <param name="expertId">安全专家主键</param>
        public static void DeleteExpertId(string expertId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Technique_Expert item = db.Technique_Expert.FirstOrDefault(e => e.ExpertId == expertId);
            if (item != null)
            {
                if (!string.IsNullOrEmpty(item.AttachUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, item.AttachUrl);
                }
                if (!string.IsNullOrEmpty(item.PhotoUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, item.PhotoUrl);
                }

                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(item.ExpertId);
                db.Technique_Expert.DeleteOnSubmit(item);
                db.SubmitChanges();
            }
        }
    }
}
