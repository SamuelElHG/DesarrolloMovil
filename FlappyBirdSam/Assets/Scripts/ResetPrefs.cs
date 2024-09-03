using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPrefs : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save(); // Asegúrate de guardar los cambios
        Debug.Log("Todos los PlayerPrefs han sido eliminados.");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
