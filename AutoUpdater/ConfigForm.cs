using AutoUpdater.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoUpdater
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }

        private void Button1_Click( object sender, EventArgs e )
        {

            DirectoryInfo di = new DirectoryInfo( Environment.CurrentDirectory );
            List<Config.UpdateItem> list = new List<Config.UpdateItem>();
            if( checkBox1.Checked )
            {
                GetUpdateItems( di, list, Application.ExecutablePath );
            }
            else
            {
                GetUpdateItems( di, list, null );
            }
            AutoUpdaterConfig config = new AutoUpdaterConfig();

            try
            {
                if( File.Exists( textBox1.Text ) )
                {
                    config = Newtonsoft.Json.JsonConvert.DeserializeObject<AutoUpdaterConfig>( File.ReadAllText( textBox1.Text ) );
                }
            }
            catch( Exception ee)
            {

            }

            config.LastUpdateTime = DateTime.Now;
            config.UpdateList = list;

            File.WriteAllText( textBox1.Text, Newtonsoft.Json.JsonConvert.SerializeObject( config ) );
        }


        static void GetUpdateItems( DirectoryInfo di, List<UpdateItem> list, string excepte_item )
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

                GetUpdateItems( item, temp.List, excepte_item );
            }


            foreach( var item in di.GetFiles() )
            {
                if( item.FullName == excepte_item )
                {
                    continue;
                }

                list.Add( new Config.UpdateItem()
                {
                    List = null,
                    Name = item.Name
                } );
            }

        }

        private void TextBox1_TextChanged( object sender, EventArgs e )
        {
            button1.Text = $"generate {textBox1.Text}";
        }

        private void ConfigForm_Load( object sender, EventArgs e )
        {
            TextBox1_TextChanged( null, null );
        }
    }
}
