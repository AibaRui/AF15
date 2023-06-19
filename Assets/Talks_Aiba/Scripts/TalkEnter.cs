using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkEnter : MonoBehaviour
{
    [SerializeField] private TalkaManager _talkaManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _talkaManager.Collider = other;
            _talkaManager.IsEnter = true;
            _talkaManager.SetAssistPanel(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _talkaManager.Collider = null;
            _talkaManager.IsEnter = false;
            _talkaManager.SetAssistPanel(false);
        }
    }



}
