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
  public class MovementAction
  {
    public string Name;                        // nom du mouvement pour debug
    public List<MovementPhase> Phases;         // phases du mouvement
    public Func<Point, Agent, bool> Condition; // condition pour lancer le mouvement
    public Func<Point, Agent, List<Point>> ResultPositions;   // position finale approximative
    public float Cost;                          // coût du mouvement (temps total)
                                                // Nouvelle propriété :
    public Point StartPoint;
    public Vector2 StartPosition;

    public MovementAction(string name)
    {
      Name = name;
      Phases = new List<MovementPhase>();
    }

    public MovementAction AddPhase(MovementPhase phase)
    {
      Phases.Add(phase);
      return this;
    }

    // calcule le coût total du mouvement (par défaut somme des durées)
    public void CalculateCost()
    {
      Cost = 0f;
      foreach (var phase in Phases)
      {
        Cost += phase.Duration;
      }
    }
  }
}
