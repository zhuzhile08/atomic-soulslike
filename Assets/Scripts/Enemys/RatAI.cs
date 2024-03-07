using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RatAI : MonoBehaviour
{
    public int m_max_health = 3;
    public int m_current_health;
    // Start is called before the first frame update
    void Start()
    {
        m_current_health = m_max_health;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_current_health == 0)
        {
            Destroy(gameObject);
        }
         private Vector2 mousePosition;

    void Update()
    {
        mousePosition = Mouse.current.position.ReadValue();

        // Convert mouse position to world space
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        cursorPosition.z = 0f; // Ensure the z-coordinate is set to 0 (assuming your game is in 2D)

        // Calculate the direction from the object to the cursor position
        Vector3 direction = cursorPosition - transform.position;

        // Calculate the rotation angle in radians
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Create a rotation to face towards the cursor
        Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

        // Apply the rotation to the object
        transform.rotation = rotation;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "PlayerBullet(Clone)")
        {
            m_current_health -= 1;
            Destroy(collision.gameObject);
        }
    }


}
