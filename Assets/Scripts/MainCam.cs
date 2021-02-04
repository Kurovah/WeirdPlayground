using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCam : MonoBehaviour
{
    PortalScript[] portals;

    void Awake()
    {
        portals = FindObjectsOfType<PortalScript>();
    }

    void OnPreCull()
    {
        for (int i = 0; i < portals.Length; i++)
        {
            portals[i].Render();
        }
    }
}
