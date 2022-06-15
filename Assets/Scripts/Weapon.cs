using UnityEngine;
using Photon.Pun;

public class Weapon : MonoBehaviour
{
    [Header("Bullet settings")]
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private ParticleSystem _shotParticle;

    protected PhotonView _photonView;
    protected float _fireRate;
    private float _nextShot = 0;


    private void Start()
    {
        _photonView = GetComponentInParent<PhotonView>();
    }

    private void Update()
    {
        //if (!_photonView.IsMine) return;

        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);

        if (Input.GetButton("Fire1") && Time.time > _nextShot)
        {
            Shot();
        }
    }

    [PunRPC]
    private void RPC_ShotEffect()
    {
        _shotParticle.Play();
    }

    private void Shot()
    {
        //_photonView.RPC("RPC_ShotEffect", RpcTarget.All);
        _nextShot = Time.time + _fireRate;
        PhotonNetwork.Instantiate(_bullet.name, _spawnPoint.position, _spawnPoint.rotation);
    }

}
