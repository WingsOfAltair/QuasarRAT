﻿using System;
using Microsoft.Win32;
using Quasar.Common.Models;
using Quasar.Common.Utilities;

namespace Quasar.Server.Core.Registry
{
    public class RegValueHelper
    {
        private static string DEFAULT_REG_VALUE = "(Default)";

        public static bool IsDefaultValue(string valueName)
        {
            return String.IsNullOrEmpty(valueName);
        }

        public static string GetName(string valueName)
        {
            return IsDefaultValue(valueName) ? DEFAULT_REG_VALUE : valueName;
        }

        public static string RegistryValueToString(RegValueData value)
        {
            switch (value.Kind)
            {
                case RegistryValueKind.Binary:
                    return value.Data.Length > 0 ? BitConverter.ToString(value.Data).Replace("-", " ").ToLower() : "(zero-length binary value)";
                case RegistryValueKind.MultiString:
                    return string.Join(" ", ByteConverter.ToStringArray(value.Data));
                case RegistryValueKind.DWord:
                    var dword = ByteConverter.ToUInt32(value.Data);
                    return $"0x{dword:x8} ({dword.ToString()})"; // show hexadecimal and decimal
                case RegistryValueKind.QWord:
                    var qword = ByteConverter.ToUInt64(value.Data);
                    return $"0x{qword:x8} ({qword.ToString()})"; // show hexadecimal and decimal
                case RegistryValueKind.String:
                case RegistryValueKind.ExpandString:
                    return ByteConverter.ToString(value.Data);
                default:
                    return string.Empty;
            }
        }
    }
}
