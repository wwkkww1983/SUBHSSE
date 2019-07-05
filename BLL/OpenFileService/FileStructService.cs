using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Web;

namespace BLL
{
    public static class FileStructService
    {
        /// <summary>
        /// 获取附件数据流类
        /// </summary>
        /// <param name="attachUrl">附件路径</param>
        /// <returns></returns>
        public static List<byte[]> GetFileStructByAttachUrl(string attachUrl)
        {
            List<byte[]> fileContext = new List<byte[]>();
            if (!String.IsNullOrEmpty(attachUrl))
            {
                string filePath = string.Empty;
                string physicalpath = Funs.RootPath;
                    //HttpContext.Current.Request.PhysicalApplicationPath;                
                filePath = physicalpath + attachUrl;               
                if (File.Exists(filePath))
                {
                    FileInfo fileInfo = new FileInfo(filePath);
                    Stream stream = fileInfo.OpenRead();
                    //读取指定大小的文件流内容到uploadFile.Context以便上传
                    int b;
                    while (stream.Position > -1 && stream.Position < stream.Length)
                    {
                        if (stream.Length - stream.Position >= 20000000)
                        {
                            b = 20000000;
                        }
                        else
                        {
                            b = (int)(stream.Length - stream.Position);
                        }

                        byte[] filebyte = new byte[b];
                        stream.Read(filebyte, 0, b);
                        fileContext.Add(filebyte);
                    }
                    stream.Close();
                }
            }

            return fileContext;
        }

        /// <summary>
        /// 获取附件数据流类 多附件的情况
        /// </summary>
        /// <param name="attachUrl">附件路径</param>
        /// <returns></returns>
        public static List<byte[]> GetMoreFileStructByAttachUrl(string attachUrl)
        {
            List<byte[]> fileContext = new List<byte[]>();
            if (!String.IsNullOrEmpty(attachUrl))
            {
                string[] strs = attachUrl.Trim().Split(',');
                foreach (var item in strs)
                {
                    string filePath = string.Empty;
                    string physicalpath = Funs.RootPath;
                    //HttpContext.Current.Request.PhysicalApplicationPath;    
                    filePath = physicalpath + item;
                    if (File.Exists(filePath))
                    {
                        FileInfo fileInfo = new FileInfo(filePath);
                        if (fileInfo != null)
                        {
                            Stream stream = fileInfo.OpenRead();
                            if (stream != null)
                            {
                                //读取指定大小的文件流内容到uploadFile.Context以便上传
                                int b;
                                while (stream.Position > -1 && stream.Position < stream.Length)
                                {
                                    if (stream.Length - stream.Position >= 20000000)
                                    {
                                        b = 20000000;
                                    }
                                    else
                                    {
                                        b = (int)(stream.Length - stream.Position);
                                    }

                                    byte[] filebyte = new byte[b];
                                    stream.Read(filebyte, 0, b);
                                    fileContext.Add(filebyte);
                                }
                            }

                            stream.Close();
                        }
                    }
                }
            }
            return fileContext;
        }
    }
}
