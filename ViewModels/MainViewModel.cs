using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyTasks.AppData;
using System.Collections.ObjectModel;
using TaskStatus = MyTasks.Models.TaskStatus;
using ThreadingTask = System.Threading.Tasks.Task;
using Task = MyTasks.Models.Task;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Alerts;
using MyTasks.Models;
using MyTasks.Views;

namespace MyTasks.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private const string TarefaTituloPadrao = "Nova Tarefa";

        public ObservableCollection<Task> Tasks { get; set; } = [];

        public MainViewModel()
        {
            tarefaSelecionada = new();
            isNovaTarefaButtonEnabled = true;
            isFiltroButtonEnabled = true;
            isAjudarButtonEnabled = true;
            GetTasks();
        }

        [ObservableProperty]
        bool isFiltroOpen, isNovaTarefaMenuOpen, isNovaTarefaButtonEnabled, isFiltroButtonEnabled, isAjudarButtonEnabled;

        [ObservableProperty]
        int filtroIndex;

        [ObservableProperty]
        Task tarefaSelecionada;

        [ObservableProperty]
        DateTime dataAtual = GetDataAtual();

        [RelayCommand]
        void OpenFiltro()
        {
            IsFiltroOpen = true;
            IsNovaTarefaButtonEnabled = false;
            IsFiltroButtonEnabled = false;
            IsAjudarButtonEnabled = false;
        }

        [RelayCommand]
        void CloseFiltro()
        {
            IsFiltroOpen = false;
            IsNovaTarefaButtonEnabled = true;
            IsFiltroButtonEnabled = true;
            IsAjudarButtonEnabled = true;
        }

        [RelayCommand]
        void OpenNovaTarefaMenu()
        {
            IsNovaTarefaMenuOpen = true;
            IsNovaTarefaButtonEnabled = false;
            IsFiltroButtonEnabled = false;
            IsAjudarButtonEnabled = false;
        }

        [RelayCommand]
        void CloseNovaTarefaMenu()
        {
            IsNovaTarefaMenuOpen = false;
            IsNovaTarefaButtonEnabled = true;
            IsFiltroButtonEnabled = true;
            IsAjudarButtonEnabled = true;
            TarefaSelecionada = new();
            GetTasks();
        }

        [RelayCommand]
        void AdicionarTarefa()
        {
            if (!IsRequisitosConcluídos())
                return;

            if (TarefaSelecionada.Id == 0)
            {
                DataManager.AdicionarTask(TarefaSelecionada);
                GetTasks();
                MakeToast("A tarefa foi adicionada com sucesso!");
            }
            else
            {
                DataManager.EditarTask(TarefaSelecionada);
                GetTasks();
                MakeToast("A tarefa foi editada com sucesso!");
            }

            CloseNovaTarefaMenu();
        }

        [RelayCommand]
        async ThreadingTask ExcluirTarefa(Task task)
        {
            var acaoConfirmada = await Shell.Current.DisplayAlert("Alerta!", "Tem certeza de que deseja excluir esta tarefa? Esta ação é irreversível e removerá permanentemente a tarefa do sistema.", "Sim", "Não");
            if (acaoConfirmada)
            {
                DataManager.ExcluirTask(task);
                GetTasks();
                MakeToast("A tarefa foi excluida com sucesso!");
            }
        }

        [RelayCommand]
        void ConcluirTarefa(Task task)
        {
            task.TaskStatusIndex = (int)TaskStatus.Concluida;
            DataManager.EditarTask(task);
            GetTasks();
            MakeToast("A tarefa foi concluida com sucesso!");
        }

        [RelayCommand]
        void AbrirTarefa(Task task)
        {
            TarefaSelecionada = task;
            OpenNovaTarefaMenu();
        }

        [RelayCommand]
        async ThreadingTask NavigatetoHelpPage()
        {
            await Shell.Current.GoToAsync(nameof(HelpPage));
        }

        partial void OnFiltroIndexChanged(int value)
        {
            GetTasks();
        }

        partial void OnTarefaSelecionadaChanged(Task value)
        {
            TarefaSelecionada.Titulo = value.Id == 0 ? TarefaTituloPadrao : $"Tarefa {value?.Id}";
        }

        private void GetTasks()
        {
            Tasks.Clear();
            var tasksSalvas = DataManager.GetTasks();
            static bool IsPriorityStatus(TaskStatus statusIndex) => statusIndex == TaskStatus.PrioridadeBaixa || statusIndex == TaskStatus.PrioridadeMedia || statusIndex == TaskStatus.PrioridadeAlta;

            switch ((TaskFiltro)FiltroIndex)
            {
                case TaskFiltro.Concluidas: tasksSalvas = [.. DataManager.GetTasks().Where(w => (TaskStatus)w.TaskStatusIndex == TaskStatus.Concluida).OrderByDescending(w => w.DataMarcada)];
                    break;

                case TaskFiltro.PrioridadeCrescente: tasksSalvas = [.. DataManager.GetTasks().Where(w => IsPriorityStatus((TaskStatus)w.TaskStatusIndex)).OrderBy(w => w.TaskStatusIndex).ThenBy(w => w.DataMarcada)];
                    break;

                case TaskFiltro.PrioridadeDecrescente: tasksSalvas = [.. DataManager.GetTasks().Where(w => IsPriorityStatus((TaskStatus)w.TaskStatusIndex)).OrderByDescending(w => w.TaskStatusIndex).ThenBy(w => w.DataMarcada)];
                    break;

                case TaskFiltro.DataCriacaoCrescente: tasksSalvas = [.. DataManager.GetTasks().OrderBy(w => w.DataCriada)];
                    break;
                
                case TaskFiltro.DataCriacaoDecrescente: tasksSalvas = [.. DataManager.GetTasks().OrderByDescending(w => w.DataCriada)];
                    break;

                case TaskFiltro.DataMarcadaCrescente: tasksSalvas = [.. DataManager.GetTasks().OrderBy(w => w.DataMarcada)];
                    break;

                case TaskFiltro.DataMarcadaDecrescente: tasksSalvas = [.. DataManager.GetTasks().OrderByDescending(w => w.DataMarcada)];
                    break;
            }

            foreach (var task in tasksSalvas) 
                Tasks.Add(task);
        }

        private bool IsRequisitosConcluídos()
        {
            if(string.IsNullOrWhiteSpace(TarefaSelecionada.Descricao))
            {
                MakeToast("Uma descrição deve ser fornecida!");
                return false;
            }

            return true;
        }

        private static async void MakeToast(string message)
        {
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(message, duration, fontSize);

            await toast.Show();
        }

        private static DateTime GetDataAtual()
        {
            return DateTime.Now;
        }
    }
}
