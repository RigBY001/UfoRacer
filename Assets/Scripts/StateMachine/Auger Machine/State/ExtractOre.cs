using System;
using HFSM;

namespace AugerState
{
    public class ExtractOre : State
    {
        private Auger _auger;
        private Inventory _playerInventory;
        private Timer _timer;
        public Action OnDoneUnloading;

        public ExtractOre(Auger auger)
        {
            _auger = auger;
            _timer = new(0.1f);
            _timer.timeOut.AddListener(AddOre);
            _timer.Loop = true;
        }
        private void AddOre()
        {
            if (_auger.Player == null || _playerInventory == null) return;
            _playerInventory.AddOre(_auger.UnloadingOre());
            if (_auger.IsEmpty) OnDoneUnloading.Invoke();

        }

        protected override void OnEnter()
        {
            _playerInventory = _auger.Player.GetComponent<Inventory>();
            _timer.Start();
        }
        protected override void OnExit()
        {
            _timer.Stop();
        }
    }
}
