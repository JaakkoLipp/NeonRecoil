using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int health = 3;
    public float speed = 2f;
    public float patrolTime = 2f;
    public float detectionRange = 8f;

    [Header("Combat")]
    public GameObject bulletPrefab;
    public Transform  firePoint;
    public float fireRate = 1.5f;
    public float bulletSpeed = 10f;
    public LayerMask obstacleLayer;

    [Header("Death FX")]
    public GameObject deathFXPrefab;

    [Header("Muzzle Flash")]
    public GameObject muzzleFlashPrefab;
    public float      muzzleFlashLife = 0.5f;

    [Header("Animation States")]
    public Animator animator;
    public string normalState = "Patrol";
    public string alertState  = "AlertColorPulse";
    public float  fadeInTime  = 0f;
    public float  fadeOutTime = 0.1f;

    public AudioClip shootSfx;
    private AudioSource audioSource;
    
    Rigidbody2D rb;
    Transform   player;
    float       patrolTimer;
    float       nextFireTime;
    bool        isAlerting;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;

        rb         = GetComponent<Rigidbody2D>();
        player     = GameObject.FindWithTag("Player")?.transform;
        patrolTimer = patrolTime;
        if (animator == null) animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        bool seesPlayer = CanSeePlayer();

        // Animation state transitions
        if (seesPlayer && !isAlerting)
        {
            isAlerting = true;
            animator.CrossFade(alertState, fadeInTime);
        }
        else if (!seesPlayer && isAlerting)
        {
            isAlerting = false;
            animator.CrossFade(normalState, fadeOutTime);
        }

        // Main behavior: chase/shoot or patrol
        if (seesPlayer)
        {
            FacePlayer();
            ShootPlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        float dir = transform.localScale.x > 0 ? 1f : -1f;
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);

        patrolTimer -= Time.deltaTime;
        if (patrolTimer <= 0f)
        {
            transform.localScale = new Vector3(-transform.localScale.x,
                                               transform.localScale.y,
                                               transform.localScale.z);
            patrolTimer = patrolTime;
        }
    }

    void FacePlayer()
    {
        float sx = player.position.x >= transform.position.x
                 ? Mathf.Abs(transform.localScale.x)
                 : -Mathf.Abs(transform.localScale.x);
        transform.localScale = new Vector3(sx,
                                           transform.localScale.y,
                                           transform.localScale.z);
    }

    bool CanSeePlayer()
    {
        float dist = Vector2.Distance(firePoint.position, player.position);
        if (dist > detectionRange) return false;

        Vector2 dir = (player.position - firePoint.position).normalized;
        var hit = Physics2D.Raycast(firePoint.position, dir, dist, obstacleLayer);
        return hit.collider == null || hit.collider.CompareTag("Player");
    }

    void ShootPlayer()
    {
        if (Time.time < nextFireTime) return;
        nextFireTime = Time.time + fireRate;

        var bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        var rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 dir = (player.position - firePoint.position).normalized;
        rb.linearVelocity = dir * bulletSpeed;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

        audioSource.PlayOneShot(shootSfx, 0.5f);

        if (muzzleFlashPrefab != null)
        {
            var fx = Instantiate(muzzleFlashPrefab,
                                firePoint.position,
                                firePoint.rotation,
                                firePoint);

            Destroy(fx, muzzleFlashLife);
        }
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health <= 0) Die();
    }

    void Die()
    {
        // Death effect and remove enemy
        if (deathFXPrefab != null)
            Instantiate(deathFXPrefab, transform.position, Quaternion.identity);

        ScoreManager.Instance.AddScoreForEnemyKill();
        Destroy(gameObject);
    }
}
