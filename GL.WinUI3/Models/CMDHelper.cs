using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp1.Models
{

    public class CMD_Helper
    {
        private const int _ReadSize = 1024;
        private Process _CMD;//cmd进程
        private Encoding _OutEncoding;//输出字符编码
        private Stream _OutStream;//基础输出流
        private Stream _ErrorStream;//错误输出流
        public event Action<string> Output;//输出事件
        public event Action<string> Error;//错误事件
        public event Action Exited;//退出事件
        private bool _Run;//循环控制
        private byte[] _TempBuffer;//临时缓冲
        private byte[] _ReadBuffer;//读取缓存区

        private byte[] _ETempBuffer;//临时缓冲
        private byte[] _ErrorBuffer;//错误读取缓存区
        public string filename { get; set; }
        public CMD_Helper(string path)
        {
            _CMD = new Process();
            _CMD.StartInfo.UseShellExecute = false;//是否使用操作系统shell启动
            _CMD.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            _CMD.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            _CMD.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            _CMD.StartInfo.CreateNoWindow = true;//不显示程序窗口
            _CMD.Exited += _CMD_Exited;
            _ReadBuffer = new byte[_ReadSize];
            _ErrorBuffer = new byte[_ReadSize];
            filename = path;
            _CMD.StartInfo.FileName = filename;
            ReStart();
        }

        /// <summary>
        /// 停止使用，关闭进程和循环线程
        /// </summary>
        public void Stop()
        {
            _Run = false;
            _CMD.Close();
        }


        /// <summary>
        /// 重新启用
        /// </summary>
        public void ReStart()
        {
            Stop();
            _CMD.Start();
            _OutEncoding = _CMD.StandardOutput.CurrentEncoding;
            _OutStream = _CMD.StandardOutput.BaseStream;
            _ErrorStream = _CMD.StandardError.BaseStream;
            _Run = true;
            _CMD.StandardInput.AutoFlush = true;
            ReadResult();
            ErrorResult();
        }

        //退出事件
        private void _CMD_Exited(object sender, EventArgs e)
        {
            Exited?.Invoke();
        }

        /// <summary>
        /// 执行cmd命令
        /// </summary>
        /// <param name="cmd">需要执行的命令</param>
        public void RunCMD(string cmd)
        {

            if (!_Run)
            {
                if (cmd.Trim().Equals("/restart", StringComparison.CurrentCultureIgnoreCase))
                {
                    ReStart();
                }
                return;
            }
            if (_CMD.HasExited)
            {
                Stop();
                return;
            }
            _CMD.StandardInput.WriteLine(cmd);
        }



        //异步读取输出结果
        private void ReadResult()
        {
            if (!_Run)
            {
                return;
            }
            _OutStream.BeginRead(_ReadBuffer, 0, _ReadSize, ReadEnd, null);
        }

        //一次异步读取结束
        private void ReadEnd(IAsyncResult ar)
        {
            int count = _OutStream.EndRead(ar);

            if (count < 1)
            {
                if (_CMD.HasExited)
                {
                    Stop();
                }
                return;
            }

            if (_TempBuffer == null)
            {
                _TempBuffer = new byte[count];
                Buffer.BlockCopy(_ReadBuffer, 0, _TempBuffer, 0, count);
            }
            else
            {
                byte[] buff = _TempBuffer;
                _TempBuffer = new byte[buff.Length + count];
                Buffer.BlockCopy(buff, 0, _TempBuffer, 0, buff.Length);
                Buffer.BlockCopy(_ReadBuffer, 0, _TempBuffer, buff.Length, count);
            }

            if (count < _ReadSize)
            {
                string str = _OutEncoding.GetString(_TempBuffer);
                Output?.Invoke(str);
                _TempBuffer = null;
            }

            ReadResult();
        }


        //异步读取错误输出
        private void ErrorResult()
        {
            if (!_Run)
            {
                return;
            }
            _ErrorStream.BeginRead(_ErrorBuffer, 0, _ReadSize, ErrorCallback, null);
        }

        private void ErrorCallback(IAsyncResult ar)
        {
            int count = _ErrorStream.EndRead(ar);

            if (count < 1)
            {
                if (_CMD.HasExited)
                {
                    Stop();
                }
                return;
            }

            if (_ETempBuffer == null)
            {
                _ETempBuffer = new byte[count];
                Buffer.BlockCopy(_ErrorBuffer, 0, _ETempBuffer, 0, count);
            }
            else
            {
                byte[] buff = _ETempBuffer;
                _ETempBuffer = new byte[buff.Length + count];
                Buffer.BlockCopy(buff, 0, _ETempBuffer, 0, buff.Length);
                Buffer.BlockCopy(_ErrorBuffer, 0, _ETempBuffer, buff.Length, count);
            }

            if (count < _ReadSize)
            {
                string str = _OutEncoding.GetString(_ETempBuffer);
                Error?.Invoke(str);
                _ETempBuffer = null;
            }

            ErrorResult();
        }

        ~CMD_Helper()
        {
            _Run = false;
            _CMD?.Close();
            _CMD?.Dispose();
        }

    }
}
