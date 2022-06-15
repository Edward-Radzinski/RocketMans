using UnityEngine;

public class Speen : MonoBehaviour
{
    [SerializeField] private float speed = 1f;

    private void Update ()
    {
        transform.Rotate (Vector3.forward, speed * Time.deltaTime * 100f);
    }
}