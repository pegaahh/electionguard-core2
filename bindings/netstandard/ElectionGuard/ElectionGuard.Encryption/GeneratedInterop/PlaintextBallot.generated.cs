// DO NOT MODIFY THIS FILE
// This file is generated via ElectionGuard.InteropGenerator at /src/interop-generator

using System;
using System.Runtime.InteropServices;

namespace ElectionGuard
{
    public partial class PlaintextBallot
    {
        #region Properties
        /// <Summary>
        /// A unique Ballot ID that is relevant to the external system and must be unique within the dataset of the election.
        /// </Summary>
        public unsafe string ObjectId
        {
            get
            {
                var status = External.GetObjectId(Handle, out IntPtr value);
                status.ThrowIfError();
                var data = Marshal.PtrToStringAnsi(value);
                NativeInterface.Memory.FreeIntPtr(value);
                return data;
            }
        }

        /// <Summary>
        /// The Object Id of the ballot style in the election manifest.  This value is used to determine which contests to expect on the ballot, to fill in missing values, and to validate that the ballot is well-formed.
        /// </Summary>
        public unsafe string StyleId
        {
            get
            {
                var status = External.GetStyleId(Handle, out IntPtr value);
                status.ThrowIfError();
                var data = Marshal.PtrToStringAnsi(value);
                NativeInterface.Memory.FreeIntPtr(value);
                return data;
            }
        }

        /// <Summary>
        /// The size of the Contests collection.
        /// </Summary>
        public unsafe ulong ContestsSize
        {
            get
            {
                return External.GetContestsSize(Handle);
            }
        }

        #endregion

        #region Methods
        #endregion

        #region Extern
        private unsafe static class External {
            [DllImport(
                NativeInterface.DllName,
                EntryPoint = "eg_plaintext_ballot_get_object_id",
                CallingConvention = CallingConvention.Cdecl, 
                SetLastError = true
            )]
            internal static extern Status GetObjectId(
                NativeInterface.PlaintextBallot.PlaintextBallotHandle handle
                , out IntPtr objectId
            );

            [DllImport(
                NativeInterface.DllName,
                EntryPoint = "eg_plaintext_ballot_get_style_id",
                CallingConvention = CallingConvention.Cdecl, 
                SetLastError = true
            )]
            internal static extern Status GetStyleId(
                NativeInterface.PlaintextBallot.PlaintextBallotHandle handle
                , out IntPtr objectId
            );

            [DllImport(
                NativeInterface.DllName,
                EntryPoint = "eg_plaintext_ballot_get_contests_size",
                CallingConvention = CallingConvention.Cdecl, 
                SetLastError = true
            )]
            internal static extern ulong GetContestsSize(
                NativeInterface.PlaintextBallot.PlaintextBallotHandle handle
            );

        }
        #endregion
    }
}