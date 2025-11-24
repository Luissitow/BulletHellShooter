using UnityEngine;

public enum BulletMovementType
{
    Straight,      // Movimiento recto normal
    Homing,        // Sigue al jugador
    Wave           // Movimiento en zigzag/onda
}

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // Velocidad de la bala
    public BulletMovementType movementType = BulletMovementType.Straight;
    
    private Vector2 direction;
    private Transform player;
    private float waveFrequency = 5f; // Frecuencia del zigzag
    private float waveAmplitude = 2f; // Amplitud del zigzag
    private float timeAlive = 0f;

    void Start()
    {
        // Buscar al jugador si el movimiento es homing
        if (movementType == BulletMovementType.Homing)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        timeAlive += Time.deltaTime;

        switch (movementType)
        {
            case BulletMovementType.Straight:
                MoveStraight();
                break;
            case BulletMovementType.Homing:
                MoveHoming();
                break;
            case BulletMovementType.Wave:
                MoveWave();
                break;
        }

        // Destruir la bala si sale de la pantalla
        if (transform.position.y > 100 || transform.position.y < -100 ||
            transform.position.x > 100 || transform.position.x < -100)
        {
            DestroyBullet();
        }
    }

    private void MoveStraight()
    {
        // Movimiento recto normal
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void MoveHoming()
    {
        // Seguir al jugador
        if (player != null)
        {
            Vector2 targetDirection = (player.position - transform.position).normalized;
            
            // Interpolar suavemente entre la dirección actual y la dirección hacia el jugador
            direction = Vector2.Lerp(direction, targetDirection, Time.deltaTime * 2f);
            direction.Normalize();
        }
        
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void MoveWave()
    {
        // Movimiento en zigzag/onda
        Vector2 moveDirection = direction;
        
        // Calcular el desplazamiento perpendicular para crear el efecto de onda
        Vector2 perpendicular = new Vector2(-direction.y, direction.x);
        float wave = Mathf.Sin(timeAlive * waveFrequency) * waveAmplitude;
        
        Vector2 finalPosition = (Vector2)transform.position + 
                               (moveDirection * speed * Time.deltaTime) + 
                               (perpendicular * wave * Time.deltaTime);
        
        transform.position = finalPosition;
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    public void SetMovementType(BulletMovementType type)
    {
        movementType = type;
    }

    private void DestroyBullet()
    {
        // Notificar al jefe que una bala ha sido destruida
        BossController boss = FindFirstObjectByType<BossController>();
        if (boss != null)
        {
            boss.DecreaseBulletCount();
        }
        Destroy(gameObject);
    }
}