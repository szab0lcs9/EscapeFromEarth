using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    #region Sound Control

    [SerializeField] Slider musicVolumeSlider;
    [SerializeField] Slider soundsVolumeSlider;


    public void ToggleMusic() => AudioManager.Instance.ToggleMusic();

    public void ToggleSFX() => AudioManager.Instance.ToggleSFX();

    public void AdjustMusicVolume() => AudioManager.Instance.AdjustMusicVolume(musicVolumeSlider.value);

    public void AdjustSFXVolume() => AudioManager.Instance.AdjustSFXVolume(soundsVolumeSlider.value);

    public void PlaySFXSound(string name) => AudioManager.Instance.PlaySFX(name);

    #endregion
}
