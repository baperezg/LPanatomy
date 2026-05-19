using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ButtonHideShow : MonoBehaviour
{
    [System.Serializable]
    public class ToggleItem
    {
        public GameObject geometry;
        // Toggle text if required)
        //public TMP_Text label;
        //public string hideText;
        //public string showText;
    }

    public ToggleItem skin;
    public ToggleItem subcutaneous;
    public ToggleItem muscleLeft;
    public ToggleItem muscleRight;    
    public ToggleItem supraspinousLigament;
    public ToggleItem ligaments;
    public ToggleItem epidural;
    public ToggleItem panel;
    public ToggleItem label;

    // Generic toggle method
    private void Toggle(ToggleItem item)
    {
        // Change geometry visibility
        bool isActive = item.geometry.activeSelf;
        item.geometry.SetActive(!isActive);

        // Update text (if required)
        //item.label.text = isActive ? item.showText : item.hideText;
    }

    // Button callbacks
    public void ClickButtonSkin() => Toggle(skin);
    public void ClickButtonSubcutaneous() => Toggle(subcutaneous);
    public void ClickButtonMuscleLeft() => Toggle(muscleLeft);
    public void ClickButtonMuscleRight() => Toggle(muscleRight);    
    public void ClickButtonSupraspinousLigament() => Toggle(supraspinousLigament);
    public void ClickButtonLigaments() => Toggle(ligaments);
    public void ClickButtonEpidural() => Toggle(epidural);
    public void ClickButtonPanel() => Toggle(panel);
    public void ClickButtonLabel() => Toggle(label);



}
