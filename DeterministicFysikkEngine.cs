using UnityEngine;
using System.Collections.Generic;

public class DeterministicPhysicsEngine : MonoBehaviour
{
    // Tidsskrittet for fysikken (16 = 1000/60)
    private int tidsskritt = 16;

    // Den nåværende tiden
    private int tid = 0;

    // Liste over objekter som skal kjøres fysikken på
    private List<GameObject> objekter = new List<GameObject>();

    // Tidsskritt-teller
    private int tidsskrittTeller = 0;

    void Start()
    {
        GameObject mittObjekt = GameObject.Find("Slime");
        LeggTilObjekt(mittObjekt);
    }

    void Update()
    {
        // Kjør fysikken med fast tidsskritt
        tidsskrittTeller++;
        if (tidsskrittTeller >= tidsskritt)
        {
            // Kjør fysikken på alle objekter
            foreach (GameObject obj in objekter)
            {
                DeterministicCollider collider = obj.GetComponent<DeterministicCollider>();
                if (collider != null)
                {
                    collider.SetVelocity(0, -1); // Tyngdekraft
                }
            }
            tidsskrittTeller = 0;
        }
    }

    // Legg til et objekt til fysikken
    public void LeggTilObjekt(GameObject obj)
    {
        objekter.Add(obj);
    }
}