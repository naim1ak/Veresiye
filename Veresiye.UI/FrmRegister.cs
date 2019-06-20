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
    public partial class FrmRegister : Form
    {
        private readonly IUserService userService;
        public FrmRegister(IUserService userService)
        {
            this.userService = userService;
            InitializeComponent();
        }
        private void FrmRegister_Load(object sender, EventArgs e)
        {
        }
        private void BtnSignUp_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text != txtPasswordConfirm.Text)
            {
                MessageBox.Show("Parola ve Parola Doğrula Alanları Aynı Olmalıdır!");
                return;
            }
            var user = new User();
            user.UserName = txtUserName.Text;
            user.Password = txtPassword.Text;
            user.CompanyName = txtCompanyName.Text;
            user.Phone = txtPhone.Text;
            user.City = txtCity.Text;
            user.Region = txtRegion.Text;
            var status = userService.Register(user);
            switch (status)
            {
                case RegisterStatus.Success:
                    MessageBox.Show("Kullanıcı Başarıyla Oluşturuldu.");
                    this.Close();
                    break;
                case RegisterStatus.InvalidFields:
                    MessageBox.Show("Kullanıcı Adı Boş Olmaz.");
                    break;
                case RegisterStatus.UserAlreadyExist:
                    MessageBox.Show("Bu Kullanıcı Adı Daha Önce Kullanılmış.");
                    break;
            }
        }
    }
}
