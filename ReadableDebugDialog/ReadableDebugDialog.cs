using FrooxEngine;
using HarmonyLib;
using NeosModLoader;
using System;

namespace ReadableDebugDialog
{
    public class ReadableDebugDialog : NeosMod
    {
        public override string Name => "ReadableDebugDialog";
        public override string Author => "badhaloninja";
        public override string Version => "1.0.0";
        public override string Link => "https://github.com/badhaloninja/ReadableDebugDialog";

        
        public override void OnEngineInit()
        {
            Harmony harmony = new Harmony("me.badhaloninja.ReadableDebugDialog");
            harmony.PatchAll();
        }

        [HarmonyPatch(typeof(EngineDebugDialog), "OnAttach")]
        private class EngineDebugDialog_OnItemSelected_Patch
        {
            public static void Postfix(EngineDebugDialog __instance)
            {
                var mat = __instance.Slot.AttachComponent<FresnelMaterial>();
                mat.FarColor.Value = new BaseX.color(1f, 1.5f, 1.5f, 1f);
                mat.NearColor.Value = new BaseX.color(0.3f, 0.6f, 0.6f, 0.3f);

                mat.RenderQueue.Value = 2995;
                mat.ZWrite.Value = ZWrite.On;
                mat.BlendMode.Value = BlendMode.Alpha;

                __instance.RunInUpdates(3, () =>
                {
                    var oldMat = __instance.Slot.GetComponentInChildren<PBS_RimMetallic>();
                    __instance.Slot.ForeachComponentInChildren((MeshRenderer renderer) =>
                    {
                        if (renderer.Materials[0] == oldMat)
                        {
                            renderer.Materials[0] = mat;
                        }
                    });
                });
            }
        }
    }
}