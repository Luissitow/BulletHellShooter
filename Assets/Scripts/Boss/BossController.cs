using System.Collections;
using TMPro; // Importar TextMeshPro
using UnityEngine;
using UnityEngine.UI; // Para el contador de balas en la UI

public class BossController : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform[] firePoints; // Puntos desde donde se disparan las balas
    public TextMeshProUGUI bulletCounterText; // Texto de la UI para el contador de balas

    private int activeBullets = 0; // Contador de balas activas
    private int totalBulletsCreated = 0; // Total de balas creadas
    private int totalBulletsDestroyed = 0; // Total de balas destruidas

    void Start()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("⚠️ CRÍTICO: Debes arrastrar el prefab Bullet al campo 'Bullet Prefab' en el Inspector del objeto Boss.");
            return;
        }
        
        if (firePoints == null || firePoints.Length == 0)
        {
            Debug.LogError("⚠️ CRÍTICO: Debes arrastrar el objeto Firepoint al array 'Fire Points' en el Inspector del objeto Boss.");
            return;
        }
        
        StartCoroutine(ShootPatterns());
    }

    void Update()
    {
        // Actualizar el contador de balas en la UI
        if (bulletCounterText != null)
        {
            bulletCounterText.text = "Balas: " + activeBullets.ToString() + 
                                   "\nBalas creadas: " + totalBulletsCreated.ToString() +
                                   "\nBalas destruidas: " + totalBulletsDestroyed.ToString();
            bulletCounterText.ForceMeshUpdate();
        }
    }

    IEnumerator ShootPatterns()
    {
        while (true)
        {
            // Patrón 1: Disparo circular
            yield return StartCoroutine(ShootCircularPattern());

            // Patrón 2: Disparo lineal
            yield return StartCoroutine(ShootLinearPattern());

            // Patrón 3: Disparo en espiral
            yield return StartCoroutine(ShootSpiralPattern());
        }
    }

    IEnumerator ShootCircularPattern()
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("BulletPrefab no está asignado en el BossController.");
            yield break;
        }
        
        if (firePoints == null || firePoints.Length == 0)
        {
            Debug.LogError("FirePoints no está asignado o está vacío en el BossController.");
            yield break;
        }
        
        if (firePoints[0] == null)
        {
            Debug.LogError("El primer elemento de FirePoints es nulo. Asegúrate de arrastrar el Firepoint al array.");
            yield break;
        }

        float angleStep = 360f / 20; // Dividir el círculo en 20 balas
        float angle = 0f;

        for (int i = 0; i < 20; i++) // Disparar durante 20 iteraciones = 10 segundos (20 x 0.5s)
        {
            for (int j = 0; j < 20; j++)
            {
                float bulletDirX = Mathf.Cos((angle * Mathf.PI) / 180f);
                float bulletDirY = Mathf.Sin((angle * Mathf.PI) / 180f);

                Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
                Vector2 bulletDirection = bulletMoveVector.normalized;

                GameObject bullet = Instantiate(bulletPrefab, firePoints[0].position, Quaternion.identity);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                
                if (bulletScript != null)
                {
                    bulletScript.SetDirection(bulletDirection);
                    bulletScript.SetMovementType(BulletMovementType.Straight); // Patrón circular: movimiento recto
                    activeBullets++;
                    totalBulletsCreated++;
                }
                else
                {
                    Debug.LogError("El prefab Bullet no tiene el script Bullet asignado.");
                    Destroy(bullet);
                }

                angle += angleStep;
            }

            angle = 0f;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator ShootLinearPattern()
    {
        if (firePoints == null || firePoints.Length == 0 || bulletPrefab == null)
        {
            Debug.LogError("FirePoints o BulletPrefab no están asignados en el BossController.");
            yield break;
        }

        for (int i = 0; i < 20; i++) // Disparar durante 20 iteraciones = 10 segundos (20 x 0.5s)
        {
            foreach (Transform firePoint in firePoints)
            {
                // Generar dirección aleatoria en 360 grados
                float randomAngle = Random.Range(0f, 360f);
                float bulletDirX = Mathf.Cos((randomAngle * Mathf.PI) / 180f);
                float bulletDirY = Mathf.Sin((randomAngle * Mathf.PI) / 180f);
                Vector2 randomDirection = new Vector2(bulletDirX, bulletDirY).normalized;

                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                
                if (bulletScript != null)
                {
                    bulletScript.SetDirection(randomDirection);
                    bulletScript.SetMovementType(BulletMovementType.Straight); // Movimiento recto en dirección aleatoria
                    activeBullets++;
                    totalBulletsCreated++;
                }
                else
                {
                    Debug.LogError("El prefab Bullet no tiene el script Bullet asignado.");
                    Destroy(bullet);
                }
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator ShootSpiralPattern()
    {
        if (firePoints == null || firePoints.Length == 0 || bulletPrefab == null)
        {
            Debug.LogError("FirePoints o BulletPrefab no están asignados en el BossController.");
            yield break;
        }

        float angle = 0f;

        for (int i = 0; i < 100; i++) // Disparar durante 100 iteraciones = 10 segundos (100 x 0.1s)
        {
            float bulletDirX = Mathf.Cos((angle * Mathf.PI) / 180f);
            float bulletDirY = Mathf.Sin((angle * Mathf.PI) / 180f);

            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
            Vector2 bulletDirection = bulletMoveVector.normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoints[0].position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            
            if (bulletScript != null)
            {
                bulletScript.SetDirection(bulletDirection);
                bulletScript.SetMovementType(BulletMovementType.Wave); // Patrón espiral: movimiento en onda
                activeBullets++;
                totalBulletsCreated++;
            }
            else
            {
                Debug.LogError("El prefab Bullet no tiene el script Bullet asignado.");
                Destroy(bullet);
            }

            angle += 10f; // Incrementar el ángulo para crear el efecto de espiral
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void DecreaseBulletCount()
    {
        activeBullets--;
        totalBulletsDestroyed++;
    }
}
