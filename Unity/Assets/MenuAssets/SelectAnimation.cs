
using UnityEngine;

public class SelectAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 selectionposition;
    private Vector3 initialposition;
    private void Awake()
    {
        initialposition = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, selectionposition, 0.1f);

    }

    private void OnDisable()
    {
        transform.position = initialposition;
    }
}
