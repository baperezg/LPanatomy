using UnityEngine;

public class AndroidVibration : MonoBehaviour
{
    private static AndroidJavaObject vibrator;

    private void Awake()
    {
#if UNITY_ANDROID && !UNITY_EDITOR

        AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");

        // Get the Vibrator service
        AndroidJavaObject context = currentActivity.Call<AndroidJavaObject>("getApplicationContext");
        vibrator = context.Call<AndroidJavaObject>("getSystemService", "vibrator");
#endif
    }

    /// <summary>
    /// Simple vibration (default duration).
    /// </summary>
    public static void Vibrate()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (vibrator != null)
        {
            vibrator.Call("vibrate", 100); // 100 ms
        }
#else
        Handheld.Vibrate();
#endif
    }

    /// <summary>
    /// Vibrate for a specific duration (milliseconds).
    /// </summary>
    public static void Vibrate(long milliseconds)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (vibrator != null)
        {
            vibrator.Call("vibrate", milliseconds);
        }
#else
        Handheld.Vibrate();
#endif
    }

    /// <summary>
    /// Vibrate with a pattern (array of timings).
    /// </summary>
    public static void Vibrate(long[] pattern, int repeat)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (vibrator != null)
        {
            vibrator.Call("vibrate", pattern, repeat);
        }
#endif
    }

    /// <summary>
    /// Cancel ongoing vibration.
    /// </summary>
    public static void Cancel()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        if (vibrator != null)
        {
            vibrator.Call("cancel");
        }
#endif
    }
}
