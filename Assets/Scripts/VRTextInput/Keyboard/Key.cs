using System.Collections;
using System.Collections.Generic;

public class Key
{
    private string keyValue;
    private string shiftkeyValue;
    public string KeyValue
    {
        get
        {
            return this.keyValue;
        }
        set
        {
            this.keyValue = value;
        }
    }

    public string ShiftkeyValue
    {
        get
        {
            return this.shiftkeyValue;
        }
        set
        {
            this.shiftkeyValue = value;
        }
    }

    //Konstruktor
    public Key(string keyValue, string shiftkeyValue)
    {
        this.keyValue = keyValue;
        this.shiftkeyValue = shiftkeyValue;
    }
}
