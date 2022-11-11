﻿using System;
using System.Runtime.InteropServices;

namespace ElectionGuard
{
    /// <summary>
    /// An element of the smaller `mod q` space, i.e., in [0, Q), where Q is a 256-bit prime.
    /// </summary>
    public class ElementModQ : DisposableBase
    {
        /// <summary>
        /// Number of 64-bit ints that make up the 256-bit prime
        /// </summary>
        public const ulong MaxSize = 4;

        /// <Summary>
        /// Get the integer representation of the element
        /// </Summary>
        public ulong[] Data { 
            get => GetNative();
            internal set => NewNative(value);
        }

        internal NativeInterface.ElementModQ.ElementModQHandle Handle;

        /// <summary>
        /// Create a `ElementModQ`
        /// </summary>
        /// <param name="data">data used to initialize the `ElementModQ`</param>
        public ElementModQ(ulong[] data)
        {
            try
            {
                NewNative(data);
            }
            catch (Exception ex)
            {
                throw new ElectionGuardException("construction error", ex);
            }
        }

        /// <summary>
        /// Create a `ElementModQ`
        /// </summary>
        /// <param name="hex">string representing the hex bytes of the initialized data</param>
        /// <param name="uncheckedInput">if data is checked or not</param>
        public ElementModQ(string hex, bool uncheckedInput = false)
        {
            var status = uncheckedInput ?
                NativeInterface.ElementModQ.FromHexUnchecked(hex, out Handle)
                : NativeInterface.ElementModQ.FromHex(hex, out Handle);
            status.ThrowIfError();
        }

        internal ElementModQ(NativeInterface.ElementModQ.ElementModQHandle handle)
        {
            Handle = handle;
        }

        /// <Summary>
        /// exports a hex representation of the integer value in Big Endian format
        /// </Summary>
        public string ToHex()
        {
            var status = NativeInterface.ElementModQ.ToHex(Handle, out IntPtr pointer);
            if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
            {
                throw new ElectionGuardException($"ToHex Error Status: {status}");
            }
            var value = Marshal.PtrToStringAnsi(pointer);
            NativeInterface.Memory.FreeIntPtr(pointer);
            return value;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        protected override void DisposeUnmanaged()
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
        {
            base.DisposeUnmanaged();

            if (Handle == null || Handle.IsInvalid) return;
            Handle.Dispose();
            Handle = null;
        }

        private unsafe void NewNative(ulong[] data)
        {
            fixed (ulong* pointer = new ulong[MaxSize])
            {
                for (ulong i = 0; i < MaxSize; i++)
                {
                    pointer[i] = data[i];
                }

                var status = NativeInterface.ElementModQ.New(pointer, out Handle);
                if (status != Status.ELECTIONGUARD_STATUS_SUCCESS)
                {
                    throw new ElectionGuardException($"createNative Error Status: {status}");
                }
            }
        }

        private unsafe ulong[] GetNative()
        {
            if (Handle == null)
            {
                return null;
            }

            var data = new ulong[MaxSize];
            fixed (ulong* element = new ulong[MaxSize])
            {
                NativeInterface.ElementModQ.GetData(Handle, &element, out ulong size);
                if (size != MaxSize)
                {
                    throw new ElectionGuardException($"wrong size, expected: {MaxSize}, actual: {size}");
                }

                if (element == null)
                {
                    throw new ElectionGuardException("element is null");
                }

                for (ulong i = 0; i < MaxSize; i++)
                {
                    data[i] = element[i];
                }
            }

            return data;
        }
    }
}