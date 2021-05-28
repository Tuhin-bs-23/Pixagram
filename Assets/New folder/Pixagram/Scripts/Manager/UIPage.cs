using UnityEngine;
using UnityEngine.Events;
public class UIPage : MonoBehaviour
{
    public UnityEvent OnPageActive;
    public UnityEvent OnPageClose;
    public UnityEvent backButtonFunction;
    public bool navBarShow = true;
    public bool blockBackButtonAction;


    public void ShowPage()
    {
        gameObject.SetActive(true);
       
        AppManager.instance.pageManager.ToggleBottomNavigation(navBarShow);
        
        OnPageActive?.Invoke();

    }
    public void HidePage()
    {
        gameObject.SetActive(false);
        OnPageClose?.Invoke();
    }
    public bool PageVisibility()
    {
        return gameObject.activeSelf;
    }
}
