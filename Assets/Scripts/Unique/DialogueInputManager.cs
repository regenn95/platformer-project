using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Shows controls for Dialogue Editor, also handles scrollspeed by pressing and holding down on 'Space'
namespace DialogueEditor
{
    public class DialogueInputManager : MonoBehaviour
    {

        private void Update()
        {
            if (ConversationManager.Instance != null)
            {
                UpdateConversationInput();
            }
        }

        private void UpdateConversationInput()
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                    ConversationManager.Instance.SelectPreviousOption();
                else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                    ConversationManager.Instance.SelectNextOption();
                else if (Input.GetKey(KeyCode.Space))
                    ConversationManager.Instance.ScrollSpeed = 0f;
                else if (Input.GetKeyUp(KeyCode.Space))
                    ConversationManager.Instance.ScrollSpeed = 1f;

                if (Input.GetKeyDown(KeyCode.Space))
                    ConversationManager.Instance.PressSelectedOption();   
                    
            }
        }
    }
}
