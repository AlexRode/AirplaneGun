using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public GameObject bulletPrefab;
    public Transform firepoint;
    public float fireForce = 20f;

    public float fireRate = 0.001f; // Tempo entre disparos
    public float forceVariation = 5f;
    private float nextFireTime = 0.001f;
    public int bulletsPerShot = 3;
    public float maxSprayAngle = 10f; // Máximo ângulo de desvio em graus
    public float recoilForce = 20f;
    public float angleVariation = 2f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime) // Verifica se o botão esquerdo do mouse está pressionado
        {
            Fire();
           
            nextFireTime = Time.time + fireRate; // Atualiza o tempo para o próximo disparo
        }
    }

    public void Fire()
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {

            // Adiciona uma variação aleatória ao ângulo de disparo
            float angleOffset = Random.Range(-angleVariation, angleVariation);
            float deviation = maxSprayAngle * (i - (bulletsPerShot - 1) / 2.0f) + angleOffset;
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, firepoint.eulerAngles.z + deviation));

            // Adiciona uma variação aleatória à força de disparo
            float forceOffset = Random.Range(-forceVariation, forceVariation);
            float finalFireForce = fireForce + forceOffset;

            // Cria a bala e aplica a força com as variações
            GameObject bullet = Instantiate(bulletPrefab, firepoint.position, bulletRotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * finalFireForce, ForceMode2D.Impulse);

        }

    }
    
  

}
