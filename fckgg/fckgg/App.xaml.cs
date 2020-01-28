using System.Windows;

namespace fckgg
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public DisplayRootRegistry displayRootRegistry = new DisplayRootRegistry();
        public App()
        {
            displayRootRegistry.RegisterWindowType<OrderWindowViewModel, OrderWindow>();
        }
    }
}
