using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private Interactable target;
    private Pickup heldObject;
    private HingeJoint joint;
    private Rigidbody rigid;
    [SerializeField]
    protected Transform heldParent;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponentInParent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, transform.forward, out hitInfo, float.MaxValue, LayerMask.GetMask("Interactable"));
        if (hitInfo.transform == null)
        {
            //no hit
            target?.SetLookedAt(false);
            target = null;
        }
        else
        {
            Interactable hitTarget = hitInfo.transform.GetComponent<Interactable>();
            if (hitTarget == null)
            {
                Debug.LogError("Target missing interactable");
                return;
            }
            if (hitTarget != target)
            {
                target?.SetLookedAt(false);
                hitTarget.SetLookedAt(true);
                target = hitTarget;
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            if(target != null) {
                target.Interact(heldObject, this);
            } else if(heldObject != null)
            {
                Drop();
            }
        }
    }

    public void Drop()
    {
        if(heldObject == null)
        {
            return;
        }
        Destroy(joint);
        heldObject.OnDrop();
        heldObject = null;
    }

    public void SetHeldObject(Pickup held)
    {
        Drop();
        heldObject = held;
        joint = rigid.transform.AddComponent<HingeJoint>();
        joint.connectedBody = held.GetComponentInParent<Rigidbody>();
        joint.autoConfigureConnectedAnchor = false;
        joint.anchor = heldParent.localPosition;
        joint.connectedAnchor = Vector3.zero;
        joint.enableCollision = false;
        joint.useSpring = true;
        heldObject.OnPickup();
    }
}
