using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fixy : MonoBehaviour
{
    public List<Material> mats;

    [EditorCools.Button]
    void Fixit()
    {
        List<string> whats = mats.Select(x => x.ToString().Split("(")[0].Trim()).ToList();
        FindObjectsOfType<GameObject>().ToList().ForEach(g =>
        {
            if (g.TryGetComponent(out MeshRenderer m))
            {
                string what = m.sharedMaterial.ToString().Split("(")[0].Trim();

                if (!whats.Contains(what)) return;

                if (whats.IndexOf(what) <= 1)
                {
                    g.tag = "Sand";
                    g.layer = 3;
                }
                else if (whats.IndexOf(what) <= 3)
                {
                    g.tag = "Snow";
                    g.layer = 3;
                }
                else
                {
                    Debug.Log(g.name);
                    GameObject tmp = g.GetComponentsInParent<Transform>()[1].gameObject;
                    tmp.tag = "Plank";
                    tmp.layer = 3;
                }

            }
        });
    }
}
