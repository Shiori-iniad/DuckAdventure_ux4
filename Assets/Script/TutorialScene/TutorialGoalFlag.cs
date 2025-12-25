using UnityEngine;

public class TutorialGoalFlag : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;

        if (collision.CompareTag("Player"))
        {
            triggered = true;

            TutorialManager.Instance.StartDamageTalk();
        }
    }
}

