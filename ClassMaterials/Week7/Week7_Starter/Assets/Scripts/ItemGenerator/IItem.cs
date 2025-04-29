using System;
public interface IItem
{

    public void SetUp(ScriptableItem item);
    public void Accept(IItemVisitor visitor);

}
