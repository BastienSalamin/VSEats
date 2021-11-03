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
    public class CommandesDB
    {
        private IConfiguration Configuration { get; }

        public CommandesDB(IConfiguration configuration)
        {
            Configuration = configuration;
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
