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

namespace Veresiye.UI
{
    public partial class FrmCompanyAdd : Form
    {
        public FrmCompanies MasterForm { get; set; }
        public FrmCompanyAdd()
        {
            InitializeComponent();
        }

        private void FrmCompanyAdd_Load(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            // Validasyonlar
            if (string.IsNullOrEmpty(txtFirmName.Text))
            {
                MessageBox.Show("Firma adı gereklidir.");
                return;
            }
            else if (string.IsNullOrEmpty(txtPhone.Text))
            {
                MessageBox.Show("Firma Telefonu gereklidir.");
                return;
            }
            else if (string.IsNullOrEmpty(txtCity.Text))
            {
                MessageBox.Show("Şehir gereklidir.");
                return;
            }
            else if (string.IsNullOrEmpty(txtRegion.Text))
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
                db.Companies.Add(company);
                db.SaveChanges();
            }
            // Grid yenilenir
            if (MasterForm != null)
            {
                MasterForm.LoadCompanies();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
