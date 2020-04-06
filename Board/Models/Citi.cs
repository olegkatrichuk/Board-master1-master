using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Models
{
  public class Citi
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public string NameUa { get; set; }

    public List<State> State { get; set; }
  }
}
