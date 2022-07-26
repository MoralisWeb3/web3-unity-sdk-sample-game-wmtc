using System;
using MoralisUnity.Samples.Shared.Attributes;
using MoralisUnity.Sdk.Events;
using UnityEngine;

namespace MoralisUnity.Samples.Shared.Data.Types
{
    /// <summary>
    /// Wrapper so a type can be observable via events
    /// </summary>
    public class Observable<T> where T : struct, IConvertible
    {
        //  Events ----------------------------------------
        public ObservableUnityEvent<T> OnValueChanged = new ObservableUnityEvent<T>();
        
        //  Properties ------------------------------------
        public T Value
        {
            set
            {
                _value = OnValueChanging(_value, value);
                OnValueChanged.Invoke(_value);
            }
            get
            {
                return _value;
                
            }
        }

        //  Fields ----------------------------------------
        [InspectorComment("Note: Value shown for debugging. Do not edit via inspector.")]
        [SerializeField]
        private string _dummyInspectorComment = "";
        
        [SerializeField]
        private T _value;
        
        //  Constructor Methods ---------------------------
        public Observable ()
        {
            
        }

        //  Methods ---------------------------------------
        protected virtual T OnValueChanging (T oldValue, T newValue)
        {
            return newValue;
        }
        
        //  Event Handlers --------------------------------
    }
}