using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab02
{
    public class SearchResult
    {
        public string Name { get; set; }
        public string Faculty { get; set; }
        public string Position { get; set; }
        public string Salary { get; set; }

        public override string ToString()
        {
            // Формат відображення кожного результату у списку
            return $"{Name} ({Faculty}, {Position}, {Salary} грн)";
        }
    }

}
