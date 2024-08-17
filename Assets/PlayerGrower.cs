using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerGrower : MonoBehaviour
{
    private PlayerMovement playerMovement;

    public float animSpeed;

    [System.Serializable]
    private class OffsetGameObject
    {
        public GameObject gameObject;
        [SerializeField] float scaleFactor;

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

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement.moved.AddListener(Grow);
        for (int i = 0; i < parts.Count; i++)
        {
            if (current < i)
            {
                parts[i].gameObject.SetActive(false);
            }
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

    void Grow(Vector3 _)
    {
        if (current < parts.Count - 1)
        {
            parts[++current].gameObject.SetActive(true);

            StartCoroutine(Animate());
            IEnumerator Animate()
            {
                float initialScale = parts[current].GetScaleOffset();
                for (float i = 0; i < 1; i += animSpeed * Time.deltaTime)
                {
                    parts[current].SetScale(Mathf.Lerp(0, initialScale, i));
                    yield return new WaitForEndOfFrame();
                }
            }
        }
    }
}
