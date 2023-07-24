using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PuntuationText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = ("Score: "+PlayerPrefs.GetInt("Score").ToString());
    }
}
