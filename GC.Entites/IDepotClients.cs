using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.Entites
{
    public interface IDepotClients
    {
        void AjouterClient(Client p_client);
        List<Client> ListerClients();
        Client RechercherClient(Guid p_guid);
        void ModifierClient(Client p_client);
    }
}
