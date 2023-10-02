using UnityEngine.UIElements;

namespace SCT
{
    public class TreeItemElement : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<TreeItemElement, UxmlTraits> { }
        public new class UxmlTraits : VisualElement.UxmlTraits { }

        public TreeItemElement() : base() 
        {
            Image icon = new Image();
            Label label = new Label();

            Add(icon);
            Add(label);
        }
    }
}
