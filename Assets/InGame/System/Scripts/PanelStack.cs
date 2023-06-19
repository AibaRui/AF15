using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelStack : MonoBehaviour
{
    [Header("インベントリ大本のパネル")]
    [SerializeField] private GameObject _inventory;

    private bool _isOpen = false;

    private Stack<GameObject> _stackPanel = new Stack<GameObject>();


    public bool IsOpen => _isOpen;
    public Stack<GameObject> StackPanel => _stackPanel;

    [SerializeField] private InputManager _inputManager;

    public void OpenInventory()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _isOpen = true;
        _inventory.SetActive(true);
        AddPanel(_inventory);
    }

    public void AddPanel(GameObject panel)
    {
        _stackPanel.Push(panel);
    }


    private void Update()
    {
        if (_inputManager.IsDownEscape && _stackPanel.Count > 0)
        {
            _stackPanel.Pop().SetActive(false);
            GameManager.Instance.PauseManager.PauseEnd();

            if(_stackPanel.Count>=0)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                _isOpen = false;
            }
        }
    }

}
