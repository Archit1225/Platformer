using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public GameObject cam;
    public float parallaxEffect;
    private float startPos;
    private float length;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.x;//Here startPos=0, this stores 'x' coordinate of Backgrounds
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //stores how much distance bg must move relative to camera
        float distance = cam.transform.position.x * parallaxEffect;

        //
        float movement = cam.transform.position.x * (1-parallaxEffect);

        //Each fixed frame rate it checks camera position and updates the background position relatively
        transform.position = new Vector3 (startPos+distance, transform.position.y, transform.position.z);

        //whenever the player/camera moves beyond the length then the below code changes the startPos
        //which then later continues the above code
        if(movement < startPos-length)
        {
            startPos -= length;
        }
        else if(movement > startPos+length)
        {
            startPos += length;
        }
    }
}
