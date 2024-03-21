using BepInEx;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace WeaponSelector;

internal static class SaveFile
{
    /* TODO:
     *  1. Look for bugs
     *  2. I don't know */

    private static string savePath = null;

    public delegate void SaveEvent();
    public static event SaveEvent SaveActions;

    public static string SavePath
    {
        get => savePath ?? FindSave();
    }

    public static (WeaponChoice, WeaponTrait, CurseChoice) SaveData
    {
        get { return LoadSave(); }
        set { SaveToFile(value); }
    }

    private static void SaveToFile ((WeaponChoice, WeaponTrait, CurseChoice) data)
    {
        int[] index =
        {
            (int)data.Item1,
            (int)data.Item2,
            (int)data.Item3
        };

        List<string> indexes = index.Select(x => x.ToString()).ToList();

        File.WriteAllText(SavePath, string.Empty);
        File.AppendAllLines(SavePath, indexes);

        SaveActions?.Invoke();
    }

    private static (WeaponChoice, WeaponTrait, CurseChoice) LoadSave()
    {
        string[] data = File.ReadAllLines(SavePath);
        var choices = EnumHelpers.GetDefault();

        int[] a =
        {
            ParseData(data.IndexIfItExists(0), ChangeType.Weapon, choices.Item1.EnumLength()),
            ParseData(data.IndexIfItExists(1), ChangeType.Trait,  choices.Item2.EnumLength()),
            ParseData(data.IndexIfItExists(2), ChangeType.Curse,  choices.Item3.EnumLength()),
        };

        choices = ((WeaponChoice)a[0], (WeaponTrait)a[1], (CurseChoice)a[2]);

        return choices;
    }

    private static int ParseData(string a, ChangeType type, int max)
    {
        bool flag = Int32.TryParse(a, out int index);

        if (!flag)
        {
            Plugin.Instance?.LogWarning($"Couldn't read config data from {Path.GetFileName(SavePath)}. {type} settings now saved as default.");
            return EnumHelpers.DefaultFromInt(type);
        }

        if (index >= max)
        {
            Plugin.Instance?.LogWarning($"Tried to load index out of range. {type} settings now saved as default.");
            return EnumHelpers.DefaultFromInt(type);
        }

        return index;
    }


    private static string FindSave()
    {
        string configName = "WeaponChoice.txt";

        string[] files = Directory.GetFiles(Paths.PluginPath, configName, SearchOption.AllDirectories);

        if(files.Length == 0)
        {
            Plugin.Instance?.LogWarning($"Couldn't find file \"{configName}\". Creating that file instead.");
            string path = Path.Combine(Paths.PluginPath, configName);
            File.Create(path).Dispose();
            savePath = path;
            return savePath;
        }
        else if (files.Length > 1)
        {
            Plugin.Instance?.LogWarning($"Unexpected behavior: More than one file named \"{configName}\".");
        }

        savePath = files[0];
        return savePath;
    }
}
