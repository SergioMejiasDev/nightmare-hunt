using UnityEngine;

/// <summary>
/// Clase que permite al jugador disparar.
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    /// <summary>
    /// Daño de los disparos.
    /// </summary>
    [SerializeField] int shotDamage = 10;
    /// <summary>
    /// Tiempo que transcurre entre disparos.
    /// </summary>
    [SerializeField] float cadency = 0.1f;
    /// <summary>
    /// Distancia que alcanzarán los disparos.
    /// </summary>
    [SerializeField] float range = 25.0f;
    /// <summary>
    /// Duración de los disparos.
    /// </summary>
    [SerializeField] float shotDuration = 0.1f;
    /// <summary>
    /// Contador de tiempo.
    /// </summary>
    float timer;

    /// <summary>
    /// El rayo que se generará al disparar.
    /// </summary>
    Ray shotRay = new Ray();
    /// <summary>
    /// El Raycast del disparo.
    /// </summary>
    RaycastHit shotHit;
    /// <summary>
    /// La cápa asignada a los enemigos a los que se les puede disparar.
    /// </summary>
    int shotMask;
    /// <summary>
    /// Las partículas que se emiten al disparar.
    /// </summary>
    [SerializeField] ParticleSystem gunParticles = null;
    /// <summary>
    /// Componente AudioSource del jugador.
    /// </summary>
    [SerializeField] AudioSource audioSource = null;
    /// <summary>
    /// Sonido del disparo.
    /// </summary>
    [SerializeField] AudioClip shootClip = null;
    /// <summary>
    /// Posición hacia la que se va a disparar.
    /// </summary>
    [SerializeField] Transform shootPoint = null;
    /// <summary>
    /// Línea que se dibuja al disparar el jugador.
    /// </summary>
    [SerializeField] LineRenderer shotLine = null;

    void Start()
    {
        shotMask = LayerMask.GetMask("Shootable");

        timer = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > cadency)
        {
            if ((Input.GetButton("Fire1")) && (Time.timeScale == 1))
            {
                Shoot();
            }
        }

        if (timer > cadency * shotDuration)
        {
            shotLine.enabled = false;
        }
    }

    /// <summary>
    /// Función que se activa cada vez que el jugador dispara.
    /// </summary>
    void Shoot()
    {
        timer = 0.0f;

        audioSource.clip = shootClip;
        audioSource.Play();

        gunParticles.Stop();
        gunParticles.Play();

        shotLine.enabled = true;
        shotLine.SetPosition(0, shotLine.transform.position);

        shotRay.origin = shootPoint.position;
        shotRay.direction = shootPoint.forward;

        if (Physics.Raycast(shotRay, out shotHit, range, shotMask))
        {
            if (shotHit.collider.GetComponent<EnemyHealth>())
            {
                shotHit.collider.GetComponent<EnemyHealth>().TakeDamage(shotDamage);
            }

            shotLine.SetPosition(1, shotHit.point);
        }
        
        else
        {
            shotLine.SetPosition(1, shotRay.origin + shotRay.direction * range);
        }
    }
}