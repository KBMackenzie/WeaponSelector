using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace WeaponSelector;

[BepInPlugin(PluginGuid, PluginName, PluginVer)]
public class Plugin : BaseUnityPlugin
{
    public const string PluginGuid  = "kel.cotl.weaponselector";
    public const string PluginName  = "Weapon Selector";
    public const string PluginVer   = "1.0.3";

    internal static ManualLogSource myLogger;

    private void Awake()
    {
        myLogger = Logger; // Make log source

        Logger.LogInfo($"Loaded {PluginName} successfully!");

        Harmony harmony = new Harmony("kel.harmony.weaponselector");
        harmony.PatchAll();

        void Choice() => WeaponPatches.Weapon = SaveFile.SaveData.Item1;
        void Trait()  => WeaponPatches.Trait = SaveFile.SaveData.Item2;
        void Curse()  => WeaponPatches.Curse = SaveFile.SaveData.Item3;

        SaveFile.SaveActions += Choice;
        SaveFile.SaveActions += Trait;
        SaveFile.SaveActions += Curse;
        Choice(); Trait(); Curse();
    }
}
