using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.IO;
using System.Collections;
using AutoUpdater.Config;
using System.Net.Http;
using System.Threading.Tasks;

namespace AutoUpdater
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Updater : Form
    {
        public Updater()
        {
            InitializeComponent();
        }


        public static void ControlHorizontally( Control c )
        {
            if( c.Parent == null )
            {
                return;
            }

            c.Location = new Point( ( c.Parent.Width - c.Width ) / 2, c.Location.Y );

        }

        private async void AutoUpdate_Shown( object sender, EventArgs e )
        {
            if( Config?.AutoUpdaterConfig == null )
            {
                MessageBox.Show( "配置信息丢失" );
                Close();
                return;
            }

            HttpClient client = new HttpClient();
            string remote_uri = Config.AutoUpdaterConfig.RemoteUpdateConfig;
            string config = null;
            while( Config.LinkTimes != 0 )
            {
                try
                {
                    config = await client.GetStringAsync( remote_uri );
                    break;
                }
                catch( Exception eeee)
                {
                    Config.LinkTimes--;
                    await Task.Run( () => Thread.Sleep(Config.TimeSpan) );
                }
            }
            AutoUpdaterConfig updater_config = null;
            try
            {
                updater_config = Newtonsoft.Json.JsonConvert.DeserializeObject<AutoUpdaterConfig>( config );
            }
            catch( Exception eeee)
            {
                LogFun( "远程更新列表有误" );
                this.Close();
                return;
            }

            Uri uri = new Uri( remote_uri );


            await DownLoadFiles( updater_config, $"{uri.Scheme}://{uri.Authority}" );
            tips.Text = "下载完毕";
            updater_config.RemoteUpdateConfig = remote_uri;
            SavaConfigFun( updater_config );

            if( !string.IsNullOrWhiteSpace( updater_config.LuanchApplication ) )
            {
                StartApplication( new FileInfo( updater_config.LuanchApplication ) );
            }
            this.Close();
        }

        static void StartApplication( FileInfo fi )
        {
            try
            {
                using( Process p = new Process() )
                {
                    p.StartInfo.FileName = fi.Name;
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.WorkingDirectory = fi.DirectoryName;
                    p.Start();//启动程序
                }
            }
            catch( Exception e)
            {
                
            }
        }

        public Action<object> LogFun
        {
            get;set;
        }


        internal Action<AutoUpdaterConfig> SavaConfigFun
        {
            get;set;
        }

        private async Task DownLoadFiles( AutoUpdaterConfig updater_config, string base_uri )
        {
            updater_config.LastUpdateTime = DateTime.Now;
            await AutoUpdaterHelper.GetUpdateItems( new DirectoryInfo( Environment.CurrentDirectory ), base_uri, "", updater_config.UpdateList, updater_config.UpdateLog,updater_config.NoUpdateLog, LogFun, s => tips.Text = $"正在下载{s}" );
            updater_config.UpdateList = null;
        }

        private void AutoUpdate_SizeChanged( object sender, EventArgs e )
        {
            ControlHorizontally( total_process );
            ControlHorizontally( current_process );
            ControlHorizontally( tips );
            ControlHorizontally( label_now_download );
        }


        private void label_now_download_TextChanged( object sender, EventArgs e )
        {
            ControlHorizontally( label_now_download );
        }

        internal SelfConfig Config
        {
            get; set;
        }

        private void tips_Click( object sender, EventArgs e )
        {

        }

        private void tips_SizeChanged( object sender, EventArgs e )
        {
            ControlHorizontally( tips );
        }

        private void Updater_Load( object sender, EventArgs e )
        {
            if( !string.IsNullOrWhiteSpace( this.Config.Background ) && File.Exists( this.Config.Background ) )
            {
                this.BackgroundImage = Image.FromFile( this.Config.Background );
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            this.WindowState = FormWindowState.Maximized;
        }
    }
}

