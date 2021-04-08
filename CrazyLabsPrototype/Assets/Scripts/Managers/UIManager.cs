using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private PlayMenu playMenu;
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private DebugMenu debugMenu;
    [SerializeField] private EndMenu endMenu;
    [SerializeField] private BootCamera bootCamera;
    public Slider ForceSlider;
    public Slider FuelTankSlider;
    public Slider ForceIncreaseMultiplier;
    public Slider ForceDecreaseMultiplier;
    public Slider Sensitivity;
    [SerializeField] Slider fuelbar;
    [SerializeField] Slider distbar;

    #region Events

    public Events.EventButtonClicked playMenuPlayClicked;
    public Events.EventButtonClicked playMenuDebugClicked;
    public Events.EventButtonClicked debugMenuBackClicked;
    public Events.EventButtonClicked endMenuReturnClicked;
    public Events.EventButtonClicked endMenuExitClicked;

    #endregion

    #region activating and deactivating menu

    public void SetPlayMenuActive(bool active)
    {
        playMenu.gameObject.SetActive(active);
    }

    public void SetMainMenuActive(bool active)
    {
        mainMenu.gameObject.SetActive(active);
    }

    public void SetDebugMenuActive(bool active)
    {
        debugMenu.gameObject.SetActive(active);
    }

    public void SetEndMenuActive(bool active)
    {
        endMenu.gameObject.SetActive(active);
    }

    public void SetEndMenuStatement(bool won)
    {
        endMenu.SetStatement(won);
    }

    public void SetBootCameraActive(bool active)
    {
        bootCamera.gameObject.SetActive(active);
    }

    #endregion

    #region Buttons

    public void playClicked()
    {
        playMenuPlayClicked.Invoke();
    }

    public void debugClicked()
    {
        playMenuDebugClicked.Invoke();
    }

    public void backClicked()
    {
        debugMenuBackClicked.Invoke();
    }

    public void returnClicked()
    {
        endMenuReturnClicked.Invoke();
    }

    public void exitClicked()
    {
        endMenuExitClicked.Invoke();
    }

    #endregion

    public void UpdateFuelBar(float val)
    {
        fuelbar.value = val;
    }

    public void UpdateDistBar(float val)
    {
        distbar.value = val;
    }
}
