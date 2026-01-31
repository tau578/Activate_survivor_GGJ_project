using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Main Menu Elements")]
    public GameObject titleText;
    public GameObject playButton;
    public GameObject optionsButton;
    public GameObject creditsButton;
    public GameObject quitButton;

    [Header("Options Menu Elements")]
    public GameObject optionsGroup; // Родитель Options_submenu
    public Slider volumeSlider;
    public GameObject volumeLabel;
    public GameObject backFromOptionsButton;
    public GameObject optionsTitleText;

    [Header("Credits Menu Elements")]
    public GameObject creditsGroup; // Родитель Credits_submenu
    public GameObject creditsText;
    public GameObject backFromCreditsButton;
    public GameObject creditsTitleText;

    private const string VolumeKey = "MasterVolume";

    void Start()
    {
        ShowMainMenu();

        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        AudioListener.volume = savedVolume;

        if (volumeSlider != null)
            volumeSlider.value = savedVolume;
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Level");
    }

    public void OnOptionsButtonClick()
    {
        HideMainMenu();
        if (optionsGroup != null) optionsGroup.SetActive(true); // Включаем родителя
    }

    public void OnCreditsButtonClick()
    {
        HideMainMenu();
        if (creditsGroup != null) creditsGroup.SetActive(true); // Включаем родителя
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void OnBackFromOptionsClick()
    {
        if (optionsGroup != null) optionsGroup.SetActive(false); // Выключаем родителя
        ShowMainMenu();
    }

    public void OnBackFromCreditsClick()
    {
        if (creditsGroup != null) creditsGroup.SetActive(false); // Выключаем родителя
        ShowMainMenu();
    }

    public void OnVolumeSliderChanged(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }

    private void ShowMainMenu()
    {
        if (titleText != null) titleText.SetActive(true);
        if (playButton != null) playButton.SetActive(true);
        if (optionsButton != null) optionsButton.SetActive(true);
        if (creditsButton != null) creditsButton.SetActive(true);
        if (quitButton != null) quitButton.SetActive(true);
    }

    private void HideMainMenu()
    {
        if (titleText != null) titleText.SetActive(false);
        if (playButton != null) playButton.SetActive(false);
        if (optionsButton != null) optionsButton.SetActive(false);
        if (creditsButton != null) creditsButton.SetActive(false);
        if (quitButton != null) quitButton.SetActive(false);
    }
}