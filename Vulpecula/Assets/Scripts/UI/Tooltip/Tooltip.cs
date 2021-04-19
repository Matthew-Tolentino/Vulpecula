using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class Tooltip : MonoBehaviour
{
    public TextMeshProUGUI headerField;
    public TextMeshProUGUI contentField;
    public LayoutElement LayoutElement;

    public int characterWrapLimit;

    public RectTransform rectTransform;

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetText(string content, string header = "") {
        if (string.IsNullOrEmpty(header)) {
            headerField.gameObject.SetActive(false);
        } else {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

        int headerLen = headerField.text.Length;
            int contentLen = contentField.text.Length;

            LayoutElement.enabled = (headerLen > characterWrapLimit || contentLen > characterWrapLimit) ? true : false;
    }

    private void Update() {
        if (Application.isEditor) {
            int headerLen = headerField.text.Length;
            int contentLen = contentField.text.Length;

            LayoutElement.enabled = (headerLen > characterWrapLimit || contentLen > characterWrapLimit) ? true : false;
        }

        Vector2 position = Input.mousePosition;

        float pivotX = position.x / Screen.width;
        float pivotY = position.y / Screen.height;

        rectTransform.pivot = new Vector2(pivotX, pivotY);
        transform.position = position;
    }
}
