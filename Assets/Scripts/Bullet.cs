using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private PhotonView _photonView;

    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }

    private void Update()
    {
        Invoke("Destroy", 3);
        transform.Translate(Vector2.right * _speed * Time.deltaTime);
    }

    [PunRPC]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Player>()?.GetComponent<PhotonView>()?.RPC("TakeDamage", RpcTarget.All, _damage);
        _photonView.RPC("Destroy", RpcTarget.All);
    }

    [PunRPC]
    private void Destroy()
    {
        if (_photonView.IsMine && PhotonNetwork.IsConnected)
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
    }


}
