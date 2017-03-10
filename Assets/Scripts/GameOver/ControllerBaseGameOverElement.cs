using UnityEngine;
using UnityEngine.UI;

public class ControllerBaseGameOverElement : MonoBehaviour
{
    [SerializeField] protected Text text;
    [SerializeField] protected Button button;

    protected virtual void Awake() {
    }

    protected virtual void Start() {
        Hide();
        SetText();
        RegisterClickHandler();
    }

    protected virtual void SetText() { }
    protected virtual void RegisterClickHandler() { }

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
