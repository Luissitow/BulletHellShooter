// using UnityEngine;
// using System.Collections;

// public class BossPattern : MonoBehaviour
// {
//     [Header("Referencias")]
//     public GameObject bulletPrefab;

//     [Header("Configuración General")]
//     public float bulletSpeed = 6f;
//     public float patternDuration = 5f;

//     private void Start()
//     {
//         StartCoroutine(PatternSequence());
//     }

//     private IEnumerator PatternSequence()
//     {
//         // PATRÓN 1: CÍRCULO
//         yield return StartCoroutine(PatternCircular(5f));

//         // PATRÓN 2: ESPIRAL
//         yield return StartCoroutine(PatternSpiral(5f));

//         // PATRÓN 3: LINEAL
//         yield return StartCoroutine(PatternLineal(5f));

//         // Reiniciar patrón (loop infinito)
//         StartCoroutine(PatternSequence());
//     }

//     // -----------------------------------------
//     //   PATRÓN 1: CÍRCULO GIRATORIO
//     // -----------------------------------------
//     private IEnumerator PatternCircular(float time)
//     {
//         float timer = 0f;
//         float angle = 0f;

//         while (timer < time)
//         {
//             int bullets = 16;
//             for (int i = 0; i < bullets; i++)
//             {
//                 float currentAngle = angle + (360f / bullets) * i;
//                 ShootAtAngle(currentAngle);
//             }

//             angle += 10f; // ROTAR todo el círculo
//             timer += 0.2f;
//             yield return new WaitForSeconds(0.2f);
//         }
//     }

//     // -----------------------------------------
//     //   PATRÓN 2: ESPIRAL
//     // -----------------------------------------
//     private IEnumerator PatternSpiral(float time)
//     {
//         float timer = 0f;
//         float angle = 0f;

//         while (timer < time)
//         {
//             ShootAtAngle(angle);
//             angle += 15f; // aumenta siempre -> espiral
//             timer += 0.05f;
//             yield return new WaitForSeconds(0.05f);
//         }
//     }

//     // -----------------------------------------
//     //   PATRÓN 3: DISPARO LINEAL RÁPIDO
//     // -----------------------------------------
//     private IEnumerator PatternLineal(float time)
//     {
//         float timer = 0f;

//         while (timer < time)
//         {
//             // derecha
//             ShootAtAngle(0);
//             // izquierda
//             ShootAtAngle(180);
//             // arriba
//             ShootAtAngle(90);
//             // abajo
//             ShootAtAngle(270);

//             timer += 0.15f;
//             yield return new WaitForSeconds(0.15f);
//         }
//     }

//     // -----------------------------------------
//     //  FUNCIÓN PARA DISPARAR EN UN ÁNGULO
//     // -----------------------------------------
//     private void ShootAtAngle(float angleDegrees)
//     {
//         Quaternion rot = Quaternion.Euler(0, 0, angleDegrees);
//         Vector3 dir = rot * Vector3.right; 

//         GameObject b = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
//         b.GetComponent<Bullet>().SetDirection(dir, bulletSpeed);
//     }
// }
