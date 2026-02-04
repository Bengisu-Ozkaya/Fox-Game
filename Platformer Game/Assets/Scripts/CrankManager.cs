using System;
using UnityEngine;

public class CrankManager : MonoBehaviour
{
    private bool isCrankActive;

    [SerializeField] UIManager manager;

    [SerializeField] private string crankAxis; // "x" veya "y"
    [SerializeField] private Sprite openCrank;
    [SerializeField] private Sprite closeCrank;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = closeCrank;
    }

    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleCrank();
        }
    }
    // Crank'ý aç/kapa 
    public void ToggleCrank()
    {
        isCrankActive = !isCrankActive; // durum tersine çevrilir
        spriteRenderer.sprite = isCrankActive ? openCrank : closeCrank;

        // Butonlarý göster/gizle
        if (crankAxis == "x")
            manager.ShowCrankButtonX(isCrankActive);
        else if (crankAxis == "y")
            manager.ShowCrankButtonY(isCrankActive);

        Debug.Log("Crank " + (isCrankActive ? "açýldý" : "kapandý"));
    }

    public bool crankStatus()
    {
        return isCrankActive;
    }

    public string getCrankAxis()
    {
        return crankAxis;
    }
}
