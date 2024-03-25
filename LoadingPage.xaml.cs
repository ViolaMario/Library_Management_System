using LibraryManager.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LibraryManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoadingPage : Page
    {
        public LoadingPage()
        {
            this.InitializeComponent();
            FetchData();
        }

        private async void FetchData()
        {
            // 在后台线程中执行文件读取操作
            await Task.Run(() =>
            {
                string fileName = "LibraryData.json";
                string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                // 确保文件存在
                if (File.Exists(jsonFilePath))
                {
                    // 从本地文件读取 JSON 数据
                    string jsonString = File.ReadAllText(jsonFilePath);

                    LibraryJSONSerializer serializer = new LibraryJSONSerializer();
                    var data = serializer.ParseJSON(jsonString);

                    // 更新数据
                    MemberStore.Instance.members = data.Members;
                    BookStore.Instance.books = data.Books;
                    DVDStore.Instance.DVDs = data.DVDs;
                }
                else
                {
                    // 文件不存在
                    Console.WriteLine($"File '{fileName}' not found.");
                }
            });

            // 完成文件读取后，加载页面
            LoadMembersPage();

            //var task = NetworkManager.FetchJSONData("https://raw.githubusercontent.com/mradazzouz/DevSA2022/main/LibraryData.json");
            //bool result = await task;
            //if (result)
            //{
            //    LoadMembersPage();
            //}
        }

        //Loading a new Page
        private void LoadMembersPage()
        {
            this.Frame.Navigate(typeof(MemberSearchPage));
        }
    }
}
