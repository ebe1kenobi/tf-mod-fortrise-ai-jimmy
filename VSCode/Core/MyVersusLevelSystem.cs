using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monocle;
using Microsoft.Xna.Framework;
using MonoMod.Utils;
using TowerFall;
using System.Xml.Linq;
using System.Collections;
using System.Runtime.Remoting.Messaging;
using System.Xml;

namespace TFModFortRiseAiGraph
{
  internal class MyVersusLevelSystem
  {
    internal static void Load()
    {
      On.TowerFall.VersusLevelSystem.GenLevels += GenLevels_patch;
      On.TowerFall.VersusLevelSystem.GetNextRoundLevel += GetNextRoundLevel_patch;
      
    }

    internal static void Unload()
    {
      On.TowerFall.VersusLevelSystem.GenLevels -= GenLevels_patch;
      On.TowerFall.VersusLevelSystem.GetNextRoundLevel -= GetNextRoundLevel_patch;
    }

    public static void GenLevels_patch(On.TowerFall.VersusLevelSystem.orig_GenLevels orig, global::TowerFall.VersusLevelSystem self, global::TowerFall.MatchSettings matchSettings)
    {
      if (!MyTFGame.sandbox) {
        orig(self, matchSettings);
        return;
      }
      var dynData = DynamicData.For(self);
      dynData.Set("levels", self.VersusTowerData.GetLevels(matchSettings));
      dynData.Dispose();
    }

    public static XmlElement GetNextRoundLevel_patch(On.TowerFall.VersusLevelSystem.orig_GetNextRoundLevel orig, global::TowerFall.VersusLevelSystem self, global::TowerFall.MatchSettings matchSettings, int roundIndex, out int randomSeed)
    {
      if (!MyTFGame.sandbox)
      {
        return orig(self, matchSettings, roundIndex, out randomSeed);
      }

      var dynData = DynamicData.For(self);
      dynData.Set("levels", self.VersusTowerData.GetLevels(matchSettings));
      List<string> levels = (List<string>)dynData.Get("levels");
      //Logger.Info("levels.count = " + levels.Count);

      if (levels.Count == 0)
      {
        //self.GenLevels(matchSettings);
        dynData.Invoke("GenLevels", matchSettings);
      }
      //Logger.Info("levels.count = " + levels.Count);
      //Logger.Info("levels[0] = " + levels[7]);

      dynData.Set("lastLevel", levels[MyTFGame.sublevel]); 


      ////self.lastLevel = self.levels[0];
      //dynData.Set("lastLevel", ((List<string>)dynData.Get("levels"))[0]);
      ////self.levels.RemoveAt(0);
      //((List<string>)dynData.Get("levels")).RemoveAt(0);
      randomSeed = 0;
      return Calc.LoadXML((string)dynData.Get("lastLevel"))["level"];
    }
  }
}
