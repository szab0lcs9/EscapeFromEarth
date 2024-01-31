using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    Vector3 playerPosition;
    LineRenderer laser;
    GameObject player;
    WaitForSeconds shotDuration = new WaitForSeconds(0.1f);

    [SerializeField]
    private int shootingDamage;

    [SerializeField]
    private float fireRate = 0.20f;

    [SerializeField]
    private float shootingRange;

    [SerializeField]
    private float hitForce = 0.5f;

    private float nextShootIn;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > nextShootIn)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        nextShootIn = Time.time + fireRate;
        StartCoroutine(LaserEffect());
        playerPosition = player.transform.position;
        laser.SetPosition(0, playerPosition);

        if (Physics.Raycast(playerPosition, player.transform.forward, out RaycastHit hit, shootingRange))
        {
            IEnemy enemy = hit.transform.GetComponent<IEnemy>();

            if (enemy != null)
            {
                IDamageable damageableEnemy = hit.transform.GetComponent<IDamageable>();

                if (damageableEnemy != null)
                {
                    damageableEnemy.TakeDamage(shootingDamage);
                }
            }
            laser.SetPosition(1, hit.point);

            if (hit.rigidbody != null)
                hit.rigidbody.AddForce(-hit.normal * hitForce);

        }
        else
        {
            laser.SetPosition(1, playerPosition + (player.transform.forward * shootingRange));
        }
    }

    private IEnumerator LaserEffect()
    {
        laser.enabled = true;

        yield return shotDuration;

        laser.enabled = false;
    }
}
