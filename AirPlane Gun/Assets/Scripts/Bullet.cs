using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    /* private void OnCollisionEnter2D(Collision2D collision)
     {
       Destroy(gameObject);
     }

    */
    void Start()
    {
        Collider2D bulletCollider = GetComponent<Collider2D>();
        // Ignorar colis�es com outras balas
        foreach (Bullet bullet in FindObjectsOfType<Bullet>())
        {
            Collider2D otherBulletCollider = bullet.GetComponent<Collider2D>();
            if (otherBulletCollider != bulletCollider)
            {
                Physics2D.IgnoreCollision(bulletCollider, otherBulletCollider);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        DestructibleSprite destructible = collision.collider.GetComponent<DestructibleSprite>();
        if (destructible)
        {
            Vector2 hitPoint = collision.GetContact(0).point; // Pega o ponto de colis�o
            destructible.DestroyPixel(hitPoint);
        }

        Destroy(gameObject); // Destrua a bala ap�s a colis�o
    }
    /*     private void OnCollisionEnter2D(Collision2D collision)
     {
         DestructibleSprite destructible = collision.collider.GetComponent<DestructibleSprite>();
         if (destructible)
         {
             Vector2 hitPoint = collision.GetContact(0).point; // Pega o ponto de colis�o
             destructible.DestroyPixel(hitPoint);
         }

         // A bala � destru�da ap�s o impacto
         Destroy(gameObject);
     }
    */
}
