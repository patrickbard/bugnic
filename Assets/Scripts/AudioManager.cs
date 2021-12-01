using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance;

    public static List<Sound> _soundtracks;
    
    public static List<Sound> _flySwatterSounds;
    public static List<Sound> _bugDeathSounds;
    
    public static List<Sound> _beginningSounds;
    public static List<Sound> _gameOverSounds;
    public static List<Sound> _winningSounds;
    public static bool isApplicationPaused;


    // public AudioMixerGroup mixerGroup;
    public string startingSoundtrackName;
    public List<Sound> soundtracks;
    public List<Sound> flySwatterSounds;
    public List<Sound> bugDeathSounds;
    
    public List<Sound> beginningSounds;
    public List<Sound> gameOverSounds;
    public List<Sound> winningSounds;

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            _soundtracks = soundtracks;

            _flySwatterSounds = flySwatterSounds;
            _bugDeathSounds = bugDeathSounds;
            
            _beginningSounds = beginningSounds;
            _gameOverSounds = gameOverSounds;
            _winningSounds = winningSounds;

            InitializeSoundList(_soundtracks);
            InitializeSoundList(_flySwatterSounds);
            InitializeSoundList(_bugDeathSounds);
            InitializeSoundList(_beginningSounds);
            InitializeSoundList(_gameOverSounds);
            InitializeSoundList(_winningSounds);

            // PlaySoundtrackByName(startingSoundtrackName);
            // PlayRandomSoundtrack();
            PlayRandomBeginningSong();
        }
    }

    private void InitializeSoundList(List<Sound> sounds) {
        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.playOnAwake = false;
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
    }

    void OnApplicationFocus(bool hasFocus) {
        isApplicationPaused = false;
    }

    void OnApplicationPause(bool pauseStatus) {
        isApplicationPaused = true;
    }

    private void Update() {
        if (!isApplicationPaused) {
            bool isPlayingBeginningSong = beginningSounds.Any(sound => sound.source.isPlaying);
            bool isPlayingSoundtrack = soundtracks.Any(sound => sound.source.isPlaying);
            bool isPlayingGameOverSong = gameOverSounds.Any(sound => sound.source.isPlaying);

            
            switch (SceneManager.GetActiveScene().buildIndex) {
                case 0:
                    if (!isPlayingBeginningSong) {
                        StopPlayingAll();
                        PlayRandomBeginningSong();
                    }
                    break;
                case 1:
                    if (!isPlayingSoundtrack) {
                        StopPlayingAll();
                        PlayRandomSoundtrack();
                    }
                    break;
                case 2:
                    if (!isPlayingGameOverSong) {
                        StopPlayingAll();
                        PlayRandomGameOverSong();
                    }
                    break;
            }
        }
    }

    public static void PlaySoundtrackByName(string soundName) {
        foreach (Sound soundtrack in _soundtracks) {
            soundtrack.source.Stop();
        }
        PlayByName(soundName, _soundtracks);
    }
    
    public static void PlaySoundtrackByNameFrom(string soundName, float fromSeconds, float toSeconds) {
        PlayByName(soundName, _soundtracks, fromSeconds, toSeconds);
    }
    
    public static void PlayByName(string soundName, List<Sound> sounds, float fromSeconds = -1, float toSeconds = -1) {
        Sound sound = sounds.Find( item => item.name == soundName);

        if (sound == null) {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }

        Play(sound, fromSeconds, toSeconds);
    }

    public static void Play(Sound sound, float fromSeconds = -1, float toSeconds = -1) {
        sound.source.volume = sound.volume * (1f + Random.Range(-sound.volumeVariance / 2f, sound.volumeVariance / 2f));
        sound.source.pitch = sound.pitch * (1f + Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));

        if (fromSeconds > 0) {
            sound.source.time = fromSeconds;
        }

        sound.source.Play();

        if (toSeconds > 0) {
            sound.source.SetScheduledEndTime(AudioSettings.dspTime + (toSeconds - fromSeconds));
        }
    }

    public static void PlayRandomSoundtrack() {
        PlayRandomSoundFromList(_soundtracks);
    }
    
    public static void PlayRandomFlySwatterSound() {
        PlayRandomSoundFromList(_flySwatterSounds);
    }
    
    public static void PlayRandomBugDeathSound() {
        PlayRandomSoundFromList(_bugDeathSounds);
    }

    public static void PlayRandomBeginningSong() {
        PlayRandomSoundFromList(_beginningSounds);
    }
    public static void PlayRandomGameOverSong() {
        PlayRandomSoundFromList(_gameOverSounds);
    }
    public static void PlayRandomWinningSound() {
        PlayRandomSoundFromList(_winningSounds);
    }

    public static void StopPlayingList(List<Sound> sounds) {
        if (sounds == null || sounds.Count <= 0) {
            return;
        }

        foreach (Sound sound in sounds) {
            sound.source.Stop();
        }
    }
    
    public static void StopPlayingAll() {
        StopPlayingList(_soundtracks);
        StopPlayingList(_flySwatterSounds);
        StopPlayingList(_bugDeathSounds);
        StopPlayingList(_beginningSounds);
        StopPlayingList(_gameOverSounds);
        StopPlayingList(_winningSounds);
    }

    public static void PlayRandomSoundFromList(List<Sound> sounds) {
        if (sounds == null || sounds.Count <= 0) {
            return;
        }

        StopPlayingList(sounds);

        var sound = sounds[Random.Range(0, sounds.Count)];
        Play(sound);
    }
}