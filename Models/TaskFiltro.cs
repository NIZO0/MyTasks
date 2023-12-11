using System.ComponentModel;

namespace MyTasks.Models
{
    public enum TaskFiltro
    {
        [Description("Mostra todas as tarefas.")]
        Todas,

        [Description("Mostra só as tarefas concluídas.")]
        Concluidas,

        [Description("Mostra as tarefas com prioridade em ordem crescente.")]
        PrioridadeCrescente,

        [Description("Mostra as tarefas com prioridade em ordem decrescente.")]
        PrioridadeDecrescente,

        [Description("Mostra as tarefas de acordo com a data de criação em ordem crescente.")]
        DataCriacaoCrescente,

        [Description("Mostra as tarefas de acordo com a data de criação em ordem decrescente.")]
        DataCriacaoDecrescente,

        [Description("Mostra as tarefas de acordo com a data marcada em ordem crescente.")]
        DataMarcadaCrescente,

        [Description("Mostra as tarefas de acordo com a data marcada em ordem decrescente.")]
        DataMarcadaDecrescente,
    }
}
