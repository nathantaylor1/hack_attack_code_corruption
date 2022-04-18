using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public enum OptionToEdit
{
    FXVolume,
    MusicVolume
}

public class OptionsSlider : MonoBehaviour
{
    public OptionToEdit option;
    public Slider slider;

    private void Start()
    {
        PauseMenu.PauseMenuOpened += UpdateSlider;
        UpdateSlider(null, null);
    }

    private void UpdateSlider(object sender, EventArgs e)
    {
        Debug.Log("Update Slider!");
        switch (option)
        {
            case OptionToEdit.MusicVolume:
                slider.value = AudioManager.instance.GetMusicVolume();
                break;
            
            case OptionToEdit.FXVolume:
                slider.value = AudioManager.instance.GetFXVolume();
                break;
        }
    }

    public void UpdateVal(float val)
    {
        switch (option)
        {
            case OptionToEdit.MusicVolume:
                AudioManager.instance.SetMusicVolume(val);
                break;
            
            case OptionToEdit.FXVolume:
                AudioManager.instance.SetFXVolume(val);
                break;
        }
    }
}
