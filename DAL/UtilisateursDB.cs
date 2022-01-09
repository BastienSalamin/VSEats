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
    public class UtilisateursDB : IUtilisateursDB
    {
        private IConfiguration Configuration { get; }

        public UtilisateursDB(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public int UpdateUtilisateur(int idUtilisateur, int idLocalite, string nom, string prenom, string login, string motDePasse, string adresse, string numTelephone)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Update Utilisateurs set idLocalite = @idLocalite,  nom = @nom, prenom = @prenom, login = @login, motDePasse = @motDePasse, adresse = @adresse, numTelephone = @numTelephone WHERE IdUtilisateur = @idUtilisateur";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
                    cmd.Parameters.AddWithValue("@idLocalite", idLocalite);
                    cmd.Parameters.AddWithValue("@nom", nom);
                    cmd.Parameters.AddWithValue("@prenom", prenom);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@motDePasse", motDePasse);
                    cmd.Parameters.AddWithValue("@adresse", adresse);
                    cmd.Parameters.AddWithValue("@numTelephone", numTelephone);

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

        public int AddUtilisateur(int idLocalite, string nom, string prenom, string login, string motDePasse, string adresse, string numTelephone)
        {
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {

                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Insert into Utilisateurs ( IdLocalite, Nom, Prenom, Login, MotDePasse, Adresse, NumTelephone) values (@idLocalite, @nom, @prenom, @login, @motDePasse, @adresse, @numTelephone)";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@idLocalite", idLocalite);
                    cmd.Parameters.AddWithValue("@nom", nom);
                    cmd.Parameters.AddWithValue("@prenom", prenom);
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@motDePasse", motDePasse);
                    cmd.Parameters.AddWithValue("@adresse", adresse);
                    cmd.Parameters.AddWithValue("@numTelephone", numTelephone);

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

        public Utilisateurs GetUtilisateurs(int idUtilisateur)
        {
            Utilisateurs results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Utilisateurs WHERE IdUtilisateur = @idUtilisateur";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);


                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            results = new Utilisateurs();

                            results.IdUtilisateur = (int)dr["IdUtilisateur"];

                            results.IdLocalite = (int)dr["IdLocalite"];

                            results.Nom = (string)dr["Nom"];

                            results.Prenom = (string)dr["Prenom"];

                            results.Login = (string)dr["Login"];

                            results.MotDePasse = (string)dr["MotDePasse"];

                            results.Adresse = (string)dr["Adresse"];

                            if (dr["NumTelephone"] != DBNull.Value)
                                results.NumTelephone = (string)dr["NumTelephone"];



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

        public int GetIdUtilisateurs(string login, string motDePasse)
        {
            Utilisateurs util = null;
            int result = 0;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select IdUtilisateur from Utilisateurs WHERE Login = @login AND MotDePasse =  @motDePasse";

                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@motDePasse", motDePasse);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            util = new Utilisateurs();

                            util.IdUtilisateur = (int)dr["IdUtilisateur"];

                            result = util.IdUtilisateur;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public Utilisateurs GetUtilisateurs(string login, string motDePasse)
        {
            Utilisateurs results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Utilisateurs WHERE Login = @login AND MotDePasse = @motDePasse";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@motDePasse", motDePasse);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            results = new Utilisateurs();

                            results.IdUtilisateur = (int)dr["IdUtilisateur"];

                            results.IdLocalite = (int)dr["IdLocalite"];

                            results.Nom = (string)dr["Nom"];

                            results.Prenom = (string)dr["Prenom"];

                            results.Login = (string)dr["Login"];

                            results.MotDePasse = (string)dr["MotDePasse"];

                            results.Adresse = (string)dr["Adresse"];

                            if (dr["NumTelephone"] != DBNull.Value)
                                results.NumTelephone = (string)dr["NumTelephone"];

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

        public List<Utilisateurs> GetUtilisateurs()
        {
            List<Utilisateurs> results = null;
            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            try
            {
                using (SqlConnection cn = new SqlConnection(connectionString))
                {
                    string query = "Select * from Utilisateurs";
                    SqlCommand cmd = new SqlCommand(query, cn);

                    cn.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            if (results == null)
                                results = new List<Utilisateurs>();

                            Utilisateurs utilisateur = new Utilisateurs();

                            utilisateur.IdUtilisateur = (int)dr["IdUtilisateur"];

                            utilisateur.IdLocalite = (int)dr["IdLocalite"];

                            utilisateur.Nom = (string)dr["Nom"];

                            utilisateur.Prenom = (string)dr["Prenom"];

                            utilisateur.Login = (string)dr["Login"];

                            utilisateur.MotDePasse = (string)dr["MotDePasse"];

                            utilisateur.Adresse = (string)dr["Adresse"];

                            if (dr["NumTelephone"] != DBNull.Value)
                                utilisateur.NumTelephone = (string)dr["NumTelephone"];

                            results.Add(utilisateur);

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
