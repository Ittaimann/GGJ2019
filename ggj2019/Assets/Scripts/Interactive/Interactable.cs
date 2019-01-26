using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A base class for providing behaviors for interactable objects.
/// </summary>
public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    protected Outline outline;

    void Awake()
    {
        // Nab outline on object, and whine if it doesn't exist.
        if (!outline)
            outline = GetComponent<Outline>();
        if (!outline)
            Debug.LogWarning(string.Format("Interactable object '{0}' has no outline", gameObject.name));
    }

    /// <summary>
    /// The function that gets called when the player interacts with the object.
    /// (Object can be validated using a Component check instead of clogging up layers list.)
    /// </summary>
    public abstract void Interact();
}
