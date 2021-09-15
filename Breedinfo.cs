using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;
using System.Threading;

namespace DogManage
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class Breedinfo : Form
    {
        string[] g_strgouchachapetid = new string[50];//用于保存够查查数据列表的petid
        public Breedinfo()
        {
            InitializeComponent();
            this.Wb_breedinfo.ScriptErrorsSuppressed = true;
            string str = string.Empty;
            str = System.Environment.CurrentDirectory;
            str = str + "\\Breedinfopage.htm";
            GetMainHtml(str);
            Wb_breedinfo.ObjectForScripting = this;//important
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        //加载html文件
        public void GetMainHtml(string html)
        {
            string str = System.Environment.CurrentDirectory;
            str = str + "\\" + html;
            Wb_breedinfo.Navigate(html);
            //Wb_newbreed.ObjectForScripting = this;//important
            string breedid = CCommonData.g_breed_user_petId;
            //Redrawbreedinfohead();
        }

        private void Wb_breedinfo_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //重绘head
            Redrawbreedinfohead();
        }
        //加载主页面左侧
        private void Redrawbreedinfohead()
        {
            if (CCommonData.g_nowprebreedmydog != null)
            {
                HtmlElement hlbreedinfomydog = this.Wb_breedinfo.Document.GetElementById("breedinfopage_mydoginfo");
                hlbreedinfomydog.InnerHtml += "<img src='" + CCommonData.g_nowprebreedmydog.petUrl + "' style='display: block; height:100px; width:100px;' /><div>"
                       + "<h3><span class='label label-info' style='margin-left:15px; margin-bottom:5px;'>id：" + CCommonData.g_nowprebreedmydog.id + "</span></h3>"
                       + "<h3><span class='label label-info' style='margin-left:15px; margin-bottom:5px;'>等级：" + CCommonData.Getraremiaosu(CCommonData.Getjixiformattr(CCommonData.g_nowprebreedmydog.attributes)) + "</span></h3>"
                       + "<h3><span class='label label-info' style='margin-left:15px; margin-bottom:5px;'>代数：" + CCommonData.g_nowprebreedmydog.generation + "</span></h3>"
                       + "<h3><span class='label label-info' style='margin-left:15px; margin-bottom:5px;'>稀有：" + CCommonData.Getshuxingstr(CCommonData.g_nowprebreedmydog.attributes)[0] + "</span></h3>"
                       + "<h3><span class='label label-info' style='margin-left:15px; margin-bottom:5px;'>非希：" + CCommonData.Getshuxingstr(CCommonData.g_nowprebreedmydog.attributes)[1] + "</span></h3></div>";
            }
        }
        //加载需要查询的种狗
        public void Findbreedobject(string chaxuntiaojian, string chaxungen, string chaxunxiyoudu)
        {
            int i = 0;
            HtmlElement hlgccbreedlist = this.Wb_breedinfo.Document.GetElementById("breedinfopage_breedmarketdoginfo_breedlist");
            hlgccbreedlist.InnerHtml = "";
            Allgccbackdata ltAllgccbackdata = CCommonData.searchdogdatafromlcgmarket(chaxuntiaojian, chaxungen, chaxunxiyoudu);
            //Allgccbackdata ltAllgccbackdata = CCommonData.searchdogdata(chaxuntiaojian, chaxungen);
            if (ltAllgccbackdata.ltgouchachabackdata != null)
            {
                if (ltAllgccbackdata.ltgouchachabackdata.Count > 0)
                {
                    try
                    {
                        foreach (var gouchachabackdata in ltAllgccbackdata.ltgouchachabackdata)
                        {
                            #region 处理够查查数据
                            int tmpgen = 0;
                            int.TryParse(gouchachabackdata.generation.Replace("\"", ""), out tmpgen);
                            breedzhonggou breedzhonggoubody = new breedzhonggou();
                            breedzhonggoubody.Doggen = tmpgen;
                            breedzhonggoubody.Dogprice = int.Parse(gouchachabackdata.amount.Split('.')[0].Replace("\"", ""));
                            breedzhonggoubody.Dogpetid = gouchachabackdata.dogpetid.Replace("\"", "") + "";
                            #endregion







                            hlgccbreedlist.InnerHtml += "<tr style=\"margin-left:15px;\">"
                                                    + "<td><img  src='" + gouchachabackdata.imgurl + "'  alt=''  style='display: block; height:80px; width:80px;' border='0' /></td>"
                                                    + "<td>" + gouchachabackdata.dogid.Replace("\"", "") + "</td>"
                                                    + "<td>" + breedzhonggoubody.Dogprice + "</td>"
                                                    + "<td>" + gouchachabackdata.xiyoudu + "</td>"
                                                    + "<td>" + gouchachabackdata.generation + "</td>"
                                                    + "<td>" + gouchachabackdata.xiyoushuxing + "</td>"
                                                    + "<td><button  id=\"" + breedzhonggoubody.Dogpetid + " \" class=\"btn btn-primary\" onclick=\"breednow(" + i.ToString() + ")\">选它！！！</button></td>"
                                                    + "</tr>";
                            g_strgouchachapetid[i] = breedzhonggoubody.Dogpetid;
                            i++;

                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        //加载需要查询的种狗fromid
        public void Findbreedobjectfrompetid(string zhonggoupetid)
        {
            int i = 0;
            HtmlElement hlgccbreedlist = this.Wb_breedinfo.Document.GetElementById("breedinfopage_breedmarketdoginfo_breedlist");
            hlgccbreedlist.InnerHtml = "";

                           hlgccbreedlist.InnerHtml += "<tr style=\"margin-left:15px;\">"
                                                    + "<td><button  id=\"" + zhonggoupetid + " \" class=\"btn btn-primary\" onclick=\"breednow(" + i.ToString() + ")\">选它！！！</button></td>"
                                                    + "</tr>";
                           g_strgouchachapetid[i] = zhonggoupetid;
        }

        //跳转到breedingpage界面
        public void Tobreedingpage(string PetIdnum)
        {
            //判断是否在冷冻期
            string petid = g_strgouchachapetid[int.Parse(PetIdnum)];
            string getiddog = CCommonData.DogManagedataByID(petid);
            ArrayList tmparram = CCommonData.Getsignledoginfo(getiddog);
            //反序列化dog信息
            signledoginfo tmpdoginfo = CCommonData.Deserializesignledog(tmparram);

            if (tmpdoginfo.id != 0)
            {
                CCommonData.g_nowprebreedmydog = tmpdoginfo;
                if (tmpdoginfo.isCooling != "true")
                {
                    CCommonData.g_breeding_gouchacha_petId = petid;
                    CCommonData.g_breed_gouchacha_amount = tmpdoginfo.amount.ToString();
                    Breeding fBreeding = new Breeding();
                    fBreeding.ShowDialog();
                }
                else
                {
                    MessageBox.Show("当前狗狗正在休息，请重新挑选狗狗");
                }

            }



        }
    }
}
