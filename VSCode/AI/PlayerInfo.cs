using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerFall;

namespace TFModFortRiseAiGraph
{
  public class PlayerInfo
  {
    public PlayerState state;
    public Player.PlayerStates towerFallState;
    public int X;
    public int Y;
    public bool onGround = false;
    public bool GrabEdge = false;
    public bool CanWallJump = false;
    public Facing Facing;
    public int NbArrows = 0;
    //HasShield
    //HasWings
    //List arrows
    public Vector2 Speed = Vector2.Zero;

    public PlayerInfo()
    {
      Reset();
    }

    public void Reset()
    {
      state = PlayerState.Idle;
      X = 0;
      Y = 0;
      onGround = false;
      GrabEdge = false;
      CanWallJump = false;
      Speed = Vector2.Zero;
      Facing = Facing.Right;
    }
  }

  public enum PlayerState
  {
    Idle,
    Moving,
    Attacking,
    Jumping,
    Falling
  }
}
