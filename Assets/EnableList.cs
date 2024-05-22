using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableList : MonoBehaviour
{
    public GameObject[] ObjectsToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("HandTag"))
        {
            foreach(GameObject Obj in ObjectsToEnable)
            {
                Obj.SetActive(true);
            }
        }
    }
}
