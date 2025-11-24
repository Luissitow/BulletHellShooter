using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 15f; // Velocidad de la bala del jugador (más rápida)
    private Vector2 direction;

    void Update()
    {
        // Mover la bala en la dirección establecida
        transform.Translate(direction * speed * Time.deltaTime);

        // Destruir la bala si sale de la pantalla
        if (transform.position.y > 50 || transform.position.y < -50 ||
            transform.position.x > 50 || transform.position.x < -50)
        {
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Destruir la bala si golpea al jefe
        if (other.CompareTag("Boss"))
        {
            Destroy(gameObject);
            // Aquí puedes agregar lógica para dañar al jefe
        }
    }
}
