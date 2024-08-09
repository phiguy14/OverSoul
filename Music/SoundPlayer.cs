using UnityEngine;
using OSSC;
public enum SoundTypes
{
    Environment,
    Player,
    Slime,
    Crow
}


public static class SoundPlayer
{
    private static SoundController soundController;
    private static ISoundCue music;
    public static ISoundCue Play(string soundName, SoundTypes type, bool looped = false)
    {
        var settings = new PlaySoundSettings();
        settings.Init();
        settings.isLooped = looped;
        settings.name = soundName;
        settings.categoryName = type.ToString();
        Debug.Log("type: " + settings.categoryName);
        return PlaySound(settings);
    }

    public static void PlayMusicBG(string soundName)
    {
        if (music != null && music.IsPlaying) music.Stop();

        var settings = new PlaySoundSettings();
        settings.Init();
        settings.name = soundName;

        Debug.Log(soundName);
        Debug.Log("settings" + settings.name);

        settings.fadeInTime = .5f;
        settings.fadeOutTime = .5f;
        settings.categoryName = "Music";
        settings.isLooped = true;

        if (soundController == null)
        {
            soundController = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<SoundController>();
            Debug.Log("sound controller mounted");
        }
        music = soundController.Play(settings);
        Debug.Log(music);
    }

    private static ISoundCue PlaySound(PlaySoundSettings settings)
    {
        if (soundController == null) { soundController = GameObject.FindGameObjectWithTag("EventSystem").GetComponent<SoundController>(); }
        return soundController.Play(settings);
    }
}
