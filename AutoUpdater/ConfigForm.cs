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
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace AutoUpdater
{
    public partial class ConfigForm : Form
    {
        public ConfigForm()
        {
            InitializeComponent();
        }
        static System.Web.Script.Serialization.JavaScriptSerializer jss = new System.Web.Script.Serialization.JavaScriptSerializer();
        static ConfigForm()
        {
            jss.RegisterConverters( new JavaScriptConverter[] { new DateTimeConvert() } );
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
                    config = jss.Deserialize<AutoUpdaterConfig>( File.ReadAllText( textBox1.Text ) );
                }
            }
            catch( Exception ee)
            {

            }

            config.LastUpdateTime = DateTime.Now;
            config.UpdateList = list;

            File.WriteAllText( textBox1.Text, jss.Serialize( config ) );
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
