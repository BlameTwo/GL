using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCHelper
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Security.Principal;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;

    namespace OceanLauncher.Utils
    {
        public class PatchHelper
        {
            const string METADATA_PATH = "patch-metadata";
            const string FILE_NAME = "global-metadata.dat";


            public string GetPatchDir()
            {
                var ret = "";

                string file_path = Path.Combine(Path.GetDirectoryName(cfg.Path), "YuanShen_Data", "Managed", "Metadata");
                string file_path_osrel = Path.Combine(Path.GetDirectoryName(cfg.Path), "GenshinImpact_Data", "Managed", "Metadata");

                if (GetCilentType() == CilentType.osrel)
                {
                    file_path = file_path_osrel;
                }
                return file_path;
            }

            public PatchHelper()
            {
                try
                {
                   
                }
                catch
                {

                }
            }

            public enum CilentType
            {
                cnrel,
                osrel,
                notsupported
            }

            public CilentType GetCilentType()
            {
                string file_path = Path.Combine(Path.GetDirectoryName(cfg.Path), "YuanShen_Data", "Managed", "Metadata");

                string file_path_osrel = Path.Combine(Path.GetDirectoryName(cfg.Path), "GenshinImpact_Data", "Managed", "Metadata");

                if (Directory.Exists(file_path_osrel))
                {
                    return CilentType.osrel;
                }
                else if (Directory.Exists(file_path))
                {
                    return CilentType.cnrel;
                }
                else
                {
                    return CilentType.notsupported;
                }

            }

            public void Patch()
            {

                if (!IsAdministrator())
                {
                    Debug.WriteLine("请以启动器方式启动");
                    return;
                }

                try
                {
                    Path.GetDirectoryName(cfg.Path);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("游戏路径错误");
                    return
                    ;
                }
                string file_path = GetPatchDir();

                //备份
                if (!File.Exists(Path.Combine(file_path, FILE_NAME + ".bak")))
                {
                    File.Copy(
                        Path.Combine(file_path, FILE_NAME),
                        Path.Combine(file_path, FILE_NAME + ".bak"));
                }

                //修补
                if (GetCilentType() == CilentType.cnrel)
                {


                    string patched_file = Path.Combine(METADATA_PATH, "cnrel-" + FILE_NAME);
                    if (File.Exists(patched_file))
                    {
                        File.Copy(patched_file, Path.Combine(file_path, FILE_NAME), true
                            );
                    }
                    else
                    {
                        Debug.WriteLine("文件不存在");
                        return;
                    }
                }
                else if (GetCilentType() == CilentType.osrel)
                {

                    string patched_file = Path.Combine(METADATA_PATH, "osrel-" + FILE_NAME);
                    if (File.Exists(patched_file))
                    {
                        File.Copy(patched_file, Path.Combine(file_path, FILE_NAME), true
                            );
                    }
                    else
                    {
                        Debug.WriteLine("文件不存在");
                        return;
                    }
                }
                else
                {
                    Debug.WriteLine("不受支持的客户端类型");
                    return;
                }
                Debug.WriteLine("成功添加补丁");
            }

            public void UnPatch()
            {
                if (!IsAdministrator())
                {
                    Debug.WriteLine("未获取原神文件夹的读写权限，请以管理员身份运行启动器！");
                    return;
                }

                try
                {
                    Path.GetDirectoryName(cfg.Path);

                }
                catch (Exception ex)
                {

                    Debug.WriteLine("游戏路径不正确"+ex.Message);
                    return
                    ;
                }
                string file_path = GetPatchDir();

                if (GetCilentType() == CilentType.cnrel)
                {

                    if (File.Exists(Path.Combine(file_path, FILE_NAME + ".bak")))
                    {
                        File.Copy(
                            Path.Combine(file_path, FILE_NAME + ".bak"),
                            Path.Combine(file_path, FILE_NAME), true);
                    }
                    else
                    {
                        Debug.WriteLine("未找到文件");
                    }
                }
                else if (GetCilentType() == CilentType.osrel)
                {

                    if (File.Exists(Path.Combine(file_path, FILE_NAME + ".bak")))
                    {
                        File.Copy(
                            Path.Combine(file_path, FILE_NAME + ".bak"),
                            Path.Combine(file_path, FILE_NAME), true);
                    }
                    else
                    {
                        Debug.WriteLine("未找到备份文件");
                    }
                }
                else
                {

                    Debug.WriteLine("不受支持的客户端类型");
                    return;
                }


                Debug.WriteLine("成功卸载补丁");

            }

            public void OpenFolder()
            {
                if (!Directory.Exists(METADATA_PATH))
                {
                    Directory.CreateDirectory(METADATA_PATH);
                }

                System.Diagnostics.Process.Start(Path.Combine(Environment.CurrentDirectory, METADATA_PATH));

            }

            public void OpenPatchFolder()
            {
                if (!Directory.Exists(METADATA_PATH))
                {
                    Directory.CreateDirectory(METADATA_PATH);
                }

                System.Diagnostics.Process.Start(GetPatchDir());

            }

            public bool IsPatched()
            {
                var dir = GetPatchDir();
                var r = false;
                try
                {
                    //不相同即为已 Patch
                    r = !isValidFileContent(Path.Combine(dir, FILE_NAME), Path.Combine(dir, FILE_NAME + ".bak"));

                }
                catch (Exception ex)
                {

                }
                return r;
            }

            public static bool isValidFileContent(string filePath1, string filePath2)
            {
                //创建一个哈希算法对象
                using (HashAlgorithm hash = HashAlgorithm.Create())
                {
                    using (FileStream file1 = new FileStream(filePath1, FileMode.Open), file2 = new FileStream(filePath2, FileMode.Open))
                    {
                        byte[] hashByte1 = hash.ComputeHash(file1);//哈希算法根据文本得到哈希码的字节数组
                        byte[] hashByte2 = hash.ComputeHash(file2);
                        string str1 = BitConverter.ToString(hashByte1);//将字节数组装换为字符串
                        string str2 = BitConverter.ToString(hashByte2);
                        return (str1 == str2);//比较哈希码
                    }
                }
            }

        }
    }
}
