using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nationala2022
{
    public class Harta
    {

        public static Dictionary<int, string> digitToObject = new Dictionary<int, string>
        {
            {1,"Meduza1" },
            {2,"Meduza2" },
            {3,"Meduza3" },
            {4,"Meduza4" },
            {5,"Sticla" },
            {6,"Hartie" },
            {7,"Plastic" },
            {8,"Robot" },
            {9,"Dreapta-Jos" },
            {10,"Stanga-Jos" },
            {11,"Stanga-Sus" },
            {12,"Dreapta-Sus" }
        };

        public static Dictionary<string, int> objectToDigit = new Dictionary<string, int>
        {
            {"Meduza1" ,1},
            {"Meduza2",2 },
            {"Meduza3",3 },
            {"Meduza4",4 },
            {"Sticla",5 },
            {"Hartie",6},
            {"Plastic",7 },
            {"Robot" , 8         },
            {"Dreapta-Jos" , 9 },
            {"Stanga-Jos" , 10 },
            {"Stanga-Sus" , 11 },
            {"Dreapta-Sus" , 12 }
        };
    }
}
