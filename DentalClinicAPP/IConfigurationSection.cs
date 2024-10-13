﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicAPP
{
    public interface IConfigurationSection
    {
        //
        // Summary:
        //     Gets the key this section occupies in its parent.
        string Key { get; }
        //
        // Summary:
        //     Gets the full path to this section within the Microsoft.Extensions.Configuration.IConfiguration.
        string Path { get; }
        //
        // Summary:
        //     Gets or sets the section value.
        string Value { get; set; }
    }
}