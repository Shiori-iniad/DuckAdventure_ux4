using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Fall Game Over");

            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.SendMessage("Die");
                // または player.TakeDamage(999);
            }
        }
    }
}
