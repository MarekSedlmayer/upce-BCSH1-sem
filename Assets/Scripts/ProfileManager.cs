using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Handles IO.
/// </summary>
public static class ProfileManager
{
    public static ProfileData Profile = null;
    private static int _index = -1;
    public static int Index => _index;

    private static readonly string _wildCardString = "*";
    private static readonly string _fileExtension = ".txt";
    private static readonly string _profileFileName = "profile_" + _wildCardString + _fileExtension;

    public static void SetActiveProfile(ProfileData profile, int index)
    {
        Profile = profile;
        _index = index;
    }

    private static string BuildProfilePathFromIndex(int index)
    {
        return Path.Combine(Application.persistentDataPath, _profileFileName.Replace(_wildCardString, (index + 1).ToString()));
    }

    public static void SaveProfile(ProfileData profile, int index)
    {
        File.WriteAllText(BuildProfilePathFromIndex(index), JsonUtility.ToJson(profile));
    }

    public static bool DeleteProfile(int index)
    {
        string path = BuildProfilePathFromIndex(index);
        if (File.Exists(path))
        {
            File.Delete(path);
            return true;
        }
        return false;
    }

    public static ProfileData LoadProfile(int index)
    {
        string path = BuildProfilePathFromIndex(index);
        if (File.Exists(path))
        {
            return JsonUtility.FromJson<ProfileData>(File.ReadAllText(path));
        }
        return null;
    }
}
