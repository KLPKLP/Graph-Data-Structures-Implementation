using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemA.Core
{
    public class Road : IHasCost
    {
        public double Time { get; set; } 
        public bool IsProblematic { get; set; }
        public double Cost => Time;

        public Road(double time, bool isProblematic = false)
        {
            if (time < 0) // možná pak <= 0, pokud chceme povolit nulové časy
            {
                throw new ArgumentOutOfRangeException(nameof(time), "Čas musí být kladný.");
            }
            Time = time;
            IsProblematic = isProblematic;
        }

        public override string ToString()
            => IsProblematic ? $"{Time} (problematická)" : $"{Time}";
    }
}
