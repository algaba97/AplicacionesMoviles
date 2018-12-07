using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Tile {

	public Block(uint touches, bool canfall)
    {
        _pendingtouches = touches;
        fall = canfall;
    }
}
