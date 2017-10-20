using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Dynamics;

public class PunchHand1 : MonoBehaviour
{

    public SteamVR_TrackedObject hand;
    private Rigidbody rBody;
    public GameObject bone;
    public Muscle head;
    public Collider left_head;
    public Collider right_head;
    public Collider front_head;
    public Animator anim;

    public float unpin = 500f;
    public float force = 1000f;
    private Vector3 point;
    private Vector3 dir;

    // Use this for initialization
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rBody.transform.position=hand.transform.position;
        // rBody.transform.rotation = hand.transform.rotation;
        rBody.MovePosition(hand.transform.position);
        rBody.MoveRotation(hand.transform.rotation);
       
       
    }

    void OnCollisionEnter(Collision other)
    {

        //Debug.Log("collision");
       // Debug.Log(rBody.velocity.magnitude);
        // var broadcaster = other.collider.attachedRigidbody.GetComponent<MuscleCollisionBroadcaster>();
        //Debug.Log(broadcaster);
        Rigidbody otherR = other.gameObject.GetComponentInChildren<Rigidbody>();
         if (other == null)
             return;

         Vector3 avgPoint = Vector3.zero;
         foreach (ContactPoint p in other.contacts)
         {
             avgPoint += p.point;
         }

         avgPoint /= other.contacts.Length;
         point = avgPoint;
         dir = (avgPoint - transform.position).normalized;
          // if ((other.impulse.magnitude > 3) & (other.relativeVelocity.magnitude > 3) & (rBody.velocity.magnitude >  3))
          // {
          //  Debug.Log("Collision");
          //  Debug.Log("impulse" + other.impulse.magnitude);
          // Debug.Log("relativeVelocity" + other.relativeVelocity.magnitude);
          // Debug.Log("transform" + other.transform);
          //  Debug.Log("rBody Vel" + rBody.velocity.magnitude);
          // broadcaster.Hit(unpin, dir * 8000f* rBody.velocity.magnitude, avgPoint);
          //  rBody.GetComponentInChildren<Collider>().isTrigger = true;
          // }
       
         // Debug.Log(rBody.GetComponentInChildren<Collider>().isTrigger);
         
          //otherR.AddForceAtPosition(dir * 10000f, avgPoint);

        if ((other.collider == right_head) && (anim.GetBool("GetHitLeft") == false) && (anim.GetBool("GetHitRight") == false) && (anim.GetBool("GetHitFront") == false))
         {
             //anim.SetTrigger("GetHitLeft");

             //Debug.Log(other.collider);
         }

         if ((other.collider == left_head) && (anim.GetBool("GetHitLeft") == false) && (anim.GetBool("GetHitRight") == false) && (anim.GetBool("GetHitFront") == false))
         {
             //  anim.SetTrigger("GetHitRight");
             // Debug.Log(other.collider);
         }

         if ((other.collider == front_head) && (anim.GetBool("GetHitLeft") == false) && (anim.GetBool("GetHitRight") == false) && (anim.GetBool("GetHitFront") == false))
         {
             //  anim.SetTrigger("GetHitFront");
             //  Debug.Log(other.collider);
         }

    }

    void OnTriggerExit(Collider other)
    {
       //rBody.GetComponentInChildren<Collider>().isTrigger = false;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("rigger");
    }
}