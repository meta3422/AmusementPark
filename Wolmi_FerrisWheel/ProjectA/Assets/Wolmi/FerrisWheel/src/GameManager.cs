using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject yongYongPrefab;

    private void Awake()
    {
        _instance = this;
    }

    public void CreateNewYongYong()
    {
        Instantiate(yongYongPrefab, spawnPoint.position, Quaternion.identity);
    }
}
