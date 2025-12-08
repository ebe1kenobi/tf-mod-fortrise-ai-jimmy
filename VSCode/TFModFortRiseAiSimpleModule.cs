using System;
using System.Diagnostics;
using FortRise;
using MonoMod.ModInterop;
using System.Diagnostics;

namespace TFModFortRiseAiGraph
{
  [Fort("com.ebe1.kenobi.TFModFortRiseAiExample", "TFModFortRiseAiExample")]
  public class TFModFortRiseAiSimpleModule : FortModule
  {
    public static TFModFortRiseAiSimpleModule Instance;
    public static bool EightPlayerMod;
    public static bool PlayTagMod;

    public override Type SettingsType => typeof(TFModFortRiseAiSimpleSettings);
    public static TFModFortRiseAiSimpleSettings Settings => (TFModFortRiseAiSimpleSettings)Instance.InternalSettings;

    public TFModFortRiseAiSimpleModule()
    {
      if (!Debugger.IsAttached)
      {
        //Debugger.Launch(); // Proposera d’attacher Visual Studio
      }
      Instance = this;
      Logger.Init("LOGAiJimmy");
    }

    public override void LoadContent()
    {
    }

    public override void Load()
    {
      MyTFGame.Load();
      MyLevel.Load();
      MyPlayer.Load();
      //MyVersusLevelSystem.Load();
      typeof(LoaderAIImport).ModInterop();
      typeof(EigthPlayerImport).ModInterop();
      EightPlayerMod = IsModExists("WiderSetMod");
      PlayTagMod = IsModExists("PlayTag");
    }

    //public static bool IsModExistsWiderSetMod() {
    //  return RiseCore.IsModExists("WiderSetMod");
    //}

    public override void Unload()
    {
      MyTFGame.Unload();
      MyLevel.Unload();
      MyPlayer.Unload();
      //MyVersusLevelSystem.Unload();
    }
  }
}
