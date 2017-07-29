using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreen : MonoBehaviour {

	



    public void Show()
    {

        MenuManager.Instance.Show(this);

    }


    public void Hide()
    {

        MenuManager.Instance.Close();
    }


   

}
