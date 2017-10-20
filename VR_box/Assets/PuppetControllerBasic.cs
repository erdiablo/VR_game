using UnityEngine;
using System.Collections;
using RootMotion.Dynamics;

/// <summary>
/// Adjusts PuppetMaster and BehaviourPuppet behaviour, dynamic pin and mapping weight control based on collisions with the hands and objects.
/// </summary>
public class PuppetControllerBasic : MonoBehaviour
{

    public PuppetMaster puppetMaster;
    public BehaviourPuppet puppet;
    public float drag = 2f;

    private Muscle[] muscles = new Muscle[0];
    private float[] normalPinWeights = new float[0];
    private float[] normalMuscleWeights = new float[0];
    private float dam = 0f;
    private float damTime = -100f;
    private float damV;
    private float map, mapV;
    private float dragW = 1f;

    void Start()
    {
        // Register to get a call from the puppet if it collides with anything
        puppet.OnCollisionImpulse += OnCollisionImpulse;
        puppet.OnPreMuscleHit += OnMuscleHit;
        puppet.OnHierarchyChanged += OnHierarchyChanged;

        puppet.masterProps.normalMode = BehaviourPuppet.NormalMode.Active;

        muscles = new Muscle[puppetMaster.muscles.Length];
        normalPinWeights = new float[puppetMaster.muscles.Length];
        normalMuscleWeights = new float[puppetMaster.muscles.Length];

        for (int i = 0; i < puppetMaster.muscles.Length; i++)
        {
            muscles[i] = puppetMaster.muscles[i];
            normalPinWeights[i] = puppetMaster.muscles[i].props.pinWeight;
            normalMuscleWeights[i] = puppetMaster.muscles[i].props.muscleWeight;
        }
    }

    void OnHierarchyChanged()
    {

    }

    void Update()
    {
        // Dynamically adjust drag, pin and mapping weights based on collisions so we can have perfect animation until there is a collision
        dragW = Mathf.MoveTowards(dragW, 1f, Time.deltaTime * 2f);

        bool unpinned = !puppet.enabled || puppet.state == BehaviourPuppet.State.Unpinned || !puppetMaster.isAlive || puppetMaster.isKilling;

        float damTarget = Time.time > damTime + 0.5f ? 0f : 1f;
        if (unpinned) damTarget = 1f;

        float mapTarget = damTarget;
        if (unpinned) mapTarget = 1f;

        float sDampTime = damTarget > dam ? 0.05f : (puppet.enabled ? 0.5f : 0.05f);
        dam = Mathf.SmoothDamp(dam, damTarget, ref damV, sDampTime);

        float mDampTime = mapTarget > map ? 0.05f : 0.5f;
        map = Mathf.SmoothDamp(map, mapTarget, ref mapV, mDampTime);

        if (unpinned) dam = Mathf.Min(dam, map);

        float d = unpinned ? 0f : drag * dragW;
        float angularD = unpinned ? 0.005f : drag * dragW;
        float m = Mathf.Lerp(0f, 1f, map);

        for (int i = 0; i < puppetMaster.muscles.Length; i++)
        {
             puppet.puppetMaster.muscles[i].props.pinWeight = Mathf.Lerp(1f, normalPinWeights[i], dam);
             puppet.puppetMaster.muscles[i].props.muscleWeight = Mathf.Lerp(1f, normalMuscleWeights[i], dam);

            puppet.puppetMaster.muscles[i].rigidbody.drag = d;
            puppet.puppetMaster.muscles[i].rigidbody.angularDrag = angularD;

            puppet.puppetMaster.muscles[i].state.mappingWeightMlp = m;
        }
        puppet.puppetMaster.muscles[7].rigidbody.drag = 5000;
        puppet.puppetMaster.muscles[7].rigidbody.angularDrag = 5000;
    }

    // Called by BehaviourPuppet when it collides with something
    void OnCollisionImpulse(MuscleCollision c, float impulse)
    {
     
        if (c.muscleIndex == 8)
         {
            Debug.Log(impulse);
            float[] newNormalPinWeights = new float[puppetMaster.muscles.Length];
            float[] newNormalMuscleWeights = new float[puppetMaster.muscles.Length];



            for (int i = 0; i < puppetMaster.muscles.Length; i++)
            {
                newNormalPinWeights[i] = -1f;
                newNormalMuscleWeights[i] = -1f;
            }

            for (int i = 0; i < puppetMaster.muscles.Length; i++)
            {
                for (int m = 0; m < muscles.Length; m++)
                {
                    if (puppetMaster.muscles[i] == muscles[m])
                    {
                        newNormalPinWeights[i] = normalPinWeights[m];
                        newNormalMuscleWeights[i] = normalMuscleWeights[m];
                    }
                }
            }

            for (int i = 0; i < puppetMaster.muscles.Length; i++)
            {
                if (newNormalPinWeights[i] == -1f) newNormalPinWeights[i] = puppetMaster.muscles[i].props.pinWeight;
                if (newNormalMuscleWeights[i] == -1f) newNormalMuscleWeights[i] = puppetMaster.muscles[i].props.muscleWeight;
            } 

            newNormalPinWeights[0] = 1f;
            newNormalMuscleWeights[0] = 1f;

            puppetMaster.muscles[0].props.pinWeight = newNormalPinWeights[0];
            puppetMaster.muscles[0].props.muscleWeight = newNormalMuscleWeights[0];

            newNormalPinWeights[7] = 0f;
            newNormalMuscleWeights[7] = 1f;

            newNormalPinWeights[8] = 0f;
            newNormalMuscleWeights[8] = 0.03f;

            puppetMaster.muscles[7].props.pinWeight = newNormalPinWeights[7];
            puppetMaster.muscles[7].props.muscleWeight = newNormalMuscleWeights[7];

            puppetMaster.muscles[8].props.pinWeight = newNormalPinWeights[7];
            puppetMaster.muscles[8].props.muscleWeight = newNormalMuscleWeights[7];

            muscles = new Muscle[puppetMaster.muscles.Length];
            muscles[7] = puppetMaster.muscles[7];
            muscles[8] = puppetMaster.muscles[8];
            muscles[9] = puppetMaster.muscles[9];
            muscles[10] = puppetMaster.muscles[10];
            muscles[11] = puppetMaster.muscles[11];
            muscles[12] = puppetMaster.muscles[12];

            normalPinWeights = newNormalPinWeights;
            normalMuscleWeights = newNormalMuscleWeights;
            damTime = Time.time;
            Debug.Log("HIT");
        }

        if ((c.muscleIndex == 7)&& (impulse > 10)&&(c.collision.rigidbody.velocity.magnitude>3))
        {
            Debug.Log(c.collision.rigidbody.velocity.magnitude);
            Debug.Log(impulse);
            float[] newNormalPinWeights = new float[puppetMaster.muscles.Length];
            float[] newNormalMuscleWeights = new float[puppetMaster.muscles.Length];

            newNormalPinWeights[7] = 0.9f;
            newNormalMuscleWeights[7] = 1f;
            newNormalPinWeights[7] = 1f;
            newNormalMuscleWeights[7] = 1f;
            newNormalPinWeights[0] = 1f;
            newNormalMuscleWeights[0] = 1f;

            for (int i = 0; i < puppetMaster.muscles.Length; i++)
            {
                newNormalPinWeights[i] = -1f;
                newNormalMuscleWeights[i] = -1f;
            }

            for (int i = 0; i < puppetMaster.muscles.Length; i++)
            {
                for (int m = 0; m < muscles.Length; m++)
                {
                    if (puppetMaster.muscles[i] == muscles[m])
                    {
                        newNormalPinWeights[i] = normalPinWeights[m];
                        newNormalMuscleWeights[i] = normalMuscleWeights[m];
                    }
                }
            }

            for (int i = 0; i < puppetMaster.muscles.Length; i++)
            {
                if (newNormalPinWeights[i] == -1f) newNormalPinWeights[i] = puppetMaster.muscles[i].props.pinWeight;
                if (newNormalMuscleWeights[i] == -1f) newNormalMuscleWeights[i] = puppetMaster.muscles[i].props.muscleWeight;
            }



            muscles = new Muscle[puppetMaster.muscles.Length];
            muscles[7] = puppetMaster.muscles[7];
            muscles[9] = puppetMaster.muscles[9];
            muscles[10] = puppetMaster.muscles[10];
            muscles[11] = puppetMaster.muscles[11];
            muscles[12] = puppetMaster.muscles[12];

            normalPinWeights = newNormalPinWeights;
            normalMuscleWeights = newNormalMuscleWeights;
            damTime = Time.time;
            Debug.Log("HIT");
        }


    }

    // Called by BehaviourPuppet when it's muscles are hit via MuscleCollisionBroadCaster.Hit()
    void OnMuscleHit(MuscleHit hit)
    {
        Debug.Log("ARA");
        damTime = Time.time;
        dragW = 0f;
    }
}