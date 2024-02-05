using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    AudioSource audioSource;

    public AudioClip takeDamegeClip;
    public AudioClip throwProjectileClip;
    public AudioClip footStepsClip;
    public GameObject footStepsObj;

    Vector2 lookDirection = new Vector2(1, 0);

    [SerializeField] float speed = 3.0f;
    bool isInvincible;
    float invincibleTimer;
    float invincibleTime = 2.0f;

    float horizontalInput;
    float verticalInput;

    public GameObject projectilePrefab;


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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontalInput, verticalInput);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rb.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));

            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();

                character?.DisplayDialog();
            }
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
            animator.SetTrigger("Hit");

            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = invincibleTime;
            PlaySound(takeDamegeClip);

        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rb.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");

        PlaySound(throwProjectileClip);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    void StepSoundPlay() {
        audioSource.PlayOneShot(footStepsClip);
    }
}
