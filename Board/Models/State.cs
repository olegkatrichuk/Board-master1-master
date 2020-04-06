using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Models
{
  public class State
  {
    public int Id { get; set; }

    public string SubName { get; set; }

    public string SubNameUa { get; set; }

    public int CitiId { get; set; }

    public Citi Citi { get; set; }
  }
}
