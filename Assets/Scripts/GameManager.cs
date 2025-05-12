using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject chestPrefab;
    [SerializeField] private int numberOfChests;
    
    [SerializeField] private Vector3 spawnPosition = new Vector3(45.5f, 2.25f, 49.5f);
    [SerializeField] private int rowSize = 48;
    [SerializeField] private float chestWidth = 2.0f;
    [SerializeField] private float chestHeight = 2.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < numberOfChests; i++)
        {
            Instantiate(chestPrefab, 
                 spawnPosition + new Vector3(-(i%rowSize)*chestWidth, 0, -Mathf.Floor(i/rowSize)*chestHeight),
                        Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
