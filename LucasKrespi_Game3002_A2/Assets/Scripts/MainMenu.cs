using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    Canvas Menu;
    [SerializeField]
    Canvas Instructions;
    // Start is called before the first frame update
    void Start()
    {
        Menu.enabled = true;
        Instructions.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void instructions()
    {
        Menu.enabled = false;
        Instructions.enabled = true;
    }

    public void back()
    {
        Menu.enabled = true;
        Instructions.enabled = false;
    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }
}
