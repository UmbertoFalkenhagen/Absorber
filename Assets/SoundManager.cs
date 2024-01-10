using UnityEngine;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [HideInInspector]
        public AudioSource source; // Ensure this line is correctly defined
    }

    public List<Sound> sounds;
    private Dictionary<string, AudioSource> playingSounds = new Dictionary<string, AudioSource>();

    public static SoundManager Instance { get; private set; }

    void Awake()
    {
        // Singleton pattern to ensure only one SoundManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Initialize AudioSource for each sound
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
        }
    }
    private void Start()
    {
        PlaySoundForever("MainSoundtrack");
        ChangeSoundVolume("EngineRunning", 0.5f);
    }

    public void PlaySoundOnce(string soundName)
    {
        Sound sound = sounds.Find(s => s.name == soundName);
        if (sound != null && sound.clip != null)
        {
            AudioSource.PlayClipAtPoint(sound.clip, transform.position);
        }
    }

    public void PlaySoundForever(string soundName)
    {
        if (playingSounds.ContainsKey(soundName))
        {
            return; // Sound is already playing
        }

        Sound sound = sounds.Find(s => s.name == soundName);
        if (sound != null && sound.clip != null)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = sound.clip;
            source.loop = true;
            source.Play();
            playingSounds[soundName] = source;
        }
    }

    public void StopSound(string soundName)
    {
        if (playingSounds.TryGetValue(soundName, out AudioSource source))
        {
            Destroy(source);
            playingSounds.Remove(soundName);
        }
    }

    public float GetSoundLength(string soundName)
    {
        Sound sound = sounds.Find(s => s.name == soundName);
        return sound != null ? sound.clip.length : 0f;
    }

    public void StopAllSounds()
    {
        foreach (var kvp in playingSounds)
        {
            Destroy(kvp.Value);
        }
        playingSounds.Clear();
    }

    public void ChangeSoundVolume(string soundName, float newVolume)
    {
        Sound sound = sounds.Find(s => s.name == soundName);
        if (sound != null && sound.source != null)
        {
            sound.source.volume = Mathf.Clamp(newVolume, 0f, 1f); // Clamp the volume between 0 and 1
        }
    }
}