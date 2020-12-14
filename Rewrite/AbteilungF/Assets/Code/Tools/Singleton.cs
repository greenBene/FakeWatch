using UnityEngine;

namespace AsserTOOLres
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        #region ===== ===== API ===== =====

        public static T GetInstance()
        {
            return SaveGetInstance();
        }

        public static bool Exists()
        {
            return myInstanceBackingField != default;
        }

        public static T ResetInstance()
        {
			if(Exists()) {
				Destroy(GetInstance());
			}
            myInstanceBackingField = default;
            return SaveGetInstance();
        }

        #endregion
        #region ===== ===== VIRTUAL ===== =====

        protected virtual void OnMyAwake() { }

		#endregion
		#region ===== ===== CORE ===== =====

		private static T myInstanceBackingField = default;

		private void Awake()
        {
            if (myInstanceBackingField != null)
            {
#if UNITY_EDITOR
                Debug.Log("multible instances of type " + typeof(T));
#endif
                Destroy(this);
                return;
            }
            myInstanceBackingField = this as T;

            OnMyAwake();
        }

        static T SaveGetInstance()
        {
            if (myInstanceBackingField == default)
            {
#if UNITY_EDITOR
                Debug.Log("no object of type " + typeof(T) + " was found.");
#endif
                myInstanceBackingField = new GameObject("SINGELTON_" + typeof(T)).AddComponent<T>();
            }
            return myInstanceBackingField;
        }

        #endregion
    }
}
