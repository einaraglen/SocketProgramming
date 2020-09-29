using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Threading;

namespace SocketProgramming {
    //GUI Controller
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            Server s = new Server();
            Client c = new Client();

            Thread RunCaller = new Thread(
            new ThreadStart(s.Run));
            // Start the thread.
            RunCaller.Start();

            //Waits for the server to start on different thread
            Thread.Sleep(1000);
            c.Run();
        }
    }
}
