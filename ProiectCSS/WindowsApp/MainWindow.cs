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
            if (dao.getApplicants().Count == 0)
            {
                admission.populateDB();
            }          
            UpdateData();
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

        private void add_button_Click(object sender, EventArgs e)
        {
            if (data_tablelayoutpanel.Controls.OfType<TextBox>().Any(x => string.IsNullOrEmpty(x.Text)))
            {
                MessageBox.Show("Please complete all fields!", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            }
            else
            {
                string pin = cnp_textbox.Text;
                string first_name = first_name_textbox.Text;
                string father_initial = father_initial_textbox.Text +".";
                string last_name = last_name_textbox.Text;
                string city = city_textbox.Text;
                string locality = locality_textbox.Text;
                string school_name = school_name_textbox.Text;
                double test_mark = Convert.ToDouble(test_mark_textbox.Text);
                double domain_mark = Convert.ToDouble(domain_mark_textbox.Text);
                double exam_average_mark = Convert.ToDouble(exam_average_textbox.Text);

                try
                {
                    admission.insertApplicant(new Applicant(pin, first_name, last_name, father_initial, city, locality,
                    school_name, test_mark, exam_average_mark, domain_mark));
                    data_tablelayoutpanel.Controls.OfType<TextBox>().ToList().ForEach(x => x.Text = string.Empty);
                    UpdateData();
                    
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
                
                
            }
        }

        private void UpdateData()
        {
            applicants_datagridview.DataSource = dao.getApplicants();
            admission.calculateAndPublishResults();
            results_datagridview.DataSource = dao.getResults().Select(x => new
            {
                x.applicant.FirstName,
                x.applicant.LastName,
                x.applicant.FatherInitial,
                x.applicant.TestMark,
                x.applicant.AvgExamen,
                x.applicant.DomainMark,
                x.applicant.GeneralAverage,
                x.result
            }).ToList();
        }

        private void applicants_datagridview_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string cnp = (string)applicants_datagridview.Rows[e.RowIndex].Cells[0].Value;
            IApplicant applicant = dao.getApplicants()[e.RowIndex];
            string property = applicants_datagridview.Columns[e.ColumnIndex].Name;
            
            if (e.ColumnIndex > 6)
            {
                double value = (double)applicants_datagridview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                applicant.GetType().GetProperty(property).SetValue(applicant, value, null);
            }
            else
            {
                string value = (string)applicants_datagridview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                applicant.GetType().GetProperty(property).SetValue(applicant, value, null);
            }
            
            admission.updateApplicant(cnp, applicant);
            UpdateData();
        }

        private void applicants_datagridview_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && applicants_datagridview.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in applicants_datagridview.SelectedRows)
                {
                    admission.deleteApplicant(row.Cells[0].Value.ToString());
                }

                UpdateData();
            }
        }

        
    }
}
