using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class MirrorTrigger : MonoBehaviour
{
    public Camera cam;

    private void Start()
    {
        cam.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HandTag"))
        {
            cam.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HandTag"))
        {
            cam.gameObject.SetActive(false);
        }
    }
}
