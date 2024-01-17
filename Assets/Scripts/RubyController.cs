using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rb;
    float horizontalInput;
    float verticalInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate() {
        Vector2 position = transform.position;
        position.x += 3f * horizontalInput * Time.deltaTime;
        position.y += 3f * verticalInput * Time.deltaTime;

        rb.MovePosition(position);
    }
}
