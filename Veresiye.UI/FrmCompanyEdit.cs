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
    public partial class FrmCompanyEdit : Form
    {
        private readonly ICompanyService companyService;
        private readonly IActivityService activityService;
        private readonly FrmActivityAdd frmActivityAdd;
        private readonly FrmActivityEdit frmActivityEdit;
        public FrmCompanies MasterForm { get; set; }
        public FrmCompanyEdit(ICompanyService companyService, IActivityService activityService, FrmActivityAdd frmActivityAdd, FrmActivityEdit frmActivityEdit)
        {
            this.companyService = companyService;
            this.activityService = activityService;
            this.frmActivityAdd = frmActivityAdd;
            this.frmActivityEdit = frmActivityEdit;
            InitializeComponent();
            this.frmActivityAdd.MdiParent = this.MdiParent;
            this.frmActivityAdd.MasterForm = this;
            this.frmActivityEdit.MdiParent = this.MdiParent;
            this.frmActivityEdit.MasterForm = this;
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
                var company = companyService.Get(this.Id);
                company.Name = txtFirmName.Text;
                company.Phone = txtPhone.Text;
                company.City = txtCity.Text;
                company.Region = txtRegion.Text;
                companyService.Update(company);
                MessageBox.Show("Firma başarıyla güncellendi.");
                MasterForm.LoadCompanies(); // Grid yenilenir
                this.Hide();
            }
        }
        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private int Id;
        public void LoadForm(int id)
        {
            var company = companyService.Get(id);
            this.Id = id;
            txtFirmName.Text = company.Name;
            txtPhone.Text = company.Phone;
            txtCity.Text = company.City;
            txtRegion.Text = company.Region;
        }
        public void LoadActivities()
        {
            var activities = activityService.GetAllByCompanyId(this.Id);
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.DataSource = activities;
        }
        private void FrmCompanyAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
        private void BtnNewActivity_Click(object sender, EventArgs e)
        {
            frmActivityAdd.Show();
            frmActivityAdd.LoadForm(this.Id);
        }
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                this.frmActivityEdit.Show();
                this.frmActivityEdit.LoadForm(int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
            }
            else
            {
                MessageBox.Show("Lütfen düzenlemek istediğiniz işlemi seçiniz.");
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedRows.Count > 0)
            {
                this.activityService.Delete(int.Parse(this.dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                this.LoadActivities();
            }
            else
            {
                MessageBox.Show("Lütfen silmek istedğiniz işlemi seçiniz.");
            }
        }

        private void FrmCompanyEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
