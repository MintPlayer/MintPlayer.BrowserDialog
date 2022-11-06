using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MintPlayer.BrowserDialog.Test
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnPickBrowser_Click(object sender, EventArgs e)
        {
            var dialog = new BrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show($"You picked {dialog.SelectedBrowser.Name}.\r\nThe executable path is {dialog.SelectedBrowser.ExecutablePath}\r\nThe version is {dialog.SelectedBrowser.Version.ProductVersion}");
            }
        }
    }
}
