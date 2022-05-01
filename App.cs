using System.Windows.Forms;

namespace CWIcon
{
    public class App : ApplicationContext
    {
        ApplicationCore AppCore;

        public App()
        {
            AppCore = new ApplicationCore();
        }
    }
}
