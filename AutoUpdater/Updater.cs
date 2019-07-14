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

namespace AutoUpdater
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AutoUpdate : Form
    {
        public AutoUpdate()
        {
            InitializeComponent();
        }

        private void label6_TextChanged( object sender, EventArgs e )
        {
            ControlHorizontally( tips );
        }

        public static void ControlHorizontally(Control c)
        {
            if( c.Parent == null )
            {
                return;
            }

            c.Location = new Point( ( c.Parent.Width - c.Width ) / 2, c.Location.Y );

        }

        private string serverXmlFile;
        private void AutoUpdate_Shown( object sender, EventArgs e )
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

        internal SelfConfig Config { get; set; }
    }
}

