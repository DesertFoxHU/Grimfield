using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRandom : MonoBehaviour
{
    public bool isTowardsRight = false;
    public float speed;
    private GameObject camera;

    public void Start()
    {
        camera = Camera.main.gameObject;
        if (Utils.Roll(50))
        {
            isTowardsRight = true;
        }
    }

    void Update()
    {
        if (isTowardsRight)
        {
            camera.transform.position = Vector3.Lerp(
                camera.transform.position,
                new Vector3(camera.transform.position.x + 1f, camera.transform.position.y, camera.transform.position.z),
                Time.deltaTime * speed);
        }
        else
        {
            camera.transform.position = Vector3.Lerp(
                camera.transform.position,
                new Vector3(camera.transform.position.x - 1f, camera.transform.position.y, camera.transform.position.z),
                Time.deltaTime * speed);
        }
    }
}
