using BeerCup.Mobile.Contracts.Services.General;
using BeerCup.Mobile.Enums;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Services.General
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettings _settings;
        private const string UserName = "UserName";
        private const string UserId = "UserId";
        private const string BattleId = "BattleId";

        public SettingsService()
        {
            _settings = CrossSettings.Current;
        }

        public void AddItem(string key, string value)
        {
            _settings.AddOrUpdateValue(key, value);
        }

        public void AddItem(string key, int value)
        {
            _settings.AddOrUpdateValue(key, value.ToString());
        }

        public string GetItem(string key)
        {
            return _settings.GetValueOrDefault(key, string.Empty);
        }

        public int GetIntItem(string key)
        {
            string storedSetting = _settings.GetValueOrDefault(key, string.Empty);
            return Int32.Parse(storedSetting);
        }

        public string UserNameSetting
        {
            get => GetItem(UserName);
            set => AddItem(UserName, value);
        }

        public int UserIdSetting
        {
            get => GetIntItem(UserId);
            set => AddItem(UserId, value);
        }

        public UserRole UserRoleSetting
        {
            //get => _settings.GetValueOrDefault(nameof(UserRoleSetting), (int) UserRole.Voter);
            //set => _settings.AddOrUpdateValue(nameof(UserRoleSetting), (int)value);

            get
            {
                var role =_settings.GetValueOrDefault(nameof(UserRoleSetting), (int) UserRole.Voter);
                return (UserRole)role;
            }
            set
            {
                _settings.AddOrUpdateValue(nameof(UserRoleSetting), (int)value);
            }
        }

        public int BattleIdSetting 
        { 
            get => GetIntItem(BattleId); 
            set => AddItem(BattleId, value);
        }
    }
}