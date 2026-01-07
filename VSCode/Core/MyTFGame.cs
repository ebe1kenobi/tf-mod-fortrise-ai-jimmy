using System;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FortRise;
using HarmonyLib;
using Microsoft.Xna.Framework;
using Monocle;
using TFModFortRiseAI.Abstractions;
using TowerFall;

namespace TFModFortRiseAiGraph
{
  internal class MyTFGame : IHookable
  {
    static bool sessionStarted = false;
    static int counter = 0;
    public static bool sandbox = false;
    public static bool displayPath = false;
    public static int level = 1;
    public static int sublevel = 1;
    public static bool customLevel = true;

    public static List<IAgentLogic> agents;

    static bool RegisterAgent = false;
    public static void Load(IHarmony harmony)
    {
      harmony.Patch(
          AccessTools.DeclaredMethod(typeof(TFGame), "Update"),
          prefix: new HarmonyMethod(Update_patch_prefix)
      );
    }

    //internal static void Load()
    //{
    //  On.TowerFall.TFGame.Update += Update_patch;
    //  //System.Threading.Thread.Sleep(3000); //wait for all tasks to finish, particulary FX load at start

    //  //while (TaskHelper.WaitForAll()) // tant qu'il reste des tasks
    //  //{
    //  //  System.Threading.Thread.Sleep(1000); //wait for all tasks to finish, particulary FX load at start
    //  //}
    //}

    //internal static void Unload()
    //{
    //  On.TowerFall.TFGame.Update -= Update_patch;
    //}

    public static void Update_patch_prefix(TFGame __instance)
    {
      if (TFModFortRiseAiSimpleModule.Instance.LoaderAIModApi.CanAddAgent() && !RegisterAgent)
      {
        Logger.Info("TFModFortRiseAiSimpleModule RegisterAgent");
        agents = [
              new AILogic(),
              new AILogic(),
              new AILogic(),
              new AILogic(),
              new AILogic(),
              new AILogic(),
              new AILogic(),
              new AILogic(),
];
        TFModFortRiseAiSimpleModule.Instance.LoaderAIModApi.RegisterAgent(
              agents
              );
        RegisterAgent = true;
      }

      //if (MyTFGame.sandbox) {
      //  //if (TFGame.GameLoaded && AI.isAgentReady && !sessionStarted) //wait 5s to sfx load
      //  if (TFGame.GameLoaded && AI.isAgentReady && !sessionStarted && counter > 1000) //wait 5s to sfx load
      //  {
      //    //base.MainMenu.State = MainMenu.MenuState.Main;
      //    AI.StartNewSession();
      //    sessionStarted = true;
      //  }
      //  counter++;
      //}
    }
  }
}
