using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class UltimateSliderUI : MonoBehaviour
{
    [SerializeField]private Image ultImage;
    [SerializeField]private Image showPoint;
    
    [SerializeField] private float timeDuration = 10f;
    private IUltimateTimer _ultimateTimer;

    [Inject]
    private void Construct(IUltimateTimer ultimateTimer)
    {
        _ultimateTimer = ultimateTimer;
    }

    private void Start()
    {
        _ultimateTimer.OnTimerUpdated += UpdateSlider;  // Register the UpdateSlider method/
        _ultimateTimer.StartUltimateTimer(timeDuration);
    }

    private void OnDestroy()
    {
        _ultimateTimer.OnTimerUpdated -= UpdateSlider;   //  Unregister to avoid memory leaks.
    }
    
    private void UpdateSlider(float value)    // Update the fill amount of the ultimate ability timer UI.
    {
        ultImage.fillAmount = value;
        float alpha = (_ultimateTimer.GetTimeRemaining() < 1) ? Mathf.Max(0, showPoint.color.a - 1) : Mathf.Min(1, showPoint.color.a + 1);
        showPoint.color = new Color(showPoint.color.r, showPoint.color.g, showPoint.color.b, alpha);
    }
}
