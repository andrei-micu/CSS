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
        private DAO dao;
        private Admission admission;

        public MainWindow()
        {
            InitializeComponent();

            dao = new DAO();
            admission = new Admission(dao);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            label1.Text = "l1";
            label2.Text = "l2";
        }
    }
}
