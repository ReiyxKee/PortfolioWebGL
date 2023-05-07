using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System.Text.RegularExpressions;


namespace ReiyxDev
{
    public class EnumStringConversion
    {
        public string EnumToString(Enum _enumType)
        {
            string enumString = _enumType.ToString().Replace("_", "");
            enumString = Regex.Replace(enumString, "(?<=[a-z])([A-Z])", " $1", RegexOptions.Compiled).Trim();
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(enumString);
        }


        public Enum StringToEnum(Type enumType, string inputEnumString)
        {
            Enum.TryParse(enumType, inputEnumString, out object parsedSkillType);

            return (Enum)parsedSkillType;
        }
    }
}