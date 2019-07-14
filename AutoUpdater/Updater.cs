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

        private void label6_TextChanged( object sender, EventArgs e )
        {
            ControlHorizontally( tips );
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
            Stream stream = null;
            string config = null;
            while( Config.LinkTimes != 0 )
            {
                try
                {
                    config = await client.GetStringAsync( Config.AutoUpdaterConfig.RemoteUpdateConfig );
                    
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
            catch( Exception )
            {
                this.Close();
                return;
            }



            await DownLoadFiles( updater_config );


        }

        private async Task DownLoadFiles( AutoUpdaterConfig updater_config )
        {
            
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
    }
}

