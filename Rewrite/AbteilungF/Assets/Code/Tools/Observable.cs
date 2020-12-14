
namespace AsserTOOLres
{
	public class Observable<T>
	{
		#region ===== ===== CALLBACK ===== =====

		public System.Action OnValueChange;
		public System.Action<Observable<T>> OnValueChangeWithObservable;
		public System.Action<T> OnValueChangeWithState;

		#endregion
		#region ===== ===== API ===== =====

		public T value
		{
			get {
				return myBackingValue;
			}
			set {
				if (value.Equals(myBackingValue))
					return;
				myBackingValue = value;
				OnValueChange?.Invoke();
				OnValueChangeWithState?.Invoke(value);
				OnValueChangeWithObservable?.Invoke(this);
			}
		}

		#endregion
		#region ===== ===== CORE ===== =====

		T myBackingValue;

		#endregion
	}
}