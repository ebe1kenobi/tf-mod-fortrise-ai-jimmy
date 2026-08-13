// Copie de l'interface exposee par le mod PlayTag. L'interop construit son proxy sur
// la FORME des membres, donc cette copie n'a pas besoin de referencer l'autre mod -
// il suffit qu'elle decrive les memes membres.
namespace TFModFortRiseGameModePlaytag;

/// <summary>
/// Ce que les autres mods peuvent savoir d'une partie de chat.
///
/// Il y en a un qui en a besoin : l'IA. Sans cela elle joue une partie de deathmatch
/// au milieu d'une partie de chat - elle fonce sur le joueur le plus proche, y compris
/// quand ce joueur est celui qui la poursuit.
///
/// Interface a part, et non des membres de plus sur une interface commune : l'interop
/// construit son proxy sur la FORME des membres, donc un appelant qui declare un
/// membre absent de la version installee n'obtient plus rien du tout. Chaque ajout
/// futur ira dans sa propre interface.
/// </summary>
public partial interface IPlayTagApi
{
  /// <summary>Vrai quand la partie en cours est une partie de chat.</summary>
  bool IsPlayTagMatch();

  /// <summary>
  /// Vrai quand ce joueur porte le chat, donc quand il est celui que les autres
  /// fuient.
  /// </summary>
  bool IsTagged(int playerIndex);

  /// <summary>
  /// L'index du joueur qui porte le chat, ou -1 s'il n'y en a pas.
  ///
  /// Rendu en plus de <see cref="IsTagged"/> parce que l'appelant type cherche
  /// justement QUI fuir : sans lui il devrait balayer les huit index.
  /// </summary>
  int TaggedPlayer();
}
