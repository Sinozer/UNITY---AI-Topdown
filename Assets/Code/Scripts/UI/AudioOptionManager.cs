// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 31/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioOptionManager : MonoBehaviour
{
    private Slider _slider;
    [SerializeField] private AudioMixer _audioMixer;

    public void OnMusicSliderChange(float value)
    {
        _audioMixer.SetFloat("Volume_Master", Mathf.Log10(value) * 20);
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _audioMixer.GetFloat("Volume_Master", out float volume);

        _slider?.SetValueWithoutNotify(Mathf.Pow(10, volume / 20));
    }
}