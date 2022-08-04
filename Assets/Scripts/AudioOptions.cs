using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

/// <summary>
/// Clase que controla los ajustes de audio.
/// </summary>
public class AudioOptions : MonoBehaviour
{
    /// <summary>
    /// Slider que controla el volumen de la música.
    /// </summary>
    [SerializeField] Slider musicSlider = null;
    /// <summary>
    /// Slider que controla el volumen de los SFX.
    /// </summary>
    [SerializeField] Slider sfxSlider = null;
    /// <summary>
    /// AudioMixer que controla el volumen de la música.
    /// </summary>
    [SerializeField] AudioMixerGroup musicMixer = null;
    /// <summary>
    /// AudioMixer que controla el volumen de los SFX.
    /// </summary>
    [SerializeField] AudioMixerGroup sfxMixer = null;
    /// <summary>
    /// El volumen de la música.
    /// </summary>
    float musicVolume;
    /// <summary>
    /// El volumen de los SFX.
    /// </summary>
    float sfxVolume;

    private void Start()
    {
        LoadOptions();
    }

    private void Update()
    {
        musicVolume = musicSlider.value;
        sfxVolume = sfxSlider.value;

        musicMixer.audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicVolume) * 20);
        sfxMixer.audioMixer.SetFloat("SFXVolume", Mathf.Log10(sfxVolume) * 20);
    }

    /// <summary>
    /// Función que guarda los ajustes de audio.
    /// </summary>
    public void SaveOptions()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Función que carga los ajustes de audio.
    /// </summary>
    void LoadOptions()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float musicVolumeLoaded = PlayerPrefs.GetFloat("MusicVolume");
            musicSlider.value = musicVolumeLoaded;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            float sfxVolumeLoaded = PlayerPrefs.GetFloat("SFXVolume");
            sfxSlider.value = sfxVolumeLoaded;
        }
    }
}