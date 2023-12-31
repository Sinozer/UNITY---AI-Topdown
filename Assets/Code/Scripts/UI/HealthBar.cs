// --------------------------------------- //
// --------------------------------------- //
//  Creation Date: 14/12/23
//  Description: AI - Topdown
// --------------------------------------- //
// --------------------------------------- //

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : EntityChild
{
    public Slider Slider;
    public Slider ChangedHealth;
    public Image Fill;
    public Gradient Gradient;

    private Coroutine _coroutine;

    private void OnEnable()
    {
        Entity.OnHealthChanged += SetHealth;
    }

    private void OnDisable()
    {
        Entity.OnHealthChanged -= SetHealth;
    }

    private void Start()
    {
        if (Entity.Data.TryFind<float>("MaxHealth", out float max) == false)
            return;

        if (Entity.Data.TryFind<float>("Health", out float health) == false)
            health = max;

        Slider.maxValue = max;
        Slider.value = health;

        ChangedHealth.maxValue = max;
        ChangedHealth.value = health;

        Gradient.Evaluate(1f);
    }

    public void SetHealth(float value)
    {
        Slider.value = value;

        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SetChangedHealth());

        Fill.color = Gradient.Evaluate(Slider.normalizedValue);
    }

    public IEnumerator SetChangedHealth()
    {
        yield return new WaitForSeconds(0.5f);

        float valueToRemove = (ChangedHealth.value - Slider.value) / 10;

        while (ChangedHealth.value > Slider.value)
        {
            ChangedHealth.value -= valueToRemove;
            yield return new WaitForSeconds(0.01f);
        }

        _coroutine = null;
    }
}
