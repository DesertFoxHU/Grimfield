using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRandom : MonoBehaviour
{
    public bool isTowardsRight = false;
    public float speed;
    private GameObject cam;

    public void Start()
    {
        cam = Camera.main.gameObject;
        if (Utils.Roll(50))
        {
            isTowardsRight = true;
        }
    }

    void Update()
    {
        if (isTowardsRight)
        {
            cam.transform.position = Vector3.Lerp(
                cam.transform.position,
                new Vector3(cam.transform.position.x + 1f, cam.transform.position.y, cam.transform.position.z),
                Time.deltaTime * speed);
        }
        else
        {
            cam.transform.position = Vector3.Lerp(
                cam.transform.position,
                new Vector3(cam.transform.position.x - 1f, cam.transform.position.y, cam.transform.position.z),
                Time.deltaTime * speed);
        }
    }
}
