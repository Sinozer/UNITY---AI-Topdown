// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider Slider;
    public Image Fill;
    public Gradient Gradient;

    public void SetMaxHealth(float maxHealth) 
    { 
        Slider.maxValue = maxHealth;
        Slider.value = maxHealth;

        Gradient.Evaluate(1f);
    }

    public void SetHealth(float value)
    {
        Slider.value = value;

        Fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }
}
