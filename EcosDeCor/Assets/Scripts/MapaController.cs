using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapaController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool healthMana;
    public float healthManaValue;
    public bool healthHealt;
    public float healthHealtValue;
    public float memoriesColected;
    public TextMeshProUGUI memoriesCristalColected;
    public 
    void Start()
    {
        healthMana = false;
        memoriesColected = 0;
    }

    // Update is called once per frame
    void Update()
    {
        memoriesCristalColected.text = memoriesColected.ToString();
    }
}
