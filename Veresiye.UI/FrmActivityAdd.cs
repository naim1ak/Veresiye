using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Veresiye.Model;
using Veresiye.Service;

namespace Veresiye.UI
{
    public partial class FrmActivityAdd : Form
    {
        public int CompanyId;
        public FrmCompanyEdit MasterForm { get; set; }
        private readonly IActivityService activityService;
        public FrmActivityAdd(IActivityService activityService)
        {
            this.activityService = activityService;
            InitializeComponent();
        }
        public void LoadForm(int companyId)
        {
            this.CompanyId = companyId;
            this.txtAmount.Clear();
            this.txtAmount.Clear();
            this.dtpTransactionDate.Value = DateTime.Now;
            this.cmbActivityType.SelectedIndex = -1;
        }

        private void FrmActivityAdd_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            //Validasyonlar
            if (txtName.Text == "")
            {
                MessageBox.Show("İşlem adı gereklidir.");
                return;
            }
            else if (txtAmount.Text == "")
            {
                MessageBox.Show("Miktar gereklidir.");
                return;
            }
            else if (cmbActivityType.SelectedIndex <= 0)
            {
                MessageBox.Show("İşlem türü gereklidir.");
                return;
            }

            // Veritabanına ekleme işlemi
            var activity = new Activity();
            activity.CompanyId = this.CompanyId;
            activity.Name = txtName.Text;
            activity.Amount = Convert.ToDecimal(txtAmount.Text);
            activity.ActivityType = (ActivityType)(cmbActivityType.SelectedIndex + 1);
            activity.TransactionDate = dtpTransactionDate.Value;
            activityService.Insert(activity);
            MasterForm.LoadActivities();
            this.Hide();
        }

        private void FrmActivityAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
