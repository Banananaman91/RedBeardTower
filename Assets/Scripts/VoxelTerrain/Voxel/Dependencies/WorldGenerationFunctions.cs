using UnityEngine;

namespace VoxelTerrain.Voxel.Dependencies
{
    public class WorldGenerationFunctions : MonoBehaviour
    {
        [SerializeField] private VoxelEngine _engine;
        [SerializeField] private ChunkGenerator _chunkGenerator;

        public ChunkGenerator ChunkGenerator => _chunkGenerator;

        public void GenerateWorld(Vector3 origin, float xDistance, float zDistance, float yPos, float size)
        {
            _chunkGenerator.Engine = _engine;
            for (float x = origin.x - xDistance; x <= origin.x + xDistance; x += Chunk.ChunkSize)
            {
                for (float z = origin.z - zDistance; z <= origin.z + zDistance; z += Chunk.ChunkSize)
                {
                    GenerateChunkData(new Vector3(x, yPos, z));
                }
            }
        }
        
        private void GenerateChunkData(Vector3 pos) => _chunkGenerator.CreateChunkJob(pos);
    }
}
