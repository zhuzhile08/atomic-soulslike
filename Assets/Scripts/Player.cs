using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour {
    public float moveSpeed;
    public float sprintSpeed;
    public float bulletSpeed;
    public GameObject bulletPrefab;

    private PlayerControls m_playerControls;
    private Rigidbody2D m_rigidbody;
    private Transform m_gunTip;

    private Vector2 m_direction;
    private Vector2 m_pointDirection;
    private float m_lastPointDirection;

    private bool m_sprinting;

    void Start() {
        m_playerControls = new PlayerControls();
        m_pointDirection = new Vector2(1, 0);
        m_lastPointDirection = -1;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_gunTip = GetComponent<Transform>().Find("GunTip");
    }

    void FixedUpdate() {
        if (m_sprinting) m_rigidbody.velocity = m_direction * sprintSpeed;
        else m_rigidbody.velocity = m_direction * moveSpeed;

        GetComponent<Transform>().localScale = new Vector3(m_lastPointDirection, 1, 1);
    }

    private void OnMove(InputValue value) {
        m_direction = value.Get<Vector2>();

        if (Math.Abs(m_direction.x) + Math.Abs(m_direction.y) == 1) {
            m_pointDirection = m_direction;

            if (m_pointDirection.x != 0) {
                m_lastPointDirection = m_pointDirection.x;
                m_gunTip.localPosition = new Vector2(1, 1);
            } else if (m_pointDirection.y == -1) {
                m_gunTip.localPosition = new Vector2(0, 0);
                m_gunTip.Rotate(new Vector3(0, 0, 90));
            } else if (m_pointDirection.y == 1) {
                m_gunTip.localPosition = new Vector2(0, 2);
                m_gunTip.Rotate(new Vector3(0, 0, -90));
            }
        }
    }
    private void OnSprint(InputValue value) {
        m_sprinting = value.isPressed;
    }
    private void OnShoot() {
        var b = GameObject.Instantiate(bulletPrefab, m_gunTip.position, m_gunTip.rotation);
        b.GetComponent<Rigidbody2D>().AddForce(m_pointDirection * bulletSpeed);
        GameObject.Destroy(b, 1.0f);
    }
}
