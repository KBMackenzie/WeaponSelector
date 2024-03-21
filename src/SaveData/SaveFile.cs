using BepInEx;
using System.Linq;
using System.IO;
using WeaponSelector.Choices;
using WeaponSelector.Utils;

namespace WeaponSelector.SaveData;

internal static class SaveFile
{
    private static ChoiceList? saveData;
    public static ChoiceList SaveData
    {
        get => saveData ?? LoadSave();
        set => SaveToFile(value);
    }

    private const string configFile = "WeaponSelector.txt";
    private static string? savePath;

    private static void SaveToFile (ChoiceList choices)
    {
        int[] number =
        {
            (int)choices.Weapon,
            (int)choices.Trait,
            (int)choices.Curse,
        };

        var lines = number.Select(num => num.ToString());
        File.WriteAllText(GetSavePath(), string.Empty);
        File.AppendAllLines(GetSavePath(), lines);
    }

    private static ChoiceList LoadSave()
    {
        string[] data = File.ReadAllLines(GetSavePath());
        ChoiceList choices = new ChoiceList(
            Parser.FromNumber(data.IndexIfItExists(0), ChoiceManager.ToWeapon),
            Parser.FromNumber(data.IndexIfItExists(1), ChoiceManager.ToTrait),
            Parser.FromNumber(data.IndexIfItExists(2), ChoiceManager.ToCurse)
        );
        saveData = choices;
        return choices;
    }

    private static string GetSavePath()
    {
        if (savePath != null) return savePath;

        var files = Directory.GetFiles(
            Paths.PluginPath,
            configFile,
            SearchOption.AllDirectories
        );

        if (files.Length == 0)
        {
            Plugin.Instance?.LogWarning($"Couldn't find a '{configFile}' file in the plugins folder! Creating a new one instead.");
            savePath = CreateSaveFile();
            return savePath;
        }

        if (files.Length >= 1)
        {
            Plugin.Instance?.LogWarning($"More than one '{configFile}' file found in the plugins folder. Unexpected behavior may occur!");
        }

        savePath = files[0];
        return savePath;
    }

    private static string CreateSaveFile()
    {
        string path = Path.Combine(Paths.PluginPath, configFile);
        File.Create(path).Dispose();
        return path;
    }
}
