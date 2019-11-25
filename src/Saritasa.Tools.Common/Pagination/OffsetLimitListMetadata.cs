﻿// Copyright (c) 2015-2019, Saritasa. All rights reserved.
// Licensed under the BSD license. See LICENSE file in the project root for full license information.

using System;

namespace Saritasa.Tools.Common.Pagination
{
    /// <summary>
    /// Offset limit metadata.
    /// </summary>
#if NET40 || NETSTANDARD2_0
    [Serializable]
#endif
    public class OffsetLimitListMetadata : TotalCountListMetadata
    {
        /// <summary>
        /// Zero based data offset.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Max number of returned items.
        /// </summary>
        public int Limit { get; set; }
    }
}
