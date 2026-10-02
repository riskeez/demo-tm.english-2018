using Tm.Mobile.Core;

namespace Tm.English
{
    public static class ViewModelBaseExtension
    {
        public static TService GetService<TService>(this ViewModelBase vm)
        {
            return ViewModelLocator.GetService<TService>();
        }

        public static TInstance GetInstance<TInstance>(this ViewModelBase vm, bool newInstance = true)
            where TInstance : class
        {
            return ViewModelLocator.GetInstance<TInstance>(newInstance);
        }
    }
}
