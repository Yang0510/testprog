using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DogManage
{
    public partial class doghousemanage : Form
    {
        public static doghousemanage pdhgMainWin = null;
        public doghousemanage()
        {
            InitializeComponent();
        }
        //新增删除账号信息，对账户进行管理
        private void Bt_accmanage_Click(object sender, EventArgs e)
        {
           // Accmanage accmanage = new Accmanage();
            //accmanage.ShowDialog();
        }
    }
}
