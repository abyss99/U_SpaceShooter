using UnityEngine;
using System.Collections;

public class MyGizmos : MonoBehaviour {

    public Color _color = Color.yellow;
    public float _radius = 0.3f;

    void OnDrawGizmos() {
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _radius);
    }

}
