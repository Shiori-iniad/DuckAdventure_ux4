using UnityEngine;

public class TutorialGoalFlag : MonoBehaviour
{
    private bool triggered = false;

    [Header("SE")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip triggerSE;



    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;

        if (collision.CompareTag("Player"))
        {
            triggered = true;

            if (audioSource != null && triggerSE != null)
            {
                audioSource.PlayOneShot(triggerSE);
            }

            TutorialManager.Instance.StartDamageTalk();
        }
    }
}

