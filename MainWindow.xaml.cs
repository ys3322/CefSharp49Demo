using CefSharp;
using CefSharp.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CefSharp49Demo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            // 注册全局js对象

            //csObject.print('11') // 同步执行
            Browser.RegisterJsObject("csObject", new Test(Browser));

            //csAsyncObject.print('11').then(aa => console.log(aa)) // 异步执行 js调用后 结果通过promise包裹
            Browser.RegisterAsyncJsObject("csAsyncObject", new Test(Browser));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Browser.ShowDevTools();
        }
    }

    public class Test
    {
        ChromiumWebBrowser Browser;
        public Test() { }
        public Test(ChromiumWebBrowser Browser) {
            this.Browser = Browser;
        }
        // => js调用方式 csObject.print("{index:1}");
        public string print(string inParams) {
            //MessageBox.Show("C# Hello!");
            Console.WriteLine("C# Hello!...");

            // 执行业务 例如打印

            Console.WriteLine("C# 打印处理中: ... ...");

            Thread.Sleep(2000); //延时2s

            // => 处理成功 调用js方法
            Browser.GetBrowser().MainFrame.ExecuteJavaScriptAsync("window.alert('打印成功！')");

            // 执行业务 例如打印
            return $"C# 处理成功: 入参：{inParams}; 打印业务成功：耗时2S.";
        }
    }
}
