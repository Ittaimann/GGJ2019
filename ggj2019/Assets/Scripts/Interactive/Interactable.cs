using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A base class for providing behaviors for interactable objects. Actual interactables should probably be children of this.
/// </summary>
public class Interactable : MonoBehaviour
{
    [SerializeField]
    protected Outline outline;

    [SerializeField]
    protected float outlineFadeDuration = 0.125f;

    private float currentOutlineStrengthLerp = 0f;

    private bool outlined = false;

    public bool interactable = true;

    void Awake()
    {
        // Nab outline on object, and whine if it doesn't exist.
        if (!outline)
            outline = GetComponent<Outline>();
        if (!outline)
            Debug.LogWarning(string.Format("Interactable object '{0}' has no outline", gameObject.name));
    }

    public void SetLookedAt(bool lookedAt)
    {
        outlined = lookedAt && interactable;
    }

    void Update()
    {
        //inefficient in running every update, but game jam
        currentOutlineStrengthLerp = Mathf.MoveTowards(currentOutlineStrengthLerp, outlined ? 1 : 0, Time.deltaTime / outlineFadeDuration);
        outline.OutlineColor = new Color(outline.OutlineColor.r, outline.OutlineColor.g, outline.OutlineColor.b, currentOutlineStrengthLerp);
    }

    /// <summary>
    /// The function that gets called when the player interacts with the object.
    /// (Object can be validated using a Component check instead of clogging up layers list.)
    /// </summary>
    public virtual void Interact(Pickup heldObject, PlayerInteractor player)
    {
    }

    /// <summary>
    /// Initialization called and controlled by game manager
    /// </summary>
    public virtual void StartDay()
    {

    }
}
