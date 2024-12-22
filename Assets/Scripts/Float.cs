using UnityEngine;

public class FloatUpDown : MonoBehaviour {
    [SerializeField]
    [Header("Floating Parameters")]
    [Tooltip("Maximum offset from the starting position in the Y-axis.")]
    private float _maxOffset = 1f;

    [SerializeField] [Tooltip("Speed of the floating motion.")]
    private float _speed = 2f;

    private Vector3 _startPosition;

    void Start() {
        _startPosition = transform.position;
    }

    void Update() {
        float newY = _startPosition.y + Mathf.Sin(Time.time * _speed) * _maxOffset;

        transform.position = new Vector3(_startPosition.x, newY, _startPosition.z);
    }
}