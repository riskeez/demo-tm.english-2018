using Tm.Mobile.CoreX.Abstract;
using Xamarin.Forms;

namespace Tm.English.Services
{
    public class FileService : IXFileHelper
    {
        public string GetLocalFilePath(string filename) => DependencyService.Get<IXFileHelper>().GetLocalFilePath(filename);

        public string GetExternalFilePath(string filename) => DependencyService.Get<IXFileHelper>().GetExternalFilePath(filename);

        public long GetFileSize(string filePath) => DependencyService.Get<IXFileHelper>().GetFileSize(filePath);}
}
