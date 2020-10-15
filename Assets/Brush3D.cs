using UnityEngine;
using VRTK;

public class Brush3D : MonoBehaviour
{
    public VRTK_InteractableObject linkedObject;
    public float spinSpeed = 360f;
    public TrailRenderer trailTemplate;

    private Transform brushTip;
    private bool spinning;
    private TrailRenderer currentTrail;
    
    protected virtual void OnEnable()
    {
        spinning = false;
        linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed += InteractableObjectUsed;
            linkedObject.InteractableObjectUnused += InteractableObjectUnused;
        }

        brushTip = transform.Find("BrushHead");
    }

    protected virtual void OnDisable()
    {
        if (linkedObject != null)
        {
            linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
            linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
        }
    }

    protected virtual void Update()
    {
        if (spinning)
        {
            var tf = brushTip.transform;
            tf.Rotate(new Vector3(0f, 0f, spinSpeed * Time.deltaTime));

        }
    }

    protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        spinning = true;
        currentTrail = Instantiate(trailTemplate, gameObject.transform, true);
        currentTrail.emitting = true;
    }

    protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
    {
        spinning = false;

        if (currentTrail)
        {
            currentTrail.transform.parent = null;
            currentTrail = null;
        }
    }
}
