using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class TouchEvent : MonoBehaviour {
    public string eventName;
    public GameObject holder;
    public System.Action<Vector3> onTouchDown;
    public System.Action<Vector3> onTouchUp;
    public System.Action<Vector3> onTouchDrag;
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("TouchEvent");
    }
}
