using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class MobileJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Joystick Settings")]
    public float joystickRange = 50f;
    public RectTransform handle;

    private StarterAssetsInputs _input;
    private Vector2 _inputDirection;
    private RectTransform _background;

    void Start()
    {
        _background = GetComponent<RectTransform>();
        _input = FindObjectOfType<StarterAssetsInputs>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - (Vector2)_background.position;
        _inputDirection = Vector2.ClampMagnitude(direction, joystickRange) / joystickRange;
        handle.anchoredPosition = Vector2.ClampMagnitude(direction, joystickRange);
        _input.MoveInput(_inputDirection);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputDirection = Vector2.zero;
        handle.anchoredPosition = Vector2.zero;
        _input.MoveInput(Vector2.zero);
    }
}