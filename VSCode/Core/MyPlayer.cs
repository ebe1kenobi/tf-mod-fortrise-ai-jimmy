using System;
using System.Collections.Generic;
using System.Net;
using Monocle;
using FortRise;
using Microsoft.Xna.Framework;
using TowerFall;
using MonoMod.Utils;

namespace TFModFortRiseAiGraph
{
  public class MyPlayer
  {
    
    internal static void Load()
    {
      On.TowerFall.Player.HurtBouncedOn += HurtBouncedOn_patch;
      
    }

    internal static void Unload()
    {
      On.TowerFall.Player.HurtBouncedOn -= HurtBouncedOn_patch;
    }

    public static void HurtBouncedOn_patch(On.TowerFall.Player.orig_HurtBouncedOn orig, global::TowerFall.Player self, int bouncerIndex) {
      //return;
      orig(self, bouncerIndex);
    }



  }
}
