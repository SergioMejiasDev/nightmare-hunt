using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Clase que contiene las variables y funciones relacionadas con la dificultad.
/// </summary>
public class DifficultyManager : MonoBehaviour
{
    /// <summary>
    /// El prefab del Zombunny.
    /// </summary>
    [SerializeField] GameObject zombunny = null;
    /// <summary>
    /// El prefab del Zombear.
    /// </summary>
    [SerializeField] GameObject zombear = null;
    /// <summary>
    /// El prefab del Hellephant.
    /// </summary>
    [SerializeField] GameObject hellephant = null;

    /// <summary>
    /// Clase encargada de generar a los enemigos.
    /// </summary>
    [SerializeField] EnemyManager enemyManager = null;

    /// <summary>
    /// La dificultad del juego.
    /// </summary>
    public int difficulty;

    /// <summary>
    /// Función que se activa cada vez que se cambia la dificultad.
    /// </summary>
    /// <param name="newDifficulty">Dificultad elegida. Del 1 al 3, de más fácil a más difícil.</param>
    public void ChangeDifficulty(int newDifficulty)
    {
        difficulty = newDifficulty;
        
        if (difficulty == 1)
        {
            enemyManager.spawnTime = 1;

            zombunny.GetComponent<EnemyHealth>().startingHealth = 20;
            zombear.GetComponent<EnemyHealth>().startingHealth = 35;
            hellephant.GetComponent<EnemyHealth>().startingHealth = 70;

            zombunny.GetComponent<NavMeshAgent>().speed = 5;
            zombear.GetComponent<NavMeshAgent>().speed = 3.5f;
            hellephant.GetComponent<NavMeshAgent>().speed = 2;
        }

        else if (difficulty == 2)
        {
            enemyManager.spawnTime = 1f;

            zombunny.GetComponent<EnemyHealth>().startingHealth = 40;
            zombear.GetComponent<EnemyHealth>().startingHealth = 55;
            hellephant.GetComponent<EnemyHealth>().startingHealth = 80;

            zombunny.GetComponent<NavMeshAgent>().speed = 7.5f;
            zombear.GetComponent<NavMeshAgent>().speed = 5;
            hellephant.GetComponent<NavMeshAgent>().speed = 4;
        }

        else if (difficulty == 3)
        {
            enemyManager.spawnTime = 1.5f;

            zombunny.GetComponent<EnemyHealth>().startingHealth = 50;
            zombear.GetComponent<EnemyHealth>().startingHealth = 65;
            hellephant.GetComponent<EnemyHealth>().startingHealth = 90;

            zombunny.GetComponent<NavMeshAgent>().speed = 10;
            zombear.GetComponent<NavMeshAgent>().speed = 7.5f;
            hellephant.GetComponent<NavMeshAgent>().speed = 6;
        }
    }
}