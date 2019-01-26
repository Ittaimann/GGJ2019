using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private Interactable target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitInfo;
        Physics.Raycast(transform.position, transform.forward, out hitInfo, float.MaxValue, LayerMask.GetMask("Interactable"));
        if(hitInfo.transform == null)
        {
            //no hit
            target?.SetLookedAt(false);
            target = null;

            return;
        }
        Interactable hitTarget = hitInfo.transform.GetComponent<Interactable>();
        if(hitTarget == null)
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
        if(Input.GetMouseButtonDown(0))
        {
            target.Interact();
        }
    }
}
