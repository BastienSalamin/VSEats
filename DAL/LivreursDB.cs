using System;
using System.Collections.Generic;
using DTO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class LivreursDB : ILivreursDB
    {
        private IConfiguration Configuration { get; }

        public LivreursDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int UpdateDisponibilite(int idLivreur, Boolean disponible)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Update Livreurs set Disponible = @disponible WHERE IdLivreur = @idLivreur";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@idLivreur", idLivreur);
                    cmd.Parameters.AddWithValue("@disponible", disponible);

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

        public int AddCommande(int idLivreur)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Update Livreurs set NbCommande = NbCommande + 1 WHERE IdLivreur = @idLivreur";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@idLivreur", idLivreur);

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

        public int RemoveCommande(int idLivreur)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Update Livreurs set NbCommande = NbCommande - 1 WHERE IdLivreur = @idLivreur";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@idLivreur", idLivreur);

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

        public List<Livreurs> GetLivreurs()
        {
            List<Livreurs> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Livreurs";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Livreurs>();

                            Livreurs livreur = new Livreurs();

                            livreur.IdLivreur = (int)dr["IdLivreur"];

                            livreur.IdLocalite = (int)dr["IdLocalite"];

                            livreur.Nom = (string)dr["Nom"];

                            livreur.Prenom = (string)dr["Prenom"];

                            livreur.NumTelephone = (string)dr["NumTelephone"];

                            livreur.NbCommande = (int)dr["NbCommande"];

                            livreur.Disponible = (Boolean)dr["Disponible"];

                            results.Add(livreur);

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

        public Livreurs GetLivreurs(string login, string motDePasse)
        {
            Livreurs livreur = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Livreurs WHERE Login = @login AND MotDePasse = @motDePasse";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@motDePasse", motDePasse);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            livreur = new Livreurs();

                            livreur.IdLivreur = (int)dr["IdLivreur"];

                            livreur.IdLocalite = (int)dr["IdLocalite"];

                            livreur.Nom = (string)dr["Nom"];

                            livreur.Prenom = (string)dr["Prenom"];

                            livreur.NumTelephone = (string)dr["NumTelephone"];

                            livreur.NbCommande = (int)dr["NbCommande"];

                            livreur.Disponible = (Boolean)dr["Disponible"];

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return livreur;
        }

        public Livreurs GetLivreurs(int idLivreur)
        {
            Livreurs livreur = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Livreurs WHERE IdLivreur = @idLivreur";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@idLivreur", idLivreur);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            livreur = new Livreurs();

                            livreur.IdLivreur = (int)dr["IdLivreur"];

                            livreur.IdLocalite = (int)dr["IdLocalite"];

                            livreur.Nom = (string)dr["Nom"];

                            livreur.Prenom = (string)dr["Prenom"];

                            livreur.NumTelephone = (string)dr["NumTelephone"];

                            livreur.NbCommande = (int)dr["NbCommande"];

                            livreur.Disponible = (Boolean)dr["Disponible"];

                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return livreur;
        }

    }
}
