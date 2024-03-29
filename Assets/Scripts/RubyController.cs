using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 position = transform.position;

        position.x += 3f * horizontalInput * Time.deltaTime;
        position.y += 3f * verticalInput * Time.deltaTime;

        transform.position = position;
    }
}
