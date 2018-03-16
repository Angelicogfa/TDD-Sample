using System.Collections.Generic;

namespace Projeto.Dominio
{
    public interface ISagaNotification
    {
        bool ExisteErros { get; }
        bool ExisteAlerta { get; }

        IEnumerable<string> ObterErros();
        IEnumerable<string> ObterAlertas();
    }
}
