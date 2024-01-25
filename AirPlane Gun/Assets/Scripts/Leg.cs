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
    public float maxSprayAngle = 10f; // M�ximo �ngulo de desvio em graus
    public float recoilForce = 20f;
    public float angleVariation = 2f;

    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime) // Verifica se o bot�o esquerdo do mouse est� pressionado
        {
            Fire();
           
            nextFireTime = Time.time + fireRate; // Atualiza o tempo para o pr�ximo disparo
        }
    }

    public void Fire()
    {
        for (int i = 0; i < bulletsPerShot; i++)
        {

            // Adiciona uma varia��o aleat�ria ao �ngulo de disparo
            float angleOffset = Random.Range(-angleVariation, angleVariation);
            float deviation = maxSprayAngle * (i - (bulletsPerShot - 1) / 2.0f) + angleOffset;
            Quaternion bulletRotation = Quaternion.Euler(new Vector3(0, 0, firepoint.eulerAngles.z + deviation));

            // Adiciona uma varia��o aleat�ria � for�a de disparo
            float forceOffset = Random.Range(-forceVariation, forceVariation);
            float finalFireForce = fireForce + forceOffset;

            // Cria a bala e aplica a for�a com as varia��es
            GameObject bullet = Instantiate(bulletPrefab, firepoint.position, bulletRotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(bullet.transform.up * finalFireForce, ForceMode2D.Impulse);

        }

    }
    
  

}
