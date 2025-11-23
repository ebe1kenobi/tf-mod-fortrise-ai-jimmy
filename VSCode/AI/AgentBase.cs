////version poursuite pas mal a garder comme base https://chatgpt.com/c/690e70ba-9428-832a-a6db-eb815e160d67
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Runtime.InteropServices;
//using System.Threading;
//using Microsoft.Xna.Framework;
//using Monocle;
//using MonoMod.Utils;
//using TowerFall;
//using System.Linq;
//using static TowerFall.Player;
//using System.Dynamic;

//namespace TFModFortRiseAiGraph
//{
//  public class AISiAgentLevelChase : TFModFortRiseLoaderAI.Agent
//  {
//    private int[,] levelGrid;
//    public const int LEVEL_WIDTH = 32;  // 64 BLOCK
//    public const int LEVEL_HEIGHT = 24; // 48 BLOCK
//    public const int BLOCK_SIZE = 10;
//    //private const int LEVEL_WIDTH = 32 * 2;  // 64 BLOCK
//    //private const int LEVEL_HEIGHT = 24 * 2; // 48 BLOCK
//    //private const int BLOCK_SIZE = 10 / 2;
//    private static bool levelCalculated = false;
//    private static bool levelPrint = false;

//    //private List<Point> currentPath = null;
//    //private int currentPathIndex = 0;
//    private Point lastStart;
//    //private Point lastGoal;

//    // Variables globales à ajouter en haut de la classe :
//    private float pathRecalcTimer = 0f;
//    private const float PATH_RECALC_INTERVAL = 0.25f; // secondes
//    private List<Point> currentPath = null;
//    private int currentPathIndex = 0;
//    private Point lastGoal = new Point(-1, -1);

//    private bool isJumping = false;
//    private int ledgeCooldown = 0;
//    private bool ledgeJump = false;
//    private int ledgeJumpCooldown = 0;
//    private int ledgeJumpDir = 0;

//    public List<Point> debugPath = new List<Point>();
//    private const float DEBUG_CELL_SIZE = 10f; // correspond à BLOCK_SIZE

//    public Player enemy;
//    public Player player;
//    private PlayerInfo playerInfo = new PlayerInfo();
//    private PlayerInfo enemyInfo = new PlayerInfo();
//    public AISiAgentLevelChase(int index, String type, PlayerInput input) : base(index, type, input)
//    {
//      playerInfo = new PlayerInfo();
//      enemyInfo = new PlayerInfo();
//    }

//    public int getIndex()
//    {
//      return index;
//    }

//    private void UpdateLevelGrid()
//    {
//      if (levelGrid == null)
//        levelGrid = new int[LEVEL_HEIGHT, LEVEL_WIDTH];

//      for (int y = 0; y < LEVEL_HEIGHT; y++)
//      {
//        for (int x = 0; x < LEVEL_WIDTH; x++)
//        {
//          // sample world point at center of cell
//          float worldX = x * BLOCK_SIZE + (BLOCK_SIZE * 0.5f);
//          float worldY = y * BLOCK_SIZE + (BLOCK_SIZE * 0.5f);
//          levelGrid[y, x] = level.CollideCheck(new Vector2(worldX, worldY), GameTags.Solid) ? 1 : 0;
//        }
//      }

//      if (!levelPrint)
//      {
//        DebugPrintGrid();
//        levelPrint = true;
//      }
//    }

//    // Méthode debug pour afficher la grille (à retirer plus tard)
//    private void DebugPrintGrid()
//    {
//      //Logger.Info("DebugPrintGrid");
//      for (int y = 0; y < LEVEL_HEIGHT; y++)
//      {
//        string line = "";
//        for (int x = 0; x < LEVEL_WIDTH; x++)
//        {
//          line += levelGrid[y, x] == 1 ? "1" : "0";
//        }
//        Logger.Info(line);
//      }
//    }
//    public override void Reset()
//    {
//      //calculate levelGrid
//      UpdateLevelGrid();
//      DebugPrintGrid();
//    }

//    public override void Move()
//    {
//      UpdatePerception();

//      //TODO
//    }


//    void UpdatePerception()
//    {
//      UpdateLevelGrid();
//      //DebugPrintGrid();
//      int playerIndex = index;
//      player = level.GetPlayer(index); //todo check 
//                                       //search first enemy
//      int enemyIndex = index == 0 ? 1 : 0;
//      enemy = level.GetPlayer(index == 0 ? 1 : 0);  //todo , test for 2 players only
//      if (player != null)
//      {
//        //Logger.Info("player" + index + " found");

//        UpdatePlayerInfo(player, playerInfo);
//        //Logger.Info("IA " + playerIndex + " pos: " + playerInfo.X + "," + playerInfo.Y);
//      }
//      if (enemy != null)
//      {
//        //Logger.Info("enemy" + (index == 0 ? 1 : 0) + " found");
//        UpdatePlayerInfo(enemy, enemyInfo);
//        //Logger.Info("enemy" + enemyIndex + " pos: " + enemyInfo.X + "," + enemyInfo.Y);
//      }
//    }

//    void UpdatePlayerInfo(Player player, PlayerInfo playerInfo)
//    {
//      var dynData = DynamicData.For(player);

//      Point cell = WorldToCell(player.Position);
//      playerInfo.X = cell.X;
//      playerInfo.Y = cell.Y;
//      //playerInfo.X = (int)player.Position.X / 5;
//      //playerInfo.Y = (int)player.Position.Y / 5;
//      playerInfo.onGround = dynData.Get<bool>("OnGround");
//      playerInfo.GrabEdge = dynData.Get<PlayerStates>("State") == PlayerStates.LedgeGrab;
//      playerInfo.Speed = player.Speed;
//      //if (0 == dynData.Get<int>("PlayerIndex"))
//      //Logger.Info(playerInfo.Speed.X.ToString());
//      playerInfo.CanWallJump = dynData.Invoke<bool>("CanWallJump", Facing.Left) || dynData.Invoke<bool>("CanWallJump", Facing.Right);
//      dynData.Dispose();
//    }

//    public Point WorldToCell(Vector2 pos)
//    {
//      int cellX = (int)(pos.X / BLOCK_SIZE);
//      int cellY = (int)(pos.Y / BLOCK_SIZE);
//      // clamp inside
//      if (cellX < 0) cellX = 0;
//      if (cellX >= LEVEL_WIDTH) cellX = LEVEL_WIDTH - 1;
//      if (cellY < 0) cellY = 0;
//      if (cellY >= LEVEL_HEIGHT) cellY = LEVEL_HEIGHT - 1;
//      return new Point(cellX, cellY);
//    }
//  }

//  class PlayerInfo
//  {
//    public PlayerState state;
//    public Player.PlayerStates towerFallState;
//    public int X;
//    public int Y;
//    public bool onGround = false;
//    public bool GrabEdge = false;
//    public bool CanWallJump = false;
//    public Facing Facing;
//    //HasShield
//    //HasWings
//    //List arrows
//    public Vector2 Speed = Vector2.Zero;

//    public PlayerInfo()
//    {
//      Reset();
//    }

//    public void Reset()
//    {
//      state = PlayerState.Idle;
//      X = 0;
//      Y = 0;
//      onGround = false;
//      GrabEdge = false;
//      CanWallJump = false;
//      Speed = Vector2.Zero;
//      Facing = Facing.Right;
//    }
//  }

//  enum PlayerState
//  {
//    Idle,
//    Moving,
//    Attacking,
//    Jumping,
//    Falling
//  }
//  class Node
//  {
//    public Point Position;
//    public float G; // coût depuis le départ
//    public float H; // heuristique (distance estimée au but)
//    public float F => G + H;
//    public Node Parent;

//    public Node(Point pos, float g, float h, Node parent = null)
//    {
//      Position = pos;
//      G = g;
//      H = h;
//      Parent = parent;
//    }
//  }
//}
