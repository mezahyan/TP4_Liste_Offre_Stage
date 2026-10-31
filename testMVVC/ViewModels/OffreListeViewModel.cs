using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using testMVVC.Models;
using testMVVC.Services;

namespace testMVVC.ViewModels
{
    class OffreListeViewModel : ViewModelBase
    {
        private readonly OffreRepository _offreRepository;

        // LE RÉSERVOIR DE DONNÉES (Lié au tableau de l'interface graphique)
        // L'ObservableCollection notifie automatiquement la vue lors d'un ajout, suppression ou vidage.
        public ObservableCollection<OffreStage> Offres { get; } = new();

        //pour gérer la ligne sélectionnée sur le tableau
        private OffreStage? _sel;
        public OffreStage? OffreSelectionnee
        {
            get => _sel;
            set => SetProperty(ref _sel, value);
        }

        public void AjouterOffre(OffreStage offreStage)
        {
            Offres.Add(offreStage);
        }

        public void SupprimerOffre(OffreStage offreStage)
        {
            Offres.Remove(offreStage);
        }


        // LES COMMANDES (Actions déclenchées par l'interface utilisateur)
        //Pour activier et désactiver les boutons 
        public ICommand ChargerOffresCommand { get; }
        public ICommand AjouterCommand { get; }
        public ICommand SupprimerCommand { get; }

        public OffreListeViewModel()
        {
            // Initialisation de la passerelle de données ADO.NET
            _offreRepository = new OffreRepository();

            AjouterCommand = new RelayCommand(
                _ => Offres.Add(new OffreStage()),
                _ => true
            );
            SupprimerCommand = new RelayCommand(
                _ => Offres.Remove(OffreSelectionnee),
                _ => OffreSelectionnee != null
            );

            ChargerOffresCommand = new RelayCommand(
                _ => ChargerOffres()
            );

            ChargerOffres();
        }

        // MÉTHODE DE CHARGEMENT (Le pont entre ADO.NET et le ViewModel)
        private void ChargerOffres()
        {
            // Nettoyage de la liste actuelle avant rafraîchissement
            Offres.Clear();

            // Extraction des données de SQL Server et conversion du JSON via le Repository
            //var donnees = _offreRepository.ObtenirToutesLesOffres();

            // --- INJECTION DE 5 ENREGISTREMENTS EN DUR POUR VOS TESTS ---
            Offres.Add(new OffreStage
            {
                IdOffre = 1,
                TitreOffre = "Développeur .NET",
                DescriptionOffre = "Développement d'applications d'entreprise en C#.",
                DureeOffre = "1 session",
                RemunerationOffre = "252 $/h",
                StatutOffre = "Ouverte",
                TechnologiesRequisesOffre = new List<string> { "C#", ".NET 8", "WPF", "SQL Server" }
            });
            Offres.Add(new OffreStage
            {
                IdOffre = 2,
                TitreOffre = "Stage DevOps",
                DescriptionOffre= "Automatisation des pipelines et gestion du cloud Azure.",
                DureeOffre = "2 semestres",
                RemunerationOffre = "Non rémunéré",
                StatutOffre = "Ouverte",
                TechnologiesRequisesOffre = new List<string> { "Docker", "Azure", "CI/CD" }
            });

            Offres.Add(new OffreStage
            {
                IdOffre = 3,
                TitreOffre = "Analyste de Données",
                DescriptionOffre = "Création de tableaux de bord de suivi de performance.",
                DureeOffre = "1 session",
                RemunerationOffre = "20 $/h",
                StatutOffre = "Fermée",
                TechnologiesRequisesOffre = new List<string> { "Python", "Power BI", "SQL" }
            });
            Offres.Add(new OffreStage
            {
                IdOffre = 4,
                TitreOffre = "Développeur Full-Stack",
                DescriptionOffre = "Conception d'applications web avec architectures modernes.",
                DureeOffre = "1 session",
                RemunerationOffre = "18 $/h",
                StatutOffre = "Fermée",
                TechnologiesRequisesOffre = new List<string> { "React", "Node.js", "MongoDB" }
            });

            Offres.Add(new OffreStage
            {
                IdOffre = 5,
                TitreOffre = "Programmeur de Jeux Vidéo",
                DescriptionOffre = "Optimisation des mécaniques de jeu et comportements IA.",
                DureeOffre = "1 session",
                RemunerationOffre = "21 $/h",
                StatutOffre = "Ouverte",
                TechnologiesRequisesOffre = new List<string> { "Unity", "C#", "Git" }
            });

            // Injection des objets C# dans la collection observable
            /*
            foreach (var offre in Offres)
            {
                Offres.Add(offre);
            }
            */
        }
    }
}
