﻿using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NDFSerialization.NDFDataTypes.Primitive
{
    public partial class DistanceMetre
    {
        private string _initialData;

        public float FloatValue;

        public override string ToString()
        {
            var replaced = DistanceMetreRegex().Replace(_initialData, FloatValue.ToString());
            return replaced;
        }

        public DistanceMetre(string value)
        {
            _initialData = value;

            if (DistanceMetreRegex().IsMatch(value))
            {
                var groups = DistanceMetreRegex().Match(value).Groups;

                if (groups.ContainsKey("DistanceValue"))
                {
                    var raw = groups["DistanceValue"].Value;

                    if (!float.TryParse(raw, CultureInfo.InvariantCulture, out FloatValue))
                    {
                        Debug.WriteLine($"{raw} was is incorrect format");
                    }
                }
            }
        }

        [GeneratedRegex("(?<DistanceValue>[\\d{1,}.]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled )]
        private static partial Regex DistanceMetreRegex();
    }
}
