using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class TouchEvent : MonoBehaviour {
    public string eventName;
    public GameObject holder;
    public delegate void OnEventDelegate();
    public OnEventDelegate onEvent;
}
