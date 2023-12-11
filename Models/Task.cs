using CommunityToolkit.Mvvm.ComponentModel;

namespace MyTasks.Models
{
    public partial class Task : ObservableObject
    {
        public long Id { get; set; }

        public string Titulo { get; set; } = "Nova Tarefa";

        public string Descricao { get; set; } = string.Empty;

        public bool IsCompleted { get; set; }
        
        public DateTime DataCriada { get; set; }

        public DateTime DataMarcada { get; set; } = DateTime.Now;

        [ObservableProperty]
        int taskStatusIndex;
    }
}
