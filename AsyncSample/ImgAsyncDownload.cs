using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AsyncSample
{
    public class ImgAsyncDownload
    {

        Func<string, string> Imagedownload = (url) =>
        {
            var clinet = new WebClient();
            string fileName = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, $"{DateTime.Now.ToString("yyyyMMddhhmmssfff")}.jpg");
            clinet.DownloadFile(url, fileName);
            return fileName;
        };

        private IAsyncResult BeginImagedownload(string url, AsyncCallback callback, object satate)
        {
            return Imagedownload.BeginInvoke(url, callback, satate);
        }

        private string EndImagedownload(IAsyncResult result)
        {
            return Imagedownload.EndInvoke(result);
        }

        public async void downloadImg()
        {
            string s = await Task<string>.Factory.FromAsync<string>(BeginImagedownload, EndImagedownload, "http://p5.urlpic.club/img1891/upload/image/20180928/92808115493.jpg", null);
            Console.WriteLine(s);
        }
    }
}
