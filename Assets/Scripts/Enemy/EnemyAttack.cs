using UnityEngine;

/// <summary>
/// Clase que permite a los enemigos atacar.
/// </summary>
public class EnemyAttack : MonoBehaviour
{
    /// <summary>
    /// El daño que hace el ataque del enemigo.
    /// </summary>
    [SerializeField] int attackDamage = 5;
    /// <summary>
    /// El tiempo que transcurre entra ataques.
    /// </summary>
    [SerializeField] float cadency = 1.0f;
    /// <summary>
    /// Verdadero si el jugador acaba de ser herido.
    /// </summary>
    bool playerHurt;
    /// <summary>
    /// Verdadero si el jugador está muerto. Falso si no lo está.
    /// </summary>
    bool playerDeath;

    /// <summary>
    /// El jugador.
    /// </summary>
    GameObject player;
    /// <summary>
    /// Componente Animator del enemigo.
    /// </summary>
    [SerializeField] Animator anim = null;

    float timer;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += PlayerDeath;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= PlayerDeath;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerDeath = false;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if (playerHurt && (timer >= cadency))
        {
            timer = 0;

            if (!playerDeath)
            {
                player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerHurt = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerHurt = false;
        }
    }

    /// <summary>
    /// Función que se activa a través del delegado cuando el jugador muere.
    /// </summary>
    void PlayerDeath()
    {
        playerDeath = true;
        anim.SetBool("PlayerDead", true);
    }
}