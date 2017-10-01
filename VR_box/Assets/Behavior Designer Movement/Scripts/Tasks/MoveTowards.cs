using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Movement
{
    [TaskDescription("Move towards the specified position. The position can either be specified by a transform or position. If the transform " +
                     "is used then the position will not be used.")]
    [TaskCategory("Movement")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=1")]
    [TaskIcon("Assets/Behavior Designer Movement/Editor/Icons/{SkinColor}MoveTowardsIcon.png")]
    public class MoveTowards : Action
    {
        [Tooltip("The speed of the agent")]
        public SharedFloat speed;
        [Tooltip("The agent has arrived when the magnitude is less than this value")]
        public SharedFloat arriveDistance = 0.1f;
        [Tooltip("Should the agent be looking at the target position?")]
        public SharedBool lookAtTarget = true;
        [Tooltip("Max rotation delta if lookAtTarget is enabled")]
        public SharedFloat maxLookAtRotationDelta;
        [Tooltip("The GameObject that the agent is moving towards")]
        public SharedGameObject target;
        [Tooltip("If target is null then use the target position")]
        public SharedVector3 targetPosition;
        Quaternion a, b;
        Vector3 a1, b1;
        private Animator anim;
        public float Velosity;
        public override TaskStatus OnUpdate()
        {
            anim = GetComponent<Animator>();
            var position = Target();
            // Return a task status of success once we've reached the target
            if (Vector3.Magnitude(transform.position - position) < arriveDistance.Value) {
                return TaskStatus.Success;
            }

            if ((Vector3.Magnitude(transform.position - position)- arriveDistance.Value) > 2)
            {
                Velosity = 2;
                speed = Velosity/2;
            }
            if ((Vector3.Magnitude(transform.position - position) - arriveDistance.Value) <= 2)
            {
                Velosity = (Vector3.Magnitude(transform.position - position) - arriveDistance.Value ) ;
                speed = Velosity/2;
            }
            if (Velosity < 0.1)
            {
                Velosity = 0;
                speed = 0;
            }

            a = transform.rotation;

            if (a == b)
            {
                transform.position = Vector3.MoveTowards(transform.position, position, speed.Value * Time.deltaTime);
                anim.SetFloat("VelX", Velosity);
            }

            b = a;
            // We haven't reached the target yet so keep moving towards it
            //
            if (lookAtTarget.Value) {
               transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(position - transform.position), maxLookAtRotationDelta.Value);
                Vector3 napr = transform.position - target.Value.transform.position;
                var distance = napr.magnitude;
                var direction = napr / distance; // This is now the normalized direction.
                Debug.Log("Direction");
                Debug.Log(direction);
                Debug.Log("Targer");
                Debug.Log(transform.forward);

                //anim.SetTrigger("WalkFor");
                //    Debug.Log("anim");
            }
            
            return TaskStatus.Running;
        }

        // Return targetPosition if targetTransform is null
        private Vector3 Target()
        {
            if (target == null || target.Value == null) {
                return targetPosition.Value;
            }
            return target.Value.transform.position;
        }

        // Reset the public variables
        public override void OnReset()
        {
            arriveDistance = 0.1f;
            lookAtTarget = true;
        }

        public override void OnEnd()
        {
            anim = GetComponent<Animator>();
     }

    }
}