using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public static class GlobalConfig 
    {
        public const int SCORES_FOR_STEP = 10;

        public static readonly List<NormalTileContentId> NormalContentIds = new List<NormalTileContentId>
        {
            NormalTileContentId.Item1,
            NormalTileContentId.Item2,
            NormalTileContentId.Item3,
            NormalTileContentId.Item4,
            NormalTileContentId.Item5,
            NormalTileContentId.Item6
        };
    }
}
