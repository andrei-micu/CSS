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
            this.StartPosition = FormStartPosition.CenterScreen;
            dao = new DAO();
            admission = new Admission(dao);
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            admission.populateDB();
            applicants_datagridview.DataSource = dao.getApplicants();
        }

        private void pin_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        private void name_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (!Char.IsLetter(ch) && ch != 8 && ch != 45 && ch != 32)
            {
                e.Handled = true;
            }
        }

        private void test_mark_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch == 46 && test_mark_textbox.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void domain_mark_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch == 46 && domain_mark_textbox.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void exam_average_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;

            if (ch == 46 && exam_average_textbox.Text.IndexOf('.') != -1)
            {
                e.Handled = true;
                return;
            }

            if (!Char.IsDigit(ch) && ch != 8 && ch != 46)
            {
                e.Handled = true;
            }
        }

        private void submit_button_Click(object sender, EventArgs e)
        {
            if (this.data_tablelayoutpanel.Controls.OfType<TextBox>().Any(x => string.IsNullOrEmpty(x.Text)))
            {
                MessageBox.Show("Please complete all fields!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
            else
            {
                string pin = cnp_textbox.Text;
                string first_name = first_name_textbox.Text;
                string father_initial = father_initial_textbox.Text;
                string last_name = last_name_textbox.Text;
                string city = city_textbox.Text;
                string locality = locality_textbox.Text;
                string school_name = school_name_textbox.Text;
                double test_mark = Convert.ToDouble(test_mark_textbox.Text);
                double domain_mark = Convert.ToDouble(domain_mark_textbox.Text);
                double exam_average_mark = Convert.ToDouble(exam_average_textbox.Text);

                admission.insertApplicant(new Applicant(pin, first_name, last_name, father_initial, city, locality,
                    school_name, test_mark, exam_average_mark, domain_mark));
                applicants_datagridview.DataSource = dao.getApplicants();
            }
        }
    }
}
