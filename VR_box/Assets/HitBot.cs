using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RootMotion.Dynamics;

public class HitBot : MonoBehaviour {

    public GameObject bot;
    public PuppetMaster puppet;
    public HumanBodyBones head;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == bot)
        {
            Debug.Log("HITEM_muscle");
            puppet.SetMuscleWeights(head, 0.02f, 0f, 1f, 1f);
        }
    }
}
