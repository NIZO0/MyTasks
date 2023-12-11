using System.ComponentModel;

namespace MyTasks.Models
{
    public enum TaskStatus
    {
        [Description("A tarefa está em estado normal.")]
        Normal,

        [Description("A tarefa está concluida.")]
        Concluida,

        [Description("A tarefa está com prioridade baixa.")]
        PrioridadeBaixa,

        [Description("A tarefa está com prioridade media.")]
        PrioridadeMedia,

        [Description("A tarefa está com prioridade alta.")]
        PrioridadeAlta,
    }
}
