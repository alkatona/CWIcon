using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Globalization;
using System.Windows.Threading;

namespace CWIcon
{
    public class App : ApplicationContext
    {
        IconCreator iconCreator;

        public App()
        {
            iconCreator = new IconCreator();
        }
    }
}
