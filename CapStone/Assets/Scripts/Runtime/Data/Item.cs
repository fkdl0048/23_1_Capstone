using System;
using Enums;

public interface Item
{
    public ItemType ItemType { get; set; }
    public int ItemPrice { get; set; }
}
