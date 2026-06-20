using UnityEngine;
using UnityEngine.EventSystems;
using StarterAssets;

public class MobileJumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private StarterAssetsInputs _input;

    void Start()
    {
        _input = FindAnyObjectByType<StarterAssetsInputs>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _input.JumpInput(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _input.JumpInput(false);
    }
}   