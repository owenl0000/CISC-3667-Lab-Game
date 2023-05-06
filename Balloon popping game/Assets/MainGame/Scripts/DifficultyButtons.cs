using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DifficultyButtons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private TextMeshProUGUI descriptionText; // Assign your TextMeshProUGUI element in the Inspector
    [SerializeField] private string description; // Enter the description for this button in the Inspector

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Show the description when the pointer is over the button
        descriptionText.text = description;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the description when the pointer leaves the button
        descriptionText.text = "";
    }
}
