using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AutoUpdater.Config
{
    static class AutoUpdaterHelper
    {
        public static void GenerateUpdateItems(DirectoryInfo di, List<UpdateItem> list, string[] excepte_items)
        {

            if (di == null)
            {
                return;
            }



            foreach (var item in di.GetDirectories())
            {
                var temp = new UpdateItem()
                {
                    Name = item.Name,
                    List = new List<UpdateItem>()
                };
                list.Add(temp);

                GenerateUpdateItems(item, temp.List, excepte_items);
            }


            foreach (var item in di.GetFiles())
            {


                if (Array.IndexOf(excepte_items, item.FullName) > 0)
                {
                    continue;
                }

                list.Add(new Config.UpdateItem()
                {
                    List = null,
                    Name = item.Name,
                    Md5 = GetFileMd5(item.FullName),
                    Version = System.Diagnostics.FileVersionInfo.GetVersionInfo(item.FullName)?.FileVersion
                });
            }

        }

        static HttpClient client = new HttpClient();

        public static async Task GetUpdateItems(DirectoryInfo di, string base_uri, string now_path, List<UpdateItem> list, UpdateLogs logs, Action<object> _log_fun, Action<string> _now_down)
        {
            Action<object> log_fun = _log_fun == null ? (s) => { }
            : _log_fun;
            Action<string> now_down = _now_down == null ? (s) => { }
            : _now_down;
            if (di == null || list == null)
            {
                return;
            }

            if (!di.Exists || list.Count < 1)
            {
                return;
            }

            foreach (var item in list)
            {
                if (item.List == null)
                {
                    string download_item = Path.Combine(di.FullName, item.Name);
                    /*
                     * 判断本地有无文件，若无则直接下载
                     * 如果有文件，则对比版本号，若本地版本号比远程版本号小，则下载
                     * 如果版本比较返回相等，则比较md5值，若不相等则下载
                     * 
                     */

                    if (File.Exists(download_item))
                    {
                        var self = System.Diagnostics.FileVersionInfo.GetVersionInfo(download_item);
                        var compare_result = self.FileVersion?.CompareVersionTo(item?.Version);
                        if (compare_result > 0 || compare_result == 0 && GetFileMd5(download_item) == item.Md5)
                        {
                            logs.SkipUpdateLog.Add(Path.Combine(now_path, item.Name));
                            continue;
                        }

                    }


                    now_down(item.Name);
                    try
                    {
                        using (var stream = await client.GetStreamAsync($"{base_uri}/{now_path}/{item.Name}"))
                        {
                            using (var fs = new FileStream(download_item, FileMode.OpenOrCreate))
                            {
                                await stream.CopyToAsync(fs);
                            }
                            logs.UpdateLog.Add(Path.Combine(now_path, item.Name));

                        }
                    }
                    catch (Exception eee)
                    {
                        logs.NoUpdateLog.Add(Path.Combine(now_path, item.Name));
                        log_fun(new Exception($"{item.Name}未正确下载", eee));
                        continue;
                    }
                }
                else
                {
                    DirectoryInfo di_temp = new DirectoryInfo($"{di.FullName}/{item.Name}");

                    if (!di_temp.Exists)
                    {
                        di_temp.Create();
                        di_temp = new DirectoryInfo($"{di.FullName}/{item.Name}");
                    }

                    await GetUpdateItems(di_temp, base_uri, $"{now_path}/{item.Name}", item.List, logs, log_fun, now_down);
                }
            }
        }

        public static int CompareVersionTo(this string this_version, string other_version)
        {
            int c1 = 0;
            int c2 = 0;
            if (this_version == null && other_version == null)
            {
                return 0;
            }

            if (this_version != null && other_version == null)
            {
                return 1;
            }

            if (this_version == null && other_version != null)
            {
                return -1;
            }

            do
            {



                var c1r = this_version.Split('.');
                var c2r = other_version.Split('.');

                int length = c1r.Length >= c2r.Length ? c1r.Length : c2r.Length;




                for (int i = 0; i < length; i++)
                {
                    try
                    {
                        c1 = Convert.ToInt32(c1r[i]);
                    }
                    catch (Exception e1)
                    {
                        c1 = 0;
                    }
                    try
                    {
                        c2 = Convert.ToInt32(c2r[i]);
                    }
                    catch (Exception e1)
                    {
                        c2 = 0;
                    }

                    if (c1 == c2)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                break;

            } while (true);





            return c1 - c2;
        }

        public static string GetFileMd5(string file_path)
        {
            return GetFileMd5(new FileInfo(file_path));
        }
        public static string GetFileMd5(FileInfo file_info)
        {
            string result = null;
            if (file_info.Exists)
            {
                using (FileStream file = file_info.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    result = BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(file)).Replace("-", "");
                }
            }
            return result;

        }

    }
}
