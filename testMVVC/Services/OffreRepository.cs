using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using testMVVC.Models;
using Microsoft.Data.SqlClient; // Assurez-vous d'avoir installé ce package via NuGet


namespace testMVVC.Services
{
        public class OffreRepository
        {
            // Remplacez cette chaîne par votre propre chaîne de connexion SQL Server
            private readonly string _connectionString = "Server=VOTRE_SERVEUR;Database=GestionStages;Trusted_Connection=True;TrustServerCertificate=True;";

            public List<OffreStage> ObtenirToutesLesOffres()
            {
                var listeOffres = new List<OffreStage>();

                // Requête SQL avec une jointure pour récupérer le nom de l'entreprise en même temps
                string query = @"
                SELECT o.id_offre, o.titre_poste, o.description_poste, 
                       o.technologies_requises, o.duree, o.remuneration, 
                       o.statut_offre, o.id_entreprise, e.nom_entreprise
                FROM Offre o
                INNER JOIN Entreprise e ON o.id_entreprise = e.id_entreprise";

                // 1. Déclaration de la connexion et de la commande (le 'using' ferme automatiquement les ressources)
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        connection.Open();

                        // 2. Exécution du lecteur de données
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var offre = new OffreStage();

                                // Mapping des champs standards
                                offre.IdOffre = reader.GetInt32(reader.GetOrdinal("id_offre"));
                                offre.TitreOffre = reader.GetString(reader.GetOrdinal("titre_poste"));
                                offre.DescriptionOffre = reader.GetString(reader.GetOrdinal("description_poste"));
                                offre.DureeOffre = reader.GetString(reader.GetOrdinal("duree"));
                                offre.StatutOffre = reader.GetString(reader.GetOrdinal("statut_offre"));
                                offre.IdEntreprise = reader.GetInt32(reader.GetOrdinal("id_entreprise"));
                                offre.NomEntreprise = reader.GetString(reader.GetOrdinal("nom_entreprise"));

                                // Gestion du champ optionnel (rémunération) qui peut être NULL en BDD
                                int indexRemuneration = reader.GetOrdinal("remuneration");
                                offre.RemunerationOffre = reader.IsDBNull(indexRemuneration) ? null : reader.GetString(indexRemuneration);

                                // 3. CONVERSION DU JSON SQL SERVER -> LIST<STRING> C#
                                int indexJson = reader.GetOrdinal("technologies_requises");
                                if (!reader.IsDBNull(indexJson))
                                {
                                    string jsonTexte = reader.GetString(indexJson);

                                    // On désérialise le texte JSON ["C#", ".NET 8"] en List<string>
                                    offre.TechnologiesRequisesOffre = JsonSerializer.Deserialize<List<string>>(jsonTexte) ?? new List<string>();
                                }

                                listeOffres.Add(offre);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // À des fins de débogage. Dans une vraie application, on utiliserait un Logger.
                        Console.WriteLine("Erreur lors de la lecture des offres : " + ex.Message);
                    }
                }

                return listeOffres;
            }

        }
    
}
