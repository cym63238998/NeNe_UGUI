using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window : MonoBehaviour
{
    public string message;

    private void Start()
    {
        transform.GetChild(0).GetComponent<Text>().text = message;
        Destroy(gameObject, 1.5f);
    }
}
