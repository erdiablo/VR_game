using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Fight
{
    [TaskDescription("Fight.")]
    [TaskCategory("Fight")]
    [HelpURL("http://www.opsive.com/assets/BehaviorDesigner/Movement/documentation.php?id=1")]


    public class  Turn: Action
    {

    public SharedGameObject target;
    private Animator anim;
    private Vector3 a, naprrr;

    public override TaskStatus OnUpdate()
            {
            a = new Vector3(0, 1, 0);
            anim = GetComponent<Animator>();
            var position = target.Value.transform.position;
            Vector3 proec_target = Vector3.ProjectOnPlane(transform.position, a);
            Vector3 proec_object = Vector3.ProjectOnPlane(target.Value.transform.position, a);
            Vector3 forvard = Vector3.ProjectOnPlane(transform.forward, a);
            Vector3 napr = proec_object - proec_target;
            var distance = napr.magnitude;
            var direction = napr / distance;
            if ((direction - forvard).magnitude < 0.3)
            {
                return TaskStatus.Success;
            }
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(position - transform.position), 1);
            Debug.Log(direction);
            Debug.Log((direction-forvard).magnitude);

            return TaskStatus.Running;

            }

    }
}