using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MyTasks.Handlers;
using MyTasks.ViewModels;
using MyTasks.Views;

namespace MyTasks
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Raleway-Light.ttf", "RalewayLight");
                    fonts.AddFont("RobotoSlab-Medium.ttf", "RobotoSlabMedium");
                }).UseMauiCommunityToolkit(); ;

            //Resolve o problema de border de picker e entry
            UIHandler.RemoveBorders();

            //Resolve o problema de margin no ShellTitleView no android
            Microsoft.Maui.Handlers.ToolbarHandler.Mapper.AppendToMapping("CustomNavigationView", (handler, view) =>
            {
                handler.PlatformView.ContentInsetStartWithNavigation = 0;
                handler.PlatformView.SetContentInsetsAbsolute(0, 0);
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            //ViewModels
            builder.Services.AddSingleton<MainViewModel>();

            //Views 
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<HelpPage>();

            return builder.Build();
        }
    }
}
