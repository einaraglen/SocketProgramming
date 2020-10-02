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

        Client c = new Client();

        public MainWindow() {
            InitializeComponent();
            StartUp();

            button.Click += button_Click;
        }

        public void StartUp() {
            Server s = new Server();
            Thread RunCaller = new Thread(new ThreadStart(s.Run));
            // Start the threads.
            RunCaller.Start();
        }

        private void button_Click(object sender, RoutedEventArgs e) {
            //Client c = new Client();
            Random r = new Random();
            c.Send("Random NR " + r.Next(10, 50)); ;
        }
    }
}
