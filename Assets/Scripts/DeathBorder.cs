using UnityEngine;

public class DeathBorder : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Player>()?.ResetValues();
    }
}
