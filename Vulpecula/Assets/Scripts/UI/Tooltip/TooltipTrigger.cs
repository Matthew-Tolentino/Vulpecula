using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour //, IPointerEnterHandler, IPointerExitHandler
{
    public string header;
    public string content;

    bool hide;

    // public void OnPointerEnter(PointerEventData eventData) {
    //     TooltipSystem.Show(content, header);
    // }

    // public void OnPointerExit(PointerEventData eventData) {
    //     TooltipSystem.Hide();
    // }

    void Update()
    {
        if (InputManager.instance.KeyDown("SpiritInc") || InputManager.instance.KeyDown("SpiritDec"))
        {
            Invoke("ShowToolTip", .1f);

            // Restart coroutine if button pressed before finishing
            if (hide) StopCoroutine("HideToolTip");

            hide = true;
            StartCoroutine("HideToolTip");
        }
    }

    void ShowToolTip()
    {
        TooltipSystem.Show(content, header);
    }

    IEnumerator HideToolTip()
    {
        yield return new WaitForSeconds(3f);

        TooltipSystem.Hide();
        hide = false;
    }
}
