using easyInputs;
using GorillaLocomotion;
using Photon.Pun.Demo.PunBasics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SizeChanger : MonoBehaviour
{
    public Transform gorillaPlayer;
    public Player player;
    public SphereCollider left;
    public SphereCollider right;
    public float lerpSpeed;
    public Vector3 maxScale;
    public Vector3 minScale;
    public bool scaling;
    public Vector3 gravityFactor;
    public bool test;

    public bool scaleup;
    public bool scaledown;
    public bool resetscale;

    private void Start()
    {
        gravityFactor = Physics.gravity;
    }

    private void Update()
    {
        player.headDistance = 0.15f * gorillaPlayer.localScale.y;
        player.minimumRaycastDistance = 0.05f * gorillaPlayer.localScale.y;
        player.unStickDistance = gorillaPlayer.localScale.y;
        player.maxArmLength = 1.5f * gorillaPlayer.localScale.y;
        player.leftHandFollower.localScale = gorillaPlayer.localScale * 0.1f;
        player.rightHandFollower.localScale = gorillaPlayer.localScale * 0.1f;

        Physics.gravity = gravityFactor * gorillaPlayer.localScale.y;

        if (gorillaPlayer.localScale.y > 2)
        {

        }

        if (test)
        {
            if (scaleup)
            {
                ScaleUp();
            }
            if (scaledown)
            {
                ScaleDown();
            }
            if (resetscale)
            {
                ResetScale();
            }
        }
        else
        {
            if (EasyInputs.GetTriggerButtonDown(EasyHand.RightHand))
            {
                scaling = true;
                ScaleUp();
            }
            else
            {
                scaling = false;
            }

            if (EasyInputs.GetTriggerButtonDown(EasyHand.LeftHand))
            {
                scaling = true;
                ScaleDown();
            }
            else
            {
                scaling = false;
            }


            if (EasyInputs.GetSecondaryButtonDown(EasyHand.RightHand))
            {
                scaling = true;
                ResetScale();
            }
            else
            {
                scaling = false;
            }
        }
    }

    public void ResetScale()
    {
        if (scaling)
        {
            gorillaPlayer.transform.localScale = Vector3.MoveTowards(gorillaPlayer.localScale, Vector3.one, lerpSpeed);
        }
    }

    public void ScaleDown()
    {
        if (scaling)
        {
            gorillaPlayer.transform.localScale = Vector3.MoveTowards(gorillaPlayer.localScale, minScale, lerpSpeed);
        }
    }

    public void ScaleUp()
    {
        if (scaling)
        {
            gorillaPlayer.transform.localScale = Vector3.MoveTowards(gorillaPlayer.localScale, maxScale, lerpSpeed);
        }
    }
}
