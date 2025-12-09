using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint1;
    public Transform firePoint2;

    [Header("bigGun")]
    private bool canBigShoot = true;
    public float timeReload = 3;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            lightGun();
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            BigGun();
        }
    }
    void lightGun()
    {
            // Tir depuis le firePoint1
            GameObject bullet1 = BulletPooler.Instance.GetBullet();
            bullet1.transform.position = firePoint1.position;
            bullet1.transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - 90);

            // Tir depuis le firePoint2
            GameObject bullet2 = BulletPooler.Instance.GetBullet();
            bullet2.transform.position = firePoint2.position;
            bullet2.transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - 90);
    }
    void BigGun()
    {
        bullet2.transform.position = firePoint2.position;
        bullet2.transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z - 90);
        canBigShoot = false;
        StartCoroutine(WaitForReload());
    }

    private IEnumerator WaitForReload()
    {
        yield return new WaitForSeconds(timeReload);
        canBigShoot = true;
    }
}

