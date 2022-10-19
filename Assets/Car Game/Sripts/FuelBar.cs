using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelBar : MonoBehaviour
{
    private Image fuelBar;
    public float currentFuel;
    private float maxFuel = 100f;
    PlayerController fuelPlayer;

    // Start is called before the first frame update
    void Start()
    {
        fuelBar = GetComponent<Image>();
        fuelPlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        currentFuel = fuelPlayer.fuel;
        fuelBar.fillAmount = currentFuel / maxFuel;
    }
}
