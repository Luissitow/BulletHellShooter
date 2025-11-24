using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 moveInput;

    void Update()
    {
        // Leer el input del teclado
        Keyboard keyboard = Keyboard.current;
        
        if (keyboard != null)
        {
            float moveX = 0f;
            float moveY = 0f;

            // Movimiento horizontal
            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
                moveX = -1f;
            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
                moveX = 1f;

            // Movimiento vertical
            if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
                moveY = 1f;
            if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
                moveY = -1f;

            Vector3 movement = new Vector3(moveX, moveY, 0);
            transform.position += movement * speed * Time.deltaTime;

            // Limitar el movimiento dentro de la pantalla
            float clampedX = Mathf.Clamp(transform.position.x, -40f, 40f);
            float clampedY = Mathf.Clamp(transform.position.y, -40f, 40f);
            transform.position = new Vector3(clampedX, clampedY, 0);
        }
    }
}
