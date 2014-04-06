using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseLibrary;
using AdmissionLibrary;

namespace WindowsApp
{
    public partial class MainWindow : Form
    {
        private DAO dao = new DAO();
        private Admission admission = new Admission();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            label1.Text = label1.Text + dao.GetHelloWorldFromDB();
            label2.Text = label2.Text + admission.GetHelloWorldFromAdmission();
        }
    }
}
