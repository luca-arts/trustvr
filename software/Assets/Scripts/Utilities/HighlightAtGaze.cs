﻿// Copyright © 2018 – Property of Tobii AB (publ) - All Rights Reserved

using Tobii.G2OM;
using UnityEngine;


// MonoBehaviour which implements the "IGazeFocusable" interface, meaning it will be called on when the object receives focus.
public class HighlightAtGaze : MonoBehaviour, IGazeFocusable
{
#pragma warning disable 649 // Field is never assigned
    [ColorUsage(true, true)]
    [SerializeField]
    private Color highlightColor = Color.red;

    [SerializeField] private float animationTime = 0.1f;
#pragma warning restore 649

    private static readonly int _emissionColor = Shader.PropertyToID("_EmissionColor");
    
    private Color _originalColor;
    private Color _targetColor;
    private bool _srp;
    private Light _light;

    private void Start()
    {
        _light = GetComponentInChildren<Light>();
        _originalColor = _light.color;
        _targetColor = _originalColor;
    }

    private void Update()
    {
        var currentColor = _light.color;

        // This lerp will fade the color of the object.
        var newColor = Color.Lerp(currentColor, _targetColor, Time.deltaTime * (1 / animationTime));
        _light.color = newColor;
    }

    // The method of the "IGazeFocusable" interface, which will be called when this object receives or loses focus.
    public void GazeFocusChanged(bool hasFocus)
    {
        // If this object received focus, fade the object's color to highlight color.
        if (hasFocus)
        {
            _targetColor = highlightColor;
        }
        // If this object lost focus, fade the object's color to it's original color.
        else
        {
            _targetColor = _originalColor;
        }
    }
}
