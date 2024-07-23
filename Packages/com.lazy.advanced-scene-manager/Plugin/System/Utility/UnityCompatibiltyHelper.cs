using UnityEngine;

namespace AdvancedSceneManager.Utility
{

    /// <summary>Contains helpers for dealing with multiple versions of unity.</summary>
    public static class UnityCompatibiltyHelper
    {

#if UNITY_2023_1_OR_NEWER
        /// <inheritdoc cref="Object.FindFirstObjectByType"/>
#else
        /// <inheritdoc cref="Object.FindObjectOfType"/>
#endif
        public static T FindFirstObjectByType<T>() where T : Object
        {
#if UNITY_2023_1_OR_NEWER
            return Object.FindFirstObjectByType<T>();
#else
            // Fallback for older versions
            return Object.FindObjectOfType<T>();
#endif
        }

    }

}