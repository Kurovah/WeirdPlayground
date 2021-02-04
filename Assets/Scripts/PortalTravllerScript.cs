using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTravllerScript : MonoBehaviour
{
    public Vector3 prevOffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Teleport(Transform from, Transform to)
    {
        transform.parent = from;
        //Debug.Log("Parent from");
        Quaternion r = transform.localRotation;
        Vector3 newPos = transform.localPosition;


        //Debug.Log("Parent to");
        transform.parent = to;
        transform.localPosition = newPos;
        transform.localRotation = r;


        //Debug.Log("Parent noParent");
        transform.parent = null;

        //transform.position = pos;
        //transform.rotation = rot;
        SnapToFloor();
        Debug.Log("Moved");
    }

    public virtual void EnterPortalThreshhold()
    {

    }

    public virtual void ExitPortalThreshhold()
    {

    }

    public void SnapToFloor()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 10, LayerMask.GetMask("Ground")))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
}
