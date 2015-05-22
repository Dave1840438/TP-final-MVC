﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumDImage.Models
{
    public class Dal
    {
        private BddContext bdd;

        public Dal()
        {
            bdd = new BddContext();
        }


        public void AjouterUtilisateur(string nomUtilisateur, string motDePasse, string nomComplet, string email)
        {
            bdd.Utilisateurs.Add(new Utilisateur { NomUtilisateur = nomUtilisateur, MotDePasse = motDePasse, NomComplet = nomComplet, Email = email });
            bdd.SaveChanges();
        }

        public void AjouterPhoto(Utilisateur user, byte[] image, String commentaire)
        {
            bdd.Photos.Add(new Photo { Utilisateur = user, Image = image, Commentaire = commentaire, Date = DateTime.Now });
            bdd.SaveChanges();
        }

        public List<Utilisateur> ListerUtilisateur()
        {
            return bdd.Utilisateurs.ToList();
        }

        public Utilisateur recupererUtilisateur(String id)
        {
            return bdd.Utilisateurs.FirstOrDefault(u => u.Id.ToString() == id);
        }

        public List<Photo> ListerPhotos()
        {
            return bdd.Photos.ToList();
        }

        public Utilisateur Authentifier(string username, string password)
        {
            return bdd.Utilisateurs.FirstOrDefault(u => u.NomUtilisateur == username && u.MotDePasse == password);
        }

        public bool UtilisateurADejaVote(string photoId, string userID)
        {
            return bdd.Votes.FirstOrDefault(v => v.Photo.Id.ToString() == photoId && v.Utilisateur.Id.ToString() == userID) != null;
        }

        public int getNbVotes(string photoId)
        {
            return bdd.Votes.Where(v => v.Photo.Id.ToString() == photoId).ToList().Count;
        }

        public void ajouterUnVote(string userID, string photoID)
        {
            Photo photo = bdd.Photos.FirstOrDefault(p => p.Id.ToString() == photoID);
            Utilisateur user = bdd.Utilisateurs.FirstOrDefault(u => u.Id.ToString() == userID);
            bdd.Votes.Add(new Vote() { Photo = photo, Utilisateur = user });
            bdd.SaveChanges();
        }

        public void Dispose()
        {
            
            bdd.Dispose();
        }
    }
}