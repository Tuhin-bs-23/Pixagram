using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
public class PageManager : SerializedMonoBehaviour
{
    public Dictionary<string, UIPage> uiPages = new Dictionary<string, UIPage>();

    [SerializeField] private UIPage currentPage;

    [SerializeField] private Stack<UIPage> Breadcrumb;

    public GameObject bottomNavBar;

    public void ShowPage(UIPage page)
    {
        uiPages.ForEach(r => r.Value.HidePage());
        currentPage = page;
        currentPage.ShowPage();
    }
    public UIPage FindPage(string pageName)
    {
        return uiPages[pageName];
    }

    public void ToggleBottomNavigation(bool status)
    {
        bottomNavBar.SetActive(status);

    }
}
