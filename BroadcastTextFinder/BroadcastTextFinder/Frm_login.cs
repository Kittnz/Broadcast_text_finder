﻿using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BroadcastTextFinder
{
    public partial class frm_Login : Form
    {
        public frm_Login()
        {
            InitializeComponent();
        }

        private void Frm_login_Load(object sender, EventArgs e)
        {
            // Load values from settings
            txt_Host.Text = Properties.Settings.Default.host;
            txt_Username.Text = Properties.Settings.Default.username;
            txt_Password.Text = Properties.Settings.Default.password;
            txt_Database.Text = Properties.Settings.Default.database;
            txt_Port.Text = Properties.Settings.Default.port;
        }

        private void Btn_OK_Click(object sender, System.EventArgs e)
		{
			MySqlConnection conn = null;
			conn = new MySqlConnection();
            conn.ConnectionString = "server=" + txt_Host.Text + "; port=" + txt_Port.Text + "; user id=" + txt_Username.Text + "; password=" + txt_Password.Text + "; database=" + txt_Database.Text;
			try
			{
                // Try db connection.
                conn.Open();

                // If db connection success, save values to settings.
				if (chkBox_SaveValues.Checked == true)
				{
                    Properties.Settings.Default.host = txt_Host.Text;
                    Properties.Settings.Default.username = txt_Username.Text;
                    Properties.Settings.Default.password = txt_Password.Text;
                    Properties.Settings.Default.database = txt_Database.Text;
                    Properties.Settings.Default.port = txt_Port.Text;
                    Properties.Settings.Default.Save();
				}

                LoadMain();
			}
			catch (MySqlException myerror)
			{
				MessageBox.Show("Error Connecting to Database please re-enter login information." + Environment.NewLine + myerror.Message);
			}
			finally
			{
                conn.Close();
				conn.Dispose();
			}
		}

        private void Btn_Cancel_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(1);
        }

        private void LoadMain()
        {
            // Open Frm_waypoint and hide login form.
            System.Windows.Forms.Form Frm_Main = new FrmBroadcastTextFinder();
            Frm_Main.Show();
            this.Hide();
        }
    }
}
