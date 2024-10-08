using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerGrower : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float animSpeed;

    public int count;
    public int countAmount;

    public static UnityEvent death = new();

    [System.Serializable]
    private class OffsetGameObject
    {
        public GameObject gameObject;
        [SerializeField] float scaleFactor;

        public float startScale;

        public float GetScaleOffset()
        {
            return (gameObject.transform.localScale.y * scaleFactor);
        }

        public void SetScale(float newScale)
        {
            gameObject.transform.localScale = new Vector3(newScale, newScale, newScale) / scaleFactor;
        }
    }
    [SerializeField] List<OffsetGameObject> parts;
    public int current;

    public void ResetSnowman()
    {
        current = 0;
        parts[current].gameObject.SetActive(true);
        parts[current].SetScale(parts[current].startScale);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement.moved.AddListener(Grow);
        PlayerMovement.stoppedMoved.AddListener(Aaaaahhhh);
        UpdateNumbers();
    }

    public void UpdateNumbers()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            parts[i].startScale = parts[i].GetScaleOffset();
            parts[i].gameObject.SetActive(current >= i);
        }
    }

    private void Update()
    {
        for (int i = 0; i < parts.Count; i++)
        {
            if (parts[i].gameObject.activeInHierarchy)
            {
                Vector3 pos = parts[i].gameObject.transform.localPosition;
                pos.y = parts[i].GetScaleOffset() / 2;
                parts.Where(x => x.gameObject.activeInHierarchy && parts.IndexOf(x) > i).ToList().ForEach(x =>
                {
                    pos.y += x.GetScaleOffset();
                });

                parts[i].gameObject.transform.localPosition = pos;
            }
        }
    }

    void Aaaaahhhh()
    {
        RaycastHit hit;
        if (!Physics.Raycast(new Ray(transform.position + (Vector3.up), Vector3.down), out hit, 100, Layers.floorMask)) return;

        if (!GetComponent<PlayerMovement>().running && hit.transform.GetComponent<PlayeMaker>())
        {
            hit.transform.GetComponent<PlayeMaker>().Go(this);
        }
    }

    void Grow(Vector3 _)
    {
        Aaaaahhhh();
        bool animated = false;

        RaycastHit hit;
        if (!Physics.Raycast(new Ray(transform.position + (Vector3.up), Vector3.down), out hit, 100, Layers.floorMask))
        {
            return;
        }

        Debug.DrawRay(transform.position + Vector3.up, Vector3.down);
        float start;
        float end;
        if (hit.transform.CompareTag("Snow"))
        {
            if (current + 1 >= parts.Count) return;

            count++;
            if (count >= countAmount)
            {
                count = 0;

                parts[++current].gameObject.SetActive(true);
                start = 0;            
                end = parts[current].startScale;

                StartCoroutine(Animate());
            }
        }
        else if (hit.transform.CompareTag("Sand"))
        {
            if (current < 0) return;

            count--;
            if (count <= -countAmount)
            {
                count = 0;

                start = parts[current].GetScaleOffset();
                end = 0;
                StartCoroutine(Animate());
                StartCoroutine(Wait());

                IEnumerator Wait()
                {
                    yield return new WaitUntil(() => animated);
                    parts[current--].gameObject.SetActive(false);

                    if (current < 0) death.Invoke();
                }
            }
        }

        IEnumerator Animate()
        {
            for (float i = 0; i < 1; i += animSpeed * Time.deltaTime)
            {
                parts[current].SetScale(Mathf.Lerp(start, end, i));
                yield return new WaitForEndOfFrame();
            }
            animated = true;
        }
    }
}
