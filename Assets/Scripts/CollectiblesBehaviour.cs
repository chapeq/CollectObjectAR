using System.Collections;
using UnityEngine;

public class CollectiblesBehaviour : MonoBehaviour
{
    [SerializeField]
    private int scorePoint = 1;
    [SerializeField]
    private AudioClip bloopSound;

    private Animator anim;
    private AudioSource source;
    private bool triggered = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            ScoreManager.Instance.AddScore(scorePoint);
            StartCoroutine(PlayAnim(other.gameObject));
        }
    }

    IEnumerator PlayAnim(GameObject player)
    {
        player.GetComponent<PlayerController>().AnimCollect();
        yield return new WaitForSeconds(0.5f);
        source.PlayOneShot(bloopSound);
        anim.SetTrigger("anim");
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}
