using System;

namespace WeaponSelector.SaveData;

public class Parser
{
    public static T FromNumber<T>(string? str, Func<int, T> callback)
    {
        bool couldParse = Int32.TryParse(str, out int value);
        if (!couldParse) {
            Plugin.Instance?.LogWarning($"Couldn't parse string {str ?? "<empty>"} as a valid number!");
            return callback(default);
        }
        return callback(value);
    }
}
