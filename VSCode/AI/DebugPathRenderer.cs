using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Monocle;
using TowerFall;

namespace TFModFortRiseAiGraph
{
  public class DebugPathRenderer : Entity
  {
    private Agent ai;

    public DebugPathRenderer(Agent agent)
    {
      ai = agent;
      Depth = -10000; // au-dessus de tout
    }

    public override void Render()
    {
      base.Render();

      if (ai == null || ai.debugPath == null || ai.debugPath.Count == 0)
        return;

      const float cellSize = Agent.BLOCK_SIZE;
      const float half = cellSize / 2f;

      // --- Lignes reliant les cases du chemin ---
      for (int i = 0; i < ai.debugPath.Count - 1; i++)
      {
        Vector2 a = new Vector2(ai.debugPath[i].X * cellSize + half, ai.debugPath[i].Y * cellSize + half);
        Vector2 b = new Vector2(ai.debugPath[i + 1].X * cellSize + half, ai.debugPath[i + 1].Y * cellSize + half);
        Draw.Line(a, b, Color.Yellow);
      }

      // --- Cases du chemin ---
      foreach (var cell in ai.debugPath)
      {
        float x = cell.X * cellSize;
        float y = cell.Y * cellSize;

        Color color = Color.Yellow * 0.4f;
        if (cell.Equals(ai.debugPath.First()))
          color = Color.Green * 0.7f; // départ
        else if (cell.Equals(ai.debugPath.Last()))
          color = Color.Red * 0.7f;   // arrivée

        Draw.Rect(x, y, cellSize, cellSize, color);
      }

      // --- Position du joueur ---
      if (ai.player != null)
      {
        Point p = ai.WorldToCell(ai.player.Position);
        Draw.Rect(p.X * cellSize, p.Y * cellSize, cellSize, cellSize, Color.Blue * 0.5f);
      }

      // --- Position de l’ennemi ---
      if (ai.enemy != null)
      {
        Point e = ai.WorldToCell(ai.enemy.Position);
        Draw.Rect(e.X * cellSize, e.Y * cellSize, cellSize, cellSize, Color.Orange * 0.5f);
      }

      // --- Optionnel : grille pour contexte ---
      DrawLevelGrid(ai);
    }

    private void DrawLevelGrid(Agent ai)
    {
      const float cellSize = Agent.BLOCK_SIZE;
      Color gridColor = new Color(255, 255, 255, 20);

      for (int y = 0; y < Agent.LEVEL_HEIGHT; y++)
      {
        for (int x = 0; x < Agent.LEVEL_WIDTH; x++)
        {
          if (ai.levelGrid == null) continue;
          int cell = ai.levelGrid[y, x];
          if (cell == 1) // bloc solide
          {
            Draw.Rect(x * cellSize, y * cellSize, cellSize, cellSize, Color.Gray * 0.3f);
          }
          else
          {
            Draw.HollowRect(x * cellSize, y * cellSize, cellSize, cellSize, gridColor);
          }
        }
      }
    }
  }
}
