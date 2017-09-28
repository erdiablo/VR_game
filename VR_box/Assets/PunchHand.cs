using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchHand : MonoBehaviour {
   
    public SteamVR_TrackedObject hand;
    private Rigidbody rBody;

	// Use this for initialization
	void Start () {
        rBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rBody.MovePosition(hand.transform.position);
        rBody.MoveRotation(hand.transform.rotation);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("HIT!");
        Rigidbody otherR = other.gameObject.GetComponentInChildren<Rigidbody>();
        if (other == null)
            return;

        Vector3 avgPoint = Vector3.zero;
        foreach(ContactPoint p in other.contacts)
        {
            avgPoint += p.point;
        }

        avgPoint /= other.contacts.Length;

        Vector3 dir = (avgPoint - transform.position).normalized;
        otherR.AddForceAtPosition(dir * 10f * rBody.velocity.magnitude, avgPoint);
    }
}
