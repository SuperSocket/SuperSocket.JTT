using System;
using System.Collections.Generic;
using System.Text;

namespace JTTServer.Config
{
    /// <summary>
    /// 日志操作类型
    /// </summary>
    public enum LoggerType
    {
        /// <summary>
        /// 输出到控制台
        /// </summary>
        Console = 1,
        /// <summary>
        /// 使用txt文件记录日志,默认存放目录为/A_Logs/yyy-MM/yyyy-MM-dd.txt
        /// </summary>
        File = 2
    }
}
