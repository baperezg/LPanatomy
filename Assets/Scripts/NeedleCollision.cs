using TMPro;
using UnityEngine;

public class NeedleCollision : MonoBehaviour
{
    [Header("UI Label")]
    public TMP_Text collisionLabel;

    //[Header("Vibration")]
    //public AndroidVibration androidVibration;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "skin")
        {
            collisionLabel.text = "Skin";
            AndroidVibration.Vibrate(50); // short vibration
        }
        if (other.gameObject.tag == "subcutaneous")
        {
            collisionLabel.text = "Subcutaneous tissue";
            AndroidVibration.Vibrate(100); // medium vibration
        }
        if (other.gameObject.tag == "inter")
        {
            collisionLabel.text = "Interspinous ligament";
            AndroidVibration.Vibrate(200); // longer vibration
        }
        if (other.gameObject.tag == "flavum")
        {
            collisionLabel.text = "Ligamentum flavum";
            AndroidVibration.Vibrate(new long[] { 0, 50, 100, 50 }, -1); // patterned vibration
        }
        if (other.gameObject.tag == "epidural")
        {
            collisionLabel.text = "Epidural space";
            AndroidVibration.Vibrate(50);
        }
        if (other.gameObject.tag == "dura")
        {
            collisionLabel.text = "Dura mater";
            AndroidVibration.Vibrate(100);
        }
    }

    private void OnTriggerStay(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }
}
