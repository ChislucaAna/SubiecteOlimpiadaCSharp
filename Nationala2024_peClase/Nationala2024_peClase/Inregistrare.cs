using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nationala2024_peClase
{
    public class Inregistrare
    {

        public int Id;
        public string Email;
        public DateTime Data;
        public int CodFazaLuna;
        public int CodZodia;

        public Inregistrare(int id, string email, DateTime data, int codFazaLuna, int codZodia)
        {
            Id = id;
            Email = email;
            Data = data;
            CodFazaLuna = codFazaLuna;
            CodZodia = codZodia;
        }

        public override string ToString()
        {
            string formattedDate = Data.ToString("MM/dd/yyyy");
            return Email + ";" + CodFazaLuna + ";" + formattedDate + ";" + CodZodia;
        }

    }
}
