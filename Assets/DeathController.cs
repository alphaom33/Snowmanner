using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathController : MonoBehaviour
{
    PlayerGrower playerGrower;

    public float speed;
    public ParticleSystem particles;
    public Transform startPos;

    // Start is called before the first frame update
    void Start()
    {
        PlayerGrower.death.AddListener(Die); 
        playerGrower = GetComponent<PlayerGrower>();
    }

    void Die()
    {
        StartCoroutine(Die());
        IEnumerator Die()
        {
            particles.Play();
            Vector3 start = transform.position;
            for (float i = 0; i < 1; i += speed * Time.deltaTime)
            {
                transform.position = Utils.VecEaseOutInSine(start, startPos.position, i);
                yield return new WaitForEndOfFrame();
            }

            playerGrower.ResetSnowman();
        }
    }

}
