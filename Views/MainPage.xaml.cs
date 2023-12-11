using MyTasks.ViewModels;

namespace MyTasks.Views
{
    public partial class MainPage : ContentPage
    {
        SwipeView? oldSwipeView;

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

        private void SwipeView_SwipeEnded(object sender, SwipeEndedEventArgs e)
        {
            if(sender is SwipeView swipeView)
            {
                if (oldSwipeView != null && oldSwipeView != swipeView)
                    oldSwipeView.Close();
                oldSwipeView = swipeView;
            }
        }
    }

}
