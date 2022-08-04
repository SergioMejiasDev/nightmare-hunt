using System.Collections;
using UnityEngine;

/// <summary>
/// Clase encargada de generar enemigos.
/// </summary>
public class EnemyManager : MonoBehaviour
{
    /// <summary>
    ///  Los posibles enemigos que pueden generarse.
    /// </summary>
    [SerializeField] GameObject[] enemies = null;
    /// <summary>
    /// El tiempo de espera entre enemigos.
    /// </summary>
    public float spawnTime = 2.0f;
    /// <summary>
    /// Las posiciones donde se generarán los enemigos.
    /// </summary>
    [SerializeField] Transform[] spawnPoints = null;
    /// <summary>
    /// La salud del jugador.
    /// </summary>
    [SerializeField] PlayerHealth playerHealth = null;

    private void OnEnable()
    {
        PlayerHealth.OnPlayerReset += StartSpawn;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerReset -= StartSpawn;
    }

    /// <summary>
    /// Función activada a través del delegado para comenzar a generar enemigos.
    /// </summary>
    void StartSpawn()
    {
        StartCoroutine(SpawnEnemies());
    }

    /// <summary>
    /// Función que se activa para generar a un enemigo.
    /// </summary>
    void Spawn()
    {
        float randomNumber = Random.value;

        GameObject enemy;

        if (randomNumber <= 0.45)
        {
            enemy = enemies[0];
        }

        else if (randomNumber <= 0.75)
        {
            enemy = enemies[1];
        }

        else
        {
            enemy = enemies[2];
        }

        Transform enemyPosition = spawnPoints[Random.Range(0, spawnPoints.Length)];

        Instantiate(enemy, enemyPosition.position, enemyPosition.rotation);
    }

    /// <summary>
    /// Corrutina que establece una espera antes de generar un nuevo enemigo.
    /// </summary>
    /// <returns></returns>
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            
            if (playerHealth.currentHealth <= 0)
            {
                yield break;
            }
            
            Spawn();
        }
    }
}