using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBehaviour : MonoBehaviour {
    [SerializeField]
    private Text myText;

    public void SetText(string text)
    {
        myText.text = text;
    }
}
