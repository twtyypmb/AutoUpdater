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
                AutoUpdaterHelper.GenerateUpdateItems( di, list, Application.ExecutablePath );
            }
            else
            {
                AutoUpdaterHelper.GenerateUpdateItems( di, list, null );
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
            MessageBox.Show( "生成完毕" );
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
