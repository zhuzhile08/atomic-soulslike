using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "PlayerBullet(Clone)")
        {
            m_current_health -= 1;
        }
    }


}
