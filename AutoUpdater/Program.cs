using AutoUpdater.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace AutoUpdater
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main( string[] args )
        {

            DefaultJsonConvertSetting();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault( false );
            if( args != null && args.Length > 0 )
            {
                Application.Run( new ConfigForm() );
            }
            else
            {
                string config_file = "AutoUpdaterConfig.json";
                string background = "bg.png";
                int time_span = 3000;
                int link_times = 10;
                AutoUpdaterConfig updaterConfig = null;

                try
                {
                    if( ConfigurationManager.AppSettings.AllKeys.Contains( "config_file" ) )
                    {
                        config_file = ConfigurationManager.AppSettings["config_file"];
                        
                    }
                    updaterConfig = Newtonsoft.Json.JsonConvert.DeserializeObject<AutoUpdaterConfig>( File.ReadAllText( config_file ) );
                }
                catch( Exception e )
                {
                    Log( e );
                    MessageBox.Show( "配置出错" );
                    return;
                }



                try
                {
                    time_span = int.Parse( ConfigurationManager.AppSettings["time_span"] );
                }
                catch( Exception ee )
                {

                }

                try
                {
                    link_times = int.Parse( ConfigurationManager.AppSettings["link_times"] );
                }
                catch( Exception ee )
                {

                }


                if( ConfigurationManager.AppSettings.AllKeys.Contains( "config_file" ) )
                {
                    background = ConfigurationManager.AppSettings["background"];
                }

                
                Log( "----------------------------------------本次更新开始----------------------------------------" );
                Application.Run( new Updater()
                {
                    Config = new Config.SelfConfig()
                    {
                        LinkTimes = link_times,
                        TimeSpan = time_span,
                        AutoUpdaterConfig = updaterConfig,
                        Background = background
                    },
                    LogFun = Log,
                    SavaConfigFun = s => File.WriteAllText( config_file, Newtonsoft.Json.JsonConvert.SerializeObject( s ) )

                } );
                Log( "----------------------------------------本次更新结束----------------------------------------" );
            }
        }

        /// <summary>
        /// Json.net默认转换设置
        /// </summary>
        private static void DefaultJsonConvertSetting()
        {
            JsonSerializerSettings setting = new JsonSerializerSettings();
            JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>( () =>
            {
                //日期类型默认格式化处理
                setting.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                setting.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                setting.Formatting = Formatting.Indented;
                //空值处理
                //setting.NullValueHandling = NullValueHandling.Ignore;

                return setting;
            } );
        }


        private static void Log( object content )
        {
            if( content == null )
            {
                return;
            }

            using( var fs = new FileStream( "UpdateLog.txt", FileMode.Append ) )
            {
                using( var sw = new StreamWriter( fs, System.Text.Encoding.UTF8 ) )
                {
                    sw.Write( $"{DateTime.Now}:{content.ToString()}{Environment.NewLine}" );
                }
            }

        }
    }
}
