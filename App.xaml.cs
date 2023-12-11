using MyTasks.Shells;

namespace MyTasks
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //Current.Resources["TarefaAbertaLightColor"] = Colors.Black;


            MainPage = new AppShell();
        }
    }
}
