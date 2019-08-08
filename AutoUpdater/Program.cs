using AutoUpdater.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web.Script.Serialization;
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
            jss.RegisterConverters( new JavaScriptConverter[] { new DateTimeConvert() } );
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
                    updaterConfig = jss.Deserialize<AutoUpdaterConfig>( File.ReadAllText( config_file ) );
                }
                catch( Exception e )
                {
                    Log( e );
                    File.WriteAllText( config_file, jss.Serialize( new AutoUpdaterConfig() ) );
                    MessageBox.Show( $"配置出错，已生成配置文件{config_file}" );
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

                Log( "" );
                Log( "" );
                Log( "" );
                Log( "" );
                Log( "" );
                Log( "" );
                Log( "" );
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
                    SavaConfigFun = s => File.WriteAllText( config_file, jss.Serialize( s ) )

                } );
                Log( "----------------------------------------本次更新结束----------------------------------------" );
            }
        }

        static System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();




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
