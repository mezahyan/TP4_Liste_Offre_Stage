using System;
using System.Collections.Generic;
using System.Text;

namespace testMVVC.Models
{
    public class OffreStage
    {
        public int IdOffre {  get; set; }
        public string TitreOffre {  get; set; } = string.Empty;
        public string SecteurOffre { get; set; } = string.Empty;
        public string StatutOffre { get; set; } = "Ouverte";
        public string DescriptionOffre { get; set; } = string.Empty;
        public string DureeOffre { get; set; } = string.Empty;
        public string? RemunerationOffre { get; set; }
        public List<string> TechnologiesRequisesOffre { get; set; } = new List<string>();

        // Cette propriété transforme automatiquement la liste en texte séparé par des virgules
        public string TechnologiesAffichage => TechnologiesRequisesOffre != null ? string.Join(", ", TechnologiesRequisesOffre) : string.Empty;


        //Clé étrangère
        public int IdEntreprise { get; set; }

        // Optionnel : Permet de stocker l'objet complet de l'entreprise si on fait une jointure manuelle
        public string? NomEntreprise { get; set; }

    }
}
