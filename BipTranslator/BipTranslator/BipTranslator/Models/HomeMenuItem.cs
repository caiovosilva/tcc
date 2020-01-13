﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BipTranslator.Models
{
    public enum MenuItemType
    {
        Browse,
        Listening,
        About
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
