namespace JTTServer.Log
{
    /// <summary>
    /// 日志组件类型
    /// </summary>
    public enum LoggerType
    {
        /// <summary>
        /// 输出到控制台
        /// </summary>
        Console = 0,
        /// <summary>
        /// 使用txt文件记录日志,默认存放目录为/A_Logs/yyy-MM/yyyy-MM-dd.txt
        /// </summary>
        File,
        /// <summary>
        /// 使用关系型数据库记录日志,例如SQlServer、MySQL、Oracle等
        /// </summary>
        RDBMS,
        /// <summary>
        /// 使用ElasticSearch记录日志
        /// </summary>
        ElasticSearch
    }
}
