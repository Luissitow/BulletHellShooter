using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 

public class BossController : MonoBehaviour
{

    public GameObject bulletPrefab; 
    public Transform[] firePoints; 
    public TextMeshProUGUI bulletCounterText; // texto de la UI para el contador de balas

    private int totalBulletsCreated = 0; // total de balas creadas
    private int totalBulletsDestroyed = 0; // total de balas destruidas
    private int activeBullets = 0; // contador de balas activas


    void Start()
    {
        if (bulletPrefab == null)
        { 
            Debug.LogError("prefab bullet no esta en el campo bullet prefab en el inspector del objeto boss");
            return;
        }
        
        if (firePoints == null || firePoints.Length == 0)
        {
            Debug.LogError(" objeto firepoint para sacar balas no esta en el objeto boss");
            return;
        }
        
        StartCoroutine(ShootPatterns());
    }

    void Update()
    {
        //contador de balas
        if (bulletCounterText != null)
        {
            bulletCounterText.text = "Balas: " + activeBullets.ToString() + 
                                   //"\nBalas creadas: " + totalBulletsCreated.ToString() +
                                   "\nBalas destruidas: " + totalBulletsDestroyed.ToString();
            bulletCounterText.ForceMeshUpdate();
        }
    }

    IEnumerator ShootPatterns()
    {
        while (true)
        {
                        
            // Patrón 1: Espiral doble con movimiento ondulatorio
            yield return StartCoroutine(PatronEspiralDobleHelix());

            //Patrón 3: Explosión en estrella de mar
            yield return StartCoroutine(PatronEstrellaDeMar());
            
            // Patrón 2: Barrido láser rotatorio
            yield return StartCoroutine(PatronBarridoLaser());



            // Patrón 4: Explosión circular (opcional)
            // yield return StartCoroutine(PatronExplosivoCircular());

            // Patrón 5: Líneas curvas rotatorias (opcional)
            // yield return StartCoroutine(PatronLineasCurvasRotatorias());
        }
    }




    // Patrón 1: Espiral doble con movimiento 
    IEnumerator PatronEspiralDobleHelix()
    {
        if (firePoints == null || firePoints.Length == 0 || bulletPrefab == null)
        {
            Debug.LogError("FirePoints o BulletPrefab no están asignados en el BossController.");
            yield break;
        }

        int numHelices = 6;
        float baseAngle = 0f;

        for (int i = 0; i < 100; i++) // 100 iteraciones × 0.1s = 10 segundos
        {
            for (int h = 0; h < numHelices; h++)
            {
                float angle = baseAngle + (360f / numHelices) * h;
                float bulletDirX = Mathf.Cos((angle * Mathf.PI) / 180f);
                float bulletDirY = Mathf.Sin((angle * Mathf.PI) / 180f);
                Vector2 direction = new Vector2(bulletDirX, bulletDirY).normalized;

                GameObject bullet = Instantiate(bulletPrefab, firePoints[0].position, Quaternion.identity);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                
                if (bulletScript != null)
                {
                    bulletScript.SetDirection(direction);
                    bulletScript.SetMovementType(BulletMovementType.Wave);
                    activeBullets++;
                    totalBulletsCreated++;
                }
                else
                {
                    Debug.LogError("El prefab Bullet no tiene el script Bullet asignado.");
                    Destroy(bullet);
                }
            }
            baseAngle += 10f;
            yield return new WaitForSeconds(0.1f);
        }
    }


    // Patrón 2: Barrido láser rotatorio
    IEnumerator PatronBarridoLaser()
    {
        int repeticiones = 3;
        float duracionTotal = 10f;
        float duracionPorRepeticion = duracionTotal / repeticiones; // ~3.33s por repetición
        int barridos = 20; // 42 x 0.08s ≈ 3.36s por repetición
        float tiempoEntreBarridos = duracionPorRepeticion / barridos; // ≈ 0.08s
        int balasPorLinea = 12;
        float radio = 6f;

        for (int repeat = 0; repeat < repeticiones; repeat++)
        {
            float angle = 0f;
            for (int i = 0; i < barridos; i++)
            {
                for (int j = 0; j < balasPorLinea; j++)
                {
                    float offset = (360f / balasPorLinea) * j;
                    float currentAngle = angle + offset;
                    float rad = currentAngle * Mathf.Deg2Rad;
                    Vector2 spawnPos = (Vector2)firePoints[0].position + new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radio;

                    GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
                    Bullet bulletScript = bullet.GetComponent<Bullet>();

                    if (bulletScript != null)
                    {
                        bulletScript.SetDirection((spawnPos - (Vector2)firePoints[0].position).normalized);
                        bulletScript.SetMovementType(BulletMovementType.Straight);
                        activeBullets++;
                        totalBulletsCreated++;
                    }
                    else
                    {
                        Debug.LogError("El prefab Bullet no tiene el script Bullet asignado.");
                        Destroy(bullet);
                    }
                }
                angle += 6f;
                yield return new WaitForSeconds(tiempoEntreBarridos);
            }
        }
    }


    // Patrón 3: Explosión en estrella de mar usando función seno
    IEnumerator PatronEstrellaDeMar()
    {
        int puntas = 6; // Número de brazos de la estrella
        float baseRadius = 5f;
        float amplitude = 0.5f;

        for (int wave = 0; wave < 4; wave++) // oleadas
        {
            for (float angle = 0; angle < 360; angle += 8f)
            {
                float rad = angle * Mathf.Deg2Rad;
                float radius = baseRadius * (1 + amplitude * Mathf.Sin(puntas * rad));
                Vector2 spawnPos = (Vector2)firePoints[0].position + new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;

                GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.identity);
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                if (bulletScript != null)
                {
                    bulletScript.SetDirection((spawnPos - (Vector2)firePoints[0].position).normalized);
                    bulletScript.SetMovementType(BulletMovementType.Straight);
                    activeBullets++;
                    totalBulletsCreated++;
                }
                else
                {
                    Debug.LogError("El prefab Bullet no tiene el script Bullet asignado.");
                    Destroy(bullet);
                }
            }
            yield return new WaitForSeconds(2.5f); // Espera 2 segundos entre oleadas
        }
    }

    //IDEAS QUE YA NO SE USAN
    // // Patrón 4: Explosión circular de balas en 360°
    // IEnumerator PatronExplosivoCircular()
    // {
    //     if (bulletPrefab == null)
    //     {
    //         Debug.LogError("BulletPrefab no está asignado en el BossController.");
    //         yield break;
    //     }
        
    //     if (firePoints == null || firePoints.Length == 0)
    //     {
    //         Debug.LogError("FirePoints no está asignado o está vacío en el BossController.");
    //         yield break;
    //     }
        
    //     if (firePoints[0] == null)
    //     {
    //         Debug.LogError("El primer elemento de FirePoints es nulo. Asegúrate de arrastrar el Firepoint al array.");
    //         yield break;
    //     }

    //     float angleStep = 360f / 36; // Dividir el círculo en 36 balas
    //     float angle = 0f;

    //     for (int i = 0; i < 10; i++) // 10 círculos × 1.5s = 15 segundos
    //     {
    //         // Disparar el círculo completo de 36 balas
    //         for (int j = 0; j < 36; j++)
    //         {
    //             float bulletDirX = Mathf.Cos((angle * Mathf.PI) / 180f);
    //             float bulletDirY = Mathf.Sin((angle * Mathf.PI) / 180f);

    //             Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
    //             Vector2 bulletDirection = bulletMoveVector.normalized;

    //             GameObject bullet = Instantiate(bulletPrefab, firePoints[0].position, Quaternion.identity);
    //             Bullet bulletScript = bullet.GetComponent<Bullet>();
                
    //             if (bulletScript != null)
    //             {
    //                 bulletScript.SetDirection(bulletDirection);
    //                 bulletScript.SetMovementType(BulletMovementType.Straight); // Patrón circular: movimiento recto
    //                 activeBullets++;
    //                 totalBulletsCreated++;
    //             }
    //             else
    //             {
    //                 Debug.LogError("El prefab Bullet no tiene el script Bullet asignado.");
    //                 Destroy(bullet);
    //             }

    //             angle += angleStep;
    //         }

    //         angle = 0f;
    //         yield return new WaitForSeconds(1.5f); // 1.5 segundos entre cada círculo
    //     }
    // }


    // // Patrón 5: Disparo de 4 líneas curvas que rotan (opcional)
    // IEnumerator PatronLineasCurvasRotatorias()
    // {
    //     if (firePoints == null || firePoints.Length == 0 || bulletPrefab == null)
    //     {
    //         Debug.LogError("FirePoints o BulletPrefab no están asignados en el BossController.");
    //         yield break;
    //     }

    //     float baseAngle = 0f;

    //     for (int i = 0; i < 100; i++) // 100 iteraciones × 0.1s = 10 segundos
    //     {
    //         // Disparar 4 líneas curvas (como el video a los 4:57)
    //         for (int line = 0; line < 4; line++)
    //         {
    //             float lineAngle = baseAngle + (90f * line); // 0°, 90°, 180°, 270°
                
    //             float bulletDirX = Mathf.Cos((lineAngle * Mathf.PI) / 180f);
    //             float bulletDirY = Mathf.Sin((lineAngle * Mathf.PI) / -180f);
    //             Vector2 direction = new Vector2(bulletDirX, bulletDirY).normalized;

    //             GameObject bullet = Instantiate(bulletPrefab, firePoints[0].position, Quaternion.identity);
    //             Bullet bulletScript = bullet.GetComponent<Bullet>();
                
    //             if (bulletScript != null)
    //             {
    //                 bulletScript.SetDirection(direction);
    //                 bulletScript.SetMovementType(BulletMovementType.Wave); // Movimiento en curva
    //                 activeBullets++;
    //                 totalBulletsCreated++;
    //             }
    //             else
    //             {
    //                 Debug.LogError("El prefab Bullet no tiene el script Bullet asignado.");
    //                 Destroy(bullet);
    //             }
    //         }

    //         baseAngle += 3f; // Velocidad de rotación de las 4 líneas
    //         yield return new WaitForSeconds(0.1f);
    //     }
    // }

    public void DecreaseBulletCount()
    {
        activeBullets--;
        totalBulletsDestroyed++;
    }
}
