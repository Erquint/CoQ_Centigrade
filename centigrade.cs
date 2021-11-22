namespace GnessErquint.Centigrade
{
  // Classic UI.
  [HarmonyLib.HarmonyPatch(typeof(XRL.UI.Sidebar), "Render")]
  class PatchSidebar
  {
    [HarmonyLib.HarmonyPostfix]
    static void Postfix(
      ConsoleLib.Console.ScreenBuffer _ScreenBuffer,
      XRL.World.GameObject ___PlayerBody
    )
    {
      string enabled = XRL.UI.Options
      .GetOption("Centigrade_Enabled");
      int[] bDraws1 = new int[2] {23, 24};
      if (
        (enabled == "Yes") &&
        !XRL.UI.Options.ModernUI &&
        // !XRL.UI.Options.OverlayUI &&
        !ConsoleLib.Console.Keyboard.bAlt &&
        !XRL.UI.Sidebar.Hidden &&
        (XRL.UI.Sidebar.SidebarState == 0) &&
        !(System.Array.Exists(bDraws1, x => x == GameManager.bDraw))
      )
      {
        string internalRange = XRL.UI.Options
        .GetOption("Centigrade_InternalRange");
        int internalTemperature = (
          ___PlayerBody.GetPart("Physics") as XRL.World.Parts.Physics
        ).Temperature;
        int centigradeTemperature = 0;
        if (internalRange == "-100..100")
        {
          centigradeTemperature = (internalTemperature + 100) / 2;
        }
        else if (internalRange == "-100..650")
        {
          centigradeTemperature = 2 * (internalTemperature + 100) / 15;
        }
        else
        {
          throw new System.Exception(
            "Centigrade: unsupported Internal Range option set!"
          );
        }
        string format = "T:{0}øC";
        int[] bDraws2 = new int[9] {25, 26, 27, 28, 29, 30, 31, 32, 33};
        // No clue why, but the following statement is required.
        XRL.UI.Sidebar.SB.AppendFormat(format, centigradeTemperature);
        if (!(System.Array.Exists(bDraws2, x => x == GameManager.bDraw)))
        {
          int num3 = (XRL.UI.Sidebar.State == "right") ? 56 : 0;
          _ScreenBuffer.Goto(num3 + 17, 3);
          _ScreenBuffer.Write(
            XRL.World.Event.NewStringBuilder()
            .AppendFormat(format, centigradeTemperature)
          );
        }
      }
    }
  }
  
  /*
  // Overlay UI.
  [HarmonyLib.HarmonyPatch(typeof(Qud.UI.PlayerStatusBar), "BeginEndTurn")]
  class PatchStatusBar
  {
    private enum StringDataType
    {
      FoodWater,
      Time,
      Temp,
      Weight,
      Zone,
      HPBar,
      PlayerName
    }
    
    [HarmonyLib.HarmonyPostfix]
    static void Postfix(
      Qud.UI.PlayerStatusBar __instance,
      XRLCore core
    )
    {
      MethodInfo methodInfo = __instance?.GetType()?.GetMethod(
        "UpdateString", BindingFlags.NonPublic | BindingFlags.Instance
      );
      if (methodInfo != null)
      {
        object[] parametersArray = new object[] {
          StringDataType.Temp,
          "T:" + (
            2 * (core.Game.Player.Body.pPhysics.Temperature + 100) / 15
          ).ToString() + "øC",
          false
        };
        methodInfo.Invoke(__instance, parametersArray);
      }
    }
// Results in:
// System.ArgumentException: Object of type 'GnessErquint.Centigrade.PatchStatusBar+StringDataType' cannot be converted to type 'Qud.UI.PlayerStatusBar+StringDataType'.
//   at System.RuntimeType.CheckValue (System.Object value, System.Reflection.Binder binder, System.Globalization.CultureInfo culture, System.Reflection.BindingFlags invokeAttr) [0x00071] in <695d1cc93cca45069c528c15c9fdd749>:0 
  }
  */
}
