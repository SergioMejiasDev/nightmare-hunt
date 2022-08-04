using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Clase que controla la salud del jugador.
/// </summary>
public class PlayerHealth : MonoBehaviour
{
    /// <summary>
    /// La salud inicial del jugador.
    /// </summary>
    [SerializeField] int startingHealth = 100;
    /// <summary>
    /// La salud actual del jugador.
    /// </summary>
    public int currentHealth;

    /// <summary>
    /// El slider con la salud del jugador.
    /// </summary>
    [SerializeField] Slider sliderHealth = null;
    /// <summary>
    /// La imagen del corazón junto a la barra de salud.
    /// </summary>
    [SerializeField] Image heart = null;
    /// <summary>
    /// La imagen roja que aparece en la pantalla al ser el jugador herido.
    /// </summary>
    [SerializeField] Image imageDamage = null;
    /// <summary>
    /// El sonido que se reproduce al morir el jugador.
    /// </summary>
    [SerializeField] AudioClip deathClip = null;

    /// <summary>
    /// Componente Animator del jugador.
    /// </summary>
    [SerializeField] Animator anim = null;
    /// <summary>
    /// Componente AudioSource del jugador.
    /// </summary>
    [SerializeField] AudioSource audioSource = null;
    /// <summary>
    /// Sonido que se reproduce al ser herido el jugador.
    /// </summary>
    [SerializeField] AudioClip hurtClip = null;
    /// <summary>
    /// Componente PlayerMovement del jugador.
    /// </summary>
    [SerializeField] PlayerMovement playerMovement = null;
    /// <summary>
    /// Componente PlayerAttack del jugador.
    /// </summary>
    [SerializeField] PlayerAttack playerAttack = null;
    /// <summary>
    /// Verdadero si el jugador acaba de ser herido.
    /// </summary>
    bool isHurt;

    /// <summary>
    /// El panel de Game Over.
    /// </summary>
    [SerializeField] GameObject panelGameOver = null;
    /// <summary>
    /// Panel que muestra la puntuación del jugador.
    /// </summary>
    [SerializeField] Text scoreText = null;

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDeath;
    public static event PlayerDelegate OnPlayerReset;

    void Update()
    {
        if (isHurt)
        {
            imageDamage.color = new Color(1.0f, 0.0f, 0.0f, 0.2f);
        }

        else
        {
            imageDamage.color = Color.Lerp(imageDamage.color, Color.clear, 10.0f * Time.deltaTime);
        }
        
        isHurt = false;
    }

    /// <summary>
    /// Función que se activa cada vez que el jugador recibe daño.
    /// </summary>
    /// <param name="loseHealth">La salud que ha perdido.</param>
    public void TakeDamage(int loseHealth)
    {
        isHurt = true;
        currentHealth -= loseHealth;
        sliderHealth.value = currentHealth;

        audioSource.clip = hurtClip;
        audioSource.Play();

        if (currentHealth <= 20)
        {
            heart.sprite = Resources.Load<Sprite>("HeartBlack");

            if (currentHealth <= 0)
            {
                Death();

                if (OnPlayerDeath != null)
                {
                    OnPlayerDeath();
                }
            }
        }
    }

    /// <summary>
    /// Función que se activa cuando el jugador muere.
    /// </summary>
    public void Death()
    {
        playerMovement.enabled = false;
        playerAttack.enabled = false;
        anim.SetBool("IsMoving", false);
        anim.SetBool("IsDie", true);

        audioSource.clip = deathClip;
        audioSource.Play();

        StartCoroutine(GameOver());
    }

    /// <summary>
    /// Función que se activa para resetear la posición inicial del jugador y su salud.
    /// </summary>
    public void ResetPlayer()
    {
        panelGameOver.SetActive(false);
        transform.position = Vector3.zero;
        anim.SetBool("IsDie", false);
        playerMovement.enabled = true;
        playerAttack.enabled = true;
        currentHealth = startingHealth;
        sliderHealth.maxValue = startingHealth;
        sliderHealth.value = currentHealth;
        heart.sprite = Resources.Load<Sprite>("Heart");

        if (OnPlayerReset != null)
        {
            OnPlayerReset();
        }
    }

    /// <summary>
    /// Corrutina que se inicia cuando el jugador muere.
    /// </summary>
    /// <returns></returns>
    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
        panelGameOver.SetActive(true);
        GameManager.gameManager.SaveHighScore();
        scoreText.enabled = false;
    }
}