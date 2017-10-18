
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class Movement : MonoBehaviour {

    public GameObject contoler_left;
    public GameObject contoler_right;
    public SteamVR_TrackedObject right_hand_controller;
    public SteamVR_TrackedObject left_hand_controller;
    private SteamVR_TrackedController contoler_left_script;
    private SteamVR_TrackedController contoler_right_script;
    float x1, y1, z1, x2, y2, z2;
    Vector3 move_vec, pos1_left, pos1_right, pos2_left, pos2_right;

    bool flag_left = false;
    bool flag_right = false;
    bool check_right_hand = false;
    // Use this for initialization
    void Start () {

        
    }
	
	// Update is called once per frame
	void Update () {

        contoler_left_script = contoler_left.GetComponent<SteamVR_TrackedController>();
        contoler_right_script = contoler_right.GetComponent<SteamVR_TrackedController>();

        bool check_left = contoler_left_script.padPressed;
        bool check_right = contoler_right_script.padPressed;


        if ((check_left == true) & (check_right == false))
        {
            //Debug.Log("true1");
            if (flag_left != false) 
            {
                pos2_left = contoler_left.transform.localPosition;
                move_vec = pos2_left - pos1_left;
                transform.Translate(-move_vec.x*2, 0, -move_vec.z * 2);
            }
            pos1_left = contoler_left.transform.localPosition;
            flag_left = true;
        }



        if ((check_left == false) & (check_right == true))
        {
           // Debug.Log("true2");
            if (flag_right != false)
            {
                pos2_right = contoler_right.transform.localPosition;
                move_vec = pos2_right - pos1_right;
                transform.Translate(-move_vec.x*2, 0, -move_vec.z*2);
            }
            pos1_right = contoler_right.transform.localPosition;
            flag_right = true;
        }

        if (check_left == false)
            flag_left = false;
        if (check_right == false)
            flag_right = false;
       

    }
}
