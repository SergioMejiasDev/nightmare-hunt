using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Clase que permite el movimiento de los enemigos.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    /// <summary>
    /// Distancia a la que empezarán a atacar los enemigos.
    /// </summary>
    public float attackRange = 1000.0f;

    /// <summary>
    /// La posición del jugador.
    /// </summary>
    Transform player;
    /// <summary>
    /// Componente NavMeshAgent del enemigo.
    /// </summary>
    [SerializeField] NavMeshAgent nav = null;
    /// <summary>
    /// Componente Animator del enemigo.
    /// </summary>
    [SerializeField] Animator anim = null;
    /// <summary>
    /// Componente EnemyHealth del enemigo.
    /// </summary>
    [SerializeField] EnemyHealth enemyHealth = null;

    /// <summary>
    /// Verdadero si el enemigo está siguiendo al jugador. Falso si no lo está haciendo.
    /// </summary>
    bool followPlayer;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDeath += PlayerDeath;
        PlayerHealth.OnPlayerReset += PlayerReset;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDeath -= PlayerDeath;
        PlayerHealth.OnPlayerReset -= PlayerReset;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        followPlayer = true;
    }

    void Update()
    {
        if ((Vector3.Distance(transform.position, player.position) < attackRange) && (enemyHealth.currentHealth > 0))
        {
            if (followPlayer)
            {
                nav.SetDestination(player.position);
            }
        }

        anim.SetBool("PlayerDead", !followPlayer);
    }

    /// <summary>
    /// Función que se activa a través del delegado cuando el jugador muere.
    /// </summary>
    void PlayerDeath()
    {
        followPlayer = false;
        nav.enabled = false;
    }

    /// <summary>
    /// Función que se activa a través del delegado cuando el jugador reinicia la partida.
    /// </summary>
    void PlayerReset()
    {
        Destroy(gameObject);
    }
}