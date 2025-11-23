using System;
using MonoMod.ModInterop;

namespace TFModFortRiseAiGraph
{
  [ModImportName("com.fortrise.EightPlayerMod")]
  public static class EigthPlayerImport
  {
    public static Func<bool> IsEightPlayer;
    public static Func<bool> LaunchedEightPlayer;
  }
}