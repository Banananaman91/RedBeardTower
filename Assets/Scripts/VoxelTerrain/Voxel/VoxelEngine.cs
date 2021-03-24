using System;
using TMPro;
using UnityEngine;
using VoxelTerrain.Voxel.Dependencies;
using VoxelTerrain.Voxel.InfoData;

namespace VoxelTerrain.Voxel
{
    [RequireComponent(typeof(WorldInfo), typeof(ChunkInfo), typeof(VoxelTypeHeights))]
    public class VoxelEngine : MonoBehaviour
    {
        public World WorldData = new World();

        [SerializeField] private WorldInfo _worldInfo;
        [SerializeField] private ChunkInfo _chunkInfo;
        [SerializeField] private VoxelTypeHeights _voxelTypeHeights;
        [SerializeField] private WorldGenerationFunctions _worldGeneration;
        [SerializeField] private float _noiseScale;
        [SerializeField] private int seed;
        [SerializeField] private GameObject _start;
        [SerializeField] private GameObject _end;


        private float _maxMagnitude;
        private bool _startGenerating;
        private float _xDistance;
        private float _zDistance;

        [HideInInspector] public Vector3 Position;
        public ChunkInfo ChunkInfo => _chunkInfo;
        private float ChunkSize => Chunk.ChunkSize * _chunkInfo.VoxelSize;
        private float ChunkHeight => Chunk.ChunkHeight * _chunkInfo.VoxelSize;
        public VoxelTypeHeights VoxelTypeHeights => _voxelTypeHeights;
        public float NoiseScale => _noiseScale;
        public int Seed => seed;
        public WorldInfo WorldInfo => _worldInfo;
        public PlaneAreaBehaviour _planeAreaBehaviour;
        private Camera CamMain => Camera.main;


        #region Unity Functions
        private void Awake()
        {
            WorldData.Engine = this;
            
        }

        public void StartGeneration(Vector3 position)
        {
            _xDistance = _planeAreaBehaviour.arPlaneExt.x < _worldInfo.Distance ? _planeAreaBehaviour.arPlaneExt.x : _worldInfo.Distance;
            
            _zDistance = _planeAreaBehaviour.arPlaneExt.y < _worldInfo.Distance ? _planeAreaBehaviour.arPlaneExt.y : _worldInfo.Distance;

            Position = position;

            var startPosition = _start.transform.position;
            startPosition.z = _zDistance;
            var endPosition = _end.transform.position;
            endPosition.z = -_zDistance;
            
            var corner = new Vector3(-_xDistance, Position.y, -_zDistance);
            _maxMagnitude = (Position - corner).magnitude;
            
            _worldGeneration.GenerateWorld(_planeAreaBehaviour.transform.position, _xDistance, _zDistance, Position.y, _chunkInfo.VoxelSize);
            _startGenerating = true;
        }

        public Vector3 NearestChunk(Vector3 pos)
        {
            var curChunkPosX = Mathf.FloorToInt(pos.x / ChunkSize) * ChunkSize;
            var curChunkPosZ = Mathf.FloorToInt(pos.z / ChunkSize) * ChunkSize;

            return new Vector3(curChunkPosX, Position.y - (ChunkHeight / 2), curChunkPosZ);
        }

        private Chunk ChunkAt(ChunkId point, bool forceLoad = true)
        {
            if (WorldData.Chunks.ContainsKey(point)) return WorldData.Chunks[point];
            if (!forceLoad) return null;

            return LoadChunkAt(point);
        }

        private Chunk LoadChunkAt(ChunkId point)
        {
            var x = point.X;
            var z = point.Z;

            var origin = new Vector3(x, -ChunkHeight / 2, z);

            return _worldGeneration.ChunkGenerator.CreateChunkJob(origin);
        }

        private void SpawnChunk(Chunk nonNullChunk, Vector3 pos)
        {
            var chunkId = new ChunkId(pos.x, pos.y, pos.z);
            WorldData.Chunks.Add(chunkId, nonNullChunk);

            var go = Instantiate(_chunkInfo.ChunkPrefab.gameObject);

            go.transform.position = pos;
            go.name = pos.ToString();

            nonNullChunk.AddEntity(go);
                    
            nonNullChunk.SetMesh(pos);
            
            WorldData.ChunkObjects.Add(chunkId, go);
        }
        
        public void RemoveChunkAt(Vector3 pos)
        {
            var point = new ChunkId(pos.x, pos.y, pos.z);
            if (WorldData.ChunkObjects.ContainsKey(point))
            {
                var go = WorldData.ChunkObjects[point];
                Destroy(go);
                WorldData.ChunkObjects.Remove(point);
            }

            if (WorldData.Chunks.ContainsKey(point))
            {
                WorldData.Chunks.Remove(point);
            }
        }

        public bool WithinRange(Vector3 pos)
        {
            var difference = Position - pos;

            return difference.magnitude <= _maxMagnitude;
        }

        private void Update()
        {
            if (!_planeAreaBehaviour)
            {
                var ray = CamMain.ViewportPointToRay(CamMain.ScreenToViewportPoint(Input.mousePosition));
        
                if (!Physics.Raycast(ray, out var hit)) return;

                var plane = hit.collider.GetComponent<PlaneAreaBehaviour>();

                if (!plane) return;

                _planeAreaBehaviour = plane;
                
                StartGeneration(hit.point);

                //_planeAreaBehaviour = FindObjectOfType<PlaneAreaBehaviour>();
            }

            if (!_startGenerating) return;

            var point = NearestChunk(Position);

            for (var x = -_xDistance; x <= _xDistance; x += ChunkSize)
            {
                for (var z = -_zDistance; z <= _zDistance; z += ChunkSize)
                {
                    var pointToCheck = new ChunkId(point.x + x, point.y - ChunkHeight / 2, point.z + z);
                    if (!WithinRange(new Vector3(pointToCheck.X, point.y - ChunkHeight / 2, pointToCheck.Z))) continue;

                    var c = ChunkAt(pointToCheck, false);

                    if (c == null)
                    {
                        c = LoadChunkAt(pointToCheck);
                        
                        if (c != null) SpawnChunk(c, new Vector3(point.x + x, point.y - ChunkHeight / 2, point.z + z));
                    }
                }
            }
        }
        #endregion
    }
}
