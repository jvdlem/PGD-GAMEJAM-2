using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public Texture2D map;
    private Vector3 position;
    public ColorToPrefab[] colorMappings;
    void Awake()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int x = 0; x < map.width; x++)
        {
            for (int y = 0; y < map.height ; y++)
            {
                    GenerateTile(x, y);
            }
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0)
        {
            //pixel is transparent so ignore
            return;
        }

        foreach (ColorToPrefab colorMapping in colorMappings)
        {
            if (colorMapping.color.Equals(pixelColor))
            {
                if (map.height % 2 == 1)
                {
                    position = new Vector3(x, 0, y - map.height / 2);
                }
                else
                {
                  position = new Vector3(x, 0, y-map.height/2+0.5f);
                }
                Instantiate(colorMapping.prefab, position, Quaternion.identity, transform);
            }
        }
    }
}
