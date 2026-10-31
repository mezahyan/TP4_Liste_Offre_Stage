using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using testMVVC.Models;

namespace testMVVC.ViewModels
{
    public class OffreViewModel:ViewModelBase
    {
        //Constructeur
        public OffreViewModel() { }

        //CHAMPS

        private string _titrePoste = string.Empty;
        public string TitrePoste
        {
            get => _titrePoste;
            set => SetProperty(ref _titrePoste, value);
        }

        private string _descriptionPoste = string.Empty;
        public string DescriptionPoste
        {
            get => _descriptionPoste;
            set => SetProperty(ref _descriptionPoste, value);
        }

        private string _duree = string.Empty;
        public string Duree
        {
            get => _duree;
            set => SetProperty(ref _duree, value);
        }

        private string? _remuneration;
        public string? Remuneration
        {
            get => _remuneration;
            set => SetProperty(ref _remuneration, value);
        }

        private string _statutOffre = "Ouverte";
        public string StatutOffre
        {
            get => _statutOffre;
            set => SetProperty(ref _statutOffre, value);
        }

        private List<string> _technologiesRequises = new();
        public List<string> TechnologiesRequises
        {
            get => _technologiesRequises;
            set => SetProperty(ref _technologiesRequises, value);
        }


        
    }
}
