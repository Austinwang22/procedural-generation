using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk
{
    public Vector3 position;

    public int xres, yres, zres;

    public Block[,,] blocks;

    public Chunk(Vector3 position, int xres, int yres, int zres) {
        this.position = position;
        this.xres = xres;
        this.yres = yres;
        this.zres = zres;
        blocks = new Block[xres, yres, zres];
    }
}