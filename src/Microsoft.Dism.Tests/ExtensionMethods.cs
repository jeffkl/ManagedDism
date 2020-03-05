// Copyright (c). All rights reserved.
//
// Licensed under the MIT license.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Microsoft.Dism.Tests
{
    internal static class ExtensionMethods
    {
        public static IntPtr ToPtr<T>(this IList<T> list)
        {
            int structSize = Marshal.SizeOf(typeof(T));

            IntPtr mainPtr = Marshal.AllocHGlobal(structSize * list.Count);

            long startPtr = mainPtr.ToInt64();

            for (int i = 0; i < list.Count; i++)
            {
                IntPtr currentPtr = new IntPtr(startPtr + (i * structSize));

                Marshal.StructureToPtr(list[i], currentPtr, false);
            }

            return mainPtr;
        }

        public static IntPtr ToPtr<T>(this T obj)
            where T : struct
        {
            IntPtr pointer = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)));

            Marshal.StructureToPtr(obj, pointer, false);

            return pointer;
        }
    }
}