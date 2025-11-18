using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("UI Text to show on hover")]
    public GameObject hoverTextObject; // Assign the text GameObject
    public string hoverText; // The text to display

    private Text textComponent;

    private void Start()
    {
        if (hoverTextObject != null)
        {
            textComponent = hoverTextObject.GetComponent<Text>();
            hoverTextObject.SetActive(false); // hide by default
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverTextObject != null)
        {
            hoverTextObject.SetActive(true);
            if (textComponent != null)
                textComponent.text = hoverText;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverTextObject != null)
            hoverTextObject.SetActive(false);
    }
}
