using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject target;


    [SerializeField] private float shootRate;
    [SerializeField] private float projectileMoveSpeed;
    private float shootTimer;
    // Update is called once per frame
    void Update()
    {
        
    }
}
