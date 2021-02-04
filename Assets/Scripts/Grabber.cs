using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    public Transform grabbedObj, cam;
    Collider col;
    public float distance, newDis,ratio;
    float oldScale;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, cam.forward);
        RaycastHit hit;
        
        if (grabbedObj != null)
        {
            
            if (Physics.Raycast(ray, out hit, 50))
            {

                ChecknewDist(hit.distance);
            }
            else
            {
                ChecknewDist(50);
            }

            if (Input.GetMouseButtonUp(0))
            {
                grabbedObj = null;
                col.enabled = true;
                col = null;
            }
        } else
        {
            
            
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit, 50, LayerMask.GetMask("Grab")))
                {
                    col = hit.collider;
                    col.enabled = false;
                    grabbedObj = hit.collider.gameObject.transform;
                    distance = Vector3.Distance(grabbedObj.position, transform.position);
                    ratio = Mathf.Atan(grabbedObj.localScale.x / (2*distance));
                    oldScale = grabbedObj.localScale.x;
                }
            }

            
        }
    }

    public void ChecknewDist(float Dis)
    {
        float newScale;
        //check along distance line to see if the ball fits
        for(int i = 0; i < Dis; i++)
        {
            newScale = 2 * i * Mathf.Tan(ratio);
            if (i + newScale / 2 >= Dis)
            {
                grabbedObj.position = cam.position + cam.forward * i;
                grabbedObj.localScale = new Vector3(newScale, newScale, newScale);
                break;
            }


        }
    }
}
