using BepInEx;
using HarmonyLib;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace WeaponSelector
{
    internal static class SaveFile
    {
        /* TODO:
         *  1. Look for bugs
         *  2. I don't know */

        static string savePath = null;

        public delegate void SaveEvent();
        public static event SaveEvent SaveActions;

        public static string SavePath
        {
            get
            {
                return savePath ?? FindSave();
            }
        }

        public static (WeaponChoices, WeaponTraits, CurseChoices) SaveData
        {
            get { return LoadSave(); }
            set { SaveToFile(value); }
        }

        static void SaveToFile ((WeaponChoices, WeaponTraits, CurseChoices) data)
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

        static (WeaponChoices, WeaponTraits, CurseChoices) LoadSave()
        {
            string[] data = File.ReadAllLines(SavePath);
            var choices = EnumHelpers.GetDefault();

            int[] a =
            {
                ParseData(data.IndexIfItExists(0), Change.Weapon, choices.Item1.EnumLength()),
                ParseData(data.IndexIfItExists(1), Change.Trait,  choices.Item2.EnumLength()),
                ParseData(data.IndexIfItExists(2), Change.Curse,  choices.Item3.EnumLength()),
            };

            choices = ((WeaponChoices)a[0], (WeaponTraits)a[1], (CurseChoices)a[2]);

            return choices;
        }

        static int ParseData(string a, Change type, int max)
        {
            bool flag = Int32.TryParse(a, out int index);

            if (!flag)
            {
                Plugin.myLogger.LogWarning($"Couldn't read config data from {Path.GetFileName(SavePath)}. {type} settings now saved as default.");
                return EnumHelpers.DefaultFromInt(type);
            }

            if (index >= max)
            {
                Plugin.myLogger.LogWarning($"Tried to load index out of range. {type} settings now saved as default.");
                return EnumHelpers.DefaultFromInt(type);
            }

            return index;
        }


        static string FindSave()
        {
            string configName = "WeaponChoice.txt";

            string[] files = Directory.GetFiles(Paths.PluginPath, configName, SearchOption.AllDirectories);

            if(files.Length == 0)
            {
                Plugin.myLogger.LogWarning($"Couldn't find file \"{configName}\". Creating that file instead.");
                string path = Path.Combine(Paths.PluginPath, configName);
                File.Create(path).Dispose();
                savePath = path;
                return savePath;
            }
            else if (files.Length > 1)
            {
                Plugin.myLogger.LogWarning($"Unexpected behavior: More than one file named \"{configName}\".");
            }

            savePath = files[0];
            return savePath;
        }
    }
}
