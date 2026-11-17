using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Paneles")]
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject scoresPanel;
    public GameObject creditsPanel;

    [Header("Botones del Menú Principal")]
    public Button[] menuButtons;

    [Header("Botones Volver")]
    public Button[] backButtons;

    [Header("Controles de Ajustes")]
    public TMP_InputField playerNameField;
    public TMP_Dropdown qualityDropdown;
    public Slider brightnessSlider;
    public Image brightnessOverlay;
    public Toggle fullscreenToggle;

    [Header("Configuración de Escena")]
    public string gameSceneName = "JuegoPrueba";
    public string menuSceneName = "SampleScene";

    void Start()
    {
        // Si estamos en la escena del menú, conectamos todo
        if (mainPanel != null)
        {
            ShowPanel(mainPanel);

            if (menuButtons.Length >= 5)
            {
                menuButtons[0].onClick.AddListener(PlayGame);
                menuButtons[1].onClick.AddListener(() => ShowPanel(settingsPanel));
                menuButtons[2].onClick.AddListener(() => ShowPanel(scoresPanel));
                menuButtons[3].onClick.AddListener(() => ShowPanel(creditsPanel));
                menuButtons[4].onClick.AddListener(QuitGame);
            }

            foreach (Button btn in backButtons)
            {
                if (btn != null)
                    btn.onClick.AddListener(() => ShowPanel(mainPanel));
            }

            if (qualityDropdown != null)
                qualityDropdown.onValueChanged.AddListener(SetQuality);

            if (fullscreenToggle != null)
                fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

            if (brightnessSlider != null)
            {
                brightnessSlider.onValueChanged.AddListener(SetBrightness);
                SetBrightness(brightnessSlider.value); // Aplicar brillo inicial
            }
        }
    }

    public void ShowPanel(GameObject panelToShow)
    {
        mainPanel.SetActive(panelToShow == mainPanel);
        settingsPanel.SetActive(panelToShow == settingsPanel);
        scoresPanel.SetActive(panelToShow == scoresPanel);
        creditsPanel.SetActive(panelToShow == creditsPanel);
    }

    public void SetBrightness(float value)
    {
        if (brightnessOverlay != null)
        {
            // Cuando el slider sube (1), la capa negra se vuelve transparente (0)
            // Cuando el slider baja (0), la pantalla se oscurece hasta un 85%
            Color c = brightnessOverlay.color;
            c.a = (1f - value) * 0.85f;
            brightnessOverlay.color = c;
        }
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(menuSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}