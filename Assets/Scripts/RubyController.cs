using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RubyController : MonoBehaviour
{
    [SerializeField] int maxHealth = 5;
    public int MaxHealth
    {
        get { return maxHealth; }
    }
    [SerializeField] float speed = 3.0f;
    int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    Rigidbody2D rb;
    float horizontalInput;
    float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x += speed * horizontalInput * Time.deltaTime;
        position.y += speed * verticalInput * Time.deltaTime;

        rb.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
