using System;
//using System.Drawing;
using System.Linq;
using System.Reflection;
using FortRise;
using IL.MonoMod;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Monocle;
using MonoMod.Utils;
using Newtonsoft.Json.Linq;
using TowerFall;
using Color = Microsoft.Xna.Framework.Color;


namespace TFModFortRiseAiGraph
{
  public class MyLevel {

    public static bool sandboxEntityCreated = false;
    private static bool flipEffect = true;
    private static float flipTime = 0f;
    private static RenderTarget2D finalTarget;
    public static int test = 0;
    internal static void Load()
    {
      On.TowerFall.Level.Update += Update_patch;
      On.TowerFall.Level.HandlePausing += HandlePausing_patch;
    }

    internal static void Unload()
    {
      On.TowerFall.Level.Update -= Update_patch;
      On.TowerFall.Level.HandlePausing -= HandlePausing_patch;
    }

    public static void Update_patch(On.TowerFall.Level.orig_Update orig, global::TowerFall.Level self) {
      //Logger.Info("Update_patch");

      if (!MyTFGame.sandbox)
      {
        //try {
        test++;
        if (MyTFGame.displayPath && test%20 == 0)
          self.Add(new DebugPathRenderer(AI.agents[0])); //current agent index to debug
        orig(self);
        //}
        //catch (Exception e) {
        //  Logger.Info("Level Update Exception: " + e.Message);
        //}
        return;
      }

      int playerIndex;
      //SANDBOX
      if (!sandboxEntityCreated)
      {
        if (MyTFGame.displayPath)
          self.Add(new DebugPathRenderer(AI.agents[1]));
        playerIndex = 0;
        Player player1;
        Player player2;
        if (Agent.testMode) {
          player1 = EntityCreator.CreatePlayer(playerIndex, self.Session.MatchSettings.GetPlayerAllegiance(playerIndex), Agent.testActionX2, Agent.testActionY2);
        } else{
          player1 = EntityCreator.CreatePlayer(playerIndex, self.Session.MatchSettings.GetPlayerAllegiance(playerIndex), AI.l1_4_X1, AI.l1_4_Y1);
        }
        self.Add(player1);
        playerIndex++;
        if (Agent.testMode)
        {
          player2 = EntityCreator.CreatePlayer(playerIndex, self.Session.MatchSettings.GetPlayerAllegiance(playerIndex), Agent.testActionX1, Agent.testActionY1);
        }
        else
        {
          player2 = EntityCreator.CreatePlayer(playerIndex, self.Session.MatchSettings.GetPlayerAllegiance(playerIndex), AI.l1_4_X2, AI.l1_4_Y2);
        }
        self.Add(player2);
        playerIndex++;

        //var player2 = EntityCreator.CreatePlayer(playerIndex, self.Session.MatchSettings.GetPlayerAllegiance(playerIndex));
        //self.Add(player2);
        //playerIndex++;
        //var player3 = EntityCreator.CreatePlayer(playerIndex, self.Session.MatchSettings.GetPlayerAllegiance(playerIndex));
        //self.Add(player3);
        //playerIndex++;
        //var player4 = EntityCreator.CreatePlayer(playerIndex, self.Session.MatchSettings.GetPlayerAllegiance(playerIndex));
        //self.Add(player4);
        //playerIndex++;

        //Entity entity = EntityCreator.CreateSlime(new JObject());
        //self.Add(entity);

        self.UpdateEntityLists();
        MyLevel.sandboxEntityCreated = true;
      }

      orig(self);

      //detectEndgame(); //reset and add player
      playerIndex = 1;
      Player player = self.GetPlayer(playerIndex); //todo check
      bool update = false;
      if (player == null || player.Dead)
      {
        //Logger.Info("player dead");
        var player1 = EntityCreator.CreatePlayer(playerIndex, self.Session.MatchSettings.GetPlayerAllegiance(playerIndex), AI.l1_4_X2, AI.l1_4_Y2);
        self.Add(player1);
        update = true;
      }
      int enemyIndex = 0;
      Player enemy = self.GetPlayer(enemyIndex);

      if (enemy == null || enemy.Dead)
      {
        //Logger.Info("enemy dead");
        var player2 = EntityCreator.CreatePlayer(enemyIndex, self.Session.MatchSettings.GetPlayerAllegiance(playerIndex), AI.l1_4_X1, AI.l1_4_Y1);
        self.Add(player2);
        update = true;
      }

      if (update)
      {
        //        //delete all corpse todo
        //        //foreach (PlayerCorpse item2 in self[GameTags.Corpse])
        //        //{
        //        //  Logger.Info("corpse found");
        //        //  item2.RemoveSelf();
        //        //}

        //        self.Remove(GameTags.Corpse); //don t work
        ////self.unt
        ////Corpse
        ////UntagEntity(entity, GameTags tag)
        self.UpdateEntityLists();
      }
      //disable arrow
      //disable stomhead

    }

    public static void HandlePausing_patch(On.TowerFall.Level.orig_HandlePausing orig, global::TowerFall.Level self)
    {
      if (MyTFGame.sandbox)
      {
        return; //todo training
      }
      sandboxEntityCreated = false;

      orig(self);
    }
  }


  //}
}
