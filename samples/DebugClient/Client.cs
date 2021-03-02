using DebugClient.Log;
using DebugClient.MessageBody;
using Microservice.Library.ConsoleTool;
using Microservice.Library.Container;
using Microservice.Library.Extension;
using SuperSocket.Client;
using SuperSocket.JTT.JTT809;
using SuperSocket.JTT.JTT809.MessageBody;
using SuperSocket.JTT.Base.Extension;
using SuperSocket.JTT.Base.Interface;
using SuperSocket.ProtoBase;
using System;
using System.Buffers;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Extension = Microservice.Library.ConsoleTool.Extension;

namespace DebugClient
{
    /// <summary>
    /// 处理类
    /// </summary>
    public class Client
    {
        public Client()
        {
            Config = AutofacHelper.GetService<SystemConfig>();
            Protocol = JTTProtocolExtension.GetProtocol<JTT809Protocol>(Config.JTTConfigFilePath, Config.JTTVersion);
            Protocol.Initialization();
            Protocol.DefaultVersionFlag = Config.JTTVersionFlag;
            Protocol.Structures = Config.Structures;
            Protocol.InternalEntitysMappings = Config.InternalEntitysMappings;
            Protocol.DataMappings = Config.DataMappings;
            Filter = Protocol.GetFilter();
        }

        /// <summary>
        /// 系统配置
        /// </summary>
        readonly SystemConfig Config;

        /// <summary>
        /// JTT809协议
        /// </summary>
        readonly JTT809Protocol Protocol;

        /// <summary>
        /// 流数据过滤器
        /// </summary>
        readonly IPipelineFilter<IJTTPackageInfo> Filter;

        /// <summary>
        /// 客户端
        /// </summary>
        IEasyClient<IJTTPackageInfo> EasyClient { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public async Task ConnectAsync()
        {
            var client = new EasyClient<IJTTPackageInfo>(Filter);
            client.PackageHandler += PackageHandler;
            client.Closed += Closed;
            EasyClient = client.AsClient();

            try
            {
                var connected = await EasyClient.ConnectAsync(
                     new IPEndPoint(
                         IPAddress.Parse(Config.ServerHost),
                         Config.ServerPort),
                     CancellationToken.None);

                if (connected)
                {
                    Logger.Log(
                        NLog.LogLevel.Trace,
                        LogType.系统跟踪,
                        $"已连接服务器, " +
                        $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.");

                    StartReceive();
                }
                else
                    Logger.Log(
                        NLog.LogLevel.Trace,
                        LogType.系统跟踪,
                        $"连接服务器失败, " +
                        $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.");
            }
            catch (Exception ex)
            {
                Logger.Log(
                    NLog.LogLevel.Error,
                    LogType.系统异常,
                    $"连接服务器时发生错误, " +
                    $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.",
                    null,
                    ex);
            }
        }

        /// <summary>
        /// 开始接收消息
        /// </summary>
        public void StartReceive()
        {
            Logger.Log(
                NLog.LogLevel.Trace,
                LogType.系统跟踪,
                $"开始接收服务器消息, " +
                $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.");

            EasyClient.StartReceive();
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        /// <returns></returns>
        public async Task CloseAsync()
        {
            await EasyClient.CloseAsync();
        }

        /// <summary>
        /// 连接关闭
        /// </summary>
        /// <returns></returns>
#pragma warning disable CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
        void Closed(object? sender, EventArgs e)
#pragma warning restore CS8632 // 只能在 "#nullable" 注释上下文内的代码中使用可为 null 的引用类型的注释。
        {
            Logger.Log(
                NLog.LogLevel.Trace,
                LogType.系统跟踪,
                $"连接已关闭, " +
                $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.");
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="messageBody">消息体</param>
        /// <returns></returns>
        async Task SendAsync(IJTTMessageBody messageBody)
        {
            await SendAsync(new JTT809PackageInfo
            {
                JTT809MessageHeader = Protocol.JTT809Handler.GetMessageHeader(Config.GnsscenterID, Config.JTTVersionFlag),
                MessageBody = messageBody
            });
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="packageInfo">消息包</param>
        /// <returns></returns>
        async Task SendAsync(JTT809PackageInfo packageInfo)
        {
            await EasyClient.SendAsync(
                Protocol.Encoder,
                packageInfo);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public async Task Login()
        {
            var login = new LoginRequestBody
            {
                UserID = Convert.ToUInt32(Extension.ReadInput("请输入用户Id: ", true, "001")),
                Password = Extension.ReadPassword("请输入密码: "),
                Msg_GnsscenterID = Convert.ToUInt32(Extension.ReadInput("请输入下级平台接入码: ", true, Config.GnsscenterID.ToString())),
                Down_link_IP = Extension.ReadInput("请输入下级平台提供对应的从链路服务端IP地址: ", true, "127.0.0.1"),
                Down_link_Port = Convert.ToUInt16(Extension.ReadInput("请输入下级平台提供对应的从链路服务端口号: ", true, "4040"))
            };

            try
            {
                Logger.Log(
                    NLog.LogLevel.Trace,
                    LogType.系统跟踪,
                    $"发送登录信息, " +
                    $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.");

                await SendAsync(login);
            }
            catch (Exception ex)
            {
                Logger.Log(
                    NLog.LogLevel.Error,
                    LogType.系统异常,
                    $"发送登录信息时异常, " +
                    $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.",
                    null,
                    ex);
            }
        }

        /// <summary>
        /// 获取转发终端清单
        /// </summary>
        /// <returns></returns>
        public async Task GetForward()
        {
            var getForward = new GetForwardEndpointRequestBody
            {

            };

            try
            {
                Logger.Log(
                    NLog.LogLevel.Trace,
                    LogType.系统跟踪,
                    $"发送获取转发终端清单请求消息, " +
                    $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.");

                await SendAsync(getForward);
            }
            catch (Exception ex)
            {
                Logger.Log(
                    NLog.LogLevel.Error,
                    LogType.系统异常,
                    $"发送获取转发终端清单请求消息时异常, " +
                    $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.",
                    null,
                    ex);
            }
        }

        /// <summary>
        /// 开始转发
        /// </summary>
        /// <returns></returns>
        public async Task Forward()
        {
            var forward = new ForwardRequestBody
            {
                Target_IP = Extension.ReadInput("请输入目标IP: "),
                Target_Port = Convert.ToUInt16(Extension.ReadInput("请输入目标端口: "))
            };

            try
            {
                Logger.Log(
                    NLog.LogLevel.Trace,
                    LogType.系统跟踪,
                    $"发送开始转发信息, " +
                    $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.");

                await SendAsync(forward);
            }
            catch (Exception ex)
            {
                Logger.Log(
                    NLog.LogLevel.Error,
                    LogType.系统异常,
                    $"发送开始转发信息时异常, " +
                    $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.",
                    null,
                    ex);
            }
        }

        /// <summary>
        /// 测试转发
        /// </summary>
        /// <returns></returns>
        public async Task TestForward()
        {
            var testForward = new TestForwardRequestBody
            {

            };

            try
            {
                Logger.Log(
                    NLog.LogLevel.Trace,
                    LogType.系统跟踪,
                    $"发送测试转发请求消息, " +
                    $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.");

                await SendAsync(testForward);
            }
            catch (Exception ex)
            {
                Logger.Log(
                    NLog.LogLevel.Error,
                    LogType.系统异常,
                    $"发送测试转发请求消息时异常, " +
                    $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.",
                    null,
                    ex);
            }
        }

        /// <summary>
        /// 取消转发
        /// </summary>
        /// <returns></returns>
        public async Task CancelForward()
        {
            var cancelForward = new CancelForwardRequestBody
            {

            };

            try
            {
                Logger.Log(
                    NLog.LogLevel.Trace,
                    LogType.系统跟踪,
                    $"发送取消转发信息, " +
                    $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.");

                await SendAsync(cancelForward);
            }
            catch (Exception ex)
            {
                Logger.Log(
                    NLog.LogLevel.Error,
                    LogType.系统异常,
                    $"发送取消转发信息时异常, " +
                    $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.",
                    null,
                    ex);
            }
        }

        /// <summary>
        /// 发送JTT1078消息
        /// </summary>
        /// <returns></returns>
        public async Task SendJTT1078Msg()
        {
            try
            {
                //await SendAsync(new UpRealVideoMsgBody
                //{
                //    DataType = 0x1801,
                //    VehicleNo = "666",
                //    VehicleColor = 2,
                //    DataLength = 35,
                //    SubBody = new RealVideoStartupReplyBody
                //    {
                //        Result = 0,
                //        ServerIP = "192.168.0.1",
                //        ServerPort = 4040
                //    }
                //});

                //return;

                var options = typeof(SuperSocket.JTT.JTT1078.Const.DataType).GetFields()
                .ToDictionary(k => k.Name, v => $"0x{v.GetValue(null):x2}");

                options.ForEach(o => o.Value.ConsoleWrite(ConsoleColor.White, o.Key, true, 1));

                string datatype = Extension.ReadInput("请选择业务类型: "),
                    subdatatype = null;

                var type = Assembly.Load(Protocol.InternalEntitysMappings[datatype].Assembly)
                    .GetType(Protocol.InternalEntitysMappings[datatype].TypeName);

                var obj = Activator.CreateInstance(type);

                Protocol.Structures
                    .Find(o => o.IsBody)
                    .Internal[datatype]
                    .ForEach(o =>
                    {
                        var property = obj.GetProperty(o.Property.Split('.'));

                        object value = null;

                        if (o.InternalKey == null)
                        {
                            string value_;
                            if (o.Property == "DataType")
                            {
                                var suboptions = typeof(SuperSocket.JTT.JTT1078.Const.SubDataType).GetFields()
                                    .ToDictionary(k => k.Name, v => $"0x{v.GetValue(null):x2}");

                                suboptions.ForEach(o => o.Value.ConsoleWrite(ConsoleColor.White, o.Key, true, 1));

                                subdatatype = Extension.ReadInput("请选择子业务类型: ");

                                value_ = subdatatype;
                                value = value_.ToObject(property.PropertyType);
                            }
                            else
                            {
                                value_ = Extension.ReadInput($"请输入{o.Explain}: ");

                                if (string.IsNullOrWhiteSpace(value_))
                                    return;

                                if (property.PropertyType == typeof(string))
                                    value = value_;
                                else
                                    value = value_.ToJson().ToObject(property.PropertyType);
                            }
                        }
                        else
                        {
                            var subtype = Assembly.Load(Protocol.InternalEntitysMappings[subdatatype].Assembly)
                                .GetType(Protocol.InternalEntitysMappings[subdatatype].TypeName);

                            value = Activator.CreateInstance(subtype);

                            o.Internal[subdatatype]
                            .ForEach(subo =>
                            {
                                var subproperty = value.GetProperty(subo.Property.Split('.'));

                                object subvalue = null;

                                var subvalue_ = Extension.ReadInput($"请输入{subo.Explain}: ");

                                if (string.IsNullOrWhiteSpace(subvalue_))
                                    return;

                                if (subproperty.PropertyType == typeof(string))
                                    subvalue = subvalue_;
                                else
                                    subvalue = subvalue_.ToObject(subproperty.PropertyType);

                                value.SetValueToProperty(subo.Property.Split('.'), subvalue);
                            });
                        }

                        obj.SetValueToProperty(o.Property.Split('.'), value);
                    });

                Logger.Log(
                  NLog.LogLevel.Trace,
                  LogType.系统跟踪,
                  $"发送JTT1078消息, " +
                  $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort} " +
                  $"\r\t\n业务类型: {datatype} " +
                  $"\r\t\n子业务类型: {subdatatype}.");

                await SendAsync((IJTTMessageBody)obj);
            }
            catch (Exception ex)
            {
                Logger.Log(
                    NLog.LogLevel.Error,
                    LogType.系统异常,
                    $"发送JTT1078消息时异常, " +
                    $"\r\t\nServer: {Config.ServerHost}:{Config.ServerPort}.",
                    null,
                    ex);
            }
        }

        /// <summary>
        /// 消息包处理
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        async ValueTask PackageHandler(EasyClient<IJTTPackageInfo> sender, IJTTPackageInfo packageInfo)
        {
            try
            {
                var jtt809PackageInfo = packageInfo as JTT809PackageInfo;

                switch (jtt809PackageInfo.JTT809MessageHeader.Msg_ID)
                {
                    case MsgID.LoginReply:
                        var body_LoginReply = jtt809PackageInfo.MessageBody as LoginReplyBody;
                        Logger.Log(
                            NLog.LogLevel.Trace,
                            LogType.系统跟踪,
                            $"接收[主链路登录应答信息], " +
                            $"\r\n\tServer: {Config.ServerHost}:{Config.ServerPort}, " +
                            $"\r\n\tResult: {body_LoginReply.Result}, " +
                            $"\r\n\tResult_Mapping: {body_LoginReply.Result_Mapping}, " +
                            $"\r\n\tVerify_Code: {body_LoginReply.Verify_Code}.");
                        break;
                    case MsgID.GetForwardEndpointReply:
                        var body_GetForwardEndpointReply = jtt809PackageInfo.MessageBody as GetForwardEndpointReplyBody;
                        Logger.Log(
                            NLog.LogLevel.Trace,
                            LogType.系统跟踪,
                            $"接收[获取转发终端清单请求应答消息], " +
                            $"\r\n\tServer: {Config.ServerHost}:{Config.ServerPort}, " +
                            $"\r\n\tTotal: {body_GetForwardEndpointReply.Total}, " +
                            $"\r\n\tEndpoints: \r\n\t\t{string.Join("\r\n\t\t", (body_GetForwardEndpointReply.Endpoints ?? new System.Collections.Generic.List<ForwardEndpoint>()).Select(o => $"{o.IP}:{o.Port}"))}.");
                        break;
                    case MsgID.ForwardReply:
                        var body_ForwardReply = jtt809PackageInfo.MessageBody as ForwardReplyBody;
                        Logger.Log(
                            NLog.LogLevel.Trace,
                            LogType.系统跟踪,
                            $"接收[开始转发应答信息], " +
                            $"\r\n\tServer: {Config.ServerHost}:{Config.ServerPort}, " +
                            $"\r\n\tResult: {body_ForwardReply.Result}, " +
                            $"\r\n\tResult_Mapping: {body_ForwardReply.Result_Mapping}.");
                        break;
                    case MsgID.CancelForwardReply:
                        var body_CancelForwardReply = jtt809PackageInfo.MessageBody as CancelForwardReplyBody;
                        Logger.Log(
                            NLog.LogLevel.Trace,
                            LogType.系统跟踪,
                            $"接收[取消转发应答信息], " +
                            $"\r\n\tServer: {Config.ServerHost}:{Config.ServerPort}, " +
                            $"\r\n\tResult: {body_CancelForwardReply.Result}, " +
                            $"\r\n\tResult_Mapping: {body_CancelForwardReply.Result_Mapping}.");
                        break;
                    case MsgID.ForwardErrorBody:
                        var body_ForwardError = jtt809PackageInfo.MessageBody as ForwardErrorBody;
                        Logger.Log(
                            NLog.LogLevel.Trace,
                            LogType.系统跟踪,
                            $"接收[转发异常消息], " +
                            $"\r\n\tServer: {Config.ServerHost}:{Config.ServerPort}, " +
                            $"\r\n\tReason: {body_ForwardError.Reason}, " +
                            $"\r\n\tReason_Mapping: {body_ForwardError.Reason_Mapping}.");
                        break;
                    default:
                        Logger.Log(
                            NLog.LogLevel.Trace,
                            LogType.系统跟踪,
                            $"接收[其他信息], " +
                            $"\r\n\tServer: {Config.ServerHost}:{Config.ServerPort}, " +
                            $"\r\n\tMsg_ID: {Protocol.JTT809Handler.Decode(Protocol.JTT809Handler.Encode(jtt809PackageInfo.JTT809MessageHeader.Msg_ID, new SuperSocket.JTT.Base.Model.CodeInfo { CodeType = SuperSocket.JTT.Base.Model.CodeType.uint16_hex }), new SuperSocket.JTT.Base.Model.CodeInfo { CodeType = SuperSocket.JTT.Base.Model.CodeType.string_hex })}, " +
                            $"\r\n\tbuffer: {string.Join('\t', jtt809PackageInfo.Buffer.ToArray().Select(o => o.To0XString()))}.");
                        break;
                }

                await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Logger.Log(
                    NLog.LogLevel.Error,
                    LogType.系统异常,
                    $"处理消息包时异常, " +
                    $"\r\n\tServer: {Config.ServerHost}:{Config.ServerPort}, " +
                    $"\r\n\tbuffer: {string.Join('\t', packageInfo.Buffer.ToArray().Select(o => o.To0XString()))}.",
                    null,
                    ex);

                await Task.FromResult(false);
            }
        }
    }
}
