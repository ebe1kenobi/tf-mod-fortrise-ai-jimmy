using Microsoft.Xna.Framework;
using TowerFall;
using System.Threading.Tasks;
using System;
using System.Threading;
using System;
using Monocle;
using FortRise;

namespace TFModFortRiseAiGraph
{
  internal class MyTFGame
  {
    static bool sessionStarted = false;
    static int counter = 0;
    public static bool sandbox = true;
    public static bool displayPath = true;
    public static int level = 1;
    public static int sublevel = 1;
    public static bool customLevel = true;
    internal static void Load()
    {
      On.TowerFall.TFGame.Update += Update_patch;
      //System.Threading.Thread.Sleep(3000); //wait for all tasks to finish, particulary FX load at start

      //while (TaskHelper.WaitForAll()) // tant qu'il reste des tasks
      //{
      //  System.Threading.Thread.Sleep(1000); //wait for all tasks to finish, particulary FX load at start
      //}
    }

    internal static void Unload()
    {
      On.TowerFall.TFGame.Update -= Update_patch;
    }

    public static void Update_patch(On.TowerFall.TFGame.orig_Update orig, global::TowerFall.TFGame self, GameTime gameTime)
    {
      orig(self, gameTime);
      if (LoaderAIImport.CanAddAgent())
      {
        AI.CreateAgent();
      }

      if (MyTFGame.sandbox) {
        //if (TFGame.GameLoaded && AI.isAgentReady && !sessionStarted) //wait 5s to sfx load
        if (TFGame.GameLoaded && AI.isAgentReady && !sessionStarted && counter > 1000) //wait 5s to sfx load
        {
          //base.MainMenu.State = MainMenu.MenuState.Main;
          AI.StartNewSession();
          sessionStarted = true;
        }
        counter++;
      }
    }
  }
}
