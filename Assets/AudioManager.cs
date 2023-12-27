// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 25/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using Sirenix.OdinInspector;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;

    [Required, SerializeField, InlineEditor] private SOAudioClipList _audioClipList;

    public void Awake()
    {
        if (_musicSource == null)
            _musicSource = transform.Find("Music").GetComponent<AudioSource>();

        if (_sfxSource == null)
            _sfxSource = transform.Find("SFX").GetComponent<AudioSource>();
    }

    #region Music
    public void PlayMusic(string name)
    {
        AudioClip clip = _audioClipList.GetAudioClip(name);
        if (clip == null)
        {
            Debug.LogError("Audio clip not found: " + name);
            return;
        }

        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void SetMusicVolume(float volume)
    {
        _musicSource.volume = volume;
    }

    public void MuteMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }
    #endregion Music

    #region SFX
    public void PlaySFX(string name)
    {
        AudioClip clip = _audioClipList.GetAudioClip(name);
        if (clip == null)
        {
            Debug.LogError("Audio clip not found: " + name);
            return;
        }

        _sfxSource.PlayOneShot(clip);
    }

    public void StopSFX()
    {
        _sfxSource.Stop();
    }

    public void SetSFXVolume(float volume)
    {
        _sfxSource.volume = volume;
    }

    public void MuteSFX()
    {
        _sfxSource.mute = !_sfxSource.mute;
    }
    #endregion SFX
}