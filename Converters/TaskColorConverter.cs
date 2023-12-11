using System.Globalization;
using TaskStatus = MyTasks.Models.TaskStatus;

namespace MyTasks.Convertors
{
    class TaskColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int taskStatusIndex && Application.Current != null)
            {
                var requestedTheme = Application.Current.RequestedTheme;

                return (TaskStatus)taskStatusIndex switch
                {
                    TaskStatus.Normal => Color.FromArgb("F0F0F0"),
                    TaskStatus.Concluida => Color.FromArgb("95D5B2"),
                    TaskStatus.PrioridadeBaixa => Color.FromArgb("FFDD00"),
                    TaskStatus.PrioridadeMedia => Color.FromArgb("FFAA00"),
                    TaskStatus.PrioridadeAlta => Color.FromArgb("FF7B00"),
                    _ => Colors.WhiteSmoke,
                };
            }

            return Colors.Black;
        }
        
        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
