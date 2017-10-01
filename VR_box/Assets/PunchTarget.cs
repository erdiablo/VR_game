using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.FinalIK;

public class PunchTarget : MonoBehaviour{

    public AimIK aimIK; // Reference to the AimIK component
    public Transform pin; // The hitting point as in the animation
    public SteamVR_TrackedObject head;
    private Rigidbody rBody;
    private bool readynow = true;
    int counter_frame = 0;
    List<Vector3> PlayerPoseHead;
    // Use this for initialization
    void Start()
    {
        PlayerPoseHead = new List<Vector3>();
        rBody = GetComponent<Rigidbody>();

    }

    void Update()
    {
       
        rBody.MovePosition(head.transform.position);
        rBody.MoveRotation(head.transform.rotation);
        PlayerPoseHead.Add(transform.position);

 
       
    }

    void LateUpdate()
    {
        if (counter_frame < 60) {
            aimIK.solver.transform.LookAt(pin.position);
            aimIK.solver.IKPosition = PlayerPoseHead[counter_frame];
        }
        else
        {
            aimIK.solver.transform.LookAt(pin.position);
            aimIK.solver.IKPosition = PlayerPoseHead[counter_frame-20];
        }
        counter_frame += 1;

    }


}