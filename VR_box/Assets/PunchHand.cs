using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Dynamics;
public class PunchHand : MonoBehaviour
{

    public SteamVR_TrackedObject hand;
    private Rigidbody rBody;
    private FixedJoint joint;
    public GameObject controller;
    public GameObject gloves;
    bool flag = false;

    // Use this for initialization
    void Start()
    {

        rBody = gloves.GetComponent<Rigidbody>();
        
    }   

    // Update is called once per frame
    void Update()
    {
        if ((controller.activeInHierarchy) & (flag==false))
        {
            Debug.Log(controller.transform.position);
            rBody.transform.position = new Vector3(controller.transform.position.x, controller.transform.position.y+0.03f, controller.transform.position.z-0.03f);
            rBody.transform.rotation = new Quaternion(controller.transform.rotation.w, controller.transform.rotation.x+0.1f, controller.transform.position.y+2.1f, controller.transform.position.z+1.5f);
            joint = gloves.AddComponent<FixedJoint>();
            joint.connectedBody = controller.GetComponent<Rigidbody>();
            flag=true;
        }
        Debug.Log(rBody.velocity);
    }



    void OnCollisionEnter(Collision other)
    {

        Rigidbody otherR = other.gameObject.GetComponentInChildren<Rigidbody>();
        if (other == null)
            return;

        Vector3 avgPoint = Vector3.zero;
        foreach (ContactPoint p in other.contacts)
        {
            avgPoint += p.point;
        }

        avgPoint /= other.contacts.Length;

        Vector3 dir = (avgPoint - transform.position).normalized;
        //otherR.AddForceAtPosition(dir * 100f * rBody.velocity.magnitude, avgPoint);

      

    }
}