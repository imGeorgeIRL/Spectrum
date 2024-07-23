#if !UNITY_2022_1_OR_NEWER

namespace UnityEngine.UIElements
{
    public class EnumField : UnityEditor.UIElements.EnumField
    {
        public new class UxmlFactory : UxmlFactory<EnumField, UxmlTraits>
        { }
        public new class UxmlTraits : UnityEditor.UIElements.EnumField.UxmlTraits
        { }
    }
}

#endif
