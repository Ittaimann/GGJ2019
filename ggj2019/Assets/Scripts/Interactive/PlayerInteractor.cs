using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private Interactable target;
    private Pickup heldObject;
    private Rigidbody rigid;
    [SerializeField]
    protected Transform heldParent;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponentInParent<Rigidbody>();
    }

    public void UpdateHeldObject()
    {
        if (heldObject != null)
        {
            heldObject.transform.position = heldParent.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, transform.forward, out hitInfo, 3f, LayerMask.GetMask("Interactable"));
        if (hitInfo.transform == null)
        {
            //no hit
            target?.SetLookedAt(false);
            target = null;
        }
        else
        {
            Interactable hitTarget = hitInfo.transform.GetComponentInParent<Interactable>();
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
        heldObject.OnDrop();
        Rigidbody heldRigid = heldObject.GetComponentInParent<Rigidbody>();
        heldRigid.constraints = RigidbodyConstraints.None;
        heldObject = null;
    }

    public void SetHeldObject(Pickup held, bool dropItem)
    {
        if(dropItem)
            Drop();
        heldObject = held;
        Rigidbody heldRigid = held.GetComponentInParent<Rigidbody>();
        heldRigid.constraints = RigidbodyConstraints.FreezeAll;

        heldRigid.MoveRotation(Quaternion.identity);

        heldObject.OnPickup();
    }
}
