using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Models
{
  public class Revision
    {
        public int Id { get; set; }



        public override string ToString()
        {
            return "Revision:" + Id;
        }
    }
}
