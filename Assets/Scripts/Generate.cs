using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generate : MonoBehaviour
{
    public GameObject grassPrefab, sandPrefab, waterPrefab;
    public GameObject[] prefabs;
    public int chunksX, chunksZ, xres, yres, zres, waterLevel, SandLevel;

    public float scale = 20f;

    public float offset = 0f;

    Chunk[,] chunks;

    void Start()
    {
        GameObject[] pre = {grassPrefab, sandPrefab, waterPrefab};
        prefabs = pre;
        chunks = new Chunk[chunksX, chunksZ];
        for (int x = 0; x < chunksX; x++) {
            for (int z = 0; z < chunksZ; z++) {
                Vector3 position = new Vector3((float) x * xres, 0f, (float) z * zres);
                Chunk c = new Chunk(position, xres, yres, zres);
                chunks[x, z] = c;
                GenerateChunk(c);
                RenderBlocks(c);
            }
        }
    }

    void GenerateChunk(Chunk c) {
        for (int x = 0; x < xres; x++) {
            for (int z = 0; z < zres; z++) {
                float noise = Mathf.PerlinNoise((float) (x + c.position.x + offset) / xres * scale, (float) (z + c.position.z + offset) / zres * scale);
                int y = (int) (noise * yres);
                if (y >= yres)
                    y = yres - 1;
                else if (y < 0)
                    y = 0;
                Vector3 pos = new Vector3(c.position.x + x, y, c.position.z + z);
                Vector3 ind = new Vector3(x, y, z);
                Block b;
                if (y <= waterLevel) {
                    b = new Block(pos, ind, BlockType.Sand, prefabs[1]);
                    for (int j = y + 1; j <= waterLevel; j++) {
                        Vector3 waterPos = new Vector3(pos.x, j, pos.z);
                        Vector3 waterInd = new Vector3(x, j, z);
                        Block water = new Block(waterPos, waterInd, BlockType.Water, prefabs[2]);
                        c.blocks[x,j,z] = water;
                        // RenderCube(water);
                    }
                }
                else if (y <= SandLevel) {
                    b = new Block(pos, ind, BlockType.Sand, prefabs[1]);
                }
                else {
                    b = new Block(pos, ind, BlockType.Grass, prefabs[0]);
                }
                c.blocks[x,y,z] = b;
                FillColumn(c, b);
                // RenderCube(b);
            }
        }
    }

    void FillColumn(Chunk c, Block b) {
        for (int y = (int) b.position.y - 3; y < (int) b.position.y; y++) {
            if (y < 0)
                break;
            Vector3 pos = new Vector3(b.position.x, y, b.position.z);
            Vector3 ind = new Vector3(b.index.x, y, b.index.z);
            Block fill = new Block(pos, ind, b.type, b.prefab);
            c.blocks[(int) ind.x, (int) ind.y, (int) ind.z] = fill;
            // RenderCube(fill);
        }
    }

    GameObject RenderCube(Block b) {
        GameObject cube = Instantiate(b.prefab, b.position, Quaternion.identity);
        cube.transform.parent = transform;
        return cube;
    }

    void RenderBlocks(Chunk c) {
        for (int x = 0; x < xres; x++) {
            for (int y = 0; y < yres; y++) {
                for (int z = 0; z < zres; z++) {
                    Block b = c.blocks[x,y,z];
                    if (b == null) {
                        continue;
                    }
                    if (y == yres - 1 || c.blocks[x,y+1,z] == null) {
                        b.toRender = true;
                        RenderCube(b);
                    }
                    else if (x - 1 >= 0 && c.blocks[x-1,y,z] == null) {
                        b.toRender = true;
                        RenderCube(b);
                    }
                    else if (x + 1 < xres && c.blocks[x+1,y,z] == null) {
                        b.toRender = true;
                        RenderCube(b);
                    }
                    else if (z - 1 >= 0 && c.blocks[x,y,z-1] == null) {
                        b.toRender = true;
                        RenderCube(b);
                    }
                    else if (z + 1 < zres && c.blocks[x,y,z+1] == null) {
                        b.toRender = true;
                        RenderCube(b);
                    }
                }
            }
        }
    }

    void Update()
    {
        
    }
}
