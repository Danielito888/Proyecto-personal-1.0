using UnityEngine;

public class BuildTool : MonoBehaviour
{
    public BuildMenu buildMenu;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (buildMenu.menuUI.activeSelf)
                buildMenu.CloseMenu();
            else
                buildMenu.OpenMenu();
        }
    }
}
