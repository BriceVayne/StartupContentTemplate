using UnityEngine.UIElements;

namespace SCT
{
    public class TitleElement : Label
    {
        public new class UxmlFactory : UxmlFactory<TitleElement, UxmlTraits> { }

        public new class UxmlTraits : TextElement.UxmlTraits { }

        public TitleElement() : base() { this.text = this.name; }
    }
}
