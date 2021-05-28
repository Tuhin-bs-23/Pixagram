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
    private void Start()
    {
        ShowPage("loginPage");
    }
    public void ShowPage(UIPage page)
    {
        /* uiPages.ForEach(r => r.Value.HidePage());
         currentPage = page;
         currentPage.ShowPage();*/

        ShowPage(page, true);
    }
    public UIPage FindPage(string pageName)
    {
        return uiPages[pageName];
    }
    public void ShowPage(string page, bool showBG = true)
    {
        ShowPage(FindPage(page), showBG);
    }

    private void ShowPage(UIPage page, bool showBG = true, bool isBack = false)
    {
        uiPages.ForEach(r => r.Value.HidePage());

        //if (!isBack && currentPage != null) Breadcrumb.Push(currentPage); 
        
        currentPage = page;
        if (!isBack && currentPage != null) Breadcrumb.Push(currentPage);
        currentPage.ShowPage();
        
    }
    public void ToggleBottomNavigation(bool status)
    {
        bottomNavBar.SetActive(status);
    }

    public void OnBackButtonPressed()
    {
        

        if (Breadcrumb.Count > 0)
        {
            if (currentPage.blockBackButtonAction)
            {
                ShowPage(Breadcrumb.Pop(), true);
               
            }
            else
            {
                ShowPage(Breadcrumb.Pop(), isBack: true);
            }

            return;
        }


        currentPage.backButtonFunction?.Invoke();
    }
}
