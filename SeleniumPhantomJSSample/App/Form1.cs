using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App
{
    public partial class Form1 : Form
    {
        string url = "http://www.taoyizhu.com/";
        IWebDriver driver = new PhantomJSDriver(GetPhantomJSDriverService());
        
        public Form1()
        {
            driver.Navigate().GoToUrl(url);
          
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
            var text= await Task.Run(() => Select(textBox1.Text));
            richTextBox1.Text = text;
        }

        private string Select(string str)
        {
            
            
            IWebElement txt = driver.FindElement(By.Id("txt_name"));//获取txt_name文本框
            txt.SendKeys(str);//设置value
            var btn = driver.FindElement(By.Id("search_btn"));//获取提交按钮
            btn.Click();//发送提交请求
            Thread.Sleep(1500);//休眠1秒等待ajax返回值
            var html = driver.PageSource;
            HtmlAgilityPack.HtmlDocument hd = new HtmlAgilityPack.HtmlDocument();//使用xpath解析html
            hd.LoadHtml(html);
            string user_time = hd.DocumentNode.SelectSingleNode("//*[@id='rate_userTime']").InnerText;//注册时间
            string rate_userIdentimg = hd.DocumentNode.SelectSingleNode("//*[@id='rate_userIdent']/img").Attributes["src"].Value;//实名认证图片
            string rate_userIDent = hd.DocumentNode.SelectSingleNode("//*[@id='rate_userIdent']").InnerText;//实名认证
            string rate_sellerLink = hd.DocumentNode.SelectSingleNode("//*[@id='rate_sellerLink']").InnerText;//店铺信息
            string rate_shopType = hd.DocumentNode.SelectSingleNode("//*[@id='rate_shopType']").InnerText;//当前主营
            string rate_userArea = hd.DocumentNode.SelectSingleNode("//*[@id='rate_userArea']").InnerText;//所在地区
            string buyerTotalNormalCount = hd.DocumentNode.SelectSingleNode("//*[@id='buyerTotalNormalCount']").InnerText;//中评
            string buyerTotalBadCount = hd.DocumentNode.SelectSingleNode("//*[@id='buyerTotalBadCount']").InnerText;//差评
            string buyerNormalBadPerNumber = hd.DocumentNode.SelectSingleNode("//*[@id='buyerNormalBadPerNumber']").InnerText;//给他人中差评比例
            string spanUserBuyerCount = hd.DocumentNode.SelectSingleNode("//*[@id='buyer_ratecount']").InnerText;//买家信誉
            string user_Levelimg = hd.DocumentNode.SelectSingleNode("//*[@id='buyer_ratecount']/img").Attributes["src"].Value;//买家等级图片
            string buyer_weekRateGood = hd.DocumentNode.SelectSingleNode("//*[@id='buyer_weekRateGood']").InnerText;//最近一周
            string buyer_averageNum = hd.DocumentNode.SelectSingleNode("//*[@id='buyer_averageNum']").InnerText;//周平均
            string buyer_monthRateGood = hd.DocumentNode.SelectSingleNode("//*[@id='buyer_monthRateGood']").InnerText;//最近一月
            string buyer_yearRateGood = hd.DocumentNode.SelectSingleNode("//*[@id='buyer_yearRateGood']").InnerText;//最近半年
            string buyer_yearOldRateGood = hd.DocumentNode.SelectSingleNode("//*[@id='buyer_yearOldRateGood']").InnerText;//半年以前
            string buyer_IpAddress = hd.DocumentNode.SelectSingleNode("//*[@id='buyer_IpAddress']").InnerText;//当前Ip地址
            string seller_ratecount = hd.DocumentNode.SelectSingleNode("//*[@id='seller_ratecount']").InnerText;//卖家信誉
            string seller_weekRateGood = hd.DocumentNode.SelectSingleNode("//*[@id='seller_weekRateGood']").InnerText;//卖家最近一周
            string seller_monthRateGood = hd.DocumentNode.SelectSingleNode("//*[@id='seller_monthRateGood']").InnerText;//卖家最近一月
            string seller_yearRateGood = hd.DocumentNode.SelectSingleNode("//*[@id='seller_yearRateGood']").InnerText;//卖家最近半年
            string seller_yearOldRateGood = hd.DocumentNode.SelectSingleNode("//*[@id='seller_yearOldRateGood']").InnerText;//半年以前
            string buyer_CurrentTime = hd.DocumentNode.SelectSingleNode("//*[@id='buyer_CurrentTime']").InnerText;//当前查询时间
            string text = $"注册时间为:{user_time}\n实名认证图片：{rate_userIdentimg}\n实名认证:{rate_userIDent}\n店铺信息:{rate_sellerLink}\n当前主营:{rate_shopType}\n所在地区:{rate_userArea}\n中评:{buyerTotalNormalCount}\n差评:{buyerTotalBadCount}\n给他人中差评比例:{buyerNormalBadPerNumber}\n买家信誉:{spanUserBuyerCount}\n买家等级图片:{user_Levelimg}\n最近一周:{buyer_weekRateGood}周平均:{buyer_averageNum}\n最近一月:{buyer_monthRateGood}\n最近半年:{buyer_yearRateGood}\n半年以前:{buyer_yearOldRateGood}\n当前Ip地址:{buyer_IpAddress}\n卖家信誉:{seller_ratecount}\n卖家最近一周{seller_weekRateGood}\n卖家最近一月:{seller_monthRateGood}\n卖家最近半年:{seller_yearRateGood}\n半年以前:{seller_yearOldRateGood}\n当前查询时间:{buyer_CurrentTime} ";
            //driver.Dispose();释放内存，再程序退出的时候执行
            driver.Navigate().GoToUrl(url);
            return text;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            driver.Quit();
        }

        private static PhantomJSDriverService GetPhantomJSDriverService()
        {
            PhantomJSDriverService service = PhantomJSDriverService.CreateDefaultService();
            return service;
        }
    }
}
