using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using Microsoft.Win32;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ServiceProcess;

namespace get_process
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private static int WM_QUERYENDSESSION = 0x80;
        private static bool systemShutdown = false;
        [DllImport("user32.dll", SetLastError = true)]
        static extern int CancelShutdown();

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == WM_QUERYENDSESSION)
            {
                systemShutdown = true;
                m.Result = (IntPtr)0;
            }
            m.Result = (IntPtr)0;
            base.WndProc(ref m);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            
            var allProcceses = Process.GetProcesses();
            Process[] processlist = Process.GetProcesses();

        foreach(Process theprocess in processlist)
          {
            listBox1.Items.Add(theprocess.ProcessName.ToString());
          }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
             
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox1.Text = listBox1.SelectedItem.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var allProcceses = Process.GetProcesses();
            Process[] processlist = Process.GetProcesses();
            foreach (Process theprocess in processlist)
            {
                try
                {
                        Process[] workers = Process.GetProcessesByName(textBox1.Text.ToString());
                        foreach (Process worker in workers)
                        {
                                worker.Kill();
                                worker.WaitForExit();
                                worker.Dispose();
                        }
                }
                catch (Exception)
                {
                }
            }
        }
        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            if (systemShutdown)
            {
                CancelShutdown(); // cancel the shutdown/logoff 
            }
        }    
    }
}
