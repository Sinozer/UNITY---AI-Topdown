// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 31/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioOptionManager : MonoBehaviour
{
    //private TextMeshProUGUI _musicText;
    [SerializeField] private AudioMixer _audioMixer;

    public void OnMusicSliderChange(float value)
    {
        _audioMixer.SetFloat("Volume_Master", Mathf.Log10(value) * 20);
    }
}