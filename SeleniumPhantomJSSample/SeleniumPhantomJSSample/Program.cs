using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
//添加selenium的引用
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

//添加引用-在程序集中添加System.Drawing
using System.Drawing.Imaging;
using OpenQA.Selenium.Chrome;
using System.IO;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;

namespace Selenium
{
    class Program
    {
        static void Main(string[] args)
        {

            //此时记得添加路径    
            using (var driver = new ChromeDriver())
            {

                //进入百度首页
                driver.Navigate().GoToUrl(@"http://www.baidu.com");
                Thread.Sleep(1000);
                //是否包含"百度"这个字符串，可以用来判断页面是否出现
                if (driver.Title == ">百度一下，你就知道")
                {
                    Console.WriteLine(" 已找到百度首页");

                }
                //设置窗体最大化
                //driver.Manage().Window.Maximize();
                //Thread.Sleep(1000);

                //找到对象     
                var colSearchBox = driver.FindElementById("kw");
                var btnClick = driver.FindElement(By.Id("su"));

                //发送搜索内容
                colSearchBox.SendKeys("京东");
                btnClick.Click();
                //等待搜索结果
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                IWebElement searchResutl = null;
                try
                {
                    searchResutl = wait.Until<IWebElement>((d) =>
                    {
                        return d.FindElement(By.Id("content_left"));

                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine("Timeout to find element:" + " " + e.Message.ToString());
                }
                //搜索结果数量
                var list = searchResutl.FindElements(By.ClassName("t"));


                //遍历有没有京东首页，负责直接打开第一个

                //foreach (IWebElement child in list)
                //{
                //    if (child.Text.Contains("京东"))
                //    {
                //        //选择正确的搜索对象
                //        child.Click();
                //        break;
                //    }
                //}

                //设置页面加载时间
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);

                //获取当前页面句柄 ，适用于一个窗体
                //var cc = driver.CurrentWindowHandle;

                //进入首页
                var homePage = driver.FindElement(By.ClassName("result"));
                var homePage_child = homePage.FindElement(By.Id("1"));
                homePage_child.FindElement(By.ClassName("favurl")).Click();

                //设置页面加载时间
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);


                //获取当前网页的句柄，使用与多个窗体
                //那么我们需要的是第二个窗体
                var currentWindowHandle = driver.WindowHandles[1];

                #region MyRegion
                //因为要现在要处理的对象在新窗体上，所以这里要进行窗体转换
                driver.SwitchTo().Window(currentWindowHandle);

                //下面将鼠标移动到左边的".Net技术",此时会滑出相应的模块
                //用XPath定位对象，此处找到"新手区"

                //移动鼠标
                //var xx = driver.FindElement(By.Id("cate_item_108698"));
                //Actions builder = new Actions(driver);
                //builder.MoveToElement(xx).Perform();

                //Thread.Sleep(2000);

                //driver.FindElement(By.Id("cate_sub_block")).FindElement(By.XPath("//a[@href='/cate/beginner/']")).Click();
                #endregion


                //登陆

                //使用PartialLinkText定位对象
                var btnLogin1 = driver.FindElement(By.PartialLinkText("登录"));
                btnLogin1.Click();
                driver.FindElement(By.PartialLinkText("账户登录")).Click();
                Thread.Sleep(200);

                var txtUserName = driver.FindElement(By.Id("loginname"));
                txtUserName.SendKeys("");//用户名

                var txtPassword = driver.FindElement(By.Id("nloginpwd"));
                txtPassword.SendKeys("");//密码

                var btnLogin2 = driver.FindElement(By.Id("loginsubmit"));
                btnLogin2.Click();
                Thread.Sleep(2000);


                //使用CssSelector定位对象
                //点击“退出”
                //var btnBackup = driver.FindElement(By.CssSelector("a[href='#']"));
                //btnBackup.Click();

                ////等待弹出框弹出后再处理它 
                //Thread.Sleep(1000);
                //IAlert result = null;
                //while (1 < 2)
                //{
                //    try
                //    {
                //        result = driver.SwitchTo().Alert();
                //    }
                //    catch (Exception)
                //    {
                //        result = null;
                //    }

                //    if (result != null)
                //    {
                //        result.Accept();
                //        break;
                //    }
                //}

                //截图
                //自动化测试中截图的图片用当前时间来命名，会起到非常不错的效果
                string pictrueName = DateTime.Now.ToString();
                if (pictrueName.Contains(':'))
                {
                    pictrueName = pictrueName.Replace(':', '_');
                }
                if (pictrueName.Contains('/'))
                {
                    pictrueName = pictrueName.Replace('/', '_');
                }



                //driver.GetScreenshot().SaveAsFile(@"C:\桌面\test\" + pictrueName + ".png", ScreenshotImageFormat.Png);
                Thread.Sleep(1000);

                //退出
                driver.Quit();
            }
        }
    }
}