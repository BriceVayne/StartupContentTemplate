using UnityEngine.UIElements;

namespace SCT
{
    public class TitleElement : Label
    {
        public new class UxmlFactory : UxmlFactory<TitleElement, UxmlTraits> { }

        public new class UxmlTraits : TextElement.UxmlTraits { }

        public new static readonly string ussClassName = "sct-label-title";

        public TitleElement() : base() { }

        public TitleElement(string title) : base(title) { }
    }
}
