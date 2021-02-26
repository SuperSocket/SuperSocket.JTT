namespace JTTCustomServer.Logger
{
    /// <summary>
    /// 日志配置
    /// </summary>
    public class LoggerConfig
    {
        public static readonly string LoggerName = "SysLog";
        public static readonly string LogType = "LogType";
        public static readonly string Data = "Data";
        public static readonly string Layout = $@"${{date:format=yyyy-MM-dd HH\:mm\:ss.ffff}}|${{level}}|日志类型:${{event-properties:item={LogType}}}|内容:${{message}}|备份数据:${{event-properties:item={Data}}}";
        public static readonly string LayoutSimplify = $@"${{date:format=yyyy-MM-dd HH\:mm\:ss.ffff}}|${{level}}|${{message}}";
    }
}
