using GalaSoft.MvvmLight.Ioc;

namespace Tm.English
{
    public static class ViewModelLocator
    {
        public static TService GetService<TService>()
        {
            return SimpleIoc.Default.GetInstance<TService>();
        }

        public static TViewModel GetInstance<TViewModel>(bool newInstance = true)
            where TViewModel : class
        {
            if (!SimpleIoc.Default.IsRegistered<TViewModel>())
            {
                SimpleIoc.Default.Register<TViewModel>();
            }

            if (newInstance)
                return SimpleIoc.Default.GetInstanceWithoutCaching<TViewModel>();
            return SimpleIoc.Default.GetInstance<TViewModel>();
        }
    }
}
