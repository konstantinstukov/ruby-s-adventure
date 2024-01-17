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

    int currentHealth;
    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    [SerializeField] float speed = 3.0f;
    bool isInvictible;
    float invictibleTimer;
    float invictibleTime = 2.0f;

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

        if (isInvictible)
        {
            invictibleTimer -= Time.deltaTime;
            if (invictibleTimer < 0)
                isInvictible = false;

        }
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
        if (amount < 0)
        {
            if (isInvictible)
                return;

            isInvictible = true;
            invictibleTimer = invictibleTime;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
