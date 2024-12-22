using UnityEngine;

public class Rotate : MonoBehaviour {
    [SerializeField] private float _speed = 100f;

    private void Update() {
        transform.RotateAround(transform.position, Vector3.up, _speed * Time.deltaTime);
    }
}