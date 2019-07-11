using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 隐患巡检（手机端）
    /// </summary>
    public static class RegistrationService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 根据主键获取隐患巡检
        /// </summary>
        /// <param name="registrationId"></param>
        /// <returns></returns>
        public static Model.View_Inspection_Registration GetRegistrationById(string registrationId)
        {
            return db.View_Inspection_Registration.FirstOrDefault(e => e.RegistrationId == registrationId);
        }

        /// <summary>
        /// 根据主键删除隐患巡检
        /// </summary>
        /// <param name="registrationId"></param>
        public static void DeleteRegistrationById(string registrationId)
        {
            Model.Inspection_Registration registration = db.Inspection_Registration.FirstOrDefault(e => e.RegistrationId == registrationId);
            if (registration!=null)
            {
                BLL.UploadFileService.DeleteFile(Funs.RootPath, registration.ImageUrl);//删除整改前图片
                BLL.UploadFileService.DeleteFile(Funs.RootPath, registration.RectificationImageUrl);//删除整改后图片
                BLL.SafetyDataItemService.DeleteSafetyDataItemByID(registrationId); // 删除安全资料项
                db.Inspection_Registration.DeleteOnSubmit(registration);
                db.SubmitChanges();
            }
        }
    }
}
