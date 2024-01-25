using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public float scale = 20f;
    public int islandCount = 10;

    private void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        Texture2D texture = new Texture2D(width, height);

        for (int i = 0; i < islandCount; i++)
        {
            // Gerar posição aleatória para o centro da ilha
            Vector2 islandCenter = new Vector2(Random.Range(0, width), Random.Range(0, height));
            // Gerar tamanho aleatório para a ilha
            float islandRadius = Random.Range(3, 10);

            // Criar ilha
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float distanceToCenter = Vector2.Distance(islandCenter, new Vector2(x, y));
                    if (distanceToCenter < islandRadius)
                    {
                        texture.SetPixel(x, y, Color.black); // Ilha
                    }
                    else
                    {
                        texture.SetPixel(x, y, Color.clear); // Água
                    }
                }
            }

        }

        // Aplica as alterações na textura
        texture.Apply();

        // Atribuir a textura a um material de um objeto para visualização
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.mainTexture = texture;
        }



    }
}
