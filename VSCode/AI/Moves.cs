using System.Collections.Generic;
using TowerFall;

namespace TFModFortRiseAiGraph
{
  public static class Moves
  {
    //public static List<InputState> Move(ref InputState input, List<InputState> moves) {
    //  input = moves[0];
    //  moves.RemoveAt(0);
    //  return moves;
    //}
    public static void Move(ref InputState input, List<InputState> moves)
    { 
      if (moves.Count == 0)
      {
      }
    }

  public static void Shoot(ref InputState input, List<InputState> moves, string direction = "")
    {
      // The arrow is shoot only if
      // - ShootPressed is true at update 1 to put Aiming = true
      // - then at update 2 ShootCheck = false (and aiming = true)
      if (moves.Count == 0)
      {
        //input.ShootCheck = true;
        //input.ShootPressed = true;
        //moves.Add(new InputState() { ShootCheck  = false, ShootPressed = false});
        InputState move = input;
        if (direction.Contains("r")) {
          //right
          input.MoveX = 1;
          input.AimAxis.X = 1;
          move.MoveX = 1;
          move.AimAxis.X = 1;
        }
        if (direction.Contains("l"))
        {
          //right
          input.AimAxis.X = -1;
          input.MoveX = -1;
          move.AimAxis.X = -1;
          move.MoveX = -1;
        }
        if (direction.Contains("u"))
        {
          //up
          input.MoveY = 1;
          input.AimAxis.Y = 1;
          move.MoveY = 1;
          move.AimAxis.Y = 1;
        }
        if (direction.Contains("d"))
        {
          //down
          input.MoveY = 1;
          input.AimAxis.Y = 1;
          move.MoveY = 1;
          move.AimAxis.Y = 1;
        }

        //first direction without shoot
        input.ShootCheck = true;
        input.ShootPressed = true;

        // Next movement to complete shoot
        //then direction + shoot
        move.ShootCheck = true;
        move.ShootPressed = true;
        moves.Add(move);

        //then direction without shoot again
        InputState move2 = move;
        move2.ShootCheck = false;
        move2.ShootPressed = false;
        moves.Add(move2);
        moves.Add(move2);
        moves.Add(move2);
        moves.Add(move2);
        moves.Add(move2);
      }
      else
      {
        input.ShootCheck = moves[0].ShootCheck;
        input.ShootPressed = moves[0].ShootCheck;
        moves.RemoveAt(0);
      }
    }
  }
}
