using System.Collections;
using UnityEngine;

public class BuffWeapon : Weapon
{
    [Header("Fire sate settings")]
    [SerializeField, Tooltip("Default fire rate of this weapon")] private float _maxFireRate;
    [SerializeField, Tooltip("Fire rate after buff")] private float _minFireRate;

    [SerializeField] private float _buffDuration;
    [SerializeField] private KeyCode _buffKey;

    private void Start()
    {
        //_fireRate = _maxFireRate;
    }

    private void Update()
    {
        if (!_photonView.IsMine) return;
        if (Input.GetKeyDown(_buffKey)) FireRateBuff();
    }

    private void FireRateBuff()
    {
        _fireRate = _minFireRate;
        StopAllCoroutines();
        StartCoroutine("FireRateCoroutine");
    }

    private IEnumerator FireRateCoroutine()
    {
        _fireRate = _maxFireRate;
        yield return new WaitForSeconds(_buffDuration);
    }
}
