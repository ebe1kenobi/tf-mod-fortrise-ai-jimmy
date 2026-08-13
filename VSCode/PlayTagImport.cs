using System;
using FortRise;
using TFModFortRiseGameModePlaytag;

namespace TFModFortRiseAiGraph
{
  /// <summary>
  /// Ce que l'IA sait d'une partie de chat, quand le mod PlayTag est installe.
  ///
  /// Sans lui, l'IA joue un deathmatch au milieu d'une partie de chat : elle fonce
  /// sur le joueur le plus proche, y compris quand ce joueur est justement celui qui
  /// la poursuit. La dependance est optionnelle - sans PlayTag, tout repond "non" et
  /// l'IA se comporte comme avant.
  /// </summary>
  public static class PlayTagImport
  {
    private static IModInterop interop;
    private static IPlayTagApi api;
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
    private static IPlayTagApi Api
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
          api = interop.GetApi<IPlayTagApi>("PlayTag");
        }
        catch (Exception e)
        {
          if (!warned)
          {
            warned = true;
            Logger.Info($"[PlayTag] GetApi a leve : {e.Message}");
          }
        }

        return api;
      }
    }

    /// <summary>Vrai quand la partie en cours est une partie de chat.</summary>
    public static bool IsPlayTagMatch
    {
      get
      {
        try
        {
          return Api?.IsPlayTagMatch() ?? false;
        }
        catch (Exception)
        {
          return false;
        }
      }
    }

    /// <summary>
    /// L'index du joueur qui porte le chat, ou -1. Toute panne rend -1 plutot que de
    /// propager : un choix de cible ne doit jamais interrompre une partie.
    /// </summary>
    public static int TaggedPlayer
    {
      get
      {
        try
        {
          return Api?.TaggedPlayer() ?? -1;
        }
        catch (Exception)
        {
          return -1;
        }
      }
    }
  }
}
