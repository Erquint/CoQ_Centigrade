// using HarmonyLib;
// using ConsoleLib.Console;
// using Qud.UI;
// using System;
// using System.Collections.Generic;
// using System.Reflection;
// using System.Runtime.CompilerServices;
// using System.Text;
// using UnityEngine;
// using UnityEngine.UI;
// using XRL.Core;
// using XRL.Rules;
// using XRL.UI;
// using XRL.World;
// using XRL.World.Capabilities;
// using XRL.World.Parts;

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
      int num3 = 0;
/*
      if (XRL.UI.Sidebar.State == "left")
      {
        num3 = 0;
      }
*/
      if (XRL.UI.Sidebar.State == "right")
      {
        num3 = 56;
      }
      
      // No clue why, but this statement is required.
      XRL.UI.Sidebar.SB.AppendFormat(
        "T:{0}øC", 2 * (
          (___PlayerBody.GetPart("Physics") as XRL.World.Parts.Physics)
            .Temperature + 100
        ) / 15
      );
      _ScreenBuffer.Goto(num3 + 17, 3);
      _ScreenBuffer.Write(
        XRL.World.Event.NewStringBuilder()
          .AppendFormat(
            "T: {0}øC", 2 * (
              (___PlayerBody.GetPart("Physics") as XRL.World.Parts.Physics)
                .Temperature + 100
            ) / 15
          )
      );
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
