using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class HSSELogService
    {
        /// <summary>
        /// 根据主键获取HSSE日志暨管理数据收集
        /// </summary>
        /// <param name="HSSELogId">HSSE日志暨管理数据收集主键</param>
        /// <returns></returns>
        public static Model.Manager_HSSELog GetHSSELogByHSSELogId(string hsseLogId)
        {
            return Funs.DB.Manager_HSSELog.FirstOrDefault(x => x.HSSELogId == hsseLogId);
        }

        /// <summary>
        /// 根据编制人日期项目获取HSSE日志暨管理数据收集
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="compileDate">编制日期</param>
        /// <param name="compileMan">编制人</param>
        /// <returns></returns>
        public static Model.Manager_HSSELog GetHSSELogByCompileManDateProjectId(string projectId, DateTime compileDate, string compileMan)
        {
            return Funs.DB.Manager_HSSELog.FirstOrDefault(x => x.ProjectId == projectId && (x.CompileDate.Value.Year == compileDate.Year && x.CompileDate.Value.Month == compileDate.Month && x.CompileDate.Value.Date == compileDate.Date) && x.CompileMan == compileMan);
        }

        /// <summary>
        /// 根据编制人和日期范围获取HSSE日志暨管理数据收集
        /// </summary>
        /// <param name="HSSELogId">HSSE日志暨管理数据收集主键</param>
        /// <returns></returns>
        public static List<Model.Manager_HSSELog> GetHSSELogListByCompileManDatesProjectId(string projectId, DateTime startTime, DateTime endTime, string compileMan)
        {
            return (from x in Funs.DB.Manager_HSSELog where x.ProjectId == projectId && x.CompileDate >= startTime && x.CompileDate <= endTime && x.CompileMan == compileMan select x).ToList();
        }

        /// <summary>
        /// 增加HSSE日志暨管理数据收集
        /// </summary>
        /// <param name="HSSELog">HSSE日志暨管理数据收集实体</param>
        public static void AddHSSELog(Model.Manager_HSSELog HSSELog)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_HSSELog newHSSELog = new Model.Manager_HSSELog
            {
                HSSELogId = HSSELog.HSSELogId,
                ProjectId = HSSELog.ProjectId,
                CompileDate = HSSELog.CompileDate,
                CompileMan = HSSELog.CompileMan,
                Weather = HSSELog.Weather,
                IsVisible = HSSELog.IsVisible,
                Num11 = HSSELog.Num11,
                Contents12 = HSSELog.Contents12,
                Contents13 = HSSELog.Contents13,
                Contents21 = HSSELog.Contents21,
                Num21 = HSSELog.Num21,
                Contents22 = HSSELog.Contents22,
                Num22 = HSSELog.Num22,
                Contents23 = HSSELog.Contents23,
                Num23 = HSSELog.Num23,
                Contents24 = HSSELog.Contents24,
                Num24 = HSSELog.Num24,
                Contents25 = HSSELog.Contents25,
                Num25 = HSSELog.Num25,
                Contents26 = HSSELog.Contents26,
                Num26 = HSSELog.Num26,
                Contents27 = HSSELog.Contents27,
                Num27 = HSSELog.Num27,
                Contents28 = HSSELog.Contents28,
                Num28 = HSSELog.Num28,
                Contents29 = HSSELog.Contents29,
                Num29 = HSSELog.Num29,
                Contents210 = HSSELog.Contents210,
                Num210 = HSSELog.Num210,
                Num211 = HSSELog.Num211,
                Contents31 = HSSELog.Contents31,
                Num31 = HSSELog.Num31,
                Contents32 = HSSELog.Contents32,
                Num32 = HSSELog.Num32,
                Contents33 = HSSELog.Contents33,
                Num33 = HSSELog.Num33,
                Contents34 = HSSELog.Contents34,
                Num34 = HSSELog.Num34,
                Contents41 = HSSELog.Contents41,
                Contents42 = HSSELog.Contents42,
                Contents43 = HSSELog.Contents43,
                Contents51 = HSSELog.Contents51,
                Contents52 = HSSELog.Contents52
            };
            db.Manager_HSSELog.InsertOnSubmit(newHSSELog);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改HSSE日志暨管理数据收集
        /// </summary>
        /// <param name="updateHSSELog"></param>
        public static void UpdateHSSELog(Model.Manager_HSSELog updateHSSELog)
        {
            Model.SUBHSSEDB db = Funs.DB;
            Model.Manager_HSSELog newHSSELog = db.Manager_HSSELog.FirstOrDefault(e => e.HSSELogId == updateHSSELog.HSSELogId);
            if (newHSSELog != null)
            {
                newHSSELog.CompileDate = updateHSSELog.CompileDate;
                newHSSELog.CompileMan = updateHSSELog.CompileMan;
                newHSSELog.Weather = updateHSSELog.Weather;
                newHSSELog.IsVisible = updateHSSELog.IsVisible;
                newHSSELog.Num11 = updateHSSELog.Num11;
                newHSSELog.Contents12 = updateHSSELog.Contents12;
                newHSSELog.Contents13 = updateHSSELog.Contents13;
                newHSSELog.Contents21 = updateHSSELog.Contents21;
                newHSSELog.Num21 = updateHSSELog.Num21;
                newHSSELog.Contents22 = updateHSSELog.Contents22;
                newHSSELog.Num22 = updateHSSELog.Num22;
                newHSSELog.Contents23 = updateHSSELog.Contents23;
                newHSSELog.Num23 = updateHSSELog.Num23;
                newHSSELog.Contents24 = updateHSSELog.Contents24;
                newHSSELog.Num24 = updateHSSELog.Num24;
                newHSSELog.Contents25 = updateHSSELog.Contents25;
                newHSSELog.Num25 = updateHSSELog.Num25;
                newHSSELog.Contents26 = updateHSSELog.Contents26;
                newHSSELog.Num26 = updateHSSELog.Num26;
                newHSSELog.Contents27 = updateHSSELog.Contents27;
                newHSSELog.Num27 = updateHSSELog.Num27;
                newHSSELog.Contents28 = updateHSSELog.Contents28;
                newHSSELog.Num28 = updateHSSELog.Num28;
                newHSSELog.Contents29 = updateHSSELog.Contents29;
                newHSSELog.Num29 = updateHSSELog.Num29;
                newHSSELog.Contents210 = updateHSSELog.Contents210;
                newHSSELog.Num210 = updateHSSELog.Num210;
                newHSSELog.Num211 = updateHSSELog.Num211;
                newHSSELog.Contents31 = updateHSSELog.Contents31;
                newHSSELog.Num31 = updateHSSELog.Num31;
                newHSSELog.Contents32 = updateHSSELog.Contents32;
                newHSSELog.Num32 = updateHSSELog.Num32;
                newHSSELog.Contents33 = updateHSSELog.Contents33;
                newHSSELog.Num33 = updateHSSELog.Num33;
                newHSSELog.Contents34 = updateHSSELog.Contents34;
                newHSSELog.Num34 = updateHSSELog.Num34;
                newHSSELog.Contents41 = updateHSSELog.Contents41;
                newHSSELog.Contents42 = updateHSSELog.Contents42;
                newHSSELog.Contents43 = updateHSSELog.Contents43;
                newHSSELog.Contents51 = updateHSSELog.Contents51;
                newHSSELog.Contents52 = updateHSSELog.Contents52;
                db.SubmitChanges();
            }
        }

        #region 从页面收集工程师日志
        /// <summary>
        /// 从页面收集工程师日志
        /// </summary>
        /// <param name="projectId">项目id</param>
        /// <param name="compileMan">工程师id</param>
        /// <param name="compileDate">日期</param>
        /// <param name="type">日志类型</param>
        /// <param name="value">日志的值</param>
        /// <param name="operateName">操作名称</param>
        public static void CollectHSSELog(string projectId, string compileMan, DateTime? compileDate, string type, string value, string operateName, int num)
        {
            Model.SUBHSSEDB db = Funs.DB;
            if (compileDate.HasValue)  ///操作日期
            {
                var puser = BLL.ProjectUserService.GetProjectUserByUserIdProjectId(projectId, compileMan);  ///操作人是安全工程师或者安全经理
                var thisUnit = BLL.CommonService.GetIsThisUnit();
                if (puser != null && thisUnit != null && thisUnit.UnitId == puser.UnitId && (puser.RoleId == BLL.Const.HSSEManager || puser.RoleId == BLL.Const.HSSEEngineer))
                {
                    if (operateName == Const.BtnAdd)     ////添加日志
                    {
                        Model.Manager_HSSELog newHSSELog = new Model.Manager_HSSELog
                        {
                            ProjectId = projectId,
                            CompileDate = compileDate,
                            CompileMan = compileMan,
                            IsVisible = true
                        };
                        var getHsseLog = GetHSSELogByCompileManDateProjectId(projectId, compileDate.Value, compileMan);
                        if (getHsseLog == null)
                        {
                            newHSSELog.HSSELogId = SQLHelper.GetNewID(typeof(Model.Manager_HSSELog));
                            AddHSSELog(newHSSELog);
                        }
                        else
                        {
                            newHSSELog = GetHSSELogByHSSELogId(getHsseLog.HSSELogId);
                            newHSSELog.IsVisible = true;
                        }

                        switch (type)
                        {
                            case "11":
                                if (newHSSELog.Num11.HasValue)
                                {
                                    newHSSELog.Num11 += num;
                                }
                                else
                                {
                                    newHSSELog.Num11 = num;
                                }
                                break;
                            case "12":
                                if (!string.IsNullOrEmpty(newHSSELog.Contents12))
                                {
                                    newHSSELog.Contents12 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents12 = value;
                                }
                                break;
                            case "13":
                                if (!string.IsNullOrEmpty(newHSSELog.Contents13))
                                {
                                    newHSSELog.Contents13 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents13 = value;
                                }
                                break;
                            case "21":
                                if (newHSSELog.Num21.HasValue)
                                {
                                    newHSSELog.Num21 += num;
                                }
                                else
                                {
                                    newHSSELog.Num21 = num;
                                }
                                if (!string.IsNullOrEmpty(newHSSELog.Contents21))
                                {
                                    newHSSELog.Contents21 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents21 = value;
                                }
                                break;
                            case "22":
                                if (newHSSELog.Num22.HasValue)
                                {
                                    newHSSELog.Num22 += num;
                                }
                                else
                                {
                                    newHSSELog.Num22 = num;
                                }
                                if (!string.IsNullOrEmpty(newHSSELog.Contents22))
                                {
                                    newHSSELog.Contents22 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents22 = value;
                                }
                                break;
                            case "23":
                                if (newHSSELog.Num23.HasValue)
                                {
                                    newHSSELog.Num23 += num;
                                }
                                else
                                {
                                    newHSSELog.Num23 = num;
                                }
                                if (!string.IsNullOrEmpty(newHSSELog.Contents23))
                                {
                                    newHSSELog.Contents23 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents23 = value;
                                }
                                break;
                            case "24":
                                if (newHSSELog.Num24.HasValue)
                                {
                                    newHSSELog.Num24 += num;
                                }
                                else
                                {
                                    newHSSELog.Num24 = num;
                                }
                                if (!string.IsNullOrEmpty(newHSSELog.Contents24))
                                {
                                    newHSSELog.Contents24 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents24 = value;
                                }
                                break;
                            case "25":
                                if (newHSSELog.Num25.HasValue)
                                {
                                    newHSSELog.Num25 += num;
                                }
                                else
                                {
                                    newHSSELog.Num25 = num;
                                }
                                if (!string.IsNullOrEmpty(newHSSELog.Contents25))
                                {
                                    newHSSELog.Contents25 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents25 = value;
                                }
                                break;
                            case "26":
                                if (newHSSELog.Num26.HasValue)
                                {
                                    newHSSELog.Num26 += num;
                                }
                                else
                                {
                                    newHSSELog.Num26 = num;
                                }
                                if (!string.IsNullOrEmpty(newHSSELog.Contents26))
                                {
                                    newHSSELog.Contents26 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents26 = value;
                                }
                                break;
                            case "27":
                                if (newHSSELog.Num27.HasValue)
                                {
                                    newHSSELog.Num27 += num;
                                }
                                else
                                {
                                    newHSSELog.Num27 = num;
                                }
                                if (!string.IsNullOrEmpty(newHSSELog.Contents27))
                                {
                                    newHSSELog.Contents27 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents27 = value;
                                }
                                break;
                            case "28":
                                if (newHSSELog.Num28.HasValue)
                                {
                                    newHSSELog.Num28 += num;
                                }
                                else
                                {
                                    newHSSELog.Num28 = num;
                                }
                                if (!string.IsNullOrEmpty(newHSSELog.Contents28))
                                {
                                    newHSSELog.Contents28 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents28 = value;
                                }
                                break;
                            case "29":
                                if (newHSSELog.Num29.HasValue)
                                {
                                    newHSSELog.Num29 += num;
                                }
                                else
                                {
                                    newHSSELog.Num29 = num;
                                }
                                if (!string.IsNullOrEmpty(newHSSELog.Contents29))
                                {
                                    newHSSELog.Contents29 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents29 = value;
                                }
                                break;
                            case "210":
                                if (newHSSELog.Num210.HasValue)
                                {
                                    newHSSELog.Num210 += num;
                                }
                                else
                                {
                                    newHSSELog.Num210 = num;
                                }
                                if (!string.IsNullOrEmpty(newHSSELog.Contents210))
                                {
                                    newHSSELog.Contents210 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents210 = value;
                                }
                                break;
                            case "211":
                                if (newHSSELog.Num211.HasValue)
                                {
                                    newHSSELog.Num211 += num;
                                }
                                else
                                {
                                    newHSSELog.Num211 = num;
                                }
                                break;
                            case "31":
                                if (newHSSELog.Num31.HasValue)
                                {
                                    newHSSELog.Num31 += num;
                                }
                                else
                                {
                                    newHSSELog.Num31 = num;
                                }
                                if (!string.IsNullOrEmpty(newHSSELog.Contents31))
                                {
                                    newHSSELog.Contents31 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents31 = value;
                                }
                                break;
                            case "32":
                                if (newHSSELog.Num32.HasValue)
                                {
                                    newHSSELog.Num32 += num;
                                }
                                else
                                {
                                    newHSSELog.Num32 = num;
                                }
                                if (!string.IsNullOrEmpty(newHSSELog.Contents32))
                                {
                                    newHSSELog.Contents32 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents32 = value;
                                }
                                break;
                            case "33":
                                if (newHSSELog.Num33.HasValue)
                                {
                                    newHSSELog.Num33 += num;
                                }
                                else
                                {
                                    newHSSELog.Num33 = num;
                                }
                                if (!string.IsNullOrEmpty(newHSSELog.Contents33))
                                {
                                    newHSSELog.Contents33 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents33 = value;
                                }
                                break;
                            case "34":
                                if (newHSSELog.Num34.HasValue)
                                {
                                    newHSSELog.Num34 += num;
                                }
                                else
                                {
                                    newHSSELog.Num34 = num;
                                }
                                if (!string.IsNullOrEmpty(newHSSELog.Contents34))
                                {
                                    newHSSELog.Contents34 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents34 = value;
                                }
                                break;
                            case "41":
                                if (!string.IsNullOrEmpty(newHSSELog.Contents41))
                                {
                                    newHSSELog.Contents41 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents41 = value;
                                }
                                break;
                            case "42":
                                if (!string.IsNullOrEmpty(newHSSELog.Contents42))
                                {
                                    newHSSELog.Contents42 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents42 = value;
                                }
                                break;
                            case "43":
                                if (!string.IsNullOrEmpty(newHSSELog.Contents43))
                                {
                                    newHSSELog.Contents43 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents43 = value;
                                }
                                break;
                            case "51":
                                if (!string.IsNullOrEmpty(newHSSELog.Contents51))
                                {
                                    newHSSELog.Contents51 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents51 = value;
                                }
                                break;
                            case "52":
                                if (!string.IsNullOrEmpty(newHSSELog.Contents52))
                                {
                                    newHSSELog.Contents52 += ("," + value);
                                }
                                else
                                {
                                    newHSSELog.Contents52 = value;
                                }
                                break;
                        }

                        UpdateHSSELog(newHSSELog);
                    }
                    else     ////删除日志
                    {
                        var getHsseLog = GetHSSELogByCompileManDateProjectId(projectId, compileDate.Value, compileMan);
                        if (getHsseLog != null)
                        {
                            switch (type)
                            {
                                case "11":
                                    if (getHsseLog.Num11.HasValue && getHsseLog.Num11 >= 1)
                                    {
                                        getHsseLog.Num11 -= num;
                                    }
                                    break;
                                case "12":
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents12))
                                    {
                                        if (getHsseLog.Contents12.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents12 = getHsseLog.Contents12.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents12.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents12 = getHsseLog.Contents12.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents12.Contains((value)))
                                        {
                                            getHsseLog.Contents12 = getHsseLog.Contents12.Replace((value), "");
                                        }
                                    }

                                    break;
                                case "13":
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents13))
                                    {
                                        if (getHsseLog.Contents13.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents13 = getHsseLog.Contents13.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents13.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents13 = getHsseLog.Contents13.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents13.Contains((value)))
                                        {
                                            getHsseLog.Contents13 = getHsseLog.Contents13.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "21":
                                    if (getHsseLog.Num21.HasValue && getHsseLog.Num21 >= 1)
                                    {
                                        getHsseLog.Num21 -= num;
                                    }
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents21))
                                    {
                                        if (getHsseLog.Contents21.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents21 = getHsseLog.Contents21.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents21.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents21 = getHsseLog.Contents21.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents21.Contains((value)))
                                        {
                                            getHsseLog.Contents21 = getHsseLog.Contents21.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "22":
                                    if (getHsseLog.Num22.HasValue && getHsseLog.Num22 >= 1)
                                    {
                                        getHsseLog.Num22 -= num;
                                    }
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents22))
                                    {
                                        if (getHsseLog.Contents22.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents22 = getHsseLog.Contents22.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents22.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents22 = getHsseLog.Contents22.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents22.Contains((value)))
                                        {
                                            getHsseLog.Contents22 = getHsseLog.Contents22.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "23":
                                    if (getHsseLog.Num23.HasValue && getHsseLog.Num23 >= 1)
                                    {
                                        getHsseLog.Num23 -= num;
                                    }
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents23))
                                    {
                                        if (getHsseLog.Contents23.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents23 = getHsseLog.Contents23.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents23.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents23 = getHsseLog.Contents23.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents23.Contains((value)))
                                        {
                                            getHsseLog.Contents23 = getHsseLog.Contents23.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "24":
                                    if (getHsseLog.Num24.HasValue && getHsseLog.Num24 >= 1)
                                    {
                                        getHsseLog.Num24 -= num;
                                    }
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents24))
                                    {
                                        if (getHsseLog.Contents24.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents24 = getHsseLog.Contents24.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents24.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents24 = getHsseLog.Contents24.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents24.Contains((value)))
                                        {
                                            getHsseLog.Contents24 = getHsseLog.Contents24.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "25":
                                    if (getHsseLog.Num25.HasValue && getHsseLog.Num25 >= 1)
                                    {
                                        getHsseLog.Num25 -= num;
                                    }
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents25))
                                    {
                                        if (getHsseLog.Contents25.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents25 = getHsseLog.Contents25.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents25.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents25 = getHsseLog.Contents25.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents25.Contains((value)))
                                        {
                                            getHsseLog.Contents25 = getHsseLog.Contents25.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "26":
                                    if (getHsseLog.Num26.HasValue && getHsseLog.Num26 >= 1)
                                    {
                                        getHsseLog.Num26 -= num;
                                    }
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents26))
                                    {
                                        if (getHsseLog.Contents26.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents26 = getHsseLog.Contents26.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents26.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents26 = getHsseLog.Contents26.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents26.Contains((value)))
                                        {
                                            getHsseLog.Contents26 = getHsseLog.Contents26.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "27":
                                    if (getHsseLog.Num27.HasValue && getHsseLog.Num27 >= 1)
                                    {
                                        getHsseLog.Num27 -= num;
                                    }
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents27))
                                    {
                                        if (getHsseLog.Contents27.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents27 = getHsseLog.Contents27.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents27.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents27 = getHsseLog.Contents27.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents27.Contains((value)))
                                        {
                                            getHsseLog.Contents27 = getHsseLog.Contents27.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "28":
                                    if (getHsseLog.Num28.HasValue && getHsseLog.Num28 >= 1)
                                    {
                                        getHsseLog.Num28 -= num;
                                    }
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents28))
                                    {
                                        if (getHsseLog.Contents28.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents28 = getHsseLog.Contents28.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents28.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents28 = getHsseLog.Contents28.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents28.Contains((value)))
                                        {
                                            getHsseLog.Contents28 = getHsseLog.Contents28.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "29":
                                    if (getHsseLog.Num29.HasValue && getHsseLog.Num29 >= 1)
                                    {
                                        getHsseLog.Num29 -= num;
                                    }
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents29))
                                    {
                                        if (getHsseLog.Contents29.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents29 = getHsseLog.Contents29.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents29.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents29 = getHsseLog.Contents29.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents29.Contains((value)))
                                        {
                                            getHsseLog.Contents29 = getHsseLog.Contents29.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "210":
                                    if (getHsseLog.Num210.HasValue && getHsseLog.Num210 >= 1)
                                    {
                                        getHsseLog.Num210 -= num;
                                    }
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents210))
                                    {
                                        if (getHsseLog.Contents210.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents210 = getHsseLog.Contents210.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents210.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents210 = getHsseLog.Contents210.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents210.Contains((value)))
                                        {
                                            getHsseLog.Contents210 = getHsseLog.Contents210.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "211":
                                    if (getHsseLog.Num211.HasValue && getHsseLog.Num211 >= 1)
                                    {
                                        getHsseLog.Num211 -= num;
                                    }
                                    break;
                                case "31":
                                    if (getHsseLog.Num31.HasValue && getHsseLog.Num31 >= 1)
                                    {
                                        getHsseLog.Num31 -= num;
                                    }
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents31))
                                    {
                                        if (getHsseLog.Contents31.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents31 = getHsseLog.Contents31.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents31.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents31 = getHsseLog.Contents31.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents31.Contains((value)))
                                        {
                                            getHsseLog.Contents31 = getHsseLog.Contents31.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "32":
                                    if (getHsseLog.Num32.HasValue && getHsseLog.Num32 >= 1)
                                    {
                                        getHsseLog.Num32 -= num;
                                    }
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents32))
                                    {
                                        if (getHsseLog.Contents32.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents32 = getHsseLog.Contents32.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents32.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents32 = getHsseLog.Contents32.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents32.Contains((value)))
                                        {
                                            getHsseLog.Contents32 = getHsseLog.Contents32.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "33":
                                    if (getHsseLog.Num33.HasValue && getHsseLog.Num33 >= 1)
                                    {
                                        getHsseLog.Num33 -= num;
                                    }
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents33))
                                    {
                                        if (getHsseLog.Contents33.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents33 = getHsseLog.Contents33.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents33.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents33 = getHsseLog.Contents33.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents33.Contains((value)))
                                        {
                                            getHsseLog.Contents33 = getHsseLog.Contents33.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "34":
                                    if (getHsseLog.Num34.HasValue && getHsseLog.Num34 >= 1)
                                    {
                                        getHsseLog.Num34 -= num;
                                    }
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents34))
                                    {
                                        if (getHsseLog.Contents34.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents34 = getHsseLog.Contents34.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents34.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents34 = getHsseLog.Contents34.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents34.Contains((value)))
                                        {
                                            getHsseLog.Contents34 = getHsseLog.Contents34.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "41":
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents41))
                                    {
                                        if (getHsseLog.Contents41.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents41 = getHsseLog.Contents41.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents41.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents41 = getHsseLog.Contents41.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents41.Contains((value)))
                                        {
                                            getHsseLog.Contents41 = getHsseLog.Contents41.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "42":
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents42))
                                    {
                                        if (getHsseLog.Contents42.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents42 = getHsseLog.Contents42.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents42.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents42 = getHsseLog.Contents42.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents42.Contains((value)))
                                        {
                                            getHsseLog.Contents42 = getHsseLog.Contents42.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "43":
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents43))
                                    {
                                        if (getHsseLog.Contents43.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents43 = getHsseLog.Contents43.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents43.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents43 = getHsseLog.Contents43.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents43.Contains((value)))
                                        {
                                            getHsseLog.Contents43 = getHsseLog.Contents43.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "51":
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents51))
                                    {
                                        if (getHsseLog.Contents51.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents51 = getHsseLog.Contents51.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents51.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents51 = getHsseLog.Contents51.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents51.Contains((value)))
                                        {
                                            getHsseLog.Contents51 = getHsseLog.Contents51.Replace((value), "");
                                        }
                                    }
                                    break;
                                case "52":
                                    if (!string.IsNullOrEmpty(getHsseLog.Contents52))
                                    {
                                        if (getHsseLog.Contents52.Contains(("," + value)))
                                        {
                                            getHsseLog.Contents52 = getHsseLog.Contents52.Replace(("," + value), "");
                                        }
                                        if (getHsseLog.Contents52.Contains((value + ",")))
                                        {
                                            getHsseLog.Contents52 = getHsseLog.Contents52.Replace((value + ","), "");
                                        }
                                        if (getHsseLog.Contents52.Contains((value)))
                                        {
                                            getHsseLog.Contents52 = getHsseLog.Contents52.Replace((value), "");
                                        }
                                    }
                                    break;
                            }

                            UpdateHSSELog(getHsseLog);
                        }
                    }
                }
            }
        }
        #endregion
    }
}