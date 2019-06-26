using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veresiye.Data;
using Veresiye.Model;
using Veresiye.Service;

namespace Veresiye.UI
{
    public partial class FrmCompanyAdd : Form
    {
        public FrmCompanies MasterForm { get; set; }
        private readonly ICompanyService companyService;
        public FrmCompanyAdd(ICompanyService companyService)
        {
            this.companyService = companyService;
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Validasyonlar
            if (txtFirmName.Text == "")
            {
                MessageBox.Show("Firma adı gereklidir.");
                return;
            }
            else if (txtPhone.Text == "")
            {
                MessageBox.Show("Firma Telefonu gereklidir.");
                return;
            }
            else if (txtCity.Text == "")
            {
                MessageBox.Show("Şehir gereklidir.");
                return;
            }
            else if (txtRegion.Text == "")
            {
                MessageBox.Show("Bölge gereklidir.");
                return;
            }

            // Veritabanına ekleme işlemi
            using (var db = new ApplicationDbContext())
            {
                var company = new Company();
                company.Name = txtFirmName.Text;
                company.Phone = txtPhone.Text;
                company.Phone = txtCity.Text;
                company.Region = txtRegion.Text;
                companyService.Insert(company);
                MessageBox.Show("Firma başarıyla eklendi.");
                MasterForm.LoadCompanies(); // Grid yenilenir
                this.Hide();
            }

        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public void LoadForm()
        {
            txtFirmName.Clear();
            txtPhone.Clear();
            txtCity.Clear();
            txtRegion.Clear();
        }
        private void FrmCompanyAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
