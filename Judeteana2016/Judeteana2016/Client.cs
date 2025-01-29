using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Judeteana2016
{
    public class Client
    {
        public int id_client;
        public string parola;
        public string nume;
        public string prenume;
        public string adresa;
        public string email;
        public int kcal_zilnice =2000;

        public Client(int id_client,string parola,string nume,
            string prenume, string adresa,string email)
        {
            this.adresa = adresa;   
            this.id_client = id_client;
            this.parola = parola;
            this.nume = nume;   
            this.prenume = prenume;
            this.adresa = adresa;
            this.email = email;
        }

        public override string ToString()
        {
             return id_client + ";" + parola + ";" + nume + ";" + prenume + ";" + adresa
            + ";" + email + ";" + kcal_zilnice;       
        }
    }
}
