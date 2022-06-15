using UnityEngine;
using Photon.Pun;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rocketForce;

    private Rigidbody2D _rb;
    private PhotonView _photonView;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _photonView = GetComponent<PhotonView>();
    }

    private void FixedUpdate()
    {
        if (!_photonView.IsMine) return;
        _rb.velocity = new Vector2(Input.GetAxis("Horizontal") * _speed, _rb.velocity.y);
        
        if(Input.GetKey(KeyCode.Space))
        {
            _rb.AddForce(transform.up * _rocketForce);
        }
    }
}
