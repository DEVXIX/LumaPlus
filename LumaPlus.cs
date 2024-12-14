using BepInEx;
using HarmonyLib;
using TMPro;
using Steamworks;
namespace LumaPlus
{
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Reflection.Emit;
    using global::System.Reflection;
    using global::System;
    using UnityEngine;
    using UnityEngine.UI;
    using Steamworks;

    public class ModInfo
    {
        public const string plugin_guid = "dev.shinter.lumaplus";

        public const string plugin_name = "LumaPlus";

        public const string plugin_version = "1.0.2";
    }
    [BepInProcess("Luma Island.exe")]
    [BepInPlugin(ModInfo.plugin_guid, ModInfo.plugin_name, ModInfo.plugin_version)]
    public class LumaPlus : BaseUnityPlugin
    {
        public static string ModdedNetworkApplicationVersion { get; private set; }
        private void Awake()
        {
            Logger.LogInfo($"Plugin {ModInfo.plugin_name} is loaded!");
            var harmony = new Harmony(ModInfo.plugin_guid);
            harmony.PatchAll();
            ModdedNetworkApplicationVersion = Application.version + " (modded)";
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
                
                lobbyInfo.IsVersionMismatch = false;
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
        [HarmonyPatch]
        internal static class VersionPatches
        {
            private static IEnumerable<MethodInfo> TargetMethods()
            {
                yield return AccessTools.Method(typeof(SteamLobbyController), "OnLobbyCreated", (Type[])null, (Type[])null);
            }

            private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> code)
            {

                CodeMatcher val = new CodeMatcher(code, (ILGenerator)null).MatchForward(false, (CodeMatch[])(object)new CodeMatch[1]
                {
                new CodeMatch((OpCode?)OpCodes.Call, (object)AccessTools.Property(typeof(Application), "version").GetGetMethod(), (string)null)
                });
                List<Label> labels = val.Instruction.labels.ToList();
                val.Instruction.labels.Clear();
                val.RemoveInstruction().Insert((CodeInstruction[])(object)new CodeInstruction[1]
                {
                new CodeInstruction(OpCodes.Call, (object)AccessTools.Property(typeof(LumaPlus), "ModdedNetworkApplicationVersion").GetGetMethod())
                });
                val.Instruction.labels = labels;
                return val.InstructionEnumeration();
            }
        }
    }
    [HarmonyPatch(typeof(SteamLobbyController))]
    [HarmonyPatch("OnLobbyEntered")]
    public class SteamLobbyControllerPatch
    {
        static bool Prefix(object __instance, LobbyEnter_t enter)
        {
            var type = __instance.GetType();
            FieldInfo lobbyIDField = type.GetField("lobbyID", BindingFlags.NonPublic | BindingFlags.Instance);
            CSteamID lobbyID = (CSteamID)lobbyIDField.GetValue(__instance);
            FieldInfo isLobbyOwnerField = type.GetField("isLobbyOwner", BindingFlags.NonPublic | BindingFlags.Instance);
            PropertyInfo hostSteamIdProperty = type.GetProperty("HostSteamId", BindingFlags.Public | BindingFlags.Instance);
            CSteamID hostSteamId = SteamMatchmaking.GetLobbyOwner(lobbyID);
            hostSteamIdProperty.SetValue(__instance, hostSteamId);
            Debug.Log("Entered lobby with owner " + hostSteamId.ToString() + ". Starting game");
            if (Network.IsClient)
            {
                isLobbyOwnerField.SetValue(__instance, false);
                Bootstrap.StartGame("Misc", false, false);
            }
            return false;
        }
    }

    namespace System.Runtime.CompilerServices
    {
        [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
        internal sealed class IgnoresAccessChecksToAttribute : Attribute
        {
            public IgnoresAccessChecksToAttribute(string assemblyName)
            {
            }
        }
    }

}
