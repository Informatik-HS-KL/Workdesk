using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layout
{
    private Key[] row1;
    private Key[] row2;
    private Key[] row3;
    private Key[] row4;

    public Key[] getRow1()
    {
        return this.row1;
    }

    public Key[] getRow2()
    {
        return this.row2;
    }

    public Key[] getRow3()
    {
        return this.row3;
    }

    public Key[] getRow4()
    {
        return this.row4;
    }

    //Konstrukor
    public Layout()
    {
        //initiiert DefaultLayout
        fillRowsWithDefault();
    }

    private void fillRowsWithDefault()
    {
        row1 = new Key[] { new Key("1", "1"), new Key("2", "2"), new Key("3", "3"), new Key("q","Q"), new Key("w", "W"), new Key("e", "E"), new Key("r", "R"), new Key("t","T"), new Key("z", "Z"), new Key("u", "U"), new Key("i", "I"), new Key("o", "O"), new Key("p", "P"), new Key("ü", "Ü"), new Key("return", "return")};
        row2 = new Key[] { new Key("4", "4"), new Key("5", "5"), new Key("6", "6"), new Key("a", "A"), new Key("s", "S"), new Key("d", "D"), new Key("f", "F"), new Key("g", "G"), new Key("h", "H"), new Key("j", "J"), new Key("k", "K"), new Key("l", "L"), new Key("ö", "Ö"), new Key("ä", "Ä"), new Key("down", "down") };
        row3 = new Key[] { new Key("7", "7"), new Key("8", "8"), new Key("9", "9"), new Key("shift", "shift"), new Key("shift", "shift"), new Key("y", "Y"), new Key("x", "X"), new Key("c", "C"), new Key("v", "V"), new Key("b", "B"), new Key("n", "N"), new Key("m", "M"), new Key("!", "!"), new Key("?", "?"), new Key("rdy", "rdy") };
        row4 = new Key[] { new Key(".", "."), new Key("0", "0"), new Key("-", "-"), new Key("sym", "sym"), new Key("sym", "sym"), new Key("@", "@"), new Key("space", "space"), new Key("space", "space"), new Key("space", "space"), new Key("space", "space"), new Key("space", "space"), new Key(",", ","), new Key(".", "."), new Key("-", "-") };
    }

    //TODO: Layout sollen auch custom verändert werden
    public void loadFromJson()
    {

    }
}
