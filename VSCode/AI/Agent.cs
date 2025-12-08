// version ok tire ratrape cherche fleche
// + test solid dangereux (miasma/lave/spikeball)
// + widerset compatible
//=> ok correct
// + ajout quest support : todo finir
// + ajout team support  totest
// + ajout trial support todo finir
// + detection grabledge lock
/////////////
// todo prendre en compte type flexhe
// todo prendre en conmpte coffre
// todo prendre en copte playtag mode
// todo faire un random sur le temps enctre chaque arrow shoot
// todo recuperer plus de une fleche ?
// todo ajouter hyperjump et superjump


//- center the player before action
//- speed = 0 ?
//- delete movex = 0 for each action whith speed = 0
//- when enemey approch and no arrow flee or attack head Y - 1
//- when arrow do not go at the enemy but go to a zone near him
//- test catch multiple arrow
// - problem avec dashup when the case the player is a cheval solid and not solid
// - add hyper jump
// - add super jump
// prendre en compte les boule
//prendre en compte les miasme
//prendre en compte les bramble arrow
//prendre en compte les bomb arrow et les trigger arrow
//prendre en compte le wide screen 8 player
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;
using TowerFall;
using static TowerFall.Arrow;
using static TowerFall.Player;

namespace TFModFortRiseAiGraph
{
  public class Agent : TFModFortRiseLoaderAI.Agent
  {
    public int[,] levelGrid;
    public const int LEVEL_WIDTH = 32;  // 32 BLOCK
    public int levelWidth = LEVEL_WIDTH;  // 32 BLOCK
    public const int LEVEL_HEIGHT = 24; // 24 BLOCK
    public int levelHeight = LEVEL_HEIGHT; // 24 BLOCK
    public const int BLOCK_SIZE = 10;
    public static bool levelCalculated = false;
    public static bool levelPrint = false;
    public Point lastStart;
    static int test = 0;

    // --- Bibliothèque de mouvements disponibles ---
    public List<MovementAction> movementLibrary;
    // --- Variables IA (à placer en haut de ta classe) ---
    public Queue<MovementAction> currentActions = new Queue<MovementAction>();
    public float actionTimer = 0f;

    //public static bool testMode = true;
    public static bool testMode = false;
    public string testActionName = "jumpup14"; // Change ici pour tester une autre action
    public static int testActionX1 = 305; //au bout du niveau a droite
    public static int testActionY1 = 222;  //au sol
    public static int testActionX2 = 15; //au bout du niveau a droite
    public static int testActionY2 = 222;  //au sol
    public int testNone = 0;

    public float testPauseTimer = 0f;
    public const float TEST_PAUSE_DURATION = 1f; // durée de pause en secondes

    public MovementAction currentAction = null;
    public int currentPhaseIndex = 0;
    public float phaseTimer = 0f;
    // Variables globales à ajouter en haut de la classe :
    public float pathRecalcTimer = 0f;
    public const float PATH_RECALC_INTERVAL = 0.25f; //0.25f; // secondes
    public List<Point> currentPath = null;
    public int currentPathIndex = 0;
    public Point lastGoal = new Point(-1, -1);

    public bool isJumping = false;
    public int ledgeCooldown = 0;
    public bool ledgeJump = false;
    public int ledgeJumpCooldown = 0;
    public int ledgeJumpDir = 0;

    public const int MAX_JUMP_HEIGHT = 3;   // cases max qu’on peut sauter
    public const int MAX_FALL_HEIGHT = 50;  // cases max qu’on peut tomber

    // --- Variables à ajouter en haut de la classe ---
    public int shootState = 0; // 0 = idle, 1 = preparing, 2 = shooting, 3 = cooldown
    public int shootFrameCounter = 0;
    public Vector2 shootDirection = Vector2.Zero;
    public const int SHOOT_HOLD_FRAMES = 2;
    public const int SHOOT_COOLDOWN_FRAMES = 5;
    // Variables supplémentaires à mettre en haut de la classe
    public float shootCooldownTimer = 0f;
    public const float SHOOT_COOLDOWN = 0.25f; // secondes entre deux tirs
    public const int MIN_X_SHOOT = 15; // portée minimale en X pour tirer
    public const int MIN_Y_SHOOT = 15; // portée minimale en Y pour tirer
    public const int MIN_X_DIAG_SHOOT = 17; // portée minimale en X pour tirer
    public const int MIN_Y_DIAG_SHOOT = 9; // portée minimale en Y pour tirer

    public const int ARROW_CATCH_RANGE = 1; // nombre de cases autour du joueur pour tenter le catch


    public List<Point> debugPath = new List<Point>();
    public List<string> debugMoves = new List<string>();
    public const float DEBUG_CELL_SIZE = 10f; // correspond à BLOCK_SIZE

    public Entity enemy;
    public Player player;
    public PlayerInfo playerInfo = new PlayerInfo();
    public List<ArrowInfo> arrows = new List<ArrowInfo>();
    public PlayerInfo enemyInfo = new PlayerInfo();
    public Agent(int index, String type, PlayerInput input) : base(index, type, input)
    {
      playerInfo = new PlayerInfo();
      enemyInfo = new PlayerInfo();
    }
    public int getIndex()
    {
      return index;
    }

    private void UpdateLevelGrid()
    {
      if (EigthPlayerImport.IsEightPlayer())
      {
        //Logger.Info("EigthPlayerImport.IsEightPlayer(): true : " + EigthPlayerImport.IsEightPlayer());
        levelWidth = 42;
        levelHeight = 24;
      }
      else
      {
        //Logger.Info("EigthPlayerImport.IsEightPlayer(): false : " + EigthPlayerImport.IsEightPlayer());
        levelWidth = 32;
        levelHeight = 24;
      }

      if (levelGrid == null)
        levelGrid = new int[levelHeight, levelWidth];

      //Logger.Info("UpdateLevelGrid");

      // Chercher le Miasma dans les entités du niveau
      Miasma miasma = level.Layers[0].GetFirst<Miasma>();
      Lava lava = level.Layers[0].GetFirst<Lava>();
      Spikeball spikeball = level.Layers[0].GetFirst<Spikeball>();

      bool hasMiasma = false;
      bool hasLava = false;
      bool hasSpikeball = false;

      for (int y = 0; y < levelHeight; y++)
      {
        for (int x = 0; x < levelWidth; x++)
        {
          // sample world point at center of cell
          float worldX = x * BLOCK_SIZE + (BLOCK_SIZE * 0.5f);
          float worldY = y * BLOCK_SIZE + (BLOCK_SIZE * 0.5f);

          Vector2 worldPoint = new Vector2(worldX, worldY);
          levelGrid[y, x] = 0;

          // Vérifier d'abord si c'est un mur solide
          if (level.CollideCheck(worldPoint, GameTags.Solid))
          {
            levelGrid[y, x] = 1;
          }
          //if (level.CollideCheck(worldPoint, GameTags.LavaCollider))
          //{
          //  levelGrid[y, x] = 2;
          //  hasLava = true;
          //}
          if (lava != null && lava.Collidable && lava.Collider.Collide(worldPoint))
          {
            levelGrid[y, x] = 2;
            hasLava = true;
          }
          // Ensuite vérifier si c'est dans le Miasma
          if (miasma != null && miasma.Collidable && miasma.Collider.Collide(worldPoint))
          {
            levelGrid[y, x] = 3;
            hasMiasma = true;
          }
          if (spikeball != null && spikeball.Collidable && spikeball.Collider.Collide(worldPoint))
          {
            levelGrid[y, x] = 4;
            hasSpikeball = true;
          }

        }
      }

      if (!levelPrint)
      //if (hasMiasma)
      //if (hasLava || hasSpikeball)
      {
        DebugPrintGrid();
        levelPrint = true;
      }
      //Logger.Info("UpdateLevelGridfin");

    }

    //private void UpdateLevelGrid()
    //{
    //  if (levelGrid == null)
    //    levelGrid = new int[levelHeight, levelWidth];

    //  for (int y = 0; y < levelHeight; y++)
    //  {
    //    for (int x = 0; x < levelWidth; x++)
    //    {
    //      // sample world point at center of cell
    //      float worldX = x * BLOCK_SIZE + (BLOCK_SIZE * 0.5f);
    //      float worldY = y * BLOCK_SIZE + (BLOCK_SIZE * 0.5f);
    //      levelGrid[y, x] = level.CollideCheck(new Vector2(worldX, worldY), GameTags.Solid) ? 1 : 0;
    //       //levelGrid[y, x] = level.CollideCheck(new Vector2(worldX, worldY), GameTags.JumpPad) ? 2 : levelGrid[y, x];
    //       //levelGrid[y, x] = level.CollideCheck(new Vector2(worldX, worldY), GameTags.TreasureChest) ? 3 : levelGrid[y, x];
    //       //levelGrid[y, x] = level.CollideCheck(new Vector2(worldX, worldY), GameTags.LavaCollider) ? 4 : levelGrid[y, x];
    //       //levelGrid[y, x] = level.CollideCheck(new Vector2(worldX, worldY), GameTags.PlayerCollider) ? 5 : levelGrid[y, x];

    //      // levelGrid[y, x] = level.CollideCheck(new Vector2(worldX, worldY), GameTags.Brambles) ? 5 : levelGrid[y, x];
    //    }
    //  }

    //  if (!levelPrint)
    //  {
    //    //DebugPrintGrid();
    //    levelPrint = true;
    //  }
    //}

    // Méthode debug pour afficher la grille (à retirer plus tard)
    private void DebugPrintGrid()
    {
      //Logger.Info("DebugPrintGrid");
      for (int y = 0; y < levelHeight; y++)
      {
        string line = "";
        for (int x = 0; x < levelWidth; x++)
        {
          line += levelGrid[y, x].ToString();
        }
        Logger.Info(line);
      }
    }


    public override void Reset()
    {
      //calculate levelGrid
      UpdateLevelGrid();
      //DebugPrintGrid();
    }

    public override void Move()
    {
      //Logger.Info("Move " + index);
      UpdatePerception();
      //return;
      // === MODE TEST ===
      if (Agent.testMode && MyTFGame.sandbox)
      {
        if (currentAction == null || currentAction.Name == "None")
        {
          TestMovementAction(testActionName);

          currentPhaseIndex = 0;
          if (currentAction != null && currentAction.Phases.Count > 0)
          {
            phaseTimer = currentAction.Phases[currentPhaseIndex].Duration;
            Logger.Info($"Test {testActionName} : Condition: {currentAction.Condition(currentAction.StartPoint, this)}");
          }
        }

        if (currentAction != null)
        {
          ExecuteActionPhases(currentAction);
        }

        if (currentAction == null)
        {
          ApplyPhaseInputs(new MovementPhase(0f));
        }
        return;
      }



      // === RECALCUL DU CHEMIN À INTERVALLES RÉGULIERS ===
      pathRecalcTimer += Engine.DeltaTime;
      if (currentAction == null && pathRecalcTimer >= PATH_RECALC_INTERVAL)
      {
        pathRecalcTimer = 0f;
        ComputeNewActionSequence();
        currentAction = null;
        currentPhaseIndex = 0;
      }

      // === LANCER UNE NOUVELLE ACTION ===
      if (currentAction == null && currentActions.Count > 0)
      {
        //Logger.Info("Queue before executing:");
        //foreach (var a in currentActions)
        //  Logger.Info("  -> " + a.Name);

        currentAction = currentActions.Dequeue();
        Logger.Info($"currentAction = {currentAction.Name}");
        currentAction.StartPoint = WorldToCell(player.Position);
        currentAction.StartPosition = player.Position;
        currentPhaseIndex = 0;

        // Réinitialiser la vitesse à zéro pour garantir un départ propre
        // La librairie de mouvement nécessite une vitesse à zéro au départ
        if (player != null)
        {
          player.Speed = Vector2.Zero;

          // Vérification supplémentaire : si la vitesse n'est pas à zéro après un court délai,
          // on attend un peu avant de commencer l'action (sécurité)
          // Cette vérification est optionnelle mais peut aider dans certains cas
        }

        if (currentAction.Phases.Count > 0)
          phaseTimer = currentAction.Phases[currentPhaseIndex].Duration;

        //Logger.Info($"ApplyPhaseInputs = {currentPhaseIndex}  < start");
        //ApplyPhaseInputs(currentAction.Phases[currentPhaseIndex]);
      }


      HandleArrowCatch();
      HandleShooting();
      if (shootState != 0)
      {
        Logger.Info("Shooting in progress, cancelling movement actions.");
        currentActions.Clear();
        currentAction = null;
        currentPhaseIndex = 0;
        phaseTimer = 0f;
        //input.inputState.MoveX = 0;
        //input.inputState.MoveY = 0;
        //input.inputState.JumpCheck = false;
        //input.inputState.JumpPressed = false;
        //input.inputState.DodgeCheck = false;
        //input.inputState.DodgePressed = false;
        return;
      }
      // === ACTION EN COURS ===
      if (currentAction != null)
      {
        //Logger.Info("ExecuteActionPhases");
        ExecuteActionPhases(currentAction);
      }
    }

    private void ExecuteActionPhases(MovementAction action)
    {
      int safetyCounter = 0; // sécurité pour éviter boucle infinie

      while (action != null && currentPhaseIndex < action.Phases.Count && safetyCounter++ < 10)
      {
        MovementPhase phase = action.Phases[currentPhaseIndex];

        // Vérifie la condition de la phase AVANT de l’exécuter
        bool conditionOk = phase.Condition == null || phase.Condition(this, action.StartPoint, action.StartPosition);

        if (!conditionOk)
        {
          // Passe directement à la suivante
          //Logger.Info($"Phase {currentPhaseIndex} ignorée ({phase}) car condition non remplie");
          currentPhaseIndex++;
          if (currentPhaseIndex < action.Phases.Count)
            phaseTimer = action.Phases[currentPhaseIndex].Duration;
          continue;
        }

        // Phase active : on applique les inputs
        phaseTimer -= Engine.DeltaTime;
        //Logger.Info($"ApplyPhaseInputs = {currentPhaseIndex}");
        ApplyPhaseInputs(phase);

        // Condition de fin atteinte ?
        if (phase.IsFinished(this, action.StartPoint, action.StartPosition, phaseTimer))
        {
          currentPhaseIndex++;
          if (currentPhaseIndex >= action.Phases.Count)
          {
            currentAction = null; // Fin de l’action complète
            return;
          }
          else
          {
            phaseTimer = action.Phases[currentPhaseIndex].Duration;
          }
        }

        break; // on sort si on est sur une phase active et valide
      }

      // Si toutes les phases ont été sautées → fin de l’action
      if (currentAction != null && currentPhaseIndex >= currentAction.Phases.Count)
      {
        currentAction = null;
      }
    }

    // --- Nouvelle fonction : calcul du chemin en actions physiques ---
    private void ComputeNewActionSequence()
    {
      Point start = new Point(playerInfo.X, playerInfo.Y);
      Point goal = ChooseGoal(start);

      // Trouve un chemin de cellules basé sur les mouvements possibles
      List<Point> path = FindPathUsingMovementLibrary(start, goal);
      this.debugPath = path != null ? new List<Point>(path) : new List<Point>();
      if (path == null || path.Count == 0)
        return;

      currentActions.Clear();
      foreach (var moveName in debugMoves)
      {
        var act = movementLibrary.FirstOrDefault(a => a.Name == moveName);
        if (act != null)
          currentActions.Enqueue(act);
      }
    }


    // --- Fonction pour retrouver quelle action correspond à un déplacement ---
    private MovementAction FindActionLeadingTo(Point from, Point to)
    {
      foreach (var move in movementLibrary)
      {
        List<Point> possible = move.ResultPositions(from, this);
        if (possible.Any(p => p == to))
          return move;
      }
      return null;
    }


    // --- Application des inputs physiques selon l’action ---
    private void ApplyPhaseInputs(MovementPhase phase)
    {
      this.input.inputState.MoveX = phase.MoveX;
      this.input.inputState.MoveY = phase.MoveY;
      this.input.inputState.JumpCheck = phase.Jump;
      this.input.inputState.JumpPressed = phase.Jump && !this.input.prevInputState.JumpCheck;
      this.input.inputState.DodgeCheck = phase.Dash;
      this.input.inputState.DodgePressed = phase.Dash && !this.input.prevInputState.DodgeCheck;
      this.input.inputState.AimAxis = new Vector2(phase.MoveX, phase.MoveY);

    }
    private void HandleArrowCatch()
    {
      if (player == null || arrows == null || arrows.Count == 0) return;

      // Parcours toutes les flèches
      foreach (var arrow in arrows)
      {
        // On ne tente de rattraper que si la flèche est encore en vol
        if (arrow.state == ArrowStates.Shooting ||
            arrow.state == ArrowStates.Drilling ||
            arrow.state == ArrowStates.Gravity ||
            arrow.state == ArrowStates.Falling)
        {
          // Calculer direction relative de la flèche par rapport au joueur
          Vector2 toPlayer = player.Position - arrow.Position;

          // Vérifier si la flèche va vers le joueur
          if (Vector2.Dot(toPlayer, arrow.Speed) > 0)
          {
            // Vérifier la proximité (X et Y en case)
            Point arrowCell = new Point(arrow.X, arrow.Y);
            Point playerCell = new Point(playerInfo.X, playerInfo.Y);

            if (Math.Abs(arrowCell.X - playerCell.X) <= ARROW_CATCH_RANGE &&
                Math.Abs(arrowCell.Y - playerCell.Y) <= ARROW_CATCH_RANGE)
            {
              // Activer le catch
              this.input.inputState.DodgeCheck = true;
              this.input.inputState.DodgePressed = !this.input.prevInputState.DodgeCheck;
              return; // on catch une flèche à la fois
            }
          }
        }
      }

      // Sinon, pas de flèche à attraper
      this.input.inputState.DodgeCheck = false;
      this.input.inputState.DodgePressed = false;
    }


    // --- Méthode pour gérer le tir ---
    private void HandleShooting()
    {
      if (player == null || enemy == null || playerInfo.NbArrows <= 0)
      {
        Logger.Info($"player == {player} || enemy == {enemy} || playerInfo.NbArrows <= {playerInfo.NbArrows}");
        shootState = 0;
        return;
      }

      // Cooldown entre les tirs
      shootCooldownTimer += Engine.DeltaTime;
      if (shootCooldownTimer < SHOOT_COOLDOWN && shootState == 0) return;

      // Calcul direction vers l'ennemi
      Vector2 dir = enemy.Position - player.Position;
      if (dir != Vector2.Zero) dir.Normalize();
      shootDirection = dir;

      // Vérifier la ligne de visée
      if (!CanShootLineOfSight(new Point(playerInfo.X, playerInfo.Y), new Point(enemyInfo.X, enemyInfo.Y)))
      {
        Logger.Info($"!CanShootLineOfSight");
        shootState = 0;
        return; // mur sur le trajet, ne pas tirer
      }

      // Déterminer le type de tir et vérifier la portée
      float deltaX = enemyInfo.X - playerInfo.X;
      float deltaY = enemyInfo.Y - playerInfo.Y;
      bool canShoot = false;

      // Tir horizontal
      if (Math.Abs(deltaY) <= 1 && Math.Abs(deltaX) <= MIN_X_SHOOT) canShoot = true;

      // Tir vertical haut
      else if (deltaX == 0 && deltaY < 0 && Math.Abs(deltaY) <= MIN_Y_SHOOT) canShoot = true;

      // Tir diagonale haut
      else if (deltaX != 0 && deltaY < 0)
      {
        int maxDiagonalX = MIN_X_DIAG_SHOOT;
        int maxDiagonalY = MIN_Y_DIAG_SHOOT; // flèche atteint Y+9 avant de retomber
        if (Math.Abs(deltaX) <= maxDiagonalX && Math.Abs(deltaY) <= maxDiagonalY) canShoot = true;
      }

      if (!canShoot)
      {
        Logger.Info($"!canShoot");
        shootState = 0;
        return; // hors portée, ne pas tirer
      }

      // --- Cycle multi-frame ---
      if (shootState == 0)
      {
        shootState = 1; // préparer le tir
        shootFrameCounter = 0;
        shootCooldownTimer = 0f; // reset cooldown
      }

      if (shootState == 1) // préparer
      {
        shootFrameCounter++;
        this.input.inputState.AimAxis = shootDirection;
        this.input.inputState.ShootCheck = true;
        this.input.inputState.ShootPressed = true;

        if (shootFrameCounter >= SHOOT_HOLD_FRAMES)
        {
          shootState = 2;
          shootFrameCounter = 0;
        }
      }
      else if (shootState == 2) // relâchement
      {
        shootFrameCounter++;
        this.input.inputState.AimAxis = shootDirection;
        this.input.inputState.ShootCheck = false;
        this.input.inputState.ShootPressed = false;

        if (shootFrameCounter >= SHOOT_COOLDOWN_FRAMES)
        {
          shootState = 0; // prêt pour prochain tir
          shootFrameCounter = 0;
          //playerInfo.NbArrows--; // décrémente le nombre de flèches
        }
      }
    }

    // --- Méthode pour vérifier si le tir est possible sans mur ---
    private bool CanShootLineOfSight(Point start, Point end)
    {
      int x0 = start.X;
      int y0 = start.Y;
      int x1 = end.X;
      int y1 = end.Y;

      int dx = Math.Abs(x1 - x0);
      int dy = Math.Abs(y1 - y0);
      int sx = x0 < x1 ? 1 : -1;
      int sy = y0 < y1 ? 1 : -1;
      int err = dx - dy;

      while (true)
      {
        // Vérifie la cellule actuelle
        if (!IsCellWalkable(x0, y0)) return false;

        if (x0 == x1 && y0 == y1) break;

        int e2 = 2 * err;
        if (e2 > -dy)
        {
          err -= dy;
          x0 += sx;
        }
        if (e2 < dx)
        {
          err += dx;
          y0 += sy;
        }
      }

      return true; // pas de mur sur la ligne
    }
    public bool IsDangerous(int x, int y)
    {
      if (x < 0 || y < 0 || x >= levelWidth || y >= levelHeight)
        return false;
      int cellValue = levelGrid[y, x];
      return cellValue == 2 || cellValue == 3 || cellValue == 4; // 2 = Lava, 3 = Miasma, 4 = Spikeball
    }

    public bool IsCellWalkable(int x, int y)
    {
      if (x < 0 || y < 0 || x >= levelWidth || y >= levelHeight)
        return false;
      int cellValue = levelGrid[y, x];
      // 0 = vide, 1 = solide, 2+ = dangereux -> non walkable
      return cellValue == 0; // Exclut les solides (1) et les zones dangereuses (2, 3, 4)
    }

    public bool IsCellsWalkable(int fromX, int toX, int fromY, int toY)
    {
      // On s’assure que les indices sont dans les bornes du niveau
      // Permet de gérer un intervalle dans les deux sens (gauche ou droite)
      int startX = Math.Min(fromX, toX);
      int endX = Math.Max(fromX, toX);
      int startY = Math.Min(fromY, toY);
      int endY = Math.Max(fromY, toY);

      // On parcourt toutes les cellules entre les deux X
      for (int x = startX; x <= endX; x++)
      {
        for (int y = startY; y <= endY; y++)
        {
          // Si la case est un mur, ce n’est pas walkable
          if (!IsCellWalkable(x, y))
            return false;
        }
        // Optionnel : vérifier aussi qu’il y a un sol dessous
        // si tu veux t’assurer qu’on ne traverse pas un vide
        // if (!HasGroundBelow(x, y))
        //     return false;
      }

      return true;
    }

    public bool IsSolid(int x, int y)
    {
      if (x < 0 || y < 0 || x >= levelWidth || y >= levelHeight)
        return false;
      return levelGrid[y, x] == 1; // 0 = vide, 1+ = obstacle
    }

    public bool IsSolid(int fromX, int toX, int y)
    {
      //Logger.Info($"IsGroundsWalkable {fromX} to {toX} y={y}");
      // On s’assure que les indices sont dans les bornes du niveau
      // Permet de gérer un intervalle dans les deux sens (gauche ou droite)
      int start = Math.Min(fromX, toX);
      int end = Math.Max(fromX, toX);

      // On parcourt toutes les cellules entre les deux X
      for (int x = start; x <= end; x++)
      {
        // Si la case est un mur, ce n’est pas walkable
        //Logger.Info($"IsGroundWalkable({x}, {y}) = {IsGroundWalkable(x, y)}");
        if (!IsSolid(x, y))
          return false;

        // Optionnel : vérifier aussi qu’il y a un sol dessous
        // si tu veux t’assurer qu’on ne traverse pas un vide
        // if (!HasGroundBelow(x, y))
        //     return false;
      }

      return true;
    }

    public bool IsSolid(int fromX, int toX, int fromY, int toY)
    {
      // On s’assure que les indices sont dans les bornes du niveau
      // Permet de gérer un intervalle dans les deux sens (gauche ou droite)
      //int startX = Math.Min(fromX, toX);
      //int endX = Math.Max(fromX, toX);
      int startY = Math.Min(fromY, toY);
      int endY = Math.Max(fromY, toY);

      // On parcourt toutes les cellules entre les deux X
      //for (int x = startX; x <= endX; x++)
      //{
      for (int y = startY; y <= endY; y++)
      {
        // Si la case est un mur, ce n’est pas walkable
        if (!IsSolid(fromX, toX, y))
          return false;
      }
      // Optionnel : vérifier aussi qu’il y a un sol dessous
      // si tu veux t’assurer qu’on ne traverse pas un vide
      // if (!HasGroundBelow(x, y))
      //     return false;
      //}

      return true;
    }

    public bool IsAreaFree(int fromX, int toX, int fromY, int toY)
    {
      int startX = Math.Min(fromX, toX);
      int endX = Math.Max(fromX, toX);
      int startY = Math.Min(fromY, toY);
      int endY = Math.Max(fromY, toY);

      for (int x = startX; x <= endX; x++)
      {
        for (int y = startY; y <= endY; y++)
        {
          if (IsSolid(x, y))
            return false; // il y a un mur -> pas libre
          if (IsDangerous(x, y))
            return false; // il y a une zone dangereuse -> pas libre
        }
      }
      return true; // toutes les cases sont libres
    }

    // Vérifie si une cellule est proche d'une zone dangereuse (dans un rayon de 1 case)
    private bool IsNearDangerousZone(int x, int y)
    {
      for (int dx = -1; dx <= 1; dx++)
      {
        for (int dy = -1; dy <= 1; dy++)
        {
          if (dx == 0 && dy == 0) continue; // sauter la case elle-même
          int checkX = x + dx;
          int checkY = y + dy;
          if (IsDangerous(checkX, checkY))
            return true;
        }
      }
      return false;
    }

    private List<Point> FindPathUsingMovementLibrary(Point start, Point goal)
    {
      if (movementLibrary == null)
        InitMovementLibrary();

      var open = new List<Node>();
      var closed = new HashSet<Point>();

      Node startNode = new Node(start, 0, Heuristic(start, goal), null) { PhaseIndex = 0 };
      open.Add(startNode);

      int safety = 0;
      const int MAX_ITER = 8000;

      while (open.Count > 0 && safety++ < MAX_ITER)
      {
        //Logger.Info("boucle ITER " + safety);
        // Trier selon le coût total F = G + H
        open.Sort((a, b) => a.F.CompareTo(b.F));
        Node current = open[0];
        open.RemoveAt(0);

        // Objectif atteint
        if (current.Position.Equals(goal))
        {
          var foundPath = ReconstructPath(current);
          this.debugPath = foundPath != null ? new List<Point>(foundPath) : new List<Point>();
          return foundPath;
        }

        if (closed.Contains(current.Position)) continue;
        closed.Add(current.Position);

        foreach (var move in movementLibrary)
        {
          //if (move.Name == "leftm1")
          //  Logger.Info($"{move.Name} {current.Position.X} {current.Position.Y} {move.Condition(current.Position, this)}");

          //if (move.Name == "jumplefetholem2m0")
          //  Logger.Info($"{move.Name} {current.Position.X} {current.Position.Y} {move.Condition(current.Position, this)}");

          if (!move.Condition(current.Position, this)) continue;

          //Logger.Info("condition ok");

          // Récupère toutes les positions possibles pour ce mouvement
          List<Point> possibleDestinations = move.ResultPositions != null
              ? move.ResultPositions(current.Position, this)
              : new List<Point> { new Point(0, 0) };

          foreach (Point dest in possibleDestinations)
          {
            Point d = dest;
            //Logger.Info("dest " + d.X + "," + d.Y);
            // Vérifier les limites
            if (d.X < 0 || d.X >= levelWidth || d.Y < 0 || d.Y >= levelHeight)
              continue;

            // ===== OPTION 1 : tester la position en l’air =====
            TryAddNode(current, d, move, open, closed, goal);

            // ===== OPTION 2 : tester la chute =====
            if (!HasGroundBelow(d.X, d.Y))
            {
              Point fallPos = SimulateFall(d);
              TryAddNode(current, fallPos, move, open, closed, goal);
            }

            //if (!HasGroundBelow(d.X, d.Y)) d = SimulateFall(d); //todo sometime i don t want to fall but stay in air

            //// Ne pas se poser dans un mur
            //if (IsSolid(d.X, d.Y)) continue;  //todo comment to have multiple option of movement
            ////if (!IsCellWalkable(d.X, d.Y)) continue;  //todo comment to have multiple option of movement
            ////Logger.Info("IsCellWalkable true");

            //// Déjà visité
            //if (closed.Contains(d)) continue;
            ////Logger.Info("pas deja visité");

            //// Ajuster le coût selon la direction : favoriser les mouvements vers le haut
            //float adjustedCost = move.Cost;
            //int deltaY = d.Y - current.Position.Y;
            //if (deltaY < 0) // mouvement vers le haut
            //{
            //  // Réduire le coût des mouvements vers le haut pour les favoriser
            //  adjustedCost *= 0.9f;
            //}

            //float g = current.G + adjustedCost;
            //Node existing = open.FirstOrDefault(n => n.Position.Equals(d));
            //if (existing == null)
            //{
            //  Node newNode = new Node(d, g, Heuristic(d, goal), current) { PhaseIndex = 0, MoveName = move.Name };
            //  open.Add(newNode);
            //}
            //else if (g < existing.G)
            //{
            //  existing.G = g;
            //  existing.Parent = current;
            //}
          }
        }
      }

      return null; // pas de chemin trouvé
    }

    void TryAddNode(Node current, Point d, MovementAction move,
                List<Node> open, HashSet<Point> closed, Point goal)
    {
      if (IsSolid(d.X, d.Y)) return;
      if (IsDangerous(d.X, d.Y)) return; // Éviter les zones dangereuses
      if (closed.Contains(d)) return;

      float adjustedCost = move.Cost;
      int deltaY = d.Y - current.Position.Y;
      if (deltaY < 0) adjustedCost *= 0.9f;

      // Pénalité supplémentaire si proche d'une zone dangereuse
      if (IsNearDangerousZone(d.X, d.Y))
      {
        adjustedCost *= 1.2f; // Augmente le coût de 20% si proche d'un danger
      }

      float g = current.G + adjustedCost;

      Node existing = open.FirstOrDefault(n => n.Position.Equals(d));
      if (existing == null)
      {
        open.Add(new Node(d, g, Heuristic(d, goal), current)
        {
          PhaseIndex = 0,
          MoveName = move.Name
        });
      }
      else
      {
        // ❌ Do NOT update existing nodes
        // First path wins → prevents MoveName corruption
        return;
      }
    }


    private float Heuristic(Point a, Point b)
    {
      int dx = Math.Abs(a.X - b.X);
      int dy = Math.Abs(a.Y - b.Y);

      // Favoriser les mouvements vers le haut (la cible est souvent au-dessus)
      // Si la cible est au-dessus, pénaliser moins les mouvements verticaux
      if (b.Y < a.Y) // cible au-dessus
      {
        // Réduire le coût des mouvements verticaux vers le haut
        return dx + (dy * 0.8f); // pénalité réduite pour monter
      }
      else if (b.Y > a.Y) // cible en dessous
      {
        // Les mouvements vers le bas sont plus faciles (gravité)
        return dx + (dy * 0.6f);
      }

      // Même hauteur
      return dx + dy;
    }

    private List<Point> ReconstructPath(Node node)
    {
      List<Point> path = new List<Point>();
      var moves = new List<string>(); // 🔥 noms des mouvements utilisés
      Node cur = node;
      while (cur != null)
      {
        path.Insert(0, cur.Position);
        if (!string.IsNullOrEmpty(cur.MoveName))
          moves.Add(cur.MoveName);
        cur = cur.Parent;
      }

      moves.Reverse();
      this.debugMoves = moves;
      Logger.Info("Chemin trouvé avec les mouvements : " + string.Join(" -> ", moves));
      return path;
    }


    void UpdatePerception()
    {
      UpdateLevelGrid();
      //UpdateMiasmaGrid();
      //Lava
      //Miasma
      //MoonGlassBlock
      //MovingPlatform
      //ProximityBlock
      //ShiftBlock
      //Spikeball
      //SwitchBlock
      //TreasureChest
      //UpdateMovingBlockGrid();   // CrackedPlatform  CrackedWall  CrumbleBlock CrumbleWall  GraniteBlock  LoopPlatform
      //JumpPad
      //UpdatePlatformTraversableGrid();
      //DebugPrintGrid();
      int playerIndex = index;
      player = level.GetPlayer(index); //todo check
                                       //search first enemy
                                       //int enemyIndex = index == 0 ? 1 : 0;
                                       //enemy = level.GetPlayer(index == 0 ? 1 : 0);  //todo , test for 2 players only


      /// ---Trouver l’ennemi le plus proche parmi tous les joueurs ---
      enemy = null;
      float bestDist = float.MaxValue;
      //check versus or quest
      if (level.Session.MatchSettings.Mode == Modes.Quest || level.Session.MatchSettings.Mode == Modes.DarkWorld) {
        //look for ennemies
        foreach (var entity in level.Layers[0].Entities){

          if (entity == null) continue;
          if (!(entity is Enemy)) continue;   

          // distance en cases (Manhattan)
          Point pc = WorldToCell(entity.Position);
          float dist = Math.Abs(pc.X - playerInfo.X) + Math.Abs(pc.Y - playerInfo.Y);

          if (dist < bestDist)
          {
            bestDist = dist;
            enemy = entity;
          }
        }
      }
      else if (level.Session.MatchSettings.Mode == Modes.Trials) {
        foreach (var entity in level.Layers[0].Entities)
        {
          if (entity == null) continue;
          if (!(entity is Dummy)) continue;
          //Logger.Info(entity.GetType().ToString());

          // distance en cases (Manhattan)
          Point pc = WorldToCell(entity.Position);
          float dist = Math.Abs(pc.X - playerInfo.X) + Math.Abs(pc.Y - playerInfo.Y);
          //levelGrid[pc.Y, pc.X] = 9;
          //Logger.Info($"{pc.Y}, {pc.X}");


          if (dist < bestDist)
          {
            bestDist = dist;
            enemy = entity;
          }
        }
        test++;
        if (test % 50 == 0)
          DebugPrintGrid();
      } else {
        foreach (Player p in level.Players)
        {
          if (p == null) continue;
          if (p.PlayerIndex == index) continue;     // ignorer soi-même
          if (p.Dead) continue;                     // ignorer joueurs morts/inactifs
          if (p.TeamColor != Allegiance.Neutral && p.TeamColor == player.TeamColor) continue; // ignorer joueurs meme team

          // distance en cases (Manhattan)
          Point pc = WorldToCell(p.Position);
          float dist = Math.Abs(pc.X - playerInfo.X) + Math.Abs(pc.Y - playerInfo.Y);

          if (dist < bestDist)
          {
            bestDist = dist;
            enemy = p;
          }
        }
      }

      if (player != null)
      {
        //Logger.Info("player" + index + " found");

        UpdatePlayerInfo(player, playerInfo);
        //Logger.Info("IA " + playerIndex + " pos: " + playerInfo.X + "," + playerInfo.Y);
      }
      if (enemy != null)
      {
        //Logger.Info("enemy" + (index == 0 ? 1 : 0) + " found");
        //UpdatePlayerInfo(enemy, enemyInfo);
        UpdateEnemyInfo(enemy, enemyInfo);
        //Logger.Info("enemy" + enemyIndex + " pos: " + enemyInfo.X + "," + enemyInfo.Y);
      }
      UpdateArrowInfo();
    }

    void UpdatePlayerInfo(Player player, PlayerInfo playerInfo)
    {
      var dynData = DynamicData.For(player);

      Point cell = WorldToCell(player.Position);
      playerInfo.X = cell.X;
      playerInfo.Y = cell.Y;
      //playerInfo.X = (int)player.Position.X / 5;
      //playerInfo.Y = (int)player.Position.Y / 5;
      playerInfo.onGround = dynData.Get<bool>("OnGround");
      playerInfo.GrabEdge = dynData.Get<PlayerStates>("State") == PlayerStates.LedgeGrab;
      playerInfo.Speed = player.Speed;
      playerInfo.NbArrows = player.Arrows.Count;
      //if (0 == dynData.Get<int>("PlayerIndex"))
      //Logger.Info(playerInfo.Speed.X.ToString());
      playerInfo.CanWallJump = dynData.Invoke<bool>("CanWallJump", Facing.Left) || dynData.Invoke<bool>("CanWallJump", Facing.Right);
      dynData.Dispose();
    }

    void UpdateEnemyInfo(Entity entity, PlayerInfo playerInfo)
    {
      //var dynData = DynamicData.For(player);

      Point cell = WorldToCell(entity.Position);
      playerInfo.X = cell.X;
      playerInfo.Y = cell.Y;
      //playerInfo.X = (int)player.Position.X / 5;
      //playerInfo.Y = (int)player.Position.Y / 5;
      //playerInfo.onGround = dynData.Get<bool>("OnGround");
      //playerInfo.GrabEdge = dynData.Get<PlayerStates>("State") == PlayerStates.LedgeGrab;
      //playerInfo.Speed = player.Speed;
      //playerInfo.NbArrows = player.Arrows.Count;
      //if (0 == dynData.Get<int>("PlayerIndex"))
      //Logger.Info(playerInfo.Speed.X.ToString());
      //playerInfo.CanWallJump = dynData.Invoke<bool>("CanWallJump", Facing.Left) || dynData.Invoke<bool>("CanWallJump", Facing.Right);
      //dynData.Dispose();
    }

    void UpdateArrowInfo()
    {
      arrows.Clear();
      foreach (Arrow arrow in level[GameTags.Arrow])
      {
        ArrowInfo arrowInfo = new ArrowInfo();
        arrowInfo.state = arrow.State;
        arrowInfo.Position = arrow.Position;
        arrowInfo.Speed = arrow.Speed;
        Point cell = WorldToCell(arrow.Position);
        arrowInfo.X = cell.X;
        arrowInfo.Y = cell.Y;
        arrows.Add(arrowInfo);
      }
    }

    public Point WorldToCell(Vector2 pos)
    {
      int cellX = (int)(pos.X / BLOCK_SIZE);
      int cellY = (int)(pos.Y / BLOCK_SIZE);
      // clamp inside
      if (cellX < 0) cellX = 0;
      if (cellX >= levelWidth) cellX = levelWidth - 1;
      if (cellY < 0) cellY = 0;
      if (cellY >= levelHeight) cellY = levelHeight - 1;
      return new Point(cellX, cellY);
    }



    public void TestMovementAction(string actionName)
    {
      if (movementLibrary == null)
        InitMovementLibrary();
      // Cherche le mouvement dans la bibliothèque
      MovementAction action = movementLibrary.FirstOrDefault(a => a.Name == actionName);
      if (action == null)
      {
        Logger.Info($"❌ Mouvement {actionName} introuvable !");
        return;
      }

      //Logger.Info($"▶️ Test du mouvement : {action.Name}");

      // Réinitialise les timers et files
      currentActions.Clear();
      currentAction = action;
      currentAction.StartPoint = WorldToCell(player.Position);
      currentAction.StartPosition = player.Position;
      currentPhaseIndex = 0;

      // Initialise le timer sur la première phase si elle existe
      if (currentAction.Phases.Count > 0)
        phaseTimer = currentAction.Phases[currentPhaseIndex].Duration;

      // Place le joueur au point de départ "propre"
      if (player != null)
      {
        player.Speed = Vector2.Zero;
        if (actionName != "None")
        {
          player.Position = new Vector2(testActionX1, testActionY1); // ajuster selon le test
          currentAction.StartPoint = WorldToCell(player.Position);
          currentAction.StartPosition = player.Position;
        }
      }

      // Force le test à commencer : applique les inputs de la première phase
      if (currentAction.Phases.Count > 0)
        ApplyPhaseInputs(currentAction.Phases[currentPhaseIndex]);
    }

    // --- Description d’un mouvement possible ---




    private void InitMovementLibrary()
    {
      movementLibrary = AIMovementLibrary.BuildLibrary(this);
    }

    // --- Aides de physique simple ---
    private bool HasGroundBelow(int x, int y)
    {
      if (y + 1 >= levelHeight) return true;
      return !IsCellWalkable(x, y + 1);
    }

    private bool ClearHeadspace(int x, int y, int height)
    {
      for (int i = 1; i <= height; i++)
      {
        if (!IsCellWalkable(x, y - i)) return false;
      }
      return true;
    }

    private Point SimulateFall(Point from)
    {
      for (int f = 1; f < 24; f++) // chute max 24 cases
      {
        // --- calcul du Y avec wrap vertical ---
        int fy = (from.Y + f) % levelHeight;

        // --- calcul du X avec wrap horizontal ---
        int fx = (from.X + levelWidth) % levelWidth;

        // Arrêter la chute si on rencontre une zone dangereuse
        if (IsDangerous(fx, fy))
        {
          // retourne la position juste au-dessus de la zone dangereuse
          int prevY = (fy - 1 + levelHeight) % levelHeight;
          return new Point(fx, prevY);
        }

        // si la case n'est pas walkable (sol, obstacle, mur, etc.)
        if (!IsCellWalkable(fx, fy))
        {
          // retourne la position juste au-dessus du bloc rencontré
          int prevY = (fy - 1 + levelHeight) % levelHeight;
          return new Point(fx, prevY);
        }
      }

      // si rien n'a stoppé la chute, on atterrit juste avant de boucler
      int landingY = (from.Y + 23) % levelHeight;  //todo correction ? 23 ?
      return new Point((from.X + levelWidth) % levelWidth, landingY);
    }

    private bool IsArrowPickupCandidate(ArrowInfo a)
    {
      return a.state == ArrowStates.Stuck || a.state == ArrowStates.LayingOnGround || a.state == ArrowStates.Buried;
    }

    private Point ChooseGoal(Point start)
    {
      if (playerInfo.NbArrows <= 0)
      {
        var target = arrows
          .Where(IsArrowPickupCandidate)
          .OrderBy(a => Math.Abs(a.X - start.X) + Math.Abs(a.Y - start.Y))
          .FirstOrDefault();

        if (target != null)
        {
          var gx = target.X;
          var gy = target.Y;
          // Vérifier que la case est walkable ET non dangereuse
          if (IsCellWalkable(gx, gy) && !IsDangerous(gx, gy))
            return new Point(gx, gy);
          var candidates = new List<Point>
         {
           new Point(gx + 1, gy),
           new Point(gx - 1, gy),
           new Point(gx, gy + 1),
           new Point(gx, gy - 1)
         };
          var best = candidates
            .Where(p => p.X >= 0 && p.Y >= 0 && p.X < levelWidth && p.Y < levelHeight
                     && IsCellWalkable(p.X, p.Y) && !IsDangerous(p.X, p.Y))
            .OrderBy(p => Math.Abs(p.X - start.X) + Math.Abs(p.Y - start.Y))
            .FirstOrDefault();
          if (!best.Equals(default(Point)))
            return best;
        }
      }
      // Vérifier que la position de l'ennemi n'est pas dans une zone dangereuse
      Point enemyGoal = new Point(enemyInfo.X, enemyInfo.Y);
      if (IsDangerous(enemyGoal.X, enemyGoal.Y))
      {
        // Chercher une position proche de l'ennemi qui n'est pas dangereuse
        var safeCandidates = new List<Point>
        {
          new Point(enemyGoal.X + 1, enemyGoal.Y),
          new Point(enemyGoal.X - 1, enemyGoal.Y),
          new Point(enemyGoal.X, enemyGoal.Y + 1),
          new Point(enemyGoal.X, enemyGoal.Y - 1)
        };
        var safeGoal = safeCandidates
          .Where(p => p.X >= 0 && p.Y >= 0 && p.X < levelWidth && p.Y < levelHeight
                   && IsCellWalkable(p.X, p.Y) && !IsDangerous(p.X, p.Y))
          .OrderBy(p => Math.Abs(p.X - start.X) + Math.Abs(p.Y - start.Y))
          .FirstOrDefault();
        if (!safeGoal.Equals(default(Point)))
          return safeGoal;
      }
      return enemyGoal;
    }

  }


  public class Node
  {
    public Point Position;      // position de la case
    public float G;             // coût depuis le départ
    public float H;             // heuristique vers le but
    public float F => G + H;    // coût total
    public Node Parent;         // pour reconstruire le chemin
    public int PhaseIndex;      // index de phase si mouvement multi-phase
    public string MoveName; // 🔥 Nom du mouvement utilisé pour arriver ici
    public Node(Point position, float g, float h, Node parent = null, int phaseIndex = 0)
    {
      this.Position = position;
      this.G = g;
      this.H = h;
      this.Parent = parent;
      this.PhaseIndex = phaseIndex;
    }
  }
}

