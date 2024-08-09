using System.Collections;
using UnityEngine;
using LevelManager;

public static class SceneManager
{
    public static Vector3 lastLoadPosition { get; private set; }
    public static LevelSetting currentLevel;
    public static LevelSetting lastLevel;
    public static Vector3 lastLevelPosition;
    public static IEnumerator WaitFrame() { yield return null; }

    public static void ChangeProperty()
    {

        var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        if (currentScene != currentLevel.name)
        {
            if (currentScene == "platformer") PlatformerValues();
            else DefaultValues();
        }
    }


    public static void LoadScene(string name, Vector3 position)
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SetLevelSetting(name);
        ChangeMusic();
        ChangeProperty();
        PixelCrushers.SaveSystem.LoadScene(name);

        if (name == "platformer")
            lastLevelPosition = position;

    }

    private static void SetLevelSetting(string name)
    {
        lastLevel = currentLevel;
        if (name.Contains("@"))
        {
            var parsedName = name.Split('@')[0];
            currentLevel = LevelSettingsDatabase.GetLevel[parsedName];
        }
        else
        {
            currentLevel = LevelSettingsDatabase.GetLevel[name];
        }
    }

    private static void ChangeMusic()
    {
        //SoundPlayer.PlayMusicBG(currentLevel.songName);


        // TODO https://trello.com/c/HTrbqU5c/9-create-system-for-non-linear-soundtrack
        // TODO grab from level settings / make music settings
        // musicController.SetTrack(Track.Level1)
        // musicController.SetIntensity(Intensity.Battle)
        // MusicController.StopAllSongs();
        //MusicController.StartFadeIn(currentLevel.songName);
    }

    public static void PlatformerValues()
    {

    }

    public static void DefaultValues()
    {

    }
}
