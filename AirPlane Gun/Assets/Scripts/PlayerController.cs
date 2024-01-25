using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Leg leg;

    Vector2 moveDirection;
    Vector2 mousePosition;
    Vector2 aimDirection;

    public float recoilForce = 20f;

    private bool shouldRecoil = false;

    // Update is called once per frame
    void Update()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (Input.GetMouseButtonDown(0)) 
        {
            shouldRecoil = true;
            leg.Fire();
 
        }
  
    }
    private void FixedUpdate()
    {
       
        //  rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        rb.velocity = moveDirection * moveSpeed;
        UpdateAimDirection();
       
      //  Vector2 aimDirection = mousePosition - rb.position;
       //   float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
         // rb.rotation = aimAngle;
      
        if (Input.GetMouseButton(0))
        {

            ApplyRecoil();
        }
       

    }

    void UpdateAimDirection()
    {
        // Atualiza aimDirection com a posição atual do mouse
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    void ApplyRecoil()
    {
        // Calcula a direção do recuo como oposta à direção de mira atual
        Vector2 recoilDirection = -aimDirection.normalized;
        rb.AddForce(recoilDirection * recoilForce, ForceMode2D.Impulse);
    }

    public void TriggerRecoil()
    {
        shouldRecoil = true; // Marca que o recuo deve ser aplicado
    }

}
