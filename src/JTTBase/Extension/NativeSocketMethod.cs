using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace SuperSocket.JTT.JTTBase.Extension
{
    /// <summary>
    /// 本机Socket方法
    /// </summary>
    public static class NativeSocketMethod
    {
        [DllImport("Ws2_32.dll")]
        public static extern int connect(IntPtr socketHandle, ref sockaddr Address, ref int Addresslen);

        [DllImport("Ws2_32.dll")]
        public static extern int getpeername(IntPtr s, ref sockaddr Address, ref int namelen);

        [DllImport("ws2_32.dll")]
        public static extern IntPtr inet_ntoa(in_addr a);

        [DllImport("Ws2_32.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern uint htonl(uint value);

        [DllImport("Ws2_32.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern uint ntohl(uint value);

        [DllImport("ws2_32.dll")]
        public static extern ushort ntohs(ushort netshort);

        [DllImport("Ws2_32.dll", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern ushort htons(ushort value);

        [DllImport("Ws2_32.dll")]
        public static extern int recv(IntPtr socketHandle, IntPtr buf, int Buffercount, int socketFlags);

        [DllImport("Ws2_32.dll")]
        public static extern int send(IntPtr socketHandle, IntPtr buf, int count, int socketFlags);

        public enum AddressFamily
        {
            AppleTalk = 0x11,
            BlueTooth = 0x20,
            InterNetworkv4 = 2,
            InterNetworkv6 = 0x17,
            Ipx = 4,
            Irda = 0x1a,
            NetBios = 0x11,
            Unknown = 0
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public delegate int DConnect(IntPtr socketHandle, ref NativeSocketMethod.sockaddr Address, ref int Addresslen);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public delegate int Drecv(IntPtr socketHandle, IntPtr buf, int Buffercount, int socketFlags);

        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode, SetLastError = true)]
        public delegate int Dsend(IntPtr socketHandle, IntPtr buf, int count, int socketFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct in_addr
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] sin_addr;
        }

        public enum ProtocolType
        {
            BlueTooth = 3,
            ReliableMulticast = 0x71,
            Tcp = 6,
            Udp = 0x11
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct sockaddr
        {
            public short sin_family;
            public ushort sin_port;
            public NativeSocketMethod.in_addr sin_addr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] sin_zero;
        }

        public enum SocketType
        {
            Unknown,
            Stream,
            DGram,
            Raw,
            Rdm,
            SeqPacket
        }

        [DllImport("kernel32")]
        public static extern int GetLastError();

        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        [DllImport("kernel32", EntryPoint = "GetPrivateProfileStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern uint GetPrivateProfileStringA(string lpApplicationName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32", EntryPoint = "WritePrivateProfileStringA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int WritePrivateProfileStringA(string lpApplicationName, string lpKeyName, string lpString, string lpFileName);

        public static uint htonl(int value)
        {
            return htonl((uint)value);
        }

        public static ushort htons(short value)
        {
            return htons((ushort)value);
        }

        public static ulong ntohl64(long value)
        {
            return ntohl64((ulong)value);
        }

        public static ulong htonl64(long value)
        {
            return htonl64((ulong)value);
        }

        public static UInt64 htonl64(UInt64 value)
        {
            UInt64 ret = 0;

            UInt32 high, low;

            low = (UInt32)(value & 0xFFFFFFFF);

            high = (UInt32)((value >> 32) & 0xFFFFFFFF);

            low = htonl(low);

            high = htonl(high);

            ret = low;

            ret <<= 32;

            ret |= high;

            return ret;
        }

        public static UInt64 ntohl64(UInt64 host)
        {
            UInt64 ret = 0;

            UInt32 high, low;

            low = (UInt32)(host & 0xFFFFFFFF);

            high = (UInt32)((host >> 32) & 0xFFFFFFFF);

            low = ntohl(low);

            high = ntohl(high);

            ret = low;

            ret <<= 32;

            ret |= high;

            return ret;
        }
    }
}
