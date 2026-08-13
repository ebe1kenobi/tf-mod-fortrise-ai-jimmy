using System;
using System.Diagnostics;
using FortRise;
using Microsoft.Extensions.Logging;
using TFModFortRiseLoaderAI;
using Teuria.WiderSet;

namespace TFModFortRiseAiGraph
{
  public class TFModFortRiseAiSimpleModule : Mod
  {
    public static TFModFortRiseAiSimpleModule Instance;

    internal Type[] Hookables = [
        typeof(MyTFGame),
        //typeof(MyLevel),
        //typeof(MyPlayer),
        //typeof(MyVersusLevelSystem),
    ];

    public static bool EightPlayerMod = false; // vrai si le mod WiderSet est present (mode 8 joueurs disponible)
    public static bool PlayTagMod = false; //todo

    // API WiderSet (dependance optionnelle) : null si le mod n'est pas installe.
    // Remplace l'ancien EigthPlayerImport de FortRise 4.
    public static IWiderSetModApi WiderSet;


    //public override Type SettingsType => typeof(TFModFortRiseAiSimpleSettings);
    //public static TFModFortRiseAiSimpleSettings Settings => (TFModFortRiseAiSimpleSettings)Instance.InternalSettings;

    public ILoaderAIModApi LoaderAIModApi { get; private set; }


    public TFModFortRiseAiSimpleModule(IModContent content, IModuleContext context, ILogger logger) : base(content, context, logger)
    {
      if (!Debugger.IsAttached)
      {
        //Debugger.Launch(); // Proposera d’attacher Visual Studio
      }
      Instance = this;
      TFModFortRiseAiGraph.Logger.Init(logger);
      foreach (var hookable in Hookables)
      {
        hookable.GetMethod(nameof(IHookable.Load))!.Invoke(null, [context.Harmony]);
      }
      //typeof(LoaderAIImport).ModInterop();
      LoaderAIModApi = context.Interop.GetApi<ILoaderAIModApi>("LoaderAI");
      // Dependance optionnelle : GetApi renvoie null si WiderSet n'est pas installe.
      WiderSet = context.Interop.GetApi<IWiderSetModApi>("Teuria.WiderSet");
      EightPlayerMod = WiderSet != null;

      // PlayTag se resout PLUS TARD, au premier besoin. Ces deux-la se resolvent ici
      // et cela marche - un mod charge avant nous est bien joignable depuis notre
      // constructeur - mais cela depend de l'ordre de chargement, qui change des
      // qu'on installe ou retire un mod. Voir PlayTagImport.
      PlayTagImport.Bind(context.Interop);
      SoccerImport.Bind(context.Interop);
    }

    //public override void LoadContent()
    //{
    //}

    //public override void Load()
    //{
    //  MyTFGame.Load();
    //  MyLevel.Load();
    //  MyPlayer.Load();
    //  //MyVersusLevelSystem.Load();
    //  typeof(LoaderAIImport).ModInterop();
    //  typeof(EigthPlayerImport).ModInterop();
    //  EightPlayerMod = IsModExists("WiderSetMod");
    //  PlayTagMod = IsModExists("PlayTag");
    //}

    //public static bool IsModExistsWiderSetMod() {
    //  return RiseCore.IsModExists("WiderSetMod");
    //}

    //public override void Unload()
    //{
    //  MyTFGame.Unload();
    //  MyLevel.Unload();
    //  MyPlayer.Unload();
    //  //MyVersusLevelSystem.Unload();
    //}
  }
}
