using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Web;

namespace BLL
{
    public static class FileInsertService
    {
        /// <summary>
        /// 获取附件数据流类
        /// </summary>
        /// <param name="attachUrl">附件路径</param>
        /// <returns></returns>
        public static void FileInsert(List<byte[]> fileContextList, string attachUrl)
        {
            if (fileContextList != null && fileContextList.Count > 0)
            {
                string physicalpath = Funs.RootPath;
                //HttpContext.Current.Request.PhysicalApplicationPath;    
                string fullPath = physicalpath + attachUrl;
                if (!File.Exists(fullPath))
                {
                    byte[] fileContext = fileContextList[0];
                    int index = fullPath.LastIndexOf("\\");
                    string filePath = fullPath.Substring(0, index);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    //string savePath = fullPath + fileName;

                    //文件读写模式
                    System.IO.FileMode fileMode = System.IO.FileMode.Create;
                    //写入文件
                    using (System.IO.FileStream fs = new System.IO.FileStream(fullPath, fileMode, System.IO.FileAccess.Write))
                    {
                        fs.Write(fileContext, 0, fileContext.Length);
                    }
                }
            }
        }

        /// <summary>
        /// 获取多附件数据流类
        /// </summary>
        /// <param name="attachUrl">附件路径</param>
        /// <returns></returns>
        public static void FileMoreInsert(List<byte[]> fileContextList, string attachUrl)
        {
            if (fileContextList != null && fileContextList.Count() > 0)
            {
                if (fileContextList.Count > 0)
                {
                    string[] strs = attachUrl.Trim().Split(',');
                    int i = 0;
                    foreach (var item in fileContextList)
                    {
                        if (strs.Count() > i)
                        {
                            string physicalpath = Funs.RootPath;
                            //HttpContext.Current.Request.PhysicalApplicationPath;    
                            string fullPath = physicalpath + strs[i];
                            if (!File.Exists(fullPath))
                            {
                                byte[] fileContext = item;
                                int index = fullPath.LastIndexOf("\\");
                                string filePath = fullPath.Substring(0, index);

                                if (!Directory.Exists(filePath))
                                {
                                    Directory.CreateDirectory(filePath);
                                }
                                //string savePath = fullPath + fileName;

                                //文件读写模式
                                System.IO.FileMode fileMode = System.IO.FileMode.Create;

                                //写入文件
                                using (System.IO.FileStream fs = new System.IO.FileStream(fullPath, fileMode, System.IO.FileAccess.Write))
                                {
                                    fs.Write(fileContext, 0, fileContext.Length);
                                }
                            }

                            i++;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 数据和附件插入到多附件表
        /// </summary>
        public static void InsertAttachFile(string attachFileId, string dataId, string attachSource, string attachUrl, List<byte[]> fileContext)
        {
            //多附件
            var attachFile = Funs.DB.AttachFile.FirstOrDefault(x => x.ToKeyId == dataId);
            if (attachFile == null && !string.IsNullOrEmpty(attachSource))
            {
                Model.AttachFile newAttachFile = new Model.AttachFile
                {
                    AttachFileId = attachFileId,
                    ToKeyId = dataId,
                    AttachSource = attachSource,
                    AttachUrl = attachUrl
                };
                Funs.DB.AttachFile.InsertOnSubmit(newAttachFile);
                Funs.DB.SubmitChanges();

                ////插入附件文件
                BLL.FileInsertService.FileMoreInsert(fileContext, attachUrl);
            }
            else
            {
                if (attachFile.AttachUrl != attachUrl)
                {
                    ///删除附件文件
                    BLL.UploadAttachmentService.DeleteFile(Funs.RootPath, attachFile.AttachUrl);
                    ////插入附件文件
                    BLL.FileInsertService.FileMoreInsert(fileContext, attachUrl);
                    attachFile.AttachSource = attachSource;
                    attachFile.AttachUrl = attachUrl;
                    Funs.DB.SubmitChanges();
                }
            }
        }
    }
}
