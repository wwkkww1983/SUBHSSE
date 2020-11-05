namespace BLL
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Linq;

    /// <summary>
    /// ������־
    /// </summary>
    public static class ErrLogInfo
    {
        /// <summary>
        /// ������־Ĭ��·��
        /// </summary>
        public static string DefaultErrLogFullPath
        {
            get;
            set;
        }

        #region д�������־
        /// <summary>
        /// ��Ĭ�ϴ�����־·��д�������־
        /// </summary>
        /// <param name="ex">�쳣</param>
        /// <param name="appendErrLog">�쳣��׷�ӵ�˵����Ϣ</param>
        /// <returns>����д��Ĵ�����Ϣ</returns>
        public static string WriteLog(string ErrId, Exception ex, params string[] appendErrLog)
        {
            using (Model.SUBHSSEDB db = new Model.SUBHSSEDB(Funs.ConnString))
            {
                StringBuilder errLog = new StringBuilder();
                if (ex != null)
                {
                    Exception innerEx = ex.InnerException;
                    errLog.Append("\r\n");
                    errLog.Append("������Ϣ��ʼ=====>\r\n");
                    errLog.Append(String.Format(CultureInfo.InvariantCulture, "��������:{0}\r\n", ex.GetType().Name));
                    errLog.Append(String.Format(CultureInfo.InvariantCulture, "������Ϣ:{0}\r\n", ex.Message));
                    errLog.Append("�����ջ:\r\n");
                    errLog.Append(String.Format(CultureInfo.InvariantCulture, "{0}\r\n", ex.StackTrace));
                    Model.Sys_ErrLogInfo newErr = new Model.Sys_ErrLogInfo
                    {
                        ErrType = ex.GetType().Name,
                        ErrMessage = ex.Message,
                        ErrStackTrace = ex.StackTrace
                    };

                    int level = 1;
                    while (innerEx != null)
                    {
                        string preString = new string('-', level * 4);
                        errLog.Append(preString);
                        if (innerEx.GetType() != null)
                        {
                            errLog.Append(String.Format(CultureInfo.InvariantCulture, "��������:{0}\r\n", innerEx.GetType().Name));
                        }
                        errLog.Append(preString);
                        errLog.Append("������Ϣ:\r\n");
                        errLog.Append(preString);
                        errLog.Append(innerEx.Message + "\r\n");
                        errLog.Append(preString);
                        errLog.Append("�����ջ:\r\n");
                        errLog.Append(new string(' ', level * 4));
                        if (innerEx.StackTrace != null)
                        {
                            errLog.Append(String.Format(CultureInfo.InvariantCulture, "{0}\r\n", innerEx.StackTrace.Replace("\r\n", "\r\n" + new string(' ', level * 4))));
                        }
                        newErr.ErrType += innerEx.GetType().Name;
                        newErr.ErrMessage += innerEx.Message;
                        newErr.ErrStackTrace += innerEx.StackTrace;

                        innerEx = innerEx.InnerException;
                        level++;
                    }

                    errLog.Append(String.Format(CultureInfo.InvariantCulture, "����ʱ��:{0}\r\n", DateTime.Now));
                    newErr.ErrTime = DateTime.Now;

                    var errlogInfo =db.Sys_ErrLogInfo.FirstOrDefault(x => x.ErrLogId == ErrId);
                    if (errlogInfo != null)
                    {
                        errlogInfo.ErrType = newErr.ErrType;
                        errlogInfo.ErrMessage = newErr.ErrMessage;
                        errlogInfo.ErrStackTrace = newErr.ErrStackTrace;
                        errlogInfo.ErrTime = newErr.ErrTime;
                        Funs.DB.SubmitChanges();
                    }
                    else
                    {
                        newErr.ErrLogId = SQLHelper.GetNewID(typeof(Model.Sys_ErrLogInfo));
                        db.Sys_ErrLogInfo.InsertOnSubmit(newErr);
                        db.SubmitChanges();
                    }
                }

                if (appendErrLog != null)
                {
                    foreach (string log in appendErrLog)
                    {
                        if (log != null)
                        {
                            errLog.Append(log);
                            errLog.Append("\r\n");
                        }
                    }

                    errLog.Append(String.Format(CultureInfo.InvariantCulture, "����ʱ��:{0}\r\n", DateTime.Now));
                }

                return WriteLog(errLog.ToString());
            }
        }

        /// <summary>
        /// ��Ĭ�ϴ�����־·��д�������־
        /// </summary>
        /// <param name="errLog">������־</param>
        /// <returns>����д��Ĵ�����Ϣ</returns>
        public static string WriteLog(string errLog)
        {
            return WriteLog(DefaultErrLogFullPath, errLog);
        }

        /// <summary>
        /// д�������־
        /// </summary>
        /// <param name="writeFullPath">��־д������·��</param>
        /// <param name="errLog">������־</param>
        /// <returns>����д��Ĵ�����Ϣ</returns>
        public static string WriteLog(string writeFullPath, string errLog)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(writeFullPath, true);                
                sw.WriteLine(errLog);
                sw.Close();
                return errLog;
            }
            catch
            {
                try
                {
                    if (sw != null)
                    {
                        sw.Close();
                    }
                }
                catch
                {
                }
            }

            return String.Empty;
        }
        #endregion
    }
}