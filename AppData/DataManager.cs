using Task = MyTasks.Models.Task;
using Android.Content;
using Newtonsoft.Json;

namespace MyTasks.AppData
{
    public static class DataManager
    {
        public static void AdicionarTask(Task task)
        {
            task.Id = GerarId();
            task.DataCriada = DateTime.Now;
            var tasks = GetTasks();
            tasks.Add(task);
            SalvarTasks(tasks);
            SalvarUltimaTaskId(task.Id);
        }

        public static void EditarTask(Task task)
        {
            var tasks = GetTasks();
            var taskDirecionada = tasks.First(w => w.Id == task.Id);
            taskDirecionada.Descricao = task.Descricao;
            taskDirecionada.IsCompleted = task.IsCompleted;
            taskDirecionada.DataMarcada = task.DataMarcada;
            taskDirecionada.TaskStatusIndex = task.TaskStatusIndex;
            SalvarTasks(tasks);
        }

        public static void ExcluirTask(Task task)
        {
            var tasks = GetTasks();
            var taskDirecionada = tasks.First(w => w.Id == task.Id);
            tasks.Remove(taskDirecionada);
            SalvarTasks(tasks);
        }

        private static long GerarId()
        {
            return GetUltimaTaskIdSalva() + 1;
        }

        private static long GetUltimaTaskIdSalva()
        {
            try
            {
                var preferences = Android.App.Application.Context.GetSharedPreferences("MyAppSettings", FileCreationMode.Private);
                return preferences?.GetLong("TaskId", 0) ?? 0;
            }
            catch
            {
                throw;
            }
        }

        private static void SalvarUltimaTaskId(long taskId)
        {
            try
            {
                var preferences = Android.App.Application.Context.GetSharedPreferences("MyAppSettings", FileCreationMode.Private);
                var editor = preferences?.Edit();
                editor?.PutLong("TaskId", taskId);
                editor?.Apply();
            }
            catch
            {
                throw;
            }
        }

        public static void SalvarTasks(List<Task> tasks)
        {
            try
            {
                string tasksJson = JsonConvert.SerializeObject(tasks);
                var preferences = Android.App.Application.Context.GetSharedPreferences("MyAppSettings", FileCreationMode.Private);
                var editor = preferences?.Edit();
                editor?.PutString("TasksList", tasksJson);
                editor?.Apply();
            }
            catch
            {
                throw;
            }
        }

        public static List<Task> GetTasks()
        {
            try
            {
                var preferences = Android.App.Application.Context.GetSharedPreferences("MyAppSettings", FileCreationMode.Private);
                string tasksJson = preferences?.GetString("TasksList", string.Empty) ?? string.Empty;
                return JsonConvert.DeserializeObject<List<Task>>(tasksJson) ?? [];
            }
            catch
            {
                throw;
            }
        }
    }
}
