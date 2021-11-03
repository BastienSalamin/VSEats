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
    public class UtilisateursDB
    {
        private IConfiguration Configuration { get; }

        public UtilisateursDB(IConfiguration configuration)
        {
            Configuration = configuration;
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

                            utilisateur.IdLocalite = (int)dr["Localite"];

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
