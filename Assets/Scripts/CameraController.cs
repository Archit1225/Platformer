using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class CameraController : MonoBehaviour
{
    public CinemachineCamera MainCam;
    public CinemachineCamera BossCam;
    private CinemachineCamera activeCam;
    private bool isBossCamActive = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MainCam.gameObject.SetActive(true);
        BossCam.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MainCam.gameObject.SetActive(isBossCamActive);
            BossCam.gameObject.SetActive(!isBossCamActive);
            isBossCamActive = !isBossCamActive;
        }
    }

}