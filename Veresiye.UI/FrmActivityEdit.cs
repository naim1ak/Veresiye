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
    public partial class FrmActivityEdit : Form
    {
        private int activityId;
        public FrmCompanyEdit MasterForm { get; set; }
        private readonly IActivityService activityService;
        public FrmActivityEdit(IActivityService activityService)
        {
            this.activityService = activityService;
            InitializeComponent();
        }
        public void LoadForm(int activityId)
        {
            this.activityId = activityId;
            var activity = activityService.Get(activityId);
            if (activity != null)
            {
                this.txtName.Text = activity.Name;
                this.txtAmount.Text = activity.Amount.ToString();
                this.dtpTransactionDate.Value = activity.TransactionDate;
                this.cmbActivityType.SelectedIndex = ((int)activity.ActivityType) - 1;
            }

        }

        private void FrmActivityEdit_FormClosing(object sender, FormClosingEventArgs e)
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
            activity.Name = txtName.Text;
            activity.Amount = Convert.ToDecimal(txtAmount.Text);
            activity.ActivityType = (ActivityType)(cmbActivityType.SelectedIndex + 1);
            activity.TransactionDate = dtpTransactionDate.Value;
            activityService.Update(activity);
            MasterForm.LoadActivities();
            this.Hide();
        }

        private void FrmActivityEdit_Load(object sender, EventArgs e)
        {

        }
    }
}
