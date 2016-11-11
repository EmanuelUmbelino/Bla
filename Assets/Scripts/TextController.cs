using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextController : MonoBehaviour {

    [SerializeField]
    private GameObject typing;
    [SerializeField]
    private GameObject[] textBoxes;
    [SerializeField]
    private GameObject textBox;
    [SerializeField]
    private string[] texts;

    private int actual = -1;

    void Start()
    {
        actual = -1;
    }
    void Update()
    {
        if(Input.anyKeyDown)
        {
            SpawnText();
        }
    }

    void SpawnText()
    {
        actual++;
        textBoxes[actual] = (GameObject)Instantiate(textBox);
        textBoxes[actual].transform.SetParent(this.transform);
        textBoxes[actual].transform.position = textBox.transform.position;
        textBoxes[actual].transform.localScale = textBox.transform.localScale;
        textBoxes[actual].GetComponent<RectTransform>().offsetMin = textBox.GetComponent<RectTransform>().offsetMin;
        textBoxes[actual].GetComponent<RectTransform>().offsetMax = textBox.GetComponent<RectTransform>().offsetMax;
        textBoxes[actual].SetActive(true);
        textBoxes[actual].GetComponent<TextBehaviour>().SetText(texts[actual]);
        Invoke("AdjustRectTransform",0.02f);
    }
    void SetPos()
    {
        for (int i = 0; i < actual; i++)
        {
            textBoxes[i].transform.position += Vector3.up * (textBoxes[actual].GetComponent<RectTransform>().rect.size.y);
        }
    }
    void AdjustRectTransform()
    {
        GameObject gameObject = textBoxes[actual];
        RectTransform transform = gameObject.GetComponent<RectTransform>();
        if (transform == null || transform.parent == null)
        {
            return;
        }

        Bounds parentBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(transform.parent);

        Vector2 parentSize = new Vector2(parentBounds.size.x, parentBounds.size.y);
        // convert anchor ration in to pixel position
        Vector2 posMin = new Vector2(parentSize.x * transform.anchorMin.x, parentSize.y * transform.anchorMin.y);
        Vector2 posMax = new Vector2(parentSize.x * transform.anchorMax.x, parentSize.y * transform.anchorMax.y);

        // add offset
        posMin = posMin + transform.offsetMin;
        posMax = posMax + transform.offsetMax;

        // convert from pixel position to anchor ratio again
        posMin = new Vector2(posMin.x / parentBounds.size.x, posMin.y / parentBounds.size.y);
        posMax = new Vector2(posMax.x / parentBounds.size.x, posMax.y / parentBounds.size.y);

        transform.anchorMin = posMin;
        transform.anchorMax = posMax;

        transform.offsetMin = Vector2.zero;
        transform.offsetMax = Vector2.zero;
        Invoke("SetPos", 0.02f);
    }   
}