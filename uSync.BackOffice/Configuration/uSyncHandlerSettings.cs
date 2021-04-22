﻿using System.Collections.Generic;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace uSync.BackOffice.Configuration
{
    [JsonObject(NamingStrategyType = typeof(DefaultNamingStrategy))]
    public class HandlerSettings
    {
        public bool Enabled { get; set; } = true;

        public IEnumerable<string> Actions { get; set; } = new List<string>();

        public bool UseFlatStructure { get; set; } = true;
        public bool GuidNames { get; set; } = false;
        public bool FailOnMissingParent { get; set; } = false;

        public string Group { get; set; } = string.Empty;

        public Dictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();

        public HandlerSettings() { }
    }

    public static class HandlerSettingsExtensions
    {
        public static TResult GetSetting<TResult>(this HandlerSettings settings, string key, TResult defaultValue)
        {
            if (settings.Settings != null && settings.Settings.ContainsKey(key))
            {
                var attempt = settings.Settings[key].TryConvertTo<TResult>();
                if (attempt) return attempt.Result;
            }

            return defaultValue;
        }

        public static HandlerSettings Clone(this HandlerSettings settings)
        {
            return new HandlerSettings
            {
                Actions = settings.Actions,
                Enabled = settings.Enabled,
                FailOnMissingParent = settings.FailOnMissingParent,
                UseFlatStructure = settings.UseFlatStructure,
                Group = settings.Group,
                GuidNames = settings.GuidNames,
                Settings = new Dictionary<string, string>(settings.Settings)
            };
        }

    }

}
