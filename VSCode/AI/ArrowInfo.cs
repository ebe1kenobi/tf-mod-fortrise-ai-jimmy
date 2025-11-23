using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TowerFall;
using static TowerFall.Arrow;

namespace TFModFortRiseAiGraph
{
  public class ArrowInfo
  {
    public ArrowStates state = ArrowStates.Shooting;
    public int X = 0;
    public int Y = 0;
    public Vector2 Speed = Vector2.Zero;
    public Vector2 Position = Vector2.Zero;

    public ArrowInfo()
    {
    }

    public void Reset()
    {
    }
  }
}
