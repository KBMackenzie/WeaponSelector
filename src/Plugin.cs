using BepInEx;
using HarmonyLib;
using WeaponSelector.Patches;
using WeaponSelector.SaveData;

namespace WeaponSelector;

[BepInPlugin(PluginGuid, PluginName, PluginVer)]
public class Plugin : BaseUnityPlugin
{
    public const string PluginGuid = "kel.cotl.weaponselector";
    public const string PluginName = "Weapon Selector";
    public const string PluginVer  = "1.2.0";

    internal static Plugin? Instance;

    public void Log(string message)
        => this.Logger.LogInfo(message);

    public void LogError(string message)
        => this.Logger.LogError(message);

    public void LogWarning(string message)
        => this.Logger.LogWarning(message);

    private void Awake()
    {
        Instance = this;
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
