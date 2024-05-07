using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSRL8Komunikaty
{
    public interface IKomunikat
    {
        string Tekst { get; set; }
    }

    public interface IDrugiTypKomunikatu
    {
        string TekstDrugiTyp { get; set; }
    }
}
