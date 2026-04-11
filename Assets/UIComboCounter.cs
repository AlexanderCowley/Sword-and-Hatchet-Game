using UnityEngine;
using TMPro;
public class UIComboCounter : MonoBehaviour
{
    TextMeshProUGUI Text;

    void OnEnable()
    {
        Text = GetComponent<TextMeshProUGUI>();
        UpdateText();
        CombatController.OnHitHandler += UpdateText;
        Text.text = $"Combo Counter: {CombatController.ComboCount}";
    }

    void UpdateText()
    {
        Debug.Log("Update Combo Text");
        Text.text = $"Combo Counter: {CombatController.ComboCount}";
    }

    void OnDisable()
    {
        CombatController.OnHitHandler -= UpdateText;
    }
}
