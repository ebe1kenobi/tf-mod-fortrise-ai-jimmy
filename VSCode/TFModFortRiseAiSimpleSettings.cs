using System.Collections.Generic;
using FortRise;
using TowerFall;
using System;
using Monocle;
using System.Linq;

namespace TFModFortRiseAiGraph
{
  public class TFModFortRiseAiSimpleSettings: ModuleSettings
  {
  }


  public static class CommandList
  {
    [Command("hello")]
    public static void SayHello(string[] args)
    {
      Engine.Instance.Commands.Log("Hello");
    }

    //change ai position
    //[Command("ppos")]
    //public static void PlayerPosition(string[] args)
    //{
    //  if (Engine.Instance.Scene is Level)
    //  {
    //    float X = Commands.ParseFloat(args, 0, 0);
    //    float Y = Commands.ParseFloat(args, 1, 0);

    //    AI.agents[1].testActionX = X;
    //    AI.agents[1].testActionY = Y;
    //    return;
    //  }
    //  Engine.Instance.Commands.Log("Command can only be used during gameplay!");
    //}
    //change mouvement tested
    [Command("m")]
    public static void Movement(string[] args)
    {
      if (Engine.Instance.Scene is Level)
      {
        String movementName = args[0];
        String Duration = args[0];
        Logger.Info(movementName);
        AI.agents[1].testActionName = movementName;
        return;
      }
      Engine.Instance.Commands.Log("Command can only be used during gameplay!");
    }

    //change data for current movement
    //mc [indice phase action] [new duration]
    //mc 1 0.5
    [Command("mc")]
    public static void MovementChange(string[] args)
    {
      if (Engine.Instance.Scene is Level)
      {
        int indicePhase = Commands.ParseInt(args, 0, 0);
        float duration = Commands.ParseFloat(args, 1, 0);
        MovementAction action = AI.agents[1].movementLibrary.FirstOrDefault(a => a.Name == AI.agents[1].testActionName);
        action.Phases[indicePhase].Duration = duration;
        Engine.Instance.Commands.Log($"change {action.Name}[{indicePhase}].Duration={duration}");

        return;
      }
      Engine.Instance.Commands.Log("Command can only be used during gameplay!");
    }

    //[Command("arrows")]
    //public static void AddArrow(string[] args)
    //{
    //  if (Engine.Instance.Scene is Level)
    //  {
    //    int num = Commands.ParseInt(args, 0, 0);
    //    if (num < 0 || num >= Arrow.ARROW_TYPES + RiseCore.ArrowsRegistry.Count)
    //    {
    //      Engine.Instance.Commands.Log("Invalid arrow type!");
    //      return;
    //    }
    //    ArrowTypes arrowTypes = (ArrowTypes)num;
    //    using (List<Entity>.Enumerator enumerator = (Engine.Instance.Scene as Level).Players.GetEnumerator())
    //    {
    //      while (enumerator.MoveNext())
    //      {
    //        Entity entity = enumerator.Current;
    //        ((Player)entity).Arrows.AddArrows(new ArrowTypes[]
    //        {
    //                    arrowTypes,
    //                    arrowTypes
    //        });
    //      }
    //      return;
    //    }
    //  }
    //  Engine.Instance.Commands.Log("Command can only be used during gameplay!");
    //}
  }
}
