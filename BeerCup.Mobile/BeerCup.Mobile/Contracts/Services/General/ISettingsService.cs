using BeerCup.Mobile.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BeerCup.Mobile.Contracts.Services.General
{
    public interface ISettingsService
    {
        void AddItem(string key, string value);
        string GetItem(string key);

        string UserNameSetting { get; set; }
        int UserIdSetting { get; set; }
        UserRole UserRoleSetting { get; set; }
    }
}
