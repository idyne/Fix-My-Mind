using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private void Update()
    {
        CheckInput();
    }
    private void CheckInput()
    {
        GameManager GM = Singleton.GM;
        if (Input.GetMouseButtonDown(0))
        {
            if (GM.gameType == GameType.DIALOGUE)
            {
                if (GM.gameState == GameState.INIT)
                {
                    GM.StartGame();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            print("Captured");
            ScreenCapture.CaptureScreenshot("D:\\screenshot.png");
        }
    }
}
