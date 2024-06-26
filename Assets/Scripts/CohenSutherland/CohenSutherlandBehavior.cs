using Services;
using Services.Event;
using UnityEngine;

namespace CohenSutherland
{
    public class CohenSutherlandBehavior : MonoBehaviour
    {
        private IEventSystem eventSystem;
        private CohenSutherlandCore core;
        private RangeManager rangeManager;

        private void Awake()
        {
            rangeManager = GetComponent<RangeManager>();
            eventSystem = ServiceLocator.Get<IEventSystem>();
            core = new CohenSutherlandCore();
            core.Refresh += Refresh;
        }

        private void OnEnable()
        {
            eventSystem.AddListener(EEvent.ResetEdge, ResetEdge);
            eventSystem.AddListener(EEvent.Launch, Launch);
            eventSystem.AddListener(EEvent.MoveNext, MoveNext);
        }

        private void OnDisable()
        {
            eventSystem.RemoveListener(EEvent.ResetEdge, ResetEdge);
            eventSystem.RemoveListener(EEvent.Launch, Launch);
            eventSystem.RemoveListener(EEvent.MoveNext, MoveNext);
        }


        private void Launch()
        {
            core.Initialize(rangeManager);
        }
        private void ResetEdge()
        {
            core.Reset();
            eventSystem.Invoke(EEvent.HasNext, false);
        }
        private void Refresh()
        {
            eventSystem.Invoke(EEvent.AfterRefreshEdge, core.data as EdgeData);
            eventSystem.Invoke(EEvent.HasNext, core.isRunning && !core.data.CullOff && !core.data.Cull);
        }
        private void MoveNext()
        {
            core.MoveNext();
        }
    }
}