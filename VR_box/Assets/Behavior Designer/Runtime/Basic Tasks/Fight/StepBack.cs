using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Fight
{
    [TaskDescription("Fight.")]
    [TaskCategory("Fight")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=1")]


    public class StepBack : Action
    {
        [Tooltip("The speed of the agent")]
        public SharedFloat speed;
        [Tooltip("The agent has arrived when the magnitude is less than this value")]
        public SharedGameObject target;
        [Tooltip("If target is null then use the target position")]
        private Animator anim;
        public float Velosity;
        private Vector3 a;

        public override TaskStatus OnUpdate()
        {
            a = new Vector3(0, 1, 0);
            anim = GetComponent<Animator>();
            Vector3 proec_target = Vector3.ProjectOnPlane(transform.position, a);
            Vector3 proec_object = Vector3.ProjectOnPlane(target.Value.transform.position, a);
            Vector3 napr = proec_target - proec_object;
            var distance = napr.magnitude;
            var direction = napr / distance;

            if (distance>1)
                return TaskStatus.Success;
            Velosity = (Vector3.Magnitude(proec_target - proec_object) - 3/2)*2;
            speed = Velosity/2;
            transform.position = Vector3.MoveTowards(transform.position, target.Value.transform.position, speed.Value * Time.deltaTime);
            
            anim.SetFloat("VelX", Velosity);
            return TaskStatus.Running;

        }
    }

}