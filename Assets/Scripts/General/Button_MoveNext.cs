public class Button_MoveNext : ButtonBase
{
    private void OnEnable()
    {
        eventSystem.AddListener<bool>(Services.EEvent.StateChange, AfterStateEdge);
    }

    private void OnDisable()
    {
        eventSystem.RemoveListener<bool>(Services.EEvent.StateChange, AfterStateEdge);
    }

    private void AfterStateEdge(bool state)
    {
        Button.interactable = state;
    }

    protected override void OnClick()
    {
        eventSystem.Invoke(Services.EEvent.MoveNext);
    }
}
