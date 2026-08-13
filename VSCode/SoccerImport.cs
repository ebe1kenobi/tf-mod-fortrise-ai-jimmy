using System;
using FortRise;
using TFModFortRiseGameModeSoccer;

namespace TFModFortRiseAiGraph
{
  /// <summary>
  /// Ce que l'IA sait d'un match de football, quand le mod Soccer est installe.
  ///
  /// Sans lui, l'IA joue un deathmatch au milieu d'un match de foot : elle poursuit le
  /// joueur le plus proche et appuie sur tir, c'est-a-dire qu'elle frappe dans le vide,
  /// le ballon etant ailleurs. La dependance est optionnelle - sans Soccer, tout repond
  /// "non" et l'IA se comporte comme avant.
  /// </summary>
  public static class SoccerImport
  {
    private static IModInterop interop;
    private static ISoccerApi api;
    private static bool warned;

    /// <summary>
    /// Retient de quoi interroger les autres mods. On ne peut PAS resoudre l'API
    /// ici : ModuleManager n'inscrit un mod dans son annuaire qu'APRES avoir execute
    /// son constructeur, donc au moment ou le notre tourne, aucun autre mod n'est
    /// encore joignable - pas meme un mod charge avant lui.
    /// </summary>
    public static void Bind(IModInterop modInterop)
    {
      interop = modInterop;
    }

    /// <summary>
    /// Resolue au premier besoin, c'est-a-dire en jeu, longtemps apres que tous les
    /// mods sont charges. Mise en cache seulement en cas de succes : une tentative
    /// infructueuse ne coute qu'une recherche dans un dictionnaire.
    /// </summary>
    private static ISoccerApi Api
    {
      get
      {
        if (api != null)
        {
          return api;
        }

        if (interop == null)
        {
          return null;
        }

        try
        {
          api = interop.GetApi<ISoccerApi>("Soccer");
        }
        catch (Exception e)
        {
          if (!warned)
          {
            warned = true;
            Logger.Info($"[Soccer] GetApi a leve : {e.Message}");
          }
        }

        return api;
      }
    }

    /// <summary>Vrai quand la partie en cours est un match de football.</summary>
    public static bool IsSoccerMatch
    {
      get
      {
        try { return Api?.IsSoccerMatch() ?? false; }
        catch (Exception) { return false; }
      }
    }

    /// <summary>Position du ballon.</summary>
    public static Microsoft.Xna.Framework.Vector2 BallPosition
    {
      get
      {
        try { return Api?.BallPosition() ?? Microsoft.Xna.Framework.Vector2.Zero; }
        catch (Exception) { return Microsoft.Xna.Framework.Vector2.Zero; }
      }
    }

    /// <summary>L'index du porteur du ballon, ou -1 s'il roule librement.</summary>
    public static int BallCarrier
    {
      get
      {
        try { return Api?.BallCarrier() ?? -1; }
        catch (Exception) { return -1; }
      }
    }

    /// <summary>Le but ou ce joueur doit marquer.</summary>
    public static Microsoft.Xna.Framework.Vector2 TargetGoal(int playerIndex)
    {
      try { return Api?.TargetGoal(playerIndex) ?? Microsoft.Xna.Framework.Vector2.Zero; }
      catch (Exception) { return Microsoft.Xna.Framework.Vector2.Zero; }
    }
  }
}
