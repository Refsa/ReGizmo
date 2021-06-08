using System;

namespace ReGizmo.Utils
{
    internal enum PlayerLoopInjectionPoint
    {
        Initialization = 0,
        EarlyUpdate,
        FixedUpdate,
        PreUpdate,
        Update,
        PostUpdate,
        EndOfFrame,
    }

    internal class InjectedSystemCallbacks
    {
        public object Owner;

        public Action OnInitialization;
        public Action OnEarlyUpdate;
        public Action OnFixedUpdate;
        public Action OnPreUpdate;
        public Action OnUpdate;
        public Action OnPostUpdate;
        public Action OnEndOfFrame;
    }

    internal interface IInjectedSystem
    {

    }

    internal struct InjectedSystems
    {
        public struct InjectedInitializationSystem : IInjectedSystem
        {
        }

        public struct InjectedEarlyUpdateSystem : IInjectedSystem
        {
        }

        public struct InjectedFixedUpdateSystem : IInjectedSystem
        {
        }

        public struct InjectedPreUpdateSystem : IInjectedSystem
        {
        }

        public struct InjectedUpdateSystem : IInjectedSystem
        {
        }

        public struct InjectedPostUpdateSystem : IInjectedSystem
        {
        }

        public struct InjectedEndOfFrameSystem : IInjectedSystem
        {
        }
    }
}