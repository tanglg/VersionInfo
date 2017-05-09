using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VersionInfo
{
    class Program
    {
        static void Main(string[] args)
        {
            
            if(args == null) return;

            string gitPath=string.Empty, outPutPath=string.Empty,format = "--pretty=format:\" %cd@%H\" --date=iso8601";
            bool isReset = false;

            foreach (var param in args)
            {
                if (param.StartsWith("gitPath"))
                {
                    gitPath = param.Split('=')[1];
                }
                else if (param.StartsWith("outPutPath"))
                {
                    outPutPath = param.Split('=')[1];
                }
                else if (param.StartsWith("format"))
                {
                    format = param.Split('=')[1];
                }
                else if (param.StartsWith("isReset"))
                {
                    isReset = bool.Parse(param.Split('=')[1]);
                }
            }
            if (string.IsNullOrEmpty(outPutPath)) throw new ArgumentException($"未设置outPutPath值");
            if (isReset)
            {//输出一个空的update.txt文件，用于生成产品输出，即不显示版本信息
                if (File.Exists(outPutPath))
                {
                    File.Delete(outPutPath);
                }

                using (var fs = File.Create(outPutPath))
                {
                }
                return;
            }

            if(string.IsNullOrEmpty(gitPath)) throw new ArgumentException($"未设置gitPath值");

            RunProcess($"git --git-dir={gitPath} log -1  {format} >{outPutPath}");
        }
        private static void RunProcess(string command)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = @"c:\windows\system32\cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(command);

            p.StandardInput.AutoFlush = true;
            p.StandardInput.WriteLine("exit");

            p.WaitForExit();
            p.Close();

        }
    }
}
