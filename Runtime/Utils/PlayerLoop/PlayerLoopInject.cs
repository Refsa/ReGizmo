using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

namespace ReGizmo.Utils
{
    internal static class PlayerLoopInject
    {
        static InjectedSystemCallbacks injectedSystem;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Inject()
        {
            var playerLoop = PlayerLoop.GetCurrentPlayerLoop();
            injectedSystem = new InjectedSystemCallbacks();

            var injectedInitializationSystem =
            MakeLoopSystem<InjectedSystems.InjectedInitializationSystem>(InjectedInitialization);
            var injectedEarlyUpdateSystem =
                MakeLoopSystem<InjectedSystems.InjectedEarlyUpdateSystem>(InjectedEarlyUpdate);
            var injectedFixedUpdateSystem =
                MakeLoopSystem<InjectedSystems.InjectedFixedUpdateSystem>(InjectedFixedUpdate);
            var injectedPreUpdateSystem =
                MakeLoopSystem<InjectedSystems.InjectedPreUpdateSystem>(InjectedPreUpdate);
            var injectedUpdateSystem =
                MakeLoopSystem<InjectedSystems.InjectedUpdateSystem>(InjectedUpdate);
            var injectedPostUpdateSystem =
                MakeLoopSystem<InjectedSystems.InjectedPostUpdateSystem>(InjectedPostUpdate);
            var injectedEndOfFrameSystem =
                MakeLoopSystem<InjectedSystems.InjectedEndOfFrameSystem>(InjectedEndOfFrameUpdate);

            if (!InjectSystem<UnityEngine.PlayerLoop.Initialization>(ref playerLoop, ref injectedInitializationSystem))
            {
                Debug.LogError("Failed to inject InjectedSystems.InjectedInitializationSystem into PlayerLoop");
            }

            if (!InjectSystem<UnityEngine.PlayerLoop.EarlyUpdate>(ref playerLoop, ref injectedEarlyUpdateSystem))
            {
                Debug.LogError("Failed to inject InjectedSystems.InjectedEarlyUpdateSystem into PlayerLoop");
            }

            if (!InjectSystem<UnityEngine.PlayerLoop.FixedUpdate>(ref playerLoop, ref injectedFixedUpdateSystem))
            {
                Debug.LogError("Failed to inject InjectedSystems.InjectedFixedUpdateSystem into PlayerLoop");
            }

            if (!InjectSystem<UnityEngine.PlayerLoop.PreUpdate>(ref playerLoop, ref injectedPreUpdateSystem))
            {
                Debug.LogError("Failed to inject InjectedSystems.InjectedPreUpdateSystem into PlayerLoop");
            }

            if (!InjectSystem<UnityEngine.PlayerLoop.Update>(ref playerLoop, ref injectedUpdateSystem))
            {
                Debug.LogError("Failed to inject InjectedSystems.InjectedUpdateSystem into PlayerLoop");
            }

            if (!InjectSystem<UnityEngine.PlayerLoop.PreLateUpdate>(ref playerLoop, ref injectedPostUpdateSystem))
            {
                Debug.LogError("Failed to inject InjectedSystems.InjectedPostUpdateSystem into PlayerLoop");
            }

            if (!InjectSystem<UnityEngine.PlayerLoop.PostLateUpdate>(ref playerLoop, ref injectedEndOfFrameSystem))
            {
                Debug.LogError("Failed to inject InjectedSystems.InjectedEndOfFrameSystem into PlayerLoop");
            }

            PlayerLoop.SetPlayerLoop(playerLoop);
        }

        /// <summary>
        /// Recursively scan and inject the given system into the PlayerLoop
        /// </summary>
        /// <param name="current"></param>
        /// <param name="injected"></param>
        /// <param name="first"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static bool InjectSystem<T>(ref PlayerLoopSystem current, ref PlayerLoopSystem injected, bool first = true)
        {
            if (current.type == typeof(T))
            {
                var newList = new PlayerLoopSystem[current.subSystemList.Length + 1];
                int offset = 0;

                if (first)
                {
                    newList[0] = injected;
                    offset = 1;
                }
                else
                {
                    newList[current.subSystemList.Length] = injected;
                }

                for (int i = 0; i < current.subSystemList.Length; i++)
                {
                    newList[i + offset] = current.subSystemList[i];
                }

                current.subSystemList = newList;

                return true;
            }

            if (current.subSystemList != null)
            {
                for (int i = 0; i < current.subSystemList.Length; i++)
                {
                    if (InjectSystem<T>(ref current.subSystemList[i], ref injected, first))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Creates a new PlayerLoopSystem wrapper to be injected into the PlayerLoop
        /// </summary>
        /// <param name="callback">callback method to run</param>
        /// <typeparam name="T">type identifier of the system</typeparam>
        /// <returns>Wrapper that can be injected with this::InjectSystem</returns>
        static PlayerLoopSystem MakeLoopSystem<T>(PlayerLoopSystem.UpdateFunction callback)
        {
            return new PlayerLoopSystem
            {
                updateDelegate = callback,
                type = typeof(T),
            };
        }

        public static void Inject(PlayerLoopInjectionPoint injectionPoint, System.Action system)
        {
            switch (injectionPoint)
            {
                case PlayerLoopInjectionPoint.Initialization:
                    injectedSystem.OnInitialization += system;
                    break;
                case PlayerLoopInjectionPoint.EarlyUpdate:
                    injectedSystem.OnEarlyUpdate += system;
                    break;
                case PlayerLoopInjectionPoint.FixedUpdate:
                    injectedSystem.OnFixedUpdate += system;
                    break;
                case PlayerLoopInjectionPoint.PreUpdate:
                    injectedSystem.OnPreUpdate += system;
                    break;
                case PlayerLoopInjectionPoint.Update:
                    injectedSystem.OnUpdate += system;
                    break;
                case PlayerLoopInjectionPoint.PostUpdate:
                    injectedSystem.OnPostUpdate += system;
                    break;
                case PlayerLoopInjectionPoint.EndOfFrame:
                    injectedSystem.OnEndOfFrame += system;
                    break;
            }
        }

        public static void Remove(PlayerLoopInjectionPoint injectionPoint, System.Action system)
        {
            switch (injectionPoint)
            {
                case PlayerLoopInjectionPoint.Initialization:
                    injectedSystem.OnInitialization -= system;
                    break;
                case PlayerLoopInjectionPoint.EarlyUpdate:
                    injectedSystem.OnEarlyUpdate -= system;
                    break;
                case PlayerLoopInjectionPoint.FixedUpdate:
                    injectedSystem.OnFixedUpdate -= system;
                    break;
                case PlayerLoopInjectionPoint.PreUpdate:
                    injectedSystem.OnPreUpdate -= system;
                    break;
                case PlayerLoopInjectionPoint.Update:
                    injectedSystem.OnUpdate -= system;
                    break;
                case PlayerLoopInjectionPoint.PostUpdate:
                    injectedSystem.OnPostUpdate -= system;
                    break;
                case PlayerLoopInjectionPoint.EndOfFrame:
                    injectedSystem.OnEndOfFrame -= system;
                    break;
            }
        }

        static void InjectedInitialization()
        {
            injectedSystem.OnInitialization?.Invoke();
        }

        static void InjectedEarlyUpdate()
        {
            injectedSystem.OnEarlyUpdate?.Invoke();
        }

        static void InjectedFixedUpdate()
        {
            injectedSystem.OnFixedUpdate?.Invoke();
        }

        static void InjectedPreUpdate()
        {
            injectedSystem.OnPreUpdate?.Invoke();
        }

        static void InjectedUpdate()
        {
            injectedSystem.OnUpdate?.Invoke();
        }

        static void InjectedPostUpdate()
        {
            injectedSystem.OnPostUpdate?.Invoke();
        }

        static void InjectedEndOfFrameUpdate()
        {
            injectedSystem.OnEndOfFrame?.Invoke();
        }
    }
}