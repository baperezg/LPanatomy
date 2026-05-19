using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class TransparencyShader : MonoBehaviour
{

    [System.Serializable]
    public class TransparentItem
    {
        public GameObject target;          // Object to make transparent
        [Range(0f, 1f)] public float defaultAlpha = 1f; // Starting alpha
        public Slider transparencySlider;  // Reference to the UI slider        
    }

    public TransparentItem[] items; // Geometries array: skin, subcutaneous, etc.
    private string propertyName = "_Transparency"; // must match Shader Graph property

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var item in items)
        {
            if (item.transparencySlider != null)
            {
                // Initialize slider
                item.transparencySlider.minValue = 0f;
                item.transparencySlider.maxValue = 1f;
                item.transparencySlider.value = item.defaultAlpha;

                // Apply initial transparency
                ApplyTransparency(item, item.defaultAlpha);

                // Wire slider event to update only this item
                item.transparencySlider.onValueChanged.AddListener(
                    alpha => ApplyTransparency(item, alpha)
                );
            }
        }
    }

    private void ApplyTransparency(TransparentItem item, float alpha)
    {
        if (item.target == null) return;

        Renderer renderer = item.target.GetComponent<Renderer>();
        if (renderer != null)
        {
            foreach (var mat in renderer.materials)
            {
                // Update the color alpha
                mat.SetFloat(propertyName, alpha);
            }
        }
    }

}
