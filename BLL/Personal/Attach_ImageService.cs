using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class Attach_ImageService
    {
        /// <summary>
        /// 根据手机APP上传内容主键获取手机APP上传内容
        /// </summary>
        /// <param name="attach_image_id">手机APP上传内容主键</param>
        /// <returns>手机APP上传内容</returns>
        public static Model.Attach_Image GetAttach_ImageByattach_image_id(string attach_image_id)
        {
            return Funs.DB.Attach_Image.FirstOrDefault(x => x.Attach_image_id == attach_image_id);
        }

        /// <summary>
        /// 根据手机APP上传内容主键删除一个手机APP上传内容
        /// </summary>
        /// <param name="attach_image_id">手机APP上传内容主键</param>
        public static void DeleteAttach_ImageByattach_image_id(string attach_image_id)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Attach_Image attach_image = db.Attach_Image.FirstOrDefault(e => e.Attach_image_id == attach_image_id);
            if (attach_image != null)
            {
                BLL.UploadFileService.DeleteFile(Funs.RootPath, attach_image.File_path);//删除文件内容
                db.Attach_Image.DeleteOnSubmit(attach_image);
                db.SubmitChanges();
            }
        }
    }
}
