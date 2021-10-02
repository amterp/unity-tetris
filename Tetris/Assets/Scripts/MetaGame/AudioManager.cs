using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioMixerGroup _musicGroup;
    [SerializeField] private AudioMixerGroup _effectsGroup;
    [SerializeField] private Sound _music;
    [SerializeField] private Sound[] _sounds;

    private Dictionary<SoundEnum, Sound> _soundsByEnum;

    void Awake()
    {
        _soundsByEnum = new Dictionary<SoundEnum, Sound>();
        SetupSounds();
        SetupMusic();
    }

    public void Play(SoundEnum soundEnum)
    {
        Sound? sound = _soundsByEnum[soundEnum];
        sound?.Source.Play();
    }

    public void StopMusic()
    {
        _music.Source.Stop();
    }

    public void PlayMusic()
    {
        if (!_music.Source.isPlaying) _music.Source.Play();
    }

    public void RestartMusic()
    {
        StopMusic();
        PlayMusic();
    }

    private void SetupSounds()
    {
        foreach (Sound sound in _sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.outputAudioMixerGroup = ResolveMixerGroup(sound.Channel);
            sound.Source.playOnAwake = false;

            if (_soundsByEnum.ContainsKey(sound.SoundEnum))
            {
                Debug.LogWarning($"Multiple sounds of enum {sound.SoundEnum} - only one allowed! Using the first one.");
            }
            else
            {
                Debug.Log("Loaded sound: " + sound.SoundEnum);
                _soundsByEnum.Add(sound.SoundEnum, sound);
            }
        }
    }

    private void SetupMusic()
    {
        _music.Source = gameObject.AddComponent<AudioSource>();
        _music.Source.clip = _music.Clip;
        _music.Source.volume = _music.Volume;
        _music.Source.outputAudioMixerGroup = _musicGroup;
        _music.Source.playOnAwake = false;
        _music.Source.loop = true;

        RestartMusic();
    }

    private AudioMixerGroup ResolveMixerGroup(SoundChannel channel)
    {
        switch (channel)
        {
            case SoundChannel.Music: return _musicGroup;
            case SoundChannel.Effects: return _effectsGroup;
            default: throw new InvalidOperationException("Unknown sound channel: " + channel);
        }
    }
}
