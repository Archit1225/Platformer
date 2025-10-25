using Unity.Cinemachine;
using UnityEngine;

public class BackgroundSwitcher : MonoBehaviour
{
    public bool isUnderground=false;
    public Transform aboveBg;
    public Transform underBg;
    public PolygonCollider2D M1;
    public PolygonCollider2D M2;
    public Transform player;
    public CinemachineConfiner2D cineCam;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    void FixedUpdate()
    {
        if (player.position.y <= -5.66f)
        {
            isUnderground = true;
            cineCam.BoundingShape2D = M2;
        }
        else
        {
            isUnderground = false;
            cineCam.BoundingShape2D = M1 ;
        }
        UpdateBackground();
    }

    void UpdateBackground()
    {
        foreach (Transform child in aboveBg)
        {
            BackgroundController controller = child.GetComponent<BackgroundController>();

            if (controller != null)
            {
                controller.enabled = !isUnderground;
            }
        }

        foreach (Transform child in underBg)
        {
            BackgroundController controller = child.GetComponent<BackgroundController>();

            if (controller != null)
            {
                controller.enabled = isUnderground;
            }
        }
    }
}
