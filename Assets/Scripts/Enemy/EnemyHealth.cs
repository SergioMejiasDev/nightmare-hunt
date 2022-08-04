using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Clase que controla la salud y la puntuación de los enemigos.
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    /// <summary>
    /// La salud inicial de los enemigos.
    /// </summary>
    public int startingHealth = 100;
    /// <summary>
    /// La salud actual de los enemigos.
    /// </summary>
    public int currentHealth;
    /// <summary>
    /// Verdadero si el enemigo está muerto. Falso si no lo está.
    /// </summary>
    bool isDead;
    /// <summary>
    /// Verdadero si el collider es una cápsula. Falso si es una esfera.
    /// </summary>
    public bool capsuleCollider;
    /// <summary>
    /// La puntuación del enemigo.
    /// </summary>
    [SerializeField] int score = 10;
    /// <summary>
    /// Sonido que se reproduce al morir el enemigo.
    /// </summary>
    [SerializeField] AudioClip deathClip = null;
    /// <summary>
    /// Componente AudioSource del enemigo.
    /// </summary>
    [SerializeField] AudioSource audioSource = null;
    /// <summary>
    /// Componente Animator del enemigo.
    /// </summary>
    [SerializeField] Animator anim = null;

    void Start()
    {
        isDead = false;
        currentHealth = startingHealth;
    }

    /// <summary>
    /// Función que se activa cada vez que el enemigo recibe daño.
    /// </summary>
    /// <param name="damage">La cantidad de daño que ha recibido.</param>
    public void TakeDamage (int damage)
    {
        if (!isDead)
        {
            audioSource.Play();
            currentHealth = currentHealth - damage;

            if (currentHealth <= 0)
            {
                audioSource.clip = deathClip;
                audioSource.Play();

                Death();
            }
        }
    }

    /// <summary>
    /// Función que se activa cuando el enemigo muere.
    /// </summary>
    void Death()
    {
        isDead = true;
        anim.SetTrigger("Die");

        if (capsuleCollider)
        {
            GetComponent<CapsuleCollider>().enabled = false;
        }

        else
        {
            GetComponent<SphereCollider>().enabled = false;
        }

        GetComponent<EnemyController>().enabled = false;
        GetComponent<EnemyAttack>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GameManager.gameManager.UpdateScore(score);
        StartCoroutine(DestroyEnemy());
    }

    /// <summary>
    /// Corrutina que elimina al enemigo de la escena tras morir.
    /// </summary>
    /// <returns></returns>
    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
    }
}