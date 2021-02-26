using System;
using System.Collections.Generic;
using System.Text;

namespace SuperSocket.JTT1078.Const
{
    /// <summary>
    /// 远程录像下载控制请求控制类型
    /// </summary>
    public struct DownloadControlType
    {
        public const byte 暂停 = 0x00;

        public const byte 继续 = 0x01;

        public const byte 取消 = 0x02;
    }
}
