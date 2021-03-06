﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;

namespace hjudge.WebHost.Data.Identity
{
    public static class IdentityHelper
    {
        public class OtherUserInfoModel
        {
            public string Name { get; set; } = string.Empty;
            public string Key { get; set; } = string.Empty;
            public string Value { get; set; } = string.Empty;
        }

        public static readonly PropertyInfo[] OtherInfoProperties = typeof(OtherUserInfo).GetProperties()
                                                        .Where(i => i.IsDefined(typeof(ItemNameAttribute), false))
                                                        .ToArray();

        public static List<OtherUserInfoModel> GetOtherUserInfo(string rawInfo)
        {
            var otherInfo = JsonSerializer.Deserialize<OtherUserInfo>(rawInfo ?? "{}");
            if (otherInfo is null) otherInfo = new OtherUserInfo();
            var otherInfoList = new List<OtherUserInfoModel>();
            
            foreach (var property in OtherInfoProperties)
            {
                var attributes = property.GetCustomAttributes(false);
                foreach (var attribute in attributes)
                {
                    if (attribute is ItemNameAttribute)
                    {
                        otherInfoList.Add(new OtherUserInfoModel
                        {
                            Key = property.Name,
                            Name = attribute.GetType().GetProperty("ItemName")?.GetValue(attribute)?.ToString() ?? string.Empty,
                            Value = property.GetValue(otherInfo)?.ToString() ?? string.Empty
                        });
                        break;
                    }
                }
            }
            return otherInfoList;
        }
    }
}
