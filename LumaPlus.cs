using BepInEx;
using HarmonyLib;
using TMPro;
namespace LumaPlus
{
    using UnityEngine;

    public class ModInfo
    {
        public const string plugin_guid = "dev.shinter.lumaplus";

        public const string plugin_name = "LumaPlus";

        public const string plugin_version = "1.0.1";
    }
    [BepInProcess("Luma Island.exe")]
    [BepInPlugin(ModInfo.plugin_guid, ModInfo.plugin_name, ModInfo.plugin_version)]
    public class LumaPlus : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo($"Plugin {ModInfo.plugin_name} is loaded!");
            var harmony = new Harmony(ModInfo.plugin_guid);
            harmony.PatchAll();
        }
        [HarmonyPatch(typeof(JoinGameRow), "OnJoinGame")]
        public class JoinGameRowPatch
        {
            static bool Prefix(JoinGameRow __instance)
            {
                // Access the private field m_lobby
                var lobby = Traverse.Create(__instance).Field<FriendLobbyInfo>("m_lobby").Value;

                // Check if lobby is not null before calling the method
                if (lobby != null)
                {
                    lobby.JoinLobby(); 
                }

                return false;
            }
        }
        [HarmonyPatch(typeof(JoinGameRow), "Initialize")]
        public class JoinGameRowButtonPatch
        {
            public static void Postfix(JoinGameRow __instance, FriendLobbyInfo lobbyInfo)
            {
                var ingametext = Traverse.Create(__instance).Field<TMP_Text>("m_playersInGameText").Value;
                var buttonjoingame = Traverse.Create(__instance).Field<ButtonWidget>("m_joinGameButton").Value;
                var versionmismatchGOJ = Traverse.Create(__instance).Field<GameObject>("m_updateRequiredDisplay").Value;
                versionmismatchGOJ.SetActive(false);
                ingametext.text = lobbyInfo.PlayersInGame + " Players / ∞";
                buttonjoingame.Interactable = true;
            }
        }

        [HarmonyPatch(typeof(FriendInfo), "CanInvite")]
        public class FriendInfoPatch
        {
            // This will replace the caninvite method
            static bool Prefix(ref bool __result)
            {
                __result = true;
                return false;
            }
        }


    }
}