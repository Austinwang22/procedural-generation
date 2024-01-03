using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType {
    Grass,
    Sand,
    Water
}

public class Block {

    public Vector3 position, index;

    public BlockType type;

    public GameObject prefab;

    public bool toRender;

    public Block(Vector3 pos, Vector3 i, BlockType t, GameObject pre) {
        position = pos;
        index = i;
        type = t;
        prefab = pre;
        toRender = false;
    }
}