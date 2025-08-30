using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
public class CircularRaycastFilter : MonoBehaviour, ICanvasRaycastFilter
{
    private Image buttonImage;
    private float imageCircleRadius;
    private RectTransform rectTransform;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        // Calculate the radius based on the RectTransform width
        imageCircleRadius = rectTransform.rect.width / 2;
    }

    // Check If the Mouse position is valid (within the circular area of the button)
    public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
    {
        // the button is not clickable when the image is disabled
        if (!buttonImage.enabled)
        {
            return false;
        }

        // Convert the screen click point to the button local space
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform,
            screenPoint,
            eventCamera,
            out Vector2 localPoint
        );

        // Calculate the distance of the click from the center of the button(0,0)
        float distance = Vector2.Distance(localPoint, Vector2.zero);

        // Allow the click if in the circle bounds
        return distance <= imageCircleRadius;
    }
}
