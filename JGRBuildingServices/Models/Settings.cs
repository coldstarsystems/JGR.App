using SQLite;
using System;

namespace JGRBuildingServices.Models
{
    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public String Value { get; set; }
    }
}