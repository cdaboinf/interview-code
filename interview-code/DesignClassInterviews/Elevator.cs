using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Threading;

namespace interview_code.DesingClassInterviews
{
    public class Elevator
    {
        public int Floor { get; set; }
        public ElevatorDirection Direction { get; set; }
        public ElevatorState State { get; set; }
        public bool DoorOpen { get; set; }
        public int Capacity { get; set; }

        public Button button5;

        public Elevator(int capacity = 10)
        {
            this.Capacity = capacity;
            this.DoorOpen = true;
            this.State = ElevatorState.Idle;
            this.Floor = 0;

            this.button5 = new Button("5");
        }
    }

    public enum ElevatorDirection
    {
        Up,
        Down
    }

    public enum ElevatorState
    {
        Moving,
        Stop,
        Idle
    }

    interface IElevatortResquestManager
    {
        void GoToFloorRequest(int floor);
        void CallElevatorRequest(ElevatorDirection direction, int floor);
    }

    public class ElevatorRequestManger : IElevatortResquestManager
    {
        private Elevator _elevator;
        private List<int> _upOperations;
        private List<int> _downOperations;

        public ElevatorRequestManger(Elevator elevator)
        {
            _elevator = elevator;
            _upOperations = new List<int>();
            _downOperations = new List<int>();

            ExecuteOperations();
        }

        public void GoToFloorRequest(int floor)
        {
            // get the current elevator state
            var currentFloor = _elevator.Floor;
            if (_elevator.State == ElevatorState.Idle)
            {
                if (floor > currentFloor)
                {
                    _upOperations.Add(floor);
                    _upOperations.Sort();
                }
                else
                {
                    _downOperations.Add(floor);
                    _downOperations.Sort();
                    _downOperations.Reverse();
                }
                return;
            }
            
            // get elavator existing state
            var direction = _elevator.Direction;
            if (direction == ElevatorDirection.Up && floor > currentFloor)
            {
                _upOperations.Add(floor);
                _upOperations.Sort();
            }
            else if (direction == ElevatorDirection.Down && floor <= currentFloor)
            {
                _downOperations.Add(floor);
                _downOperations.Sort();
                _downOperations.Reverse();
            }
        }
        
        public void CallElevatorRequest(ElevatorDirection direction, int floor)
        {
            switch (direction)
            {
                case ElevatorDirection.Down:
                    _downOperations.Add(floor);
                    _downOperations.Sort();
                    _downOperations.Reverse();
                    break;
                case ElevatorDirection.Up:
                    _upOperations.Add(floor);
                    _upOperations.Sort();
                    break;
            }
        }
        
        private void ExecuteOperations()
        {
            while (true)
            {
                if (!_upOperations.Any() && !_downOperations.Any())
                {
                    _elevator.State = ElevatorState.Idle;
                    continue;
                }
                
                var direction =  _elevator.Direction;
                _elevator.DoorOpen = false;
                
                if (direction == ElevatorDirection.Up)
                {
                    _elevator.Direction = ElevatorDirection.Up;
                    _elevator.State = ElevatorState.Moving;

                    var nextFloor = _upOperations.First(o => o > _elevator.Floor);
                    _elevator.Floor = nextFloor;
                    _upOperations.Remove(nextFloor);
                    
                    _elevator.State = ElevatorState.Stop;
                    _elevator.DoorOpen = true;
                }
                else if (direction == ElevatorDirection.Down)
                {
                    _elevator.Direction = ElevatorDirection.Down;
                    _elevator.State = ElevatorState.Moving;
                    
                    var nextFloor = _downOperations.First(o=> o < _elevator.Floor);
                    _elevator.Floor = nextFloor;
                    _downOperations.Remove(nextFloor);
                    
                    _elevator.State = ElevatorState.Stop;
                    _elevator.DoorOpen = true;
                }
            }
        }
    }

    public interface IButton
    {
        string Label { get; set; }
        void Click(ElevatorDirection direction, int floor);
    }
    
    public class Button: IButton
    {
        public Button(string label)
        {
            this.Label = label;
        }
        public string Label { get; set; }

        public void Click(ElevatorDirection direction, int floor)
        {
            
        }
    }
    
    public class ButtonElevatorPanel
    {
        private readonly ElevatorRequestManger _manager;
        public ButtonElevatorPanel(Elevator elevator)
        {
            _manager = new ElevatorRequestManger(elevator);
        }
    }
    // add button class
    // control panel class
}