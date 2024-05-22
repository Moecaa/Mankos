using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableList : MonoBehaviour
{
    public GameObject[] ObjectsToDisable;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("HandTag"))
        {
            foreach(GameObject Obj in ObjectsToDisable)
            {
                Obj.SetActive(false);
            }
        }
    }
}
