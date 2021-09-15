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
    public partial class Breedtest : Form
    {

        string g_UCookie = "BAIDUID=58A38719A3EF5449FFF450317A964CCD:FG=1; PSTM=1518579541; BIDUPSID=4BC1C6D198B0CD389D573539C57CD42E; BDUSS=lXcWk4UVY4cGFndnhFNXJRfkxFVTU0TlZjT043NldCZHJtSmZUQ1ozRGEzdnRhQVFBQUFBJCQAAAAAAAAAAAEAAADL5vIgaGZ6dGIxAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAANpR1FraUdRaZX; STOKEN=8bd7fc18d935d53b187387ba7c24768f6981588d148cf36fba6547c9a538c4bc; Hm_lvt_7885d5e68d877966f4abc55d4c798a0c=1533005388,1533111500,1533111751,1533111771; Hm_lpvt_7885d5e68d877966f4abc55d4c798a0c=1533111771";
        verimg nowverimg = new verimg();
        string fapetid = string.Empty;
        string mapetid = string.Empty;
        DataTable userdt = new DataTable();
        string namount = string.Empty;

        string g_seed = string.Empty;//购买狗狗的seed
        signledoginfo g_tmpdoginfo = new signledoginfo();//计划购买的狗狗信息
        laicishouji dm = new laicishouji();
        //实例化用户
        Dictionary<string, string> dicuser = new Dictionary<string, string>(); 
        public Breedtest()
        {
            InitializeComponent();
            DataTable usdt = new DataTable();
            userdt = new DataTable();
            List<userinfo> Ltuserinfo = dm.getUserInfoformaccess();
            if (Ltuserinfo.Count > 0)//数据库有值-无需转换
            {

                usdt.Columns.Add("account");
                usdt.Columns.Add("Cookie");
                for (int i = 0; i < Ltuserinfo.Count; i++)
                {
                    DataRow nusr = usdt.NewRow();

                    nusr["account"] = Ltuserinfo[i].account;
                    nusr["Cookie"] = Ltuserinfo[i].cookie;


                    if (i == 0)
                    {
                        this.Tb_username1.Text = Ltuserinfo[i].account;
                        this.Tb_Cookie.Text = Ltuserinfo[i].cookie;
                    }
                    usdt.Rows.Add(nusr);
                    dicuser.Add(Ltuserinfo[i].account, Ltuserinfo[i].cookie);
                    userdt = usdt;
                }
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = usdt;
            this.dataGridView_acc.DataSource = bs;





            #region//获取用户信息
            /*string userinfo = CCommonData.Readtxt("userinfo");
            if ((userinfo != null) && (userinfo != ""))
            {
                List<userinfo> userinfolist = new List<userinfo>();
                userinfolist = JsonConvert.DeserializeObject<List<userinfo>>(userinfo);
                //遍历赋值
                DataTable usdt = new DataTable();
                userdt = new DataTable();
                if (userinfolist.Count > 0)
                {

                    usdt.Columns.Add("account");
                    usdt.Columns.Add("Cookie");
                    for (int i = 0; i < userinfolist.Count; i++)
                    {
                        DataRow nusr = usdt.NewRow();

                        nusr["account"] = userinfolist[i].account;
                        nusr["Cookie"] = userinfolist[i].cookie;


                        if (i == 0)
                        {
                            this.Tb_username1.Text = userinfolist[i].account;
                            this.Tb_Cookie.Text = userinfolist[i].cookie;
                        }
                        usdt.Rows.Add(nusr);
                        dicuser.Add(userinfolist[i].account, userinfolist[i].cookie);
                        userdt = usdt;
                    }
                }
                BindingSource bs = new BindingSource();
                bs.DataSource = usdt;
                this.dataGridView_acc.DataSource = bs;
            }
            */
            g_UCookie = this.Tb_Cookie.Text;
            #endregion
        }

        private void Bt_shuxingchaxun_Click(object sender, EventArgs e)
        {
            string fdogpetid = string.Empty;
            string fdogid = this.Tb_fdogid.Text.Trim();
            fdogpetid = CCommonData.GetDogPetidByID(fdogid);

            string mdogpetid = string.Empty;
            string mdogid = this.Tb_mdogid.Text.Trim();
            mdogpetid = CCommonData.GetDogPetidByID(mdogid);

            string getiddog = CCommonData.DogManagedataByID(fdogpetid);
            ArrayList tmparram = CCommonData.Getsignledoginfo(getiddog);
            //反序列化dog信息
            signledoginfo tmpdoginfo = CCommonData.Deserializesignledog(tmparram);

            if (tmpdoginfo != null)
            {
                this.Lb_fshuxing.Text = tmpdoginfo.attributes[0].value+"-"+
                                        tmpdoginfo.attributes[1].value + "-" +
                                        tmpdoginfo.attributes[2].value + "-" +
                                        tmpdoginfo.attributes[3].value + "-" +
                                        tmpdoginfo.attributes[4].value + "-" +
                                        tmpdoginfo.attributes[5].value + "-" +
                                        tmpdoginfo.attributes[6].value + "-" +
                                        tmpdoginfo.attributes[7].value ;
                this.Lb_fdengji.Text = tmpdoginfo.rareDegree + "-" + tmpdoginfo.coolingInterval;
                this.Lb_breedprice.Text = "繁殖价格：" + tmpdoginfo.amount;
                fapetid = tmpdoginfo.petId;
                namount = tmpdoginfo.amount.ToString() ;
            }

             getiddog = CCommonData.DogManagedataByID(mdogpetid);
             tmparram = CCommonData.Getsignledoginfo(getiddog);
            //反序列化dog信息
             tmpdoginfo = CCommonData.Deserializesignledog(tmparram);

            if (tmpdoginfo != null)
            {
                this.lb_mshuxing.Text = tmpdoginfo.attributes[0].value + "-" +
                                        tmpdoginfo.attributes[1].value + "-" +
                                        tmpdoginfo.attributes[2].value + "-" +
                                        tmpdoginfo.attributes[3].value + "-" +
                                        tmpdoginfo.attributes[4].value + "-" +
                                        tmpdoginfo.attributes[5].value + "-" +
                                        tmpdoginfo.attributes[6].value + "-" +
                                        tmpdoginfo.attributes[7].value;
                this.Lb_mdengji.Text = tmpdoginfo.rareDegree + "-" + tmpdoginfo.coolingInterval;
                mapetid = tmpdoginfo.petId;
            }

            //获取验证码
            nowverimg = Getvercode(g_UCookie, tmpdoginfo);
        }
        //获取验证码
        internal verimg Getvercode(string g_UCookie,signledoginfo tmpdoginfo)
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
                Referer = "https://pet-chain.duxiaoman.com/chain/detail?channel=market&petId=" + tmpdoginfo.petId + "&validCode=&appId=1&tpl=",//来源URL     可选项   
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
            string vercodehtml = "<img src='" + newvercodeback .data+ "'>";

            
            string tmpst = newvercodeback.data.Split('=')[3];
            int tt = tmpst.Length;
            g_seed = tmpst;
            //this.Wb_vercode.Document.Write(vercodehtml);

            this.Wb_vercode.DocumentText = vercodehtml;

            /*

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


            verimg tmpverimg = new verimg();
            string cookie = result.Cookie;
            string str = GetBase64str(html);
            byte[] arr = Convert.FromBase64String(html);
            if (str != "")
            {
                arr = Convert.FromBase64String(str);
                MemoryStream ms = new MemoryStream(arr);
                Bitmap bmp = new Bitmap(ms);
                Image returnImage = bmp;

                tmpverimg.verimage = returnImage;
                tmpverimg.verseed = Getseedstr(html);
                tmpverimg.mes = ms;

                this.Pb_vercode.Image = returnImage;
                
                //string path = Application.StartupPath;
                //returnImage.Save(Guid.NewGuid() + ".png", System.Drawing.Imaging.ImageFormat.Png);
            }*/
            verimg tmpverimg = new verimg();
            return tmpverimg;
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
        //保存图片
        private static Image Base64StringToImage(string txt)
        {
            byte[] arr = Convert.FromBase64String(txt);
            MemoryStream ms = new MemoryStream(arr);
            Bitmap bmp = new Bitmap(ms);
            return bmp;
        }

        private void Bt_breed_Click(object sender, EventArgs e)
        {
            breedsource nbreedsource = new breedsource();
            nbreedsource.petId = fapetid;
            nbreedsource.senderPetId = mapetid;
            nbreedsource.amount = namount;
            nbreedsource.validCode = "";
            nbreedsource.captcha = this.Tb_vercode.Text;
            //nbreedsource.seed = nowverimg.verseed;g_seed
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
                Cookie = g_UCookie,//字符串Cookie     可选项   
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

            if(html.Contains("success"))
            {
                this.Lb_breedbackinfo.Text = "繁殖成功，必出大件！" + DateTime.Now.ToString();
            }
            else
            {
                this.Lb_breedbackinfo.Text = html + DateTime.Now.ToString();
            }
        }

        private void dataGridView_acc_DoubleClick(object sender, EventArgs e)
        {
            int index = dataGridView_acc.CurrentRow.Index;
            this.Tb_username1.Text = dataGridView_acc.Rows[index].Cells["account"].Value.ToString();
            this.Tb_Cookie.Text = dataGridView_acc.Rows[index].Cells["Cookie"].Value.ToString();
            g_UCookie = this.Tb_Cookie.Text;
        }

        private void Bt_cleardata_Click(object sender, EventArgs e)
        {
            this.Tb_fdogid.Text = "";
            this.Tb_mdogid.Text = "";
            this.lb_mshuxing.Text = "";
            this.Lb_fshuxing.Text = "";
            this.Lb_fdengji.Text = "";
            this.Lb_mdengji.Text = "";
            this.Tb_vercode.Text = "";
            this.Lb_breedprice.Text = "繁殖价格：";
            this.Lb_breedbackinfo.Text = ""; 
        }

        private void BT_dingxiangbuy_Click(object sender, EventArgs e)
        {
            string dogpetid = string.Empty;
            string dogid = this.Tb_dogbuyid.Text.Trim();


            if (dogid.Length <= 8)
            {
                dogpetid = CCommonData.GetDogPetidByID(dogid);
                if (dogpetid == "")
                {
                    MessageBox.Show("获取petid失败！");
                }
            }
            else
                dogpetid = dogid;


            string getiddog = CCommonData.DogManagedataByID(dogpetid);
            ArrayList tmparram = CCommonData.Getsignledoginfo(getiddog);
            //反序列化dog信息
            signledoginfo tmpdoginfo = CCommonData.Deserializesignledog(tmparram);
            g_tmpdoginfo = tmpdoginfo;
            if (tmpdoginfo.id != 0)
            {
                this.Lb_fshuxing.Text = tmpdoginfo.attributes[0].value + "-" +
                                        tmpdoginfo.attributes[1].value + "-" +
                                        tmpdoginfo.attributes[2].value + "-" +
                                        tmpdoginfo.attributes[3].value + "-" +
                                        tmpdoginfo.attributes[4].value + "-" +
                                        tmpdoginfo.attributes[5].value + "-" +
                                        tmpdoginfo.attributes[6].value + "-" +
                                        tmpdoginfo.attributes[7].value;
                this.Lb_fdengji.Text = tmpdoginfo.rareDegree + "-" + tmpdoginfo.coolingInterval;
                this.Lb_breedprice.Text = "销售/繁殖价格：" + tmpdoginfo.amount+"微积分";
                //fapetid = tmpdoginfo.petId;
                //namount = tmpdoginfo.amount.ToString();
            }
            


            //获取验证码
            nowverimg = Getvercode(g_UCookie, tmpdoginfo);
        }

        private void BT_buynow_Click(object sender, EventArgs e)
        {

            string html = BuyDog(this.Tb_vercode.Text.Trim(), g_seed, g_tmpdoginfo, g_UCookie);

            if (html.Contains("success"))
            {
                this.Lb_breedbackinfo.Text = "购买成功，必出大件！" + DateTime.Now.ToString();
            }
            else
            {
                this.Lb_breedbackinfo.Text = html + DateTime.Now.ToString();
            }
        }

        //购买 狗狗
        internal static string BuyDog(string vercode, string seed, signledoginfo tmpdoginfo, string g_UCookie)
        {
            string tmpresult = string.Empty;
            //判断狗狗是否在小黑屋
            HttpHelper http = new HttpHelper();
            string postdata = string.Empty;
            //Json.NET序列化
            /*{"petId":"1855454046857905147","amount":"999999999.99","seed":"1521150526","captcha":"2wep","validCode"
:"","requestId":1523259005572,"appId":1,"tpl":"","timeStamp":null,"nounce":null,"token":null}*/
            shouldJump sjdata = new shouldJump();
            
            sjdata.appId = 1;
            sjdata.nounce = null;
            sjdata.phoneType = "android";
            sjdata.requestId = CCommonData.GetTimeLikeJS();
            sjdata.timeStamp = null;
            sjdata.token = null;
            sjdata.tpl = "";
            string sjjsonData = JsonConvert.SerializeObject(sjdata);


            buydogcode dgdata = new buydogcode();
            
            dgdata.amount = tmpdoginfo.amount;
            dgdata.appId = 1;
            dgdata.captcha = vercode;
            dgdata.nounce = null;
            dgdata.petId = tmpdoginfo.petId;
            dgdata.phoneType = "android";
            dgdata.requestId = CCommonData.GetTimeLikeJS();
            dgdata.seed = seed;
            dgdata.timeStamp = null;
            dgdata.token = null;
            dgdata.tpl = "";
            dgdata.validCode = "";
            string jsonData = JsonConvert.SerializeObject(dgdata);
            HttpItem item = new HttpItem()
            {
                URL = "https://pet-chain.duxiaoman.com/data/market/shouldJump2JianDan",//URL     必需项  https://pet-chain.duxiaoman.com/data/market/shouldJump2JianDan
                Accept = "application/json",
                Method = "POST",//URL     可选项 默认为Get    
                Cookie = g_UCookie,//字符串Cookie     可选项   
                //Cookie = cks,//字符串Cookie     可选项   
                Referer = "https://pet-chain.duxiaoman.com/chain/detail?channel=market&petId=" + tmpdoginfo.petId + "&validCode=&appId=1&tpl=",//来源URL     可选项   
                Postdata = sjjsonData,//Post数据     可选项GET时不需要写   

                UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.119 Mobile Safari/537.36",//用户的浏览器类型，版本，操作系统     可选项有默认值   
                ContentType = "application/json",//返回类型    可选项有默认值   
                ProxyIp = "",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数     
            };
            HttpResult result = http.GetHtml(item);
            string html = result.Html;
            item = new HttpItem()
            {
                URL = "https://pet-chain.duxiaoman.com/data/txn/sale/create",//URL     必需项  https://pet-chain.duxiaoman.com/data/txn/sale/create
                Accept = "application/json",
                Method = "POST",//URL     可选项 默认为Get    
                Cookie = g_UCookie,//字符串Cookie     可选项   
                //Cookie = cks,//字符串Cookie     可选项   
                KeepAlive = false,
                Referer = "https://pet-chain.duxiaoman.com/chain/detail?channel=market&petId=" + tmpdoginfo.petId + "&appId1=&validCode=",//来源URL     可选项   
                Postdata = jsonData,//Post数据     可选项GET时不需要写   
                UserAgent = "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.119 Mobile Safari/537.36",//用户的浏览器类型，版本，操作系统     可选项有默认值   
                ContentType = "application/json",//返回类型    可选项有默认值   
                ProxyIp = "",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数     
            };
            result = http.GetHtml(item);
            html = result.Html;


            tmpresult = html;

            return tmpresult;

        }

        private void Bt_refvercode_Click(object sender, EventArgs e)
        {
            this.Lb_breedbackinfo.Text = string.Empty;
            nowverimg = Getvercode(g_UCookie, g_tmpdoginfo);
        }
    }
}
