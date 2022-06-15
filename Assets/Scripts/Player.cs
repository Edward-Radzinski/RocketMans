using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Player : MonoBehaviourPunCallbacks, IDamageable
{
    [SerializeField] private Text _healthText;
    [SerializeField] private Text _nickText;
    [SerializeField] private ParticleSystem _spawnEffect;


    private PhotonView _photonView;
    private Camera _mainCamera;
    private GameManager _gameManager;

    private float _health = 100;
    private int _score = 0;
    

    public float Health 
    {   
        get => _health; 
        set => _health = value; 
    }
    public int Score
    {
        get => _score;
        set => _score = value;
    }


    private void Awake()
    {
        _mainCamera = Camera.main;
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Start()
    {
        _gameManager.AddPlayer(this);
        _photonView = GetComponentInParent<PhotonView>();
        if (!_photonView.IsMine) return;
        _nickText.color = Color.white;

    }

    private void Update()
    {
        if (!_photonView.IsMine) return;
        _photonView.RPC("RPC_ShowUI", RpcTarget.All);
        transform.localRotation = Input.mousePosition.x > _mainCamera.WorldToScreenPoint(transform.position).x ? Quaternion.Euler(0, 0, 0) : Quaternion.Euler(0, -180, 0);
    }

    [PunRPC]
    private void RPC_ShowUI()
    {
        _healthText.text = Health.ToString();
        _healthText.transform.rotation = Quaternion.identity;

        _nickText.text = PhotonNetwork.NickName.ToString();
        _nickText.transform.rotation = Quaternion.identity;
    }

    [PunRPC]
    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            this.gameObject.SetActive(false);
            ResetValues();
        }
    }

    public void ResetValues()
    {
        Health = 100;
        Score++;
        transform.position = Vector3.zero;
        _photonView.RPC("RPC_SpwanEffect", RpcTarget.All);
        this.gameObject.SetActive(true);
    }

    [PunRPC]
    private void RPC_SpwanEffect()
    {
        _spawnEffect.Play();
    }
}
