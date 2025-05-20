using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Prefabs & References")]
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public Transform firePoint;

    [Header("Tuning")]
    public float bulletSpeed = 10f;
    public float explosionLifetime = 0.5f;

    public AudioClip shootSfx;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }

    void Shoot()
    {
        // Determine direction
        float direction = transform.parent ? transform.parent.localScale.x : transform.localScale.x;

        Quaternion bulletRotation = firePoint.rotation;
        if (direction < 0) bulletRotation = Quaternion.Euler(0, 180, 0);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(direction * bulletSpeed, 0);

        audioSource.PlayOneShot(shootSfx, 0.5f);

        // Muzzle flash
        if (explosionPrefab != null)
        {
            GameObject fx = Instantiate(
                explosionPrefab, firePoint.position, firePoint.rotation, firePoint);

            Destroy(fx, explosionLifetime);
        }
    }
}
