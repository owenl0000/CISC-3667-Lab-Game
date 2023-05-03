using UnityEngine;
using UnityEngine.EventSystems;

public class MenuPin : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Pin pinPrefab;
    public MenuBalloons balloonPrefab;
    public Transform launchOffset;
    public float launchSpeed = 10f;

    private Pin currentPin;
    private MenuBalloons currentBalloon;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Instantiate a new pin and balloon
        currentPin = Instantiate(pinPrefab, launchOffset.position, Quaternion.identity);
        currentBalloon = Instantiate(balloonPrefab, Vector3.zero, Quaternion.identity);
        currentBalloon.transform.parent = currentPin.transform;

        // Set the balloon's position to be slightly above the launch offset
        currentBalloon.transform.localPosition = new Vector3(0f, 0.5f, 0f);

        // Launch the pin towards the balloon
        Vector3 direction = currentBalloon.transform.position - currentPin.transform.position;
        currentPin.GetComponent<Rigidbody2D>().velocity = direction.normalized * launchSpeed;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Destroy the current pin and balloon if they exist
        if (currentPin != null)
        {
            Destroy(currentPin.gameObject);
        }
        if (currentBalloon != null)
        {
            Destroy(currentBalloon.gameObject);
        }
    }
}

