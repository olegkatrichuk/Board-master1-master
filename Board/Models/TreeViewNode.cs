using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBoard.Models
{
  public class TreeViewNode
  {
    public int Id { get; set; }
    public string Parent { get; set; }
    public string Name { get; set; }
    public string NameUa { get; set; }
  }
}
