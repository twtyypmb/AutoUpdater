using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
                Application.Run( new ConfigForm() );
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

                //空值处理
                //setting.NullValueHandling = NullValueHandling.Ignore;

                return setting;
            } );
        }
    }
}
