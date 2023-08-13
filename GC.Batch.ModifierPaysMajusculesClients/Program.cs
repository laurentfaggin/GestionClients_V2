using GC.DAL.JSON;
using GC.DAL.XML;
using GC.Entites;
using Microsoft.Extensions.Configuration;
using Unity;

namespace GC.Batch.ModifierPaysMajusculesClients
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IUnityContainer conteneur = new UnityContainer();
            IConfigurationRoot configuration = new ConfigurationBuilder()
                                                    .SetBasePath(Directory.GetParent(AppContext.BaseDirectory).FullName)
                                                    .AddJsonFile("appsettings.json", false)
                                                    .Build();

            string repertoireDepotClient = configuration["RepertoireDepotsClients"];
            string nomFichierDepotClient = configuration["NomFichierDepotClients"];
            string cheminComplet = Path.Combine(repertoireDepotClient, nomFichierDepotClient);
            string typeDepot = configuration["TypeDepot"];
            switch (typeDepot.ToLower())
            {
                case "json":
                    conteneur.RegisterType<IDepotClients, DepotClientsJSON>(TypeLifetime.Singleton, new Unity.Injection.InjectionConstructor(new object[] { cheminComplet }));
                    break;
                case "xml":
                    conteneur.RegisterType<IDepotClients, DepotClientsXML>(TypeLifetime.Singleton, new Unity.Injection.InjectionConstructor(new object[] { cheminComplet }));
                    break;
                default:
                    throw new InvalidOperationException("le type de dépot n'est pas valide, mettre json ou xml");
            }

            conteneur.RegisterType<ITraitementLot, ModifierPaysMajusculesClientsTraitementLot>(TypeLifetime.Singleton, new Unity.Injection.InjectionConstructor(new object[] { cheminComplet }));
            ITraitementLot tl = conteneur.Resolve<ModifierPaysMajusculesClientsTraitementLot>();
            tl.Executer();
        }
    }
}