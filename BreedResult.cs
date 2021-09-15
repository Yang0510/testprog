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
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class BreedResult : Form
    {
        List<userinfo> userinfolist = new List<userinfo>();
        laicishouji DogManage = laicishouji.pMainWin;
        public BreedResult()
        {
            InitializeComponent();
            string str = string.Empty;
            str = System.Environment.CurrentDirectory;
            str = str + "\\Breedresult.htm";
            GetMainHtml(str);
            this.Wb_breedresult.ObjectForScripting = this;//important
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        //加载html文件
        public void GetMainHtml(string html)
        {
            string str = System.Environment.CurrentDirectory;
            str = str + "\\" + html;
            this.Wb_breedresult.Navigate(html);
            string breedid = CCommonData.g_breed_user_petId;
        }
        //查询所有账号当日繁殖信息
        public void GettodaybreedresultBKBR()
        {
            HtmlElement hlbreedresultlist = this.Wb_breedresult.Document.GetElementById("Breedresult_breedlist");
            #region//获取用户信息
            userinfolist = DogManage.getUserInfoformaccess();
            if (userinfolist.Count > 0)
            {
                for (int i = 0; i < userinfolist.Count; i++)
                {
                    #region 获取当前账户当天剩余几次繁殖机会
                    List<mydogdingdanlistinfo> alldogddlist = new List<mydogdingdanlistinfo>();
                    Userget nowUserget = CCommonData.Getgetallaccinfofrombaidu(userinfolist[i].cookie);
                    if (nowUserget != null)
                    {
                        alldogddlist = CCommonData.Getdogddlist(userinfolist[i].cookie, 2);//获取所有订单数据，status
                    #endregion
                        foreach (var dditem in alldogddlist)
                        {
                            if ((DateTime.Parse(dditem.transDate).Date == DateTime.Now.Date) && (dditem.Status == 4))
                            {
                                hlbreedresultlist.InnerHtml += "<tr style=\"margin-left:15px;\">"
                                                            + "<td>" + userinfolist[i].account.ToString() + "</td>"
                                                            + "<td>" + dditem.id + "</td>"
                                                            + "<td style='width:70px;'></td>"
                                                            + "<td style='width:50px;'></td>"
                                                            + "<td>" + dditem.amount + "</td>"
                                                            + "<td >" + dditem.transDate + "</td>"
                                                            + "</tr>";
                            }
                        }
                    }
                }
            }
            #endregion
        }
    }
}
