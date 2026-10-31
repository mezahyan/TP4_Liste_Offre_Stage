-- 1. Création de la base de données
CREATE DATABASE GestionStages;
GO

USE GestionStages;
GO

-- 2. Création de la table Entreprise
CREATE TABLE Entreprise (
    id_entreprise INT IDENTITY(1,1),
    nom_entreprise VARCHAR(150) NOT NULL,
    secteur_activite VARCHAR(100) NOT NULL,
    adresse VARCHAR(255) NULL,
    contact_prenom_nom VARCHAR(150) NOT NULL,
    contact_courriel VARCHAR(150) NOT NULL,
    contact_telephone VARCHAR(20) NULL,
    contact_titre_poste VARCHAR(100) NULL,
    statut_entreprise VARCHAR(20) NOT NULL DEFAULT 'Active', -- Ex: Active, Archivée
    CONSTRAINT PK_Entreprise PRIMARY KEY (id_entreprise)
);
GO

-- 3. Création de la table Offre (Offre de stage)
CREATE TABLE Offre (
    id_offre INT IDENTITY(1,1),
    titre_poste VARCHAR(150) NOT NULL,
    description_poste TEXT NOT NULL,
    technologies_requises NVARCHAR(MAX) NOT NULL, -- Stocké en JSON selon vos maquettes
    duree VARCHAR(50) NOT NULL, -- Ex: 1 session, 2 semestres
    remuneration VARCHAR(50) NULL, -- Optionnel (Ex: 22 $/h, Non rémunéré)
    statut_offre VARCHAR(20) NOT NULL DEFAULT 'Ouverte', -- Ex: Ouverte, Fermée
    id_entreprise INT NOT NULL,
    CONSTRAINT PK_Offre PRIMARY KEY (id_offre),
    CONSTRAINT FK_Offre_Entreprise FOREIGN KEY (id_entreprise) 
        REFERENCES Entreprise(id_entreprise) ON DELETE CASCADE
);
GO

-- 4. Création de la table Etudiant
CREATE TABLE Etudiant (
    id_etudiant INT IDENTITY(1,1),
    prenom VARCHAR(75) NOT NULL,
    nom VARCHAR(75) NOT NULL,
    numero_matricule VARCHAR(20) NOT NULL, -- Format: AAAA-NNNN (Ex: 2024-1187)
    courriel_institutionnel VARCHAR(150) NOT NULL,
    programme_etudes VARCHAR(150) NOT NULL,
    competences NVARCHAR(MAX) NOT NULL, -- Stocké en JSON selon vos maquettes
    chemin_cv_pdf VARCHAR(255) NOT NULL, -- Chemin absolu vers le fichier PDF
    CONSTRAINT PK_Etudiant PRIMARY KEY (id_etudiant),
    CONSTRAINT UC_Etudiant_Matricule UNIQUE (numero_matricule)
);
GO

-- 5. Création de la table Candidature (Table d'association entre Offre et Étudiant)
CREATE TABLE Candidature (
    id_offre INT NOT NULL,
    id_etudiant INT NOT NULL,
    date_soumis DATETIME NOT NULL DEFAULT GETDATE(),
    statut_candidature VARCHAR(30) NOT NULL DEFAULT 'En attente', -- Ex: En attente, Acceptée, Refusée
    note_recruteur TEXT NULL,
    CONSTRAINT PK_Candidature PRIMARY KEY (id_offre, id_etudiant),
    CONSTRAINT FK_Candidature_Offre FOREIGN KEY (id_offre) 
        REFERENCES Offre(id_offre) ON DELETE NO ACTION,
    CONSTRAINT FK_Candidature_Etudiant FOREIGN KEY (id_etudiant) 
        REFERENCES Etudiant(id_etudiant) ON DELETE CASCADE
);
GO

-- LES INSERTIONS--
-- ============================================================================
-- 1. INSERTIONS DANS LA TABLE : Entreprise
-- ============================================================================
INSERT INTO Entreprise (nom_entreprise, secteur_activite, adresse, contact_prenom_nom, contact_courriel, contact_telephone, contact_titre_poste, statut_entreprise)
VALUES 
('Tech Solutions Inc.', 'Développement logiciel', '123, rue Principale, Ottawa, ON K1A 0B1', 'Isabelle Roy', 'i.roy@techsolutions.ca', '613-555-0100', 'Directrice RH', 'Active'),
('Nexus Corp', 'Infrastructure Cloud', '456, boul. Saint-Laurent, Montréal, QC H2W 1Y2', 'Marc-André Lepage', 'ma.lepage@nexuscorp.com', '514-555-0199', 'Architecte Cloud Principal', 'Active'),
('DataViz Labs', 'Data & Intelligence artificielle', '789, rue de la Gare, Québec, QC G1K 3X4', 'Sophie Chen', 's.chen@dataviz.io', '418-555-0122', 'Chef d''équipe Data', 'Active'),
('Innolab', 'Innovation & R&D', '101, avenue du Parc, Gatineau, QC J8X 3Z5', 'Julien Lavoie', 'j.lavoie@innolab.ca', '819-555-0145', 'Responsable de l''innovation', 'Archivée'),
('PixelForge Studio', 'Développement jeux vidéo', '202, rue Saint-Jean, Québec, QC G1R 1N7', 'Nathalie Morin', 'n.morin@pixelforge.ca', '418-555-0177', 'Productrice Exécutive', 'Active');
GO

-- ============================================================================
-- 2. INSERTIONS DANS LA TABLE : Offre
-- ============================================================================
-- Note : Les technologies sont stockées au format JSON (comme indiqué sur vos maquettes)
INSERT INTO Offre (titre_poste, description_poste, technologies_requises, duree, remuneration, statut_offre, id_entreprise)
VALUES 
('Développeur .NET', 'Développement d''applications d''entreprise robustes et maintenance des systèmes existants.', '["C#", ".NET 8", "WPF", "SQL Server"]', '1 session', '22 $/h', 'Ouverte', 1),
('Stage DevOps', 'Automatisation des pipelines CI/CD et gestion de l''infrastructure Cloud.', '["Docker", "Azure", "CI/CD"]', '2 semestres', 'Non rémunéré', 'Ouverte', 2),
('Analyste données', 'Création de tableaux de bord interactifs et analyse de grands ensembles de données.', '["Python", "Power BI", "SQL"]', '1 session', '20 $/h', 'Fermée', 3),
('Dev full-stack', 'Conception et architecture d''applications web modernes de bout en bout.', '["React", "Node.js", "MongoDB", "JavaScript"]', '1 session', '18 $/h', 'Fermée', 4),
('Programmeur jeux', 'Optimisation des mécaniques de jeu et programmation de comportements de personnages.', '["Unity", "C#"]', '1 session', '21 $/h', 'Ouverte', 5);
GO

-- ============================================================================
-- 3. INSERTIONS DANS LA TABLE : Etudiant
-- ============================================================================
-- Note : Les compétences sont stockées au format JSON (comme indiqué sur vos maquettes)
INSERT INTO Etudiant (prenom, nom, numero_matricule, courriel_institutionnel, programme_etudes, competences, chemin_cv_pdf)
VALUES 
('Yasmine', 'Choudhury', '2024-1187', 'yasmine.c@lacite.edu', 'BAC Informatique', '["C#", ".NET 8", "WPF", "SQL Server", "JavaScript", "React"]', 'C:\Users\etudiant\Documents\cv_choudhury.pdf'),
('Félix', 'Beaumont', '2023-4412', 'felix.b@lacite.edu', 'Génie Logiciel', '["Docker", "Azure", "Linux", "Python", "Bash"]', 'C:\Users\etudiant\Documents\cv_beaumont.pdf'),
('Léa', 'Fontaine', '2024-8851', 'lea.f@lacite.edu', 'Sciences des données', '["Python", "Power BI", "SQL", "R", "Excel"]', 'C:\Users\etudiant\Documents\cv_fontaine.pdf'),
('Karim', 'Ndiaye', '2023-9943', 'karim.n@lacite.edu', 'Développement Web', '["React", "Node.js", "MongoDB", "JavaScript", "CSS3"]', 'C:\Users\etudiant\Documents\cv_ndiaye.pdf'),
('Alexandre', 'Tremblay', '2024-3021', 'alex.t@lacite.edu', 'Conception de jeux vidéo', '["Unity", "C#", "C++", "Git"]', 'C:\Users\etudiant\Documents\cv_tremblay.pdf');
GO

-- ============================================================================
-- 4. INSERTIONS DANS LA TABLE : Candidature
-- ============================================================================
-- En respectant les liens exacts de la maquette (Ex: Yasmine sur l'offre .NET de Tech Solutions)
INSERT INTO Candidature (id_offre, id_etudiant, date_soumis, statut_candidature, note_recruteur)
VALUES 
(1, 1, '2025-10-12 14:32:00', 'En attente', 'Profil intéressant, bonne maîtrise de C# et .NET. À rencontrer pour un entretien.'),
(2, 2, '2025-10-08 09:15:00', 'Acceptée', 'Excellent candidat, autonome et maîtrise bien les concepts de conteneurisation.'),
(3, 3, '2025-10-05 11:20:00', 'Refusée', 'Le poste a été pourvu en interne avant la fin du processus de sélection.'),
(4, 4, '2025-10-14 16:45:00', 'En attente', 'CV reçu en fin de période de recrutement. En attente de l''évaluation technique.'),
(5, 5, '2025-10-15 10:00:00', 'En attente', 'Bon portfolio de projets académiques sur Unity. Relancer pour une entrevue téléphonique.');
GO

SELECT * FROM Offre;
