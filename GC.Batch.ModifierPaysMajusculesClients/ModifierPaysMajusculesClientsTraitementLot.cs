﻿using GC.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.Batch.ModifierPaysMajusculesClients
{
    class ModifierPaysMajusculesClientsTraitementLot : ITraitementLot
    {
        private readonly IDepotClients m_depotClients;

        public ModifierPaysMajusculesClientsTraitementLot(IDepotClients p_depotClients)
        {
            this.m_depotClients = p_depotClients;
        }

        public void Executer()
        {
            List<Client> clients = this.m_depotClients.ListerClients();

            foreach (Client client in clients)
            {
                bool aAuMoinsUneAdresseModifiee = false;
                List<Adresse> adresses = new List<Adresse>();
                foreach (Adresse adresse in client.Adresses)
                {
                    Adresse adresseCourante = adresse;
                    if (adresse.Pays != adresse.Pays.ToUpper())
                    {
                        adresseCourante = new Adresse(
                        adresse.AdresseId,
                        adresse.NumeroCivique,
                        adresse.InformationSupplementaire,
                        adresse.Odonyme,
                        adresse.TypeVoie,
                        adresse.CodePostal,
                        adresse.NomMunicipalite,
                        adresse.Etat,
                        adresse.Pays.ToUpper()
                        );
                        aAuMoinsUneAdresseModifiee = true;
                    }
                    adresses.Add(adresseCourante);
                }

                if (aAuMoinsUneAdresseModifiee)
                {
                    foreach (Adresse adresse in adresses)
                    {
                        client.AjouterModifierAdresse(adresse);
                    }
                    this.m_depotClients.ModifierClient(client);
                }

            }
        }
    }
}
