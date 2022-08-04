using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Clase que controla las funciones principales del juego.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;

    /// <summary>
    /// Clase que controla la dificultad del juego.
    /// </summary>
    [SerializeField] DifficultyManager difficultyManager = null;

    /// <summary>
    /// Puntuación del jugador.
    /// </summary>
    int score;
    /// <summary>
    /// Panel con la puntuación del jugador.
    /// </summary>
    [SerializeField] Text scoreText = null;
    /// <summary>
    /// Panel con la puntuación que aparece durante la pantalla de Game Over.
    /// </summary>
    [SerializeField] Text scoreMenuText = null;
    /// <summary>
    /// Panel con la máxima puntuación que aparece durante la pantalla de Game Over.
    /// </summary>
    [SerializeField] Text highScoreText = null;

    /// <summary>
    /// Panel que muestra la salud del jugador.
    /// </summary>
    [SerializeField] GameObject panelHealth = null;
    /// <summary>
    /// Panel donde se puede elegir la dificultad del juego.
    /// </summary>
    [SerializeField] GameObject panelDifficulty = null;
    /// <summary>
    /// Panel con el menú de Game Over.
    /// </summary>
    [SerializeField] GameObject panelGameOver = null;
    /// <summary>
    /// Panel con el menú de pausa.
    /// </summary>
    [SerializeField] GameObject panelPause = null;
    /// <summary>
    /// Panel con el menú principal del juego.
    /// </summary>
    [SerializeField] GameObject panelMenu = null;
    /// <summary>
    /// Panel con el menú de opciones.
    /// </summary>
    [SerializeField] GameObject panelOptions = null;
    /// <summary>
    /// Panel con el mensaje de "Loading".
    /// </summary>
    [SerializeField] GameObject panelLoading = null;
    /// <summary>
    /// Panel con el menú de ayuda.
    /// </summary>
    [SerializeField] GameObject panelHelp = null;

    /// <summary>
    /// Panel negro de transición.
    /// </summary>
    [SerializeField] GameObject panelFade = null;
    /// <summary>
    /// Imagen negra de transiciçon.
    /// </summary>
    Image fadeImage;

    private void Awake()
    {
        gameManager = this;
        fadeImage = panelFade.GetComponent<Image>();
        Time.timeScale = 1;
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += DeathPlayer;
        PlayerHealth.OnPlayerReset += ResetPlayer;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= DeathPlayer;
        PlayerHealth.OnPlayerReset -= ResetPlayer;
    }

    void Start()
    {
        StartCoroutine(InitialFade());
    }

    /// <summary>
    /// Función que se activa cada vez que aumenta la puntuación.
    /// </summary>
    /// <param name="scoreIncrease">La cantidad en que aumenta la puntuación.</param>
    public void UpdateScore(int scoreIncrease)
    {
        score += scoreIncrease;
        scoreText.text = "SCORE: " + score.ToString();
    }

    /// <summary>
    /// Función que cierra la aplicación.
    /// </summary>
    public void CloseGame()
    {
        StartCoroutine(ExitGame());
    }

    /// <summary>
    /// Función que inicia la partida desde el menú principal.
    /// </summary>
    public void LoadGame()
    {
        StartCoroutine(LoadScene(1));
    }

    /// <summary>
    /// Función que finaliza la partida y vuelve al menú principal.
    /// </summary>
    public void BackToMenu()
    {
        StartCoroutine(LoadScene(0));
    }

    /// <summary>
    /// Función a la que se llama durante el Game Over para guardar la máxima puntuación.
    /// </summary>
    public void SaveHighScore()
    {
        scoreMenuText.text = "SCORE: " + score.ToString();

        int gameType = difficultyManager.difficulty;

        int highScoreLoaded;

        if (gameType == 1)
        {
            highScoreLoaded = PlayerPrefs.GetInt("HighScore1", 0);

            if (score > highScoreLoaded)
            {
                highScoreText.text = "HIGH SCORE: " + score.ToString();
                PlayerPrefs.SetInt("HighScore1", score);
                PlayerPrefs.Save();
            }

            else
            {
                highScoreText.text = "HIGH SCORE: " + highScoreLoaded.ToString();
            }
        }

        else if (gameType == 2)
        {
            highScoreLoaded = PlayerPrefs.GetInt("HighScore2", 0);

            if (score > highScoreLoaded)
            {
                highScoreText.text = "HIGH SCORE: " + score.ToString();
                PlayerPrefs.SetInt("HighScore2", score);
                PlayerPrefs.Save();
            }

            else
            {
                highScoreText.text = "HIGH SCORE: " + highScoreLoaded.ToString();
            }
        }

        else if (gameType == 3)
        {
            highScoreLoaded = PlayerPrefs.GetInt("HighScore3", 0);

            if (score > highScoreLoaded)
            {
                highScoreText.text = "HIGH SCORE: " + score.ToString();
                PlayerPrefs.SetInt("HighScore3", score);
                PlayerPrefs.Save();
            }

            else
            {
                highScoreText.text = "HIGH SCORE: " + highScoreLoaded.ToString();
            }
        }
    }

    /// <summary>
    /// Función que se activa a través del delegado y resetea los parámetros necesarios para continuar jugando tras un Game Over.
    /// </summary>
    void ResetPlayer()
    {
        panelDifficulty.SetActive(false);
        panelHealth.SetActive(true);
        scoreText.enabled = true;
        score = 0;
        scoreText.text = "SCORE: 0";
    }

    /// <summary>
    /// Función que se activa a través del delegado cuando el jugador muere.
    /// </summary>
    void DeathPlayer()
    {
        panelHealth.SetActive(false);
        scoreText.enabled = false;
    }

    /// <summary>
    /// Función que abre el panel para elegir la dificultad.
    /// </summary>
    public void OpenPanelDifficulty()
    {
        panelGameOver.SetActive(false);
        panelHelp.SetActive(false);
        panelDifficulty.SetActive(true);
    }

    /// <summary>
    /// Función que abre y cierra el panel de pausa.
    /// </summary>
    public void PauseGame()
    {
        if (!panelPause.activeSelf)
        {
            panelPause.SetActive(true);
            Time.timeScale = 0;
        }

        else
        {
            panelPause.SetActive(false);
            Time.timeScale = 1;
        }
    }

    /// <summary>
    /// Función que abre y cierra el panel de opciones.
    /// </summary>
    public void MenuOptions()
    {
        if (!panelOptions.activeSelf)
        {
            panelOptions.SetActive(true);
            panelMenu.SetActive(false);
        }

        else
        {
            panelOptions.SetActive(false);
            panelMenu.SetActive(true);
        }
    }

    /// <summary>
    /// Corrutina que inicia la transición de cambio de escena.
    /// </summary>
    /// <param name="sceneNumber">La escena que queremos cargar.</param>
    /// <returns></returns>
    IEnumerator LoadScene(int sceneNumber)
    {
        Time.timeScale = 1;
        panelFade.SetActive(true);
        
        Color imageColor = fadeImage.color;
        float alphaValue;

        while (fadeImage.color.a < 1)
        {
            alphaValue = imageColor.a + (2 * Time.deltaTime);
            imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            fadeImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            yield return null;
        }

        panelLoading.SetActive(true);
        SceneManager.LoadScene(sceneNumber);
    }

    /// <summary>
    /// Corrutina que activa la transición inicial.
    /// </summary>
    /// <returns></returns>
    public IEnumerator InitialFade()
    {
        Color imageColor = fadeImage.color;
        float alphaValue;

        while (fadeImage.color.a > 0)
        {
            alphaValue = imageColor.a - (1 * Time.deltaTime);
            imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            fadeImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            yield return null;
        }

        panelFade.SetActive(false);
    }

    /// <summary>
    /// Corrutina que inicia la transición que cierra el juego.
    /// </summary>
    /// <returns></returns>
    IEnumerator ExitGame()
    {
        panelFade.SetActive(true);

        Color imageColor = fadeImage.color;
        float alphaValue;

        while (fadeImage.color.a < 1)
        {
            alphaValue = imageColor.a + (2 * Time.deltaTime);
            imageColor = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            fadeImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alphaValue);
            yield return null;
        }

        yield return new WaitForSeconds(1);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}