namespace BLL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class ResourcesDataService
    {
        public static Model.SUBHSSEDB db = Funs.DB;

        /// <summary>
        /// 获取参考资料信息
        /// </summary>
        /// <param name="resourcesDataId">参考资料Id</param>
        /// <returns></returns>
        public static Model.Resources_ResourcesData GetResourcesDataByResourcesDataId(string resourcesDataId)
        {
            return Funs.DB.Resources_ResourcesData.FirstOrDefault(x => x.ResourcesDataId == resourcesDataId);
        }

        /// <summary>
        /// 增加参考资料
        /// </summary>
        /// <param name="resourcesData"></param>
        public static void AddResourcesData(Model.Resources_ResourcesData resourcesData)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Resources_ResourcesData newResourcesData = new Model.Resources_ResourcesData
            {
                ResourcesDataId = resourcesData.ResourcesDataId,
                FileName = resourcesData.FileName,
                FileCode = resourcesData.FileCode,
                Remark = resourcesData.Remark,
                FileContent = resourcesData.FileContent,
                FileUrl = resourcesData.FileUrl
            };
            db.Resources_ResourcesData.InsertOnSubmit(newResourcesData);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改参考资料信息
        /// </summary>
        /// <param name="ResourcesDataId"></param>
        /// <param name="SignName"></param>
        /// <param name="def"></param>
        public static void UpdateResourcesData(Model.Resources_ResourcesData resourcesData)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Resources_ResourcesData updateResourcesData = db.Resources_ResourcesData.FirstOrDefault(e => e.ResourcesDataId == resourcesData.ResourcesDataId);
            if (updateResourcesData != null)
            {
                updateResourcesData.FileName = resourcesData.FileName;
                updateResourcesData.FileCode = resourcesData.FileCode;
                updateResourcesData.Remark = resourcesData.Remark;
                updateResourcesData.FileContent = resourcesData.FileContent;
                updateResourcesData.FileUrl = resourcesData.FileUrl;
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 删除参考资料
        /// </summary>
        /// <param name="resourcesDataId"></param>
        public static void DeleteResourcesData(string resourcesDataId)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Resources_ResourcesData deleteResourcesData = db.Resources_ResourcesData.FirstOrDefault(e => e.ResourcesDataId == resourcesDataId);
            if (deleteResourcesData != null)
            {

                if (!string.IsNullOrEmpty(deleteResourcesData.FileUrl))
                {
                    BLL.UploadFileService.DeleteFile(Funs.RootPath, deleteResourcesData.FileUrl);
                }
                ////删除附件表
                BLL.CommonService.DeleteAttachFileById(deleteResourcesData.ResourcesDataId);
                db.Resources_ResourcesData.DeleteOnSubmit(deleteResourcesData);
                db.SubmitChanges();
            }
        }
    }
}
