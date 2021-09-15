using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
namespace DogManage
{
    public partial class Accmanage : Form
    {
        doghousemanage Doghousemanage = doghousemanage.pdhgMainWin;
        DataTable userdt = new DataTable();
        public static Accmanage paccMainWin = null;
        public Accmanage()
        {
            InitializeComponent();
            #region//获取用户信息
            string mainini = CCommonData.Readtxt("mainini");
            if ((mainini != null) && (mainini != ""))
            {
                List<userinfo> userinfolist = new List<userinfo>();
                Manifestfile manifestfile = CCommonData.Analysispeizhifile(mainini);
                userinfolist = JsonConvert.DeserializeObject<List<userinfo>>(manifestfile.userinfo);
                //遍历赋值
                DataTable usdt = new DataTable();
                userdt = new DataTable();
                if (userinfolist.Count > 0)
                {

                    usdt.Columns.Add("account");
                    usdt.Columns.Add("acc_Cookie");
                    for (int i = 0; i < userinfolist.Count; i++)
                    {
                        DataRow nusr = usdt.NewRow();

                        nusr["account"] = userinfolist[i].account;
                        nusr["acc_Cookie"] = userinfolist[i].cookie;


                        if (i == 0)
                        {
                            this.Tb_username1.Text = userinfolist[i].account;
                            this.Tb_Cookie.Text = userinfolist[i].cookie;
                        }
                        usdt.Rows.Add(nusr);

                        userdt = usdt;
                    }
                }
                BindingSource bs = new BindingSource();
                bs.DataSource = usdt;
                this.dataGridView_acc.DataSource = bs;
            }
            #endregion
        }
    }
}
