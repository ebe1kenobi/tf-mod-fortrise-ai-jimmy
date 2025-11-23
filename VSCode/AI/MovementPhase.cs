using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
//using IL.MonoMod;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;
using TFModFortRiseLoaderAI;
using TowerFall;
using static TowerFall.Arrow;
using static TowerFall.Player;

namespace TFModFortRiseAiGraph
{
  public class MovementPhase
  {
    public float Duration;
    public int MoveX;
    public int MoveY;
    public bool Jump;
    public bool Dash;
    public Vector2 AimAxis;
    public Func<Agent, Point, Vector2, bool> Condition; // <- modifié
    public Func<Agent, Point, Vector2, bool> EndCondition; // <- modifié

    public MovementPhase(
        float duration,
        int moveX = 0,
        int moveY = 0,
        bool jump = false,
        bool dash = false,
        Func<Agent, Point, Vector2, bool> condition = null,
        Func<Agent, Point, Vector2, bool> endCondition = null)
    {
      Duration = duration;
      MoveX = moveX;
      MoveY = moveY;
      Jump = jump;
      Dash = dash;
      AimAxis = Vector2.Zero;
      Condition = condition;
      EndCondition = endCondition;
    }

    public bool IsFinished(Agent ai, Point startPoint, Vector2 startPosition, float timer)
    {
      if (EndCondition != null && EndCondition(ai, startPoint, startPosition))
        return true;
      //Logger.Info($"{timer}");
      return timer <= 0f;
    }
  }

}
