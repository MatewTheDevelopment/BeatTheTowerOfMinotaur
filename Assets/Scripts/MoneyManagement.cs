using UnityEngine;

public class MoneyManagement : MonoBehaviour
{
    [SerializeField] private Animation moneyAnimation;

    [SerializeField] private AnimationClip getAnimationClip;

    [SerializeField] private AudioSource moneyAudioSource;

    public void Execute()
    {
        moneyAnimation.Play(getAnimationClip.name);

        moneyAudioSource.Play();

        float currentTime = getAnimationClip.length;

        Destroy(gameObject, currentTime);
    }
}
