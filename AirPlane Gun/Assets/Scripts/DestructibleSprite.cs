using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Texture2D texture;
    private Color[] originalTexture;
    public float destructionRadius; // Raio do dano em pixels
    private PolygonCollider2D polygonCollider;

    private void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        polygonCollider = GetComponent<PolygonCollider2D>();


        // Se não existe um PolygonCollider2D, adicione um
        if (polygonCollider == null)
        {
            polygonCollider = gameObject.AddComponent<PolygonCollider2D>();
        }

        // Crie uma cópia da textura original para que possamos modificar
        texture = Instantiate(spriteRenderer.sprite.texture);
        spriteRenderer.sprite = Sprite.Create(texture, spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f), spriteRenderer.sprite.pixelsPerUnit);

       

        // Defina o raio de destruição para ser grande o suficiente para destruir o sprite
        destructionRadius = Mathf.Max(texture.width, texture.height) / 2f -500;



        /*  spriteRenderer = GetComponent<SpriteRenderer>();
          // Crie uma cópia da textura original para que possamos modificar
          texture = Instantiate(spriteRenderer.sprite.texture);
          originalTexture = texture.GetPixels();

          // Defina a textura como modificável e aplique-a ao sprite
          spriteRenderer.sprite = Sprite.Create(texture, spriteRenderer.sprite.rect, new Vector2(0.5f, 0.5f), spriteRenderer.sprite.pixelsPerUnit);

          */
    }

    public void DestroyPixel(Vector2 worldPoint)
    {
        Vector2Int pixelUV = WorldToTexturePosition(worldPoint);
        texture.SetPixel(pixelUV.x, pixelUV.y, Color.clear);
       
        // Apague um círculo de pixels na textura
        for (int y = -Mathf.CeilToInt(destructionRadius); y <= Mathf.CeilToInt(destructionRadius); y++)
        {
            for (int x = -Mathf.CeilToInt(destructionRadius); x <= Mathf.CeilToInt(destructionRadius); x++)
            {
                if (x * x + y * y <= destructionRadius * destructionRadius)
                {
                    texture.SetPixel(pixelUV.x + x, pixelUV.y + y, Color.clear);
                }
            }
        }
     
        texture.Apply();

        // Atualize o collider após a destruição dos pixels
        UpdateCollider();



        /*  Vector2Int pixelUV = WorldToTexturePosition(worldPoint);
          int radius = Mathf.CeilToInt(destructionRadius);

          // Apague um círculo de pixels ao redor do ponto de colisão
          for (int y = -radius; y <= radius; y++)
          {
              for (int x = -radius; x <= radius; x++)
              {
                  if (x * x + y * y <= radius * radius)
                  {
                      texture.SetPixel(pixelUV.x + x, pixelUV.y + y, Color.clear);
                  }
              }
          }

          texture.Apply();

          */
    }
    private void UpdateCollider()
    {
        // Você pode precisar refinar como o collider é atualizado com base na destruição dos pixels
        polygonCollider.enabled = false;
        polygonCollider.enabled = true;
    }
    private Vector2Int WorldToTexturePosition(Vector2 worldPoint)
    {
        // Converte a posição do mundo para a posição da textura
        Vector2 localPos = transform.InverseTransformPoint(worldPoint);
        Rect rect = spriteRenderer.sprite.rect;
        float ppu = spriteRenderer.sprite.pixelsPerUnit;

        int x = Mathf.FloorToInt((localPos.x * ppu) + rect.width * 0.5f);
        int y = Mathf.FloorToInt((localPos.y * ppu) + rect.height * 0.5f);

        return new Vector2Int(x, y);



        /*  Vector3 localPos = transform.InverseTransformPoint(worldPoint);
          Rect rect = spriteRenderer.sprite.rect;
          float ppu = spriteRenderer.sprite.pixelsPerUnit;

          int x = Mathf.FloorToInt((localPos.x * ppu) + rect.width * 0.5f);
          int y = Mathf.FloorToInt((localPos.y * ppu) + rect.height * 0.5f);

          return new Vector2Int(x, y);
      */
    }

    
}
