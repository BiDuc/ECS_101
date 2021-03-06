﻿using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace Drone.Pure
{
    // See the comments on the Drone.Pure.MoveSystem as the code works the same way for this one
    public class RotateSystem : ComponentSystem
    {
        struct Data
        {
            public readonly int Length;
            public ComponentDataArray<Rotation> Rotation;
            public ComponentDataArray<RotationSpeed> RotationSpeed;
        }

        [Inject] private Data m_Data;
        protected override void OnUpdate()
        {
            float dt = Time.deltaTime;
            for (int index = 0; index < m_Data.Length; ++index)
            {
                var rotation = m_Data.Rotation[index].Value;
                rotation = math.mul(math.normalize(m_Data.Rotation[index].Value), quaternion.AxisAngle(new float3(0, 1, 0), m_Data.RotationSpeed[index].Value * dt));

                m_Data.Rotation[index] = new Rotation { Value = rotation };
            }
        }
    }
}
