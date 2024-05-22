using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.VR;

public class ColorScript : MonoBehaviour
{


    public float Red;
    public float Green;
    public float Blue;
    float TrueRed;
    float TrueGreen;
    float TrueBlue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        TrueRed = Red / 10;
        TrueGreen = Green / 10;
        TrueBlue = Blue / 10;

        Color myColour = new Color(TrueRed, TrueGreen, TrueBlue);
        PhotonVRManager.SetColour(myColour);
    }

}
