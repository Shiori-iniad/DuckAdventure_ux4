using UnityEngine;

public class BearSE : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip stepSE;

    // Animation Event から呼ばれる
    public void PlaySE()
    {
        if (audioSource != null && stepSE != null)
        {
            audioSource.PlayOneShot(stepSE);
        }
    }
}
