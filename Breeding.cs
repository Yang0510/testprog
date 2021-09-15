using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

namespace DogManage
{
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class Breeding : Form
    {
        verimg nowverimg = new verimg();
        string fapetid = string.Empty;
        string mapetid = string.Empty;
        string namount = string.Empty;
        string g_seed = string.Empty;//繁殖/购买狗狗的seed
        public Breeding()
        {
            InitializeComponent();
            string str = string.Empty;
            str = System.Environment.CurrentDirectory;
            str = str + "\\Breedingpage.htm";
            GetMainHtml(str);
            WB_breeding.ObjectForScripting = this;//important
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        //加载html文件
        public void GetMainHtml(string html)
        {
            string str = System.Environment.CurrentDirectory;
            str = str + "\\" + html;
            WB_breeding.Navigate(html);
            //Wb_newbreed.ObjectForScripting = this;//important
            string breedid = CCommonData.g_breed_user_petId;
            //Redrawbreedinfohead();
        }

        private void Wb_breeding_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            //绘制验证码
            Redrawvercode();
        }
        //绘制验证码
        private void Redrawvercode()
        {

           
        }
        //获取验证码
        internal verimg Getvercode(string g_UCookie, string gouchacha_petId)
        {
            HttpHelper http = new HttpHelper();

            //https://pet-chain.duxiaoman.com/data/user/pwdHint
            shouldJump sjdata = new shouldJump();

            sjdata.appId = 1;
            sjdata.nounce = null;
            sjdata.phoneType = "android";
            sjdata.requestId = CCommonData.GetTimeLikeJS();
            sjdata.timeStamp = null;
            sjdata.token = null;
            sjdata.tpl = "";
            string sjjsonData = JsonConvert.SerializeObject(sjdata);
            HttpItem item = new HttpItem()
            {
                URL = "https://pet-chain.duxiaoman.com/data/user/pwdHint",//URL     必需项  https://pet-chain.duxiaoman.com/data/market/shouldJump2JianDan
                Accept = "application/json",
                Method = "POST",//URL     可选项 默认为Get    
                Cookie = g_UCookie,//字符串Cookie     可选项   
                //Cookie = cks,//字符串Cookie     可选项   
                Referer = "https://pet-chain.duxiaoman.com/chain/detail?channel=market&petId=" + gouchacha_petId + "&validCode=&appId=1&tpl=",//来源URL     可选项   
                Postdata = sjjsonData,//Post数据     可选项GET时不需要写   

                UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.119 Mobile Safari/537.36",//用户的浏览器类型，版本，操作系统     可选项有默认值   
                ContentType = "application/json",//返回类型    可选项有默认值   
                ProxyIp = "",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数     
            };
            HttpResult result = http.GetHtml(item);
            string html = result.Html;

            string postdata = string.Empty;
            //Json.NET序列化
            vercode dgdata = new vercode();
            dgdata.requestId = CCommonData.GetTimeLikeJS();
            dgdata.appId = 1;
            dgdata.tpl = "";
            string jsonData = JsonConvert.SerializeObject(dgdata);
            item = new HttpItem()
            {
                URL = "https://pet-chain.duxiaoman.com/data/newCaptcha/getCaptchaUrl",//URL     必需项    
                Method = "Post",//URL     可选项 默认为Get   
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写   
                Cookie = g_UCookie,//字符串Cookie     可选项   
                Referer = "pet-chain.duxiaoman.com",//来源URL     可选项   
                Postdata = jsonData,//Post数据     可选项GET时不需要写   
                Timeout = 100000,//连接超时时间     可选项默认为100000    
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000   
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0",//用户的浏览器类型，版本，操作系统     可选项有默认值   
                ContentType = "application/json",//返回类型    可选项有默认值   
                Allowautoredirect = false,//是否根据301跳转     可选项   
                ProxyIp = "",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数    

                ResultType = ResultType.String
            };
            result = http.GetHtml(item);
            html = result.Html;//新的验证码是通过获取的data字段 去百度get得到
            vercodeback newvercodeback = JsonConvert.DeserializeObject<vercodeback>(html);

            string vercodehtml = "<img src='" + newvercodeback.data + "'>";
            this.WB_breeding.DocumentText = vercodehtml;




            //获取图片
            item = new HttpItem()
            {
                URL = newvercodeback.data,//URL     必需项    
                Method = "Get",//URL     可选项 默认为Get   
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写   
                Cookie = g_UCookie,//字符串Cookie     可选项   
                Referer = "pet-chain.duxiaoman.com",//来源URL     可选项   
                Postdata = jsonData,//Post数据     可选项GET时不需要写   
                Timeout = 100000,//连接超时时间     可选项默认为100000    
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000   
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0",//用户的浏览器类型，版本，操作系统     可选项有默认值   
                ContentType = "image/jpg",//返回类型    可选项有默认值   
                Allowautoredirect = false,//是否根据301跳转     可选项   
                ProxyIp = "",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数    

                ResultType = ResultType.String
            };
            result = http.GetHtml(item);
            //Image returnImage = Image.FromStream(result.Html);
            html = result.Html;

            string tmpst = newvercodeback.data.Split('=')[3];
            int tt = tmpst.Length;
            g_seed = tmpst;
            verimg tmpverimg = new verimg();
            /*string cookie = result.Cookie;
            string str = GetBase64str(html);
            byte[] arr = Convert.FromBase64String(html);//bug
            if (str != "")
            {
                arr = Convert.FromBase64String(str);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                Image returnImage = bmp;

                tmpverimg.verimage = returnImage;
                tmpverimg.verseed = Getseedstr(html);
                tmpverimg.mes = ms;


            }
            nowverimg = tmpverimg;*/
            return nowverimg;
        }
        //获取seed
        private static string Getseedstr(string html)
        {
            ArrayList Arrallnum = new ArrayList();
            string tmp = string.Empty;
            try
            {
                if (html != null)
                {
                    string pattern = @"seed.*,";//查询seed
                    Regex r = new Regex(pattern);
                    MatchCollection matches = Regex.Matches(html, pattern);
                    foreach (Match match in matches)
                    {
                        Arrallnum.Add(match.ToString());
                    }
                    string[] strselldoginfo = (string[])Arrallnum.ToArray(typeof(string));
                    tmp = strselldoginfo[0].Split('"')[2];
                }
            }
            catch
            {
                return tmp;
            }
            return tmp;
        }
        //获取base64的二进制
        private static string GetBase64str(string html)
        {
            ArrayList Arrallnum = new ArrayList();
            string tmp = string.Empty;
            try
            {
                if (html != null)
                {
                    string pattern = @"img\042:\042.*";//查询time
                    Regex r = new Regex(pattern);
                    MatchCollection matches = Regex.Matches(html, pattern);
                    foreach (Match match in matches)
                    {
                        Arrallnum.Add(match.ToString());
                    }
                    string[] strselldoginfo = (string[])Arrallnum.ToArray(typeof(string));
                    tmp = strselldoginfo[0].Split('"')[2];
                }
            }
            catch
            {
                return tmp;
            }
            return tmp;
        }
        //获取验证码
        private void BT_revercode_Click(object sender, EventArgs e)
        {
            this.TB_vercode.Text = "";
            Userget nowUserget = CCommonData.Getgetallaccinfofrombaidu(CCommonData.g_breed_User_Cookie);
            if (nowUserget != null)
            {
                nowverimg = Getvercode(CCommonData.g_breed_User_Cookie, CCommonData.g_breeding_gouchacha_petId);
            }
            else
            {
                this.LB_breedinginfo.Text = "账号登陆失败" + DateTime.Now.ToString();
            }
        }
        //进行配种
        private void BT_Breeding_Click(object sender, EventArgs e)
        {
            this.LB_breedinginfo.Text = "";
            breedsource nbreedsource = new breedsource();
            nbreedsource.petId = CCommonData.g_breeding_gouchacha_petId;
            nbreedsource.senderPetId = CCommonData.g_breed_user_petId;
            nbreedsource.amount = CCommonData.g_breed_gouchacha_amount;
            nbreedsource.validCode = "";
            nbreedsource.captcha = this.TB_vercode.Text;
            nbreedsource.seed = g_seed;
            nbreedsource.requestId = CCommonData.GetTimeLikeJS();
            nbreedsource.appId = 1;
            nbreedsource.tpl = "";
            nbreedsource.timeStamp = null;
            nbreedsource.nounce = null;
            nbreedsource.token = null;
            HttpHelper http = new HttpHelper();
            //string postdata = string.Empty;
            string jsonData = JsonConvert.SerializeObject(nbreedsource);
            HttpItem item = new HttpItem()
            {
                URL = "https://pet-chain.duxiaoman.com/data/txn/breed/create",//URL     必需项  
                Accept = "application/json",
                Method = "POST",//URL     可选项 默认为Get    
                Cookie = CCommonData.g_breed_User_Cookie,//字符串Cookie     可选项   
                //Cookie = cks,//字符串Cookie     可选项   
                KeepAlive = false,
                Referer = "https://pet-chain.duxiaoman.com/chain/chooseMyDog?validCode=&appId=1&tpl=",//来源URL     可选项   
                Postdata = jsonData,//Post数据     可选项GET时不需要写   
                UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.119 Mobile Safari/537.36",//用户的浏览器类型，版本，操作系统     可选项有默认值   
                ContentType = "application/json",//返回类型    可选项有默认值   
                ProxyIp = "",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数    

                ResultType = ResultType.String
            };
            HttpResult result = http.GetHtml(item);
            string html = result.Html;

            if (html.Contains("success"))
            {
                this.LB_breedinginfo.Text = "繁殖成功，必出大件！" + DateTime.Now.ToString();
            }
            else
            {
                this.LB_breedinginfo.Text = html + DateTime.Now.ToString();
            }
        }
    }
}
