using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public PortalScript linkedPortal;
    public Camera portalCam;
    public Transform player;
    [HideInInspector]
    public Vector3 startPoint;
    public MeshRenderer portalPlane;
    RenderTexture viewTexture;
    public List<PortalTravllerScript> travellers;
    // Start is called before the first frame update
    void Awake()
    {
        startPoint = portalCam.transform.position;
        portalCam.enabled = false;
        //CreateNewRT();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i = 0; i < travellers.Count; i++)
        {
            PortalTravllerScript pt = travellers[i];
            Transform t = pt.transform;

            Vector3 offset = t.position - transform.position;

            int sideOfPortal = (int)Mathf.Sign(Vector3.Dot(offset, transform.forward));
            int prevSideOfPortal = (int)Mathf.Sign(Vector3.Dot(pt.prevOffset, transform.forward));

            if(sideOfPortal != prevSideOfPortal)
            {
                var m = linkedPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * player.transform.localToWorldMatrix;
                pt.Teleport(transform, linkedPortal.transform);
                travellers.RemoveAt(i);
                
                Debug.Log("Crossed");

                break;
            }


            pt.prevOffset = offset;
        }
    }

    public void CreateNewRenderTex()
    {
        if(viewTexture == null || Screen.height != viewTexture.height || Screen.width != viewTexture.width)
        {
            if(viewTexture != null)
            {
                viewTexture.Release();
            }

            viewTexture = new RenderTexture(Screen.width, Screen.height, 0);
            linkedPortal.portalCam.targetTexture = viewTexture;
            linkedPortal.portalPlane.material.SetTexture("_MainTex", viewTexture);
        }
    }

    public void Render()
    {
        
        linkedPortal.portalPlane.GetComponent<MeshRenderer>().enabled = false;
        CreateNewRenderTex();


        //seb's Method
        //*
        var m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix * player.transform.localToWorldMatrix;
        linkedPortal.portalCam.transform.SetPositionAndRotation(m.GetColumn(3),m.rotation);
        /**/



        portalCam.Render();

        linkedPortal.portalPlane.GetComponent<MeshRenderer>().enabled = true;
    }

    void OnEnityEnter(PortalTravllerScript traveller)
    {
        if (!travellers.Contains(traveller))
        {
            traveller.EnterPortalThreshhold();
            traveller.prevOffset = traveller.transform.position - transform.position;
            travellers.Add(traveller);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var t = other.GetComponent<PortalTravllerScript>();
        if (t)
        {
            OnEnityEnter(t);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var t = other.GetComponent<PortalTravllerScript>();
        if (t && travellers.Contains(t))
        {
            t.ExitPortalThreshhold();
            travellers.Remove(t);
        }
    }
}
