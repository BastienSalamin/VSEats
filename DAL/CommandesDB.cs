﻿using System;
using System.Collections.Generic;
using DTO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class CommandesDB : ICommandesDB
    {
        private IConfiguration Configuration { get; }

        public CommandesDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int UpdateCommandeLivree(int idCommande)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Update Commandes set CommandeLivree = @commandeLivree WHERE IdCommande = @idCommande";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@commandeLivree", true);
                    cmd.Parameters.AddWithValue("@idCommande", idCommande);

                    cn.Open();

                    result = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public int AddCommande(int idUtilisateur, int idLivreur, float prixTotal, int tempsLivraison, DateTime date)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {

                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into Commandes (IdUtilisateur, IdLivreur, CommandeLivree, PrixTotal, Date, TempsLivraison) values (@idUtilisateur, @idLivreur, @commandeLivree, @PrixTotal, @date, @tempsLivraison)";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
                    cmd.Parameters.AddWithValue("@idLivreur", idLivreur);
                    cmd.Parameters.AddWithValue("@commandeLivree", false);
                    cmd.Parameters.AddWithValue("@PrixTotal", prixTotal);
                    cmd.Parameters.AddWithValue("@date", date); /*Temps maximal auquel il veut être livré. Tranches de 15min. A soustraire 15min pour obtenir la fourchette.*/
                    cmd.Parameters.AddWithValue("@tempsLivraison", tempsLivraison); /*calcul entre le temps de maintenant et le temps maximal*/

                    cn.Open();



                    result = cmd.ExecuteNonQuery();
                }


            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public List<Commandes> GetCommandes()
        {
            List<Commandes> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Commandes";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Commandes>();

                            Commandes commande = new Commandes();

                            commande.IdCommande = (int)dr["IdCommande"];

                            commande.IdUtilisateur = (int)dr["IdUtilisateur"];

                            commande.IdLivreur = (int)dr["IdLivreur"];

                            commande.CommandeLivree = (Boolean)dr["CommandeLivree"];

                            commande.PrixTotal = (float)dr["PrixTotal"];

                            commande.Date = (DateTime)dr["Date"];

                            if (dr["TempsLivraison"] != DBNull.Value)
                                commande.TempsLivraison = (int)dr["TempsLivraison"];

                            results.Add(commande);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return results;
        }
    }
}
