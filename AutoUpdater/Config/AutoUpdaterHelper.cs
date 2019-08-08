﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AutoUpdater.Config
{
    class AutoUpdaterHelper
    {
        public static void GenerateUpdateItems( DirectoryInfo di, List<UpdateItem> list, string[] excepte_items )
        {

            if( di == null )
            {
                return;
            }



            foreach( var item in di.GetDirectories() )
            {
                var temp = new UpdateItem()
                {
                    Name = item.Name,
                    List = new List<UpdateItem>()
                };
                list.Add( temp );

                GenerateUpdateItems( item, temp.List, excepte_items );
            }


            foreach( var item in di.GetFiles() )
            {
                

                if( Array.IndexOf( excepte_items, item.FullName ) > 0 )
                {
                    continue;
                }

                list.Add( new Config.UpdateItem()
                {
                    List = null,
                    Name = item.Name,
                    Version = System.Diagnostics.FileVersionInfo.GetVersionInfo( item.FullName )?.FileVersion
                } );
            }

        }

        static HttpClient client = new HttpClient();

        public static async Task GetUpdateItems( DirectoryInfo di, string base_uri, string now_path, List<UpdateItem> list, UpdateLogs logs, Action<object> _log_fun, Action<string> _now_down )
        {
            Action<object> log_fun = _log_fun == null ? ( s ) => { }
            : _log_fun;
            Action<string> now_down = _now_down == null ? ( s ) => { }
            : _now_down;
            if( di == null || list == null )
            {
                return;
            }

            if( !di.Exists || list.Count < 1 )
            {
                return;
            }

            foreach( var item in list )
            {
                if( item.List == null )
                {
                    string download_item = Path.Combine( di.FullName, item.Name );
                    if( File.Exists( download_item ) )
                    {
                        var self = System.Diagnostics.FileVersionInfo.GetVersionInfo( download_item );
                        if( self.FileVersion?.CompareTo( item?.Version ) < 0 )
                        {

                        }
                        else
                        {
                            logs.SkipUpdateLog.Add( Path.Combine( now_path, item.Name ) );
                            continue;
                        }
                    }


                    now_down( item.Name );
                    try
                    {
                        using( var stream = await client.GetStreamAsync( $"{base_uri}/{now_path}/{item.Name}" ) )
                        {
                            using( var fs = new FileStream( download_item, FileMode.OpenOrCreate ) )
                            {
                                await stream.CopyToAsync( fs );
                            }
                            logs.UpdateLog.Add( Path.Combine( now_path, item.Name ) );

                        }
                    }
                    catch( Exception eee )
                    {
                        logs.NoUpdateLog.Add( Path.Combine( now_path, item.Name ) );
                        log_fun( new Exception( $"{item.Name}未正确下载", eee ) );
                        continue;
                    }
                }
                else
                {
                    DirectoryInfo di_temp = new DirectoryInfo( $"{di.FullName}/{item.Name}" );

                    if( !di_temp.Exists )
                    {
                        di_temp.Create();
                        di_temp = new DirectoryInfo( $"{di.FullName}/{item.Name}" );
                    }

                    await GetUpdateItems( di_temp, base_uri, $"{now_path}/{item.Name}", item.List, logs, log_fun, now_down );
                }
            }
        }
    }
}
