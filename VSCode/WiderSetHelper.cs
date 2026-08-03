namespace TFModFortRiseAiGraph
{
  // Petit helper pour interroger l'etat "wide" (mode 8 joueurs) du mod WiderSet
  // sans se soucier de sa presence : renvoie false si WiderSet n'est pas installe.
  // Remplace l'ancien EigthPlayerImport (MonoMod.ModInterop) de FortRise 4.
  public static class WiderSetHelper
  {
    public static bool IsWide
    {
      get => TFModFortRiseAiSimpleModule.WiderSet?.IsWide ?? false;
    }
  }
}
