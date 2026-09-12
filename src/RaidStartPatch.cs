using SPT.Reflection.Patching;
using Comfort.Common;
using EFT;
using HarmonyLib;
using System.Reflection;
using UnityEngine.SceneManagement;

namespace SimpleDeclutterContinue.Patches
{
    public class RaidStartPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GameWorld), nameof(GameWorld.OnGameStarted));
        }


        [PatchPostfix]
        private static void PatchPostfix(GameWorld __instance)
        {
            // Never let a mod error end the raid: any exception escaping a postfix on
            // OnGameStarted is caught by the game and aborts the match back to the menu.
            try
            {
                var gameWorld = __instance;
                if (gameWorld == null || gameWorld.MainPlayer == null || IsInHideout()) return;

                Plugin.isOnMap = true;

                Plugin.LogSource.LogInfo($"Plugin run clutter search...");

                // Build declutter list and validate targets in a single coroutine
                StaticManager.BeginCoroutine(Plugin.BuildDeclutterListCoroutine());

                Plugin.ApplyDeclutter();
                Plugin.ApplyFrameSavers();
            }
            catch (System.Exception ex)
            {
                Plugin.LogSource.LogError($"Declutter failed during raid start (raid continues): {ex}");
            }
        }

        private static bool IsInHideout()
        {
            // Check if "bunker_2" is one of the active scene names
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == "bunker_2")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
