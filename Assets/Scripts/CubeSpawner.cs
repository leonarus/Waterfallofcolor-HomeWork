using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public int rowSize = 20;
    public int columnSize = 20;
    public float spawnInterval = 0.5f;
    public float spawnDistanceX = 1f; 
    public float spawnDistanceZ = 1f;

    public float colorChangeInterval = 0.2f;
    public float colorChangeDuration = 0.5f;
    
    
    private void Start()
    {
        StartCoroutine(SpawnCubes());
    }

    private IEnumerator SpawnCubes()
    {
        
        float startX = -40f;
        float startZ = -40f;
        
        for (int row = 0; row < rowSize; row++)
        {
            for (int column = 0; column < columnSize; column++)
            {
                Vector3 spawnPosition = new Vector3(startX + row * spawnDistanceX, 0, startZ + column * spawnDistanceZ);
                Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
                yield return new WaitForSeconds(spawnInterval);
            }
        }
    }

    public void ChangeColors()
    {
        StartCoroutine(ChangeColorsCoroutine());
    }

    private IEnumerator ChangeColorsCoroutine()
    {
        var newColor = GetRandomColor();

        foreach (var cube in GameObject.FindGameObjectsWithTag("Cube"))
        {
            StartCoroutine(ChangeColor(cube, newColor));
            yield return new WaitForSeconds(colorChangeInterval);
        }
    }

    private IEnumerator ChangeColor(GameObject cube, Color newColor)
    {
        var cubeMaterial = cube.GetComponent<Renderer>().material;
        var startColor = cubeMaterial.color;

        float elapsedTime = 0f;

        while (elapsedTime < colorChangeDuration)
        {
            cubeMaterial.color = Color.Lerp(startColor, newColor, elapsedTime / colorChangeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cubeMaterial.color = newColor;
    }

    private Color GetRandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}